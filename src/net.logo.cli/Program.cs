using CommandLine;
using net.logo.interpreter;
using net.logo.model;
using net.logo.parser;
using sly.parser.generator;

namespace net.logo.cli;

public class Program
{
    public static int Main(string[] args)
    {
        return CommandLine.Parser.Default.ParseArguments<CompileOptions, GenerateOptions>(args)
            .MapResult(
                (CompileOptions opts) => Compile(opts),
                (GenerateOptions opts) => Generate(opts),
                errors =>
                {
                    foreach (Error error in errors)
                    {
                        Console.WriteLine(error.ToString());
                    }
                   
                    return 1;
                });
    }

    private static int Generate(GenerateOptions opts)
    {
        Console.WriteLine($"generate logo file {opts.LogoFilePath} to {opts.OutputFile}");
        
        NetLogoInterpreter interpreter = new NetLogoInterpreter(800, 600);
        interpreter.PenDown();
        interpreter.Forward(50);
        interpreter.TurnRight(90);
        interpreter.Forward(100);
        interpreter.TurnRight(90);
        interpreter.Forward(50);
        interpreter.TurnRight(90);
        interpreter.Forward(100);
        interpreter.Home();
        interpreter.PenUp();
        // interpreter.TurnRight(90);
        // interpreter.Forward(200);
        interpreter.PenDown();
        interpreter.Color("red");
        for (int i = 0; i < 360; i++)
        {
            interpreter.Forward(3);
            interpreter.TurnRight(1);
        }
        
        
        
        var svg = interpreter.GetSvg();
        if (string.IsNullOrEmpty(svg))
        {
            Console.Error.WriteLine("SVG is empty.");
            return 1;
        }
        else
        {
            File.WriteAllText(opts.OutputFile, svg);
        }
        
        return 0;
    }

    private static int Compile(CompileOptions opts)
    {
        ParserBuilder<NetLogoLexer,INetLogoModel> builder = new ParserBuilder<NetLogoLexer, INetLogoModel>("en");
        var instance = new NetLogoParser();
        var built = builder.BuildParser(instance,ParserType.EBNF_LL_RECURSIVE_DESCENT);
        if (built.IsError)
        {
            built.Errors.ForEach(e => Console.Error.WriteLine(e.Message));
            return 1;
        }

        if (File.Exists(opts.LogoFilePath))
        {
            var content = File.ReadAllText(opts.LogoFilePath);
            if (string.IsNullOrEmpty(content))
            {
                Console.Error.WriteLine("Logo file is empty.");
            }

            var parsed = built.Result.Parse(content);
            if (parsed.IsError)
            {
                parsed.Errors.ForEach(e => Console.Error.WriteLine(e.ErrorMessage));
                return 1;
            }

            Console.WriteLine("Logo file is valid.");
            return 0;
        }
        else
        {
            Console.Error.WriteLine("Logo file doesn't exist.");
        }

        return 0;
    }
}
