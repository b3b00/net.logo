namespace net.logo.model;

public class RepeatInstruction : IInstruction
{
    private readonly IExpression _count;
    private readonly List<IInstruction> _instructions;

    public RepeatInstruction(IExpression? count, List<IInstruction> instructions)
    {
        _count = count;
        _instructions = instructions;
    }

    public IExpression Count => _count;

    public List<IInstruction> Instructions => _instructions;
}