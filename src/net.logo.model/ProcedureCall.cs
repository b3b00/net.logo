namespace net.logo.model;

public class ProcedureCall : IInstruction
{
    private string _name;
    private List<IExpression> _arguments = new List<IExpression>();

    public string Name => _name;
    public List<IExpression> Arguments => _arguments;

    public ProcedureCall(string name, List<IExpression> arguments)
    {
        _name = name;
        _arguments = arguments;
    }
}