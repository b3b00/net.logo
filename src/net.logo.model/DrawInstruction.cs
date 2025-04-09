namespace net.logo.model;

public class DrawInstruction : IInstruction
{
    private DrawInstructionType _type;
    private readonly double _parameter;

    public DrawInstructionType Type => _type;
    public double Parameter => _parameter;

    public DrawInstruction(DrawInstructionType type, double parameter)
    {
        _type = type;
        _parameter = parameter;
    }
}