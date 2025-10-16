using net.logo.interpreter;
using net.logo.model;
using net.logo.parser;
using sly.parser;
using sly.parser.generator;

namespace NetLogo.services;

public interface ILogoCompiler
{
    LogoCompilationResult Compile(string logo);
}

public class LogoCompiler : ILogoCompiler
{
    private Parser<NetLogoLexer, INetLogoModel> _parser;
    
    
    public LogoCompiler()
    {
        ParserBuilder<NetLogoLexer,INetLogoModel> builder = new ParserBuilder<NetLogoLexer, INetLogoModel>("en");
        var instance = new NetLogoParser();
        var built = builder.BuildParser(instance,ParserType.EBNF_LL_RECURSIVE_DESCENT);
        if (built.IsOk)
        {
            _parser = built.Result;
        }
        else
        {
            throw new Exception("unable to build logo parser");
        }
    }
    public LogoCompilationResult Compile(string logo)
    {
        var parsed = _parser.Parse(logo);
        if (parsed.IsError)
        {
            var errors = parsed.Errors.Select(x => x.ErrorMessage).ToList();

            return errors;
        }


        if (parsed.Result is LogoProgram program)
        {
            var interpreter = new NetLogoInterpreter(1024, 768);
            interpreter.Run(program);
            var svg = interpreter.GetSvg();
            return svg;
        }

        return new List<string>() { $"this is not a net logo program but a {parsed.Result.GetType().Name}" };
    }
}