namespace net.logo.model;

public class NumericBinaryExpression : IExpression
{
    public IExpression Left { get; }
    public IExpression Right { get; }
    
    public LogoOperator Operator { get; }
    public NumericBinaryExpression(LogoOperator op, IExpression left, IExpression right)
    {
        Operator = op;
        Left = left;
        Right = right;
    }
    
    public override string ToString()
    {
        return $"{Left.ToString()} {Operator.ToString()} {Right.ToString()}";
    }
}