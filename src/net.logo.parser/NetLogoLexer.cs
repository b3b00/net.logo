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
        [Keyword("FO")]
        [Keyword("FORWARD")]
        AV,
        [Keyword("RE")]
        [Keyword("BACK")]
        [Keyword("BA")]
        RE,
        [Keyword("TD")]
        [Keyword("TR")]
        TD,
        [Keyword("TG")]
        [Keyword("TL")]
        TG,
        [Keyword("BC")]
        [Keyword("PU")]
        BC,
        [Keyword("LC")]
        [Keyword("PD")]
        LC,
        [Keyword("NETTOIE")]
        [Keyword("CLEAN")]
        CLEAN,
        [Keyword("MAISON")]
        [Keyword("HOME")]
        HOME,
        [Keyword("REPETE")]
        [Keyword("REPEAT")]
        REPEAT,
        [Keyword("PO")]
        PO,
        [Keyword("FIN")]
        [Keyword("END")]
        END,
        [Sugar("[")]
        RBRACK,
        [Sugar("]")]
        LBRACK,
        [Sugar(":")]
        COLON,
    }
}