namespace net.logo.model;

public class LogoProgram : INetLogoModel
{
    public List<IInstruction> Instructions => _instructions;

    private Dictionary<string, ProcedureDefinition> _procedures = new Dictionary<string, ProcedureDefinition>();
    private List<IInstruction> _instructions = new List<IInstruction>();

    public Dictionary<string, ProcedureDefinition> ProceduresDefinitions => _procedures;
    
    public LogoProgram()
    {
    }

    public LogoProgram(List<ProcedureDefinition> procedures, List<IInstruction> instructions)
    {
        _instructions = instructions;
        _procedures = procedures.ToDictionary(x => x.Name);
    }

    public ProcedureDefinition GetProcedureDefinition(string name)
    {
        ProcedureDefinition definition;
        if (_procedures.TryGetValue(name, out definition))
        {
            return definition;
            
        }
        return null;
    }
}