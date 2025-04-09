namespace net.logo.model;

public class BooleanUnaryExpression : IExpression
{
    public IExpression Value { get; }
    
    public LogoOperator Operator { get; }
    public BooleanUnaryExpression(LogoOperator op, IExpression value)
    {
        Operator = op;
        Value = value;
    }
}