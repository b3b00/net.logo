using System.Globalization;

namespace net.logo.interpreter;

public class Svg
{

    private static string customDecimal(double d)
    {
        var leftPart = Math.Floor(d);
        var rightPart =(int)(d - leftPart);
        return $"{(int)leftPart}.{rightPart}";

    }
    
    private static string ToEnDecimal(double d)
    {
        string formatted = d.ToString("F", CultureInfo.InvariantCulture);
        return formatted;
        // Résultat : "1234.56"
    }
    public static string Line(double x1, double y1, double x2, double y2, string color = "black", string dash = null)
    {
        var line = $@"<line x1=""{customDecimal(x1)}"" y1=""{customDecimal(y1)}"" x2=""{customDecimal(x2)}"" y2=""{customDecimal(y2)}"" style=""stroke:{color}"" ";
        if (dash != null)
        {
            line += $@"stroke-dasharray=""{dash}""";
        }
        line += " />\n";
        return line;
    }
}