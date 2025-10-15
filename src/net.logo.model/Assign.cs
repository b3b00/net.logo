namespace net.logo.model;

public class Assign : IInstruction
{
    private string? _variableName;

    private IExpression _expression;

    public string Variable => _variableName;

    public IExpression Expression => _expression;

    public Assign(string variable, IExpression expression)
    {
        _variableName = variable;
        _expression = expression;
    }
}