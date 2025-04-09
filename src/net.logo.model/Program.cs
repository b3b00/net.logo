namespace net.logo.model;

public class Program : INetLogoModel
{
    public List<IInstruction> Instructions => _instructions;

    private List<ProcedureDefinition> _procedures = new List<ProcedureDefinition>();
    private List<IInstruction> _instructions = new List<IInstruction>();

    public List<ProcedureDefinition> ProceduresDefinitions => _procedures;
    
    public Program()
    {
    }

    public Program(List<ProcedureDefinition> procedures, List<IInstruction> instructions)
    {
        _instructions = instructions;
        _procedures = procedures;
    }
    
}