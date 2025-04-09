using net.logo.model;
using sly.parser;
using net.logo.parser;
using NFluent;
using sly.parser.generator;

namespace net.logo.tests;

public class Tests
{
    public Parser<NetLogoLexer, INetLogoModel> Parser { get; set; }

    public Parser<NetLogoLexer, INetLogoModel> GetParser()
    {
        if (Parser == null)
        {
            ParserBuilder<NetLogoLexer,INetLogoModel> builder = new ParserBuilder<NetLogoLexer, INetLogoModel>("en");
            var instance = new NetLogoParser();
            var built = builder.BuildParser(instance,ParserType.EBNF_LL_RECURSIVE_DESCENT);
            Check.That(built.IsOk).IsTrue();
            Parser = built.Result;
        }

        return Parser;
    }
    
    [Fact]
    public void IfTest()
    {
        var parser = GetParser();
        var parsed = parser.Parse(@"
IF 1 < 2 [
FO 10
]");
        Check.That(parsed.IsOk).IsTrue();
        
    }
}