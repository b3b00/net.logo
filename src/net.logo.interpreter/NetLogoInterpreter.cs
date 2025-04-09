using System.Linq.Expressions;
using net.logo.model;

namespace net.logo.interpreter;

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
            case ClearInstruction clearInstruction:
            {
                _drawer.Clean();
                break;
            }
            case HomeInstruction homeInstruction:
            {
                _drawer.Home();
                break;
            }
            case IfInstruction ifInstruction:
            {
                RunIfExpression(ifInstruction);
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
        Console.WriteLine($"calling procedure {procedureCall.Name} {string.Join(", ",procedureCall.Arguments.Select(x => x.ToString()))}");
        var definition = _currentContext.GetProcedureDefinition(procedureCall.Name);
        var newContext = new InterpreterContext(_currentContext.LogoProgram);
        for (int i = 0; i < definition.Parameters.Count; i++)
        {
            var parameter = definition.Parameters[i];
            var argument = procedureCall.Arguments[i];
           
            var value = Evaluate(argument);
           
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
        var count = Evaluate(repeatInstruction.Count);
        if (!count.IsDouble)
        {
            return;
        }
        for(double i = 0; i < count.DoubleValue; i++)
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

    private void RunIfExpression(IfInstruction ifInstruction)
    {
        var cond = Evaluate(ifInstruction.Condition);
        if (cond)
        {
            for (int i = 0; i < ifInstruction.ThenInstructions.Count; i++)
            {
                var instruction = ifInstruction.ThenInstructions[i];
                RunInstruction(instruction);
            }
        }
        else
        {
            if (ifInstruction.ElseInstructions != null && ifInstruction.ElseInstructions.Count > 0)
            {
                for (int i = 0; i < ifInstruction.ElseInstructions.Count; i++)
                {
                    var instruction = ifInstruction.ElseInstructions[i];
                    RunInstruction(instruction);
                }
            }
        }
    }
    
    #region expressions

    public LogoValue Evaluate(IExpression expression)
    {
        switch (expression)
        {
            case Number n :
            {
                return n.Value;
            }
            case BooleanBinaryExpression booleanExpression:
            {
                return Evaluate(booleanExpression);
            }
            case NumericBinaryExpression numericExpression:
            {
                return Evaluate(numericExpression);
            }
            case NumericUnaryExpression unaryExpression:    
            {
                return Evaluate(unaryExpression);
            }
            case BooleanUnaryExpression booleanExpression:
            {
                return Evaluate(booleanExpression);
            }
            case Parameter parameter:
            {
                if (_currentContext.TryGetVariable(parameter.Name, out var variable))
                {
                    return variable;
                }
                throw new InvalidOperationException($"unknown variable : {parameter.Name}");
            }
            default:
            {
                throw new Exception($"Unknown expression: {expression.GetType().FullName}");
            }
        }
    }

    public LogoValue Evaluate(BooleanUnaryExpression booleanExpression)
    {
        var value = Evaluate(booleanExpression.Value);
        if (value.IsBool)
        {
            return !value;
        } 
        throw new Exception($"Unknown boolean expression: {booleanExpression.Value}");
    }

    public LogoValue Evaluate(NumericUnaryExpression numericExpression)
    {
        var value = Evaluate(numericExpression.Value);
        if (value.IsDouble)
        {
            return -value;
        } 
        throw new Exception($"Unknown numeric expression: {numericExpression.Value}");
    }

    public LogoValue Evaluate(BooleanBinaryExpression booleanBinaryExpression)
    {
        var left = Evaluate(booleanBinaryExpression.Left);
        var right = Evaluate(booleanBinaryExpression.Right);
        switch (booleanBinaryExpression.Operator)
        {
            case LogoOperator.EQUALS:
            {
                if (left == right)
                {
                    return true;
                }

                return false;
            }
            case LogoOperator.DIFFERENT:
            {
                return left != right;
            }
            case LogoOperator.LESSER:
            {
                return left < right;                
            }
            case LogoOperator.GREATER:
            {
                return left > right;
            }
            default:
            {
                return false;
            }
        }
    }
    
    #endregion
    
}