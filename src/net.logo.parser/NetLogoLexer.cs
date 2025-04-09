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
        [Sugar("[")]
        RBRACK,
        [Sugar("]")]
        LBRACK,
        [Sugar(":")]
        COLON,
    }
}