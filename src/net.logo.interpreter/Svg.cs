namespace net.logo.interpreter;

public class Svg
{
    
    private static string ToEnDecimal(double d)
    {
        return d.ToString("F").Replace(",", ".");
    }
    public static string Line(double x1, double y1, double x2, double y2, string color = "black", string dash = null)
    {
        var line = $@"<line x1=""{ToEnDecimal(x1)}"" y1=""{ToEnDecimal(y1)}"" x2=""{ToEnDecimal(x2)}"" y2=""{ToEnDecimal(y2)}"" style=""stroke:{color}"" ";
        if (dash != null)
        {
            line += $@"stroke-dasharray=""{dash}""";
        }
        line += " />\n";
        return line;
    }
}