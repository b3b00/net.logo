namespace net.logo.model;

public class IfInstruction : INetLogoModel
{
    IExpression _condition;

    public IExpression Condition => _condition;
    
    List<IInstruction> _instructions;
    
    public List<IInstruction> Instructions => _instructions;
    public IfInstruction(INetLogoModel condition, List<INetLogoModel> instructions)
    {
        _condition = condition as IExpression;
        _instructions = instructions.Cast<IInstruction>().ToList();
    }
}