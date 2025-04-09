using net.logo.model;

namespace net.logo.interpreter;

public class InterpreterContext
{
    private Dictionary<string, LogoValue> _variables { get; set; } = new Dictionary<string, LogoValue>();
    
    private LogoProgram _logoProgram;

    public LogoProgram LogoProgram => _logoProgram;
    
    public InterpreterContext(LogoProgram logoProgram) {
        _variables = new Dictionary<string, LogoValue>();
        _logoProgram = logoProgram;
    }
    
    

    public ProcedureDefinition GetProcedureDefinition(string name) => _logoProgram.GetProcedureDefinition(name);
    
    public LogoValue GetVariable(string name) => _variables[name];
    
    public double GetDoubleVariable(string name)
    {
        if (_variables.ContainsKey(name))
        {
            return _variables[name];
        }
        else
        {
            throw new Exception($"Variable {name} not found");
        }
    }
    
    public bool GetBoolVariable(string name)
    {
        if (_variables.ContainsKey(name))
        {
            return _variables[name];
        }
        else
        {
            throw new Exception($"Variable {name} not found");
        }
    }
    
    public string GetStringVariable(string name)
    {
        if (_variables.ContainsKey(name))
        {
            return _variables[name];
        }
        else
        {
            throw new Exception($"Variable {name} not found");
        }
    }
    
    public void SetVariable(string name, LogoValue value)
    {
        if (_variables.ContainsKey(name))
        {
            _variables[name] = value;
        }
        else
        {
            _variables.Add(name, value);
        }
    }
    
}