

namespace net.logo.model;

public class DrawInstruction : IInstruction
{
    private DrawInstructionType _type;
    private readonly IExpression _parameter;

    public DrawInstructionType Type => _type;
    public IExpression Parameter => _parameter;

    public DrawInstruction(DrawInstructionType type, IExpression parameter)
    {
        _type = type;
        _parameter = parameter;
    }
}