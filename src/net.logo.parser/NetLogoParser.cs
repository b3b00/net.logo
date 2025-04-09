using sly.lexer;
using sly.parser.generator;
using System.Collections.Generic;
using System.Diagnostics;
using net.logo.model;
using sly.parser.parser;

namespace net.logo.parser;

[ParserRoot("program")]
public class NetLogoParser
{
    [Production("program : instruction *")]
    public INetLogoModel Program(List<INetLogoModel> instructions)
    {
        var procedures = instructions.Where(x => x is ProcedureDefinition).Cast<ProcedureDefinition>().ToList();
        var insts = instructions.Where(x => !(x is ProcedureDefinition)).Cast<IInstruction>().ToList();
        return new LogoProgram(procedures, insts);
        ;
    }

    [Production("instruction : [ command | repeat | procedure_definition | procedure_call | if ]")]
    public INetLogoModel Instruction(INetLogoModel instruction) => instruction;

    [Production($"command : [ FO | BA | TR | TL ] {nameof(NetLogoParser)}_expressions")]
    public INetLogoModel MoveCommand(Token<NetLogoLexer> command, INetLogoModel arg)
    {
        var argument = arg as IExpression;
        switch (command.TokenID)
        {
            case NetLogoLexer.FO:
            {
                return new DrawInstruction(DrawInstructionType.Forward, argument);
            }
            case NetLogoLexer.BA:
            {
                return new DrawInstruction(DrawInstructionType.Backward, argument);
            }
            case NetLogoLexer.TR:
            {
                return new DrawInstruction(DrawInstructionType.turnRight, argument);
            }
            case NetLogoLexer.TL:
            {
                return new DrawInstruction(DrawInstructionType.turnLeft, argument);
            }
            default:
            {
                return default(INetLogoModel);
            }
        }
    }

    [Production("command : [ PD | PU ]")]
    public INetLogoModel PenCommand(Token<NetLogoLexer> command)
    {
        if (command.TokenID != NetLogoLexer.PD)
        {
            return new PenInstruction(true);
        }

        if (command.TokenID != NetLogoLexer.PU)
        {
            return new PenInstruction(false);
        }

        return default(INetLogoModel);
    }

    [Production("command : [ CLEAN | HOME ]")]
    public INetLogoModel GlobalCommand(Token<NetLogoLexer> command)
    {
        if (command.TokenID == NetLogoLexer.CLEAN)
        {
            return new ClearInstruction();
        }

        if (command.TokenID == NetLogoLexer.HOME)
        {
            return new HomeInstruction();
        }

        return null;
    }

    [Production($"repeat : REPEAT[d] {nameof(NetLogoParser)}_expressions RBRACK[d] instruction * LBRACK[d]")]
    public INetLogoModel Repeat(INetLogoModel count, List<INetLogoModel> instructions)
    {
        return new RepeatInstruction(count as IExpression, instructions.Cast<IInstruction>().ToList());
    }

    [Production("procedure_definition : PO[d] ID parameter* instruction * END[d]")]
    public INetLogoModel ProcedureDefinition(Token<NetLogoLexer> id, List<INetLogoModel> erguments,
        List<INetLogoModel> instructions)
    {
        return new ProcedureDefinition(id.Value, erguments.Cast<Parameter>().ToList(),
            instructions.Cast<IInstruction>().ToList());
    }

    [Production($"procedure_call : ID {nameof(NetLogoParser)}_expressions *")]
    public INetLogoModel ProcedureCall(Token<NetLogoLexer> procedureName, List<INetLogoModel> parameters)
    {
        return new ProcedureCall(procedureName.Value, parameters.Cast<IExpression>().ToList());
    }

    [Operand]
    [Production("parameter : COLON[d] ID")]
    public INetLogoModel Parameter(Token<NetLogoLexer> id)
    {
        return new Parameter(id.Value);
    }

    [Production("command : COLOR[d] ID")]
    public INetLogoModel Color(Token<NetLogoLexer> id)
    {
        return new ColorInstruction(id.Value);
    }

    [Operand]
    [Production("number : NUMBER")]
    public INetLogoModel Number(Token<NetLogoLexer> number)
    {
        return new Number(number.DoubleValue);
    }


    [Operand]
    [Production($"group : LPAREN[d] {nameof(NetLogoParser)}_expressions RPAREN[d]")]
    public INetLogoModel GroupExpression(INetLogoModel expression) => expression;

    [Right((int)NetLogoLexer.PLUS, 10)]
    [Left((int)NetLogoLexer.MINUS, 10)]
    [Right((int)NetLogoLexer.TIMES, 20)]
    [Left((int)NetLogoLexer.DIV, 20)]
    [NodeName("numeric")]
    public INetLogoModel Numeric(INetLogoModel left, Token<NetLogoLexer> operatorToken, INetLogoModel right)
    {
        LogoOperator op = LogoOperator.EQUALS;
        switch (operatorToken.TokenID)
        {
            case NetLogoLexer.PLUS:
            {
                op = LogoOperator.PLUS;
                break;
            }
            case NetLogoLexer.MINUS:
            {
                op = LogoOperator.MINUS;
                break;
            }
            case NetLogoLexer.TIMES:
            {
                op = LogoOperator.TIMES;
                break;
            }
            case NetLogoLexer.DIV:
            {
                op = LogoOperator.DIV;
                break;
            }
            default:
            {
                throw new InvalidOperationException($"Invalid operator {operatorToken.TokenID}");
            }
        }
        return new BooleanBinaryExpression(op, left as IExpression, right as IExpression);
    }

    [Prefix((int)NetLogoLexer.MINUS, Associativity.Right, 100)]
    [NodeName("minus")]
    public INetLogoModel Minus(Token<NetLogoLexer> operatorToken, INetLogoModel value)
    {
        return new NumericUnaryExpression(LogoOperator.MINUS, value as IExpression);
    }


    [Operation((int)NetLogoLexer.LESSER, Affix.InFix, Associativity.Right, 50)]
    [Operation((int)NetLogoLexer.GREATER, Affix.InFix, Associativity.Right, 50)]
    [Operation((int)NetLogoLexer.EQUALS, Affix.InFix, Associativity.Right, 50)]
    [Operation((int)NetLogoLexer.DIFFERENT, Affix.InFix, Associativity.Right, 50)]
    public INetLogoModel binaryComparisonExpression(INetLogoModel left, Token<NetLogoLexer> operatorToken,
        INetLogoModel right)
    {
        LogoOperator op = LogoOperator.EQUALS;
        switch (operatorToken.TokenID)
        {
            case NetLogoLexer.EQUALS:
            {
                op = LogoOperator.EQUALS;
                break;
            }
            case NetLogoLexer.DIFFERENT:
            {
                op = LogoOperator.DIFFERENT;
                break;
            }
            case NetLogoLexer.LESSER:
            {
                op = LogoOperator.LESSER;
                break;
            }
            case NetLogoLexer.GREATER:
            {
                op = LogoOperator.GREATER;
                break;
            }
            default:
            {
                throw new InvalidOperationException($"Invalid operator {operatorToken.TokenID}");
            }
        }
        return new BooleanBinaryExpression(op, left as IExpression, right as IExpression);
    }
    
    [Prefix((int)NetLogoLexer.NOT, Associativity.Right, 100)]
    public INetLogoModel Not(Token<NetLogoLexer> operatorToken, INetLogoModel value)
    {
        return new BooleanUnaryExpression(LogoOperator.NOT, value as IExpression);
    }
    
    [Production($"if : IF[d] {nameof(NetLogoParser)}_expressions LBRACK[d] instruction * RBRACK[d]")]
    public INetLogoModel If(INetLogoModel condition, List<INetLogoModel> instructions)
    {
        return new IfInstruction(condition, instructions);
    }
}

