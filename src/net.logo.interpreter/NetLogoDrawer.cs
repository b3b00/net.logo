using System.Runtime.CompilerServices;
using System.Text;

namespace net.logo.interpreter;

public class NetLogoDrawer
{

    private double _currentX;
    
    private double _currentY;

    private double _width;
    
    private double _height;

    private StringBuilder _builder;

    private double _direction = 0;

    private bool _isPenUp = true;
    private string _color;

    private bool _isPenDown => !_isPenUp;
    public NetLogoDrawer(double width, double height)
    {
        
        _width = width;
        _height = height;
        Clean();
    }

    #region drawing primitives
    
    public string GetSvg()
    {
        _builder.AppendLine("</svg>");
        return _builder.ToString();
    }
  
    public void Forward(double l)
    {
        // Convertir l'angle en radians (les fonctions trigonométriques utilisent les radians)
        double angleInRadians = _direction * Math.PI / 180;

        // Calculer les longueurs
        double adjacent = l * Math.Sin(angleInRadians);
        double opposite = l * Math.Cos(angleInRadians);

        // calculer la longueur du grand coté d'un
        if (_isPenDown)
        {
            var line = Svg.Line(_currentX, _currentY, _currentX + adjacent, _currentY + opposite, _color);
            Console.WriteLine($"LINE : {line}");
        _builder.AppendLine(line);
        }
        _currentX += adjacent;
        _currentY += opposite;
    }
    
    public void TurnLeft(double angle)
    {
        _direction -= angle;
        if (_direction < 0)
        {
            _direction += 360;
        }
    }
    
    public void TurnRight(double angle)
    {
        _direction += angle;
        if (_direction >= 360)
        {
            _direction -= 360;
        }
    }
    
    public void PenUp()
    {
        _isPenUp = true;
    }
    
    public void PenDown()
    {
        _isPenUp = false;
    }

    public void Clean()
    {
        Console.WriteLine("DRAWER.CLEAN() !");
        _color = "black";
        _builder = new StringBuilder();
        _builder.AppendLine(@$"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
<svg width=""{_width}"" height=""{_height}"" xmlns=""http://www.w3.org/2000/svg"">");
        Home();
    }
    
    public void Color(string color)
    {
        _color = color;
    }

    public void Home()
    {
        _direction = 0;
        _currentX = _width/2;
        _currentY = _height/2;
    }
    
    
    #endregion
    
    
}