namespace net.logo.model;

public class ProcedureDefinition : IInstruction
{
    private string _name;
    private List<IInstruction> _instructions = new List<IInstruction>();

    private List<Parameter> _parameters = new List<Parameter>();
    
    public List<Parameter> Parameters => _parameters;
    
    public string Name => _name;
    public List<IInstruction> Instructions => _instructions;
    
    public ProcedureDefinition(string name, List<Parameter> parameters, List<IInstruction> instructions)
    {
        _name = name;
        _instructions = instructions;
        _parameters = parameters;
    }
}