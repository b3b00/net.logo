namespace net.logo.model;

public class RepeatInstruction : IInstruction
{
    private readonly int _count;
    private readonly List<IInstruction> _instructions;

    public RepeatInstruction(int count, List<IInstruction> instructions)
    {
        _count = count;
        _instructions = instructions;
    }

    public int Count => _count;

    public List<IInstruction> Instructions => _instructions;
}