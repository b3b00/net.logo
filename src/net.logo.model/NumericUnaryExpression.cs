namespace net.logo.model;

public class NumericUnaryExpression : IExpression
{
    public IExpression Value { get; }
    
    public LogoOperator Operator { get; }
    public NumericUnaryExpression(LogoOperator op, IExpression value)
    {
        Operator = op;
        Value = value;
    }
}