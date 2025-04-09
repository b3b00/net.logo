
namespace net.logo.model;

public interface IExpression : INetLogoModel
{
    
}

public enum LogoOperator
{
    PLUS,
    MINUS,
    TIMES,
    DIV,
    LESSER,
    GREATER,
    EQUALS,
    DIFFERENT,
    NOT,
}

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
}

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