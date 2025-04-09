namespace net.logo.model;

public class BooleanBinaryExpression : IExpression
{
    public IExpression Left { get; }
    public IExpression Right { get; }
    
    public LogoOperator Operator { get; }
    public BooleanBinaryExpression(LogoOperator op, IExpression left, IExpression right)
    {
        Operator = op;
        Left = left;
        Right = right;
    }
}