using sly.lexer;
using sly.lexer.fsm;
using sly.i18n;

namespace net.logo.parser
{
    [Lexer(KeyWordIgnoreCase = true)]
    public enum NetLogoLexer
    {
        [AlphaId]
        ID,
        [Int]
        NUMBER,
        [Keyword("AV")]
        [Keyword("FO")]
        [Keyword("FORWARD")]
        FO,
        [Keyword("RE")]
        [Keyword("BACK")]
        [Keyword("BA")]
        BA,
        [Keyword("TD")]
        [Keyword("TR")]
        TR,
        [Keyword("TG")]
        [Keyword("TL")]
        TL,
        [Keyword("BC")]
        [Keyword("PD")]
        PD,
        [Keyword("LC")]
        [Keyword("PD")]
        PU,
        [Keyword("NETTOIE")]
        [Keyword("CLEAN")]
        CLEAN,
        [Keyword("MAISON")]
        [Keyword("HOME")]
        HOME,
        [Keyword("REPETE")]
        [Keyword("REPEAT")]
        REPEAT,
        [Keyword("COULEUR")]
        [Keyword("COLOR")]
        COLOR,
        [Keyword("PO")]
        PO,
        [Keyword("FIN")]
        [Keyword("END")]
        END,
        
        [Keyword("SI")]
        [Keyword("IF")]
        IF,
        [Keyword("SINON")]
        [Keyword("ELSE")]
        ELSE,
        
        [Sugar("[")]
        LBRACK,
        [Sugar("]")]
        RBRACK,
        [Sugar(":")]
        COLON,
        [MultiLineComment("(*","*)")]
        COMMENT,
        
        [Sugar("*")]
        TIMES,
        [Sugar("/")]
        DIV,
        [Sugar("+")]    
        PLUS,
        [Sugar("-")]
        MINUS,
        [Sugar("<")]
        LESSER,
        [Sugar(">")]
        GREATER,
        [Sugar("==")]
        EQUALS, 
        [Sugar("!=")]
        [Sugar("<>")]
        DIFFERENT,
        [Sugar("!=")]
        NOT,
        [Sugar("(")]
        LPAREN,
        [Sugar(")")]
        RPAREN,
        
    }
}