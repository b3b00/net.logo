using CommandLine;

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
        return 0;
    }

    private static int Compile(CompileOptions opts)
    {
        Console.WriteLine($"check logo file {opts.LogoFilePath}");
        return 0;
    }
}
