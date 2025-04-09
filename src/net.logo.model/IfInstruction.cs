namespace net.logo.model;

public class IfInstruction : IInstruction
{
    IExpression _condition;

    public IExpression Condition => _condition;
    
    List<IInstruction> _thenInstructions;
    
    public List<IInstruction> ThenInstructions => _thenInstructions;
    
    List<IInstruction> _elseInstructions = null;
    
    public List<IInstruction> ElseInstructions => _elseInstructions;
    public IfInstruction(INetLogoModel condition, List<INetLogoModel> instructions, List<INetLogoModel> elseInstructions)
    {
        if (condition is not IExpression)
        {
            throw new ArgumentException("Condition must be an expression");
        }

        if (instructions == null || instructions.Count == 0)
        {
            throw new ArgumentException("ThenInstructions cannot be null or empty");
        }

        if (elseInstructions != null && elseInstructions.Count > 0)
        {
            _elseInstructions = elseInstructions.Cast<IInstruction>().ToList();
        }
    
        _condition = condition as IExpression;
        _thenInstructions = instructions.Cast<IInstruction>().ToList();
    }
}