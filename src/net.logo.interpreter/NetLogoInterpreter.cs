using net.logo.model;

namespace net.logo.interpreter;

public class InterpreterContext
{
    private Dictionary<string, double> _variables { get; set; } = new Dictionary<string, double>();
    
    private LogoProgram _logoProgram;

    public LogoProgram LogoProgram => _logoProgram;
    
    public InterpreterContext(LogoProgram logoProgram) {
        _variables = new Dictionary<string, double>();
        _logoProgram = logoProgram;
    }
    
    

    public ProcedureDefinition GetProcedureDefinition(string name) => _logoProgram.GetProcedureDefinition(name);
    
    public double GetVariable(string name)
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
    
    public void SetVariable(string name, double value)
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

public class NetLogoInterpreter
{

    private NetLogoDrawer _drawer;
    
    private Stack<InterpreterContext> _contexts = new Stack<InterpreterContext>(); 
    
    private InterpreterContext _currentContext => _contexts.Peek();
    
    public NetLogoInterpreter(double width, double height)
    {
        _drawer = new NetLogoDrawer(width, height);
    }

    public void Run(LogoProgram logoProgram)
    {
        _contexts.Push(new InterpreterContext(logoProgram));
        foreach (var instruction in logoProgram.Instructions)
        {
            RunInstruction(instruction);
        }
    }

    public string GetSvg()
    {
        return _drawer.GetSvg();
    }
    
    private void RunInstruction(IInstruction instruction)
    {
        switch (instruction)
        {
            case DrawInstruction drawInstruction:
            {
                RunDrawInstruction(drawInstruction);
                break;
            }
            case PenInstruction penInstruction:
            {
                RunPenInstruction(penInstruction);
                break;
            }
            case ColorInstruction colorInstruction:
            {
                _drawer.Color(colorInstruction.Color);
                break;
            }
            case RepeatInstruction repeatInstruction:
            {
                RunRepeatInstruction(repeatInstruction);
                break;
            }
            case ProcedureCall procedureCall:
            {
                RunProcedureCall(procedureCall);
                break;
            }
            default:
            {
                Console.Error.WriteLine($"Unknown instruction: {instruction.GetType().FullName}");
                break;
            }
        }   
    }

    private void RunProcedureCall(ProcedureCall procedureCall)
    {
        var definition = _currentContext.GetProcedureDefinition(procedureCall.Name);
        var newContext = new InterpreterContext(_currentContext.LogoProgram);
        for (int i = 0; i < definition.Parameters.Count; i++)
        {
            var parameter = definition.Parameters[i];
            var argument = procedureCall.Arguments[i];
            double value = 0.0;
            if (argument is Parameter p)
            {
                value = _currentContext.GetVariable(p.Name);
            }
            else if (argument is Number number)
            {
                value = number.Value;
            }
            else
            {
                throw new Exception($"Unknown argument type: {argument.GetType().FullName}");
            }
            newContext.SetVariable(parameter.Name, value);
        }
        _contexts.Push(newContext);
        foreach (var instruction in definition.Instructions)
        {
            RunInstruction(instruction);
        }
        _contexts.Pop();
    }

    private void RunRepeatInstruction(RepeatInstruction repeatInstruction)
    {
        for(int i = 0; i < repeatInstruction.Count; i++)
        {
            foreach (var instruction in repeatInstruction.Instructions)
            {
                RunInstruction(instruction);
            }
        }
    }

    private void RunPenInstruction(PenInstruction penInstruction)
    {
        if (penInstruction.PenUp)
        {
            _drawer.PenUp();
        }
        else
        {
            _drawer.PenDown();
        }
    }

    private void RunDrawInstruction(DrawInstruction drawInstruction)
    {
        double arg = 0.0;
        if (drawInstruction.Parameter is Parameter p)
        {
            arg = _currentContext.GetVariable(p.Name);
        }
        else if (drawInstruction.Parameter is Number number)
        {
            arg = number.Value;
        }
        switch (drawInstruction.Type)
        {
            case DrawInstructionType.Backward:
            {
                break;
            }
            case DrawInstructionType.Forward:
            {
                _drawer.Forward(arg);
                break;
            }
            case DrawInstructionType.turnLeft:
            {
                _drawer.TurnLeft(arg);
                break;
            }
            case DrawInstructionType.turnRight:
            {
                _drawer.TurnRight(arg);
                break;
            }
        }
    }
}