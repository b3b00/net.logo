namespace net.logo.model;

public class Number : IExpression
{
    private double _value;
    public double Value => _value;
    
    public Number(double value)
    {
        _value = value;
    }
    
    public override string ToString()
    {
        return _value.ToString();
    }
}

public class RandomExpression : IExpression
{
    Random _random = new Random();

    public RandomExpression()
    {
        _random = new Random();
    }
    
    public double Next()
    {
        return _random.NextDouble();
    }
}