using sly.lexer;
using sly.lexer.fsm;
using sly.i18n;

namespace net.logo.parser
{
    public enum NetLogoLexer
    {
        [AlphaNumId]
        ID,
        [Int]
        NUMBER,
        [Keyword("AV")]
        AV,
        [Keyword("RE")]
        RE,
        [Keyword("TD")]
        TD,
        [Keyword("TG")]
        TG,
        [Keyword("BC")]
        BC,
        [Keyword("LC")]
        LC,
        [Keyword("NETTOIE")]
        CLEAN,
        [Keyword("MAISON")]
        HOME,
        [Keyword("REPETE")]
        REPEAT,
        [Keyword("PO")]
        PO,
        [Keyword("FIN")]
        END,
        [Sugar("[")]
        LBRACK,
        [Sugar("]")]
        RBRANCK,
        [Sugar(":")]
        COLON,
    }
}