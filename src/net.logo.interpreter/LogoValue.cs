using sly.parser.generator;

namespace net.logo.interpreter;

public enum LogoType    
{
    Double,
    Bool,
    String
}   

public class LogoValue
{
    public double DoubleValue { get; set; }

    public bool IsDouble => Type == LogoType.Double;

    public bool BoolValue { get; set; }

    public bool IsBool => Type == LogoType.Bool;

    public string StringValue { get; set; }

    public bool IsString => Type == LogoType.String;
    
    public LogoType Type { get; set; }
    
    
    
    public LogoValue(double value)
    {
        DoubleValue = value;
        Type = LogoType.Double;
    }

    public LogoValue(bool value)
    {
        BoolValue = value;
        Type = LogoType.Bool;
    }
    
    public LogoValue(string value)
    {
        StringValue = value;
        Type = LogoType.String;
    }
    
    public static implicit operator double(LogoValue value) => value.DoubleValue;
    public static implicit operator string(LogoValue value) => value.StringValue;
    
    public static implicit operator bool(LogoValue value) => value.BoolValue;
    
    public static implicit operator LogoValue(double value) => new LogoValue(value);
    public static implicit operator LogoValue(bool value) => new LogoValue(value);
    public static implicit operator LogoValue(string value) => new LogoValue(value);
    
    public static bool operator == (LogoValue left, LogoValue right)
    {
        if (left.Type.Equals(right.Type))
        {
            if (left.IsDouble)
                return left.DoubleValue == right.DoubleValue;
            else if (left.IsBool)
                return left.BoolValue == right.BoolValue;
            else if (left.IsString)
                return left.StringValue == right.StringValue;
            else
                throw new Exception($"Cannot compare {left.Type} with {right.Type}");
        }
        else
        {
            throw new Exception($"Cannot compare {left.Type} with {right.Type}");
        }
    }

    public static bool operator !=(LogoValue left, LogoValue right) => !(left == right);
    
    public static bool operator < (LogoValue left, LogoValue right)
    {
        if (left.Type.Equals(right.Type))
        {
            if (left.IsDouble)
                return left.DoubleValue < right.DoubleValue;
            if (left.IsString) 
                return left.StringValue.CompareTo(right.StringValue) < 0;
            else
                throw new Exception($"Cannot compare {left.Type} with {right.Type}");
        }
        else
        {
            throw new Exception($"Cannot compare {left.Type} with {right.Type}");
        }
    }
    
    public static bool operator > (LogoValue left, LogoValue right)
    {
        if (left.Type.Equals(right.Type))
        {
            if (left.IsDouble)
                return left.DoubleValue > right.DoubleValue;
            if (left.IsString) 
                return left.StringValue.CompareTo(right.StringValue) > 0;
            else
                throw new Exception($"Cannot compare {left.Type} with {right.Type}");
        }
        else
        {
            throw new Exception($"Cannot compare {left.Type} with {right.Type}");
        }
    }
    
    public static LogoValue operator + (LogoValue left, LogoValue right)
    {
        if (left.Type.Equals(right.Type))
        {
            if (left.IsDouble)
                return new LogoValue(left.DoubleValue + right.DoubleValue);
            else if (left.IsString)
                return new LogoValue(left.StringValue + right.StringValue);
            else
                throw new Exception($"Cannot add {left.Type} with {right.Type}");
        }
        else
        {
            throw new Exception($"Cannot add {left.Type} with {right.Type}");
        }
    }
    
    public static LogoValue operator - (LogoValue left, LogoValue right)
    {
        if (left.Type.Equals(right.Type))
        {
            if (left.IsDouble)
                return new LogoValue(left.DoubleValue - right.DoubleValue);
            else
                throw new Exception($"Cannot subtract {left.Type} with {right.Type}");
        }
        else
        {
            throw new Exception($"Cannot subtract {left.Type} with {right.Type}");
        }
    }
    
    public static LogoValue operator * (LogoValue left, LogoValue right)
    {
        if (left.Type.Equals(right.Type))
        {
            if (left.IsDouble)
                return new LogoValue(left.DoubleValue * right.DoubleValue);
            else
                throw new Exception($"Cannot multiply {left.Type} with {right.Type}");
        }
        else
        {
            throw new Exception($"Cannot multiply {left.Type} with {right.Type}");
        }
    }
    
    public static LogoValue operator / (LogoValue left, LogoValue right)
    {
        if (left.Type.Equals(right.Type))
        {
            if (left.IsDouble)
                return new LogoValue(left.DoubleValue / right.DoubleValue);
            else
                throw new Exception($"Cannot divide {left.Type} with {right.Type}");
        }
        else
        {
            throw new Exception($"Cannot divide {left.Type} with {right.Type}");
        }
    }

    public override string ToString()
    {
        if (IsDouble)
            return $"{Type}: {DoubleValue}";
        else if (IsString) 
            return $"{Type}: {StringValue}";
        else 
            return $"{Type}: {BoolValue}";
    }
}