namespace net.logo.model;

public class Number : IExpression
{
    private double _value;
    public double Value => _value;
    
    public Number(double value)
    {
        _value = value;
    }
}