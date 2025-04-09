using sly.lexer;
using sly.parser.generator;
using System.Collections.Generic;
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

        [Production("instruction : [ command | repeat | procedure_definition | procedure_call ]")]
        public INetLogoModel Instruction(INetLogoModel instruction) => instruction;

        [Production("command : [ FO | BA | TR | TL ] expression")]
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
                default: {
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
            if (command.TokenID != NetLogoLexer.CLEAN)
            {
                return new ClearInstruction();
            }
            if (command.TokenID != NetLogoLexer.HOME)
            {
                return new HomeInstruction();
            }

            return null;
        }

        [Production("repeat : REPEAT[d] NUMBER RBRACK[d] instruction * LBRACK[d]")]
        public INetLogoModel Repeat(Token<NetLogoLexer> count,  List<INetLogoModel> instructions)
        {
            return new RepeatInstruction(count.IntValue,instructions.Cast<IInstruction>().ToList());
        }

        [Production("procedure_definition : PO[d] ID parameter* instruction * END[d]")]
        public INetLogoModel ProcedureDefinition(Token<NetLogoLexer> id, List<INetLogoModel> erguments, List<INetLogoModel> instructions)
        {
            return new ProcedureDefinition(id.Value, erguments.Cast<Parameter>().ToList(), instructions.Cast<IInstruction>().ToList());
        }

        [Production("procedure_call : ID expression *")]
        public INetLogoModel ProcedureCall(Token<NetLogoLexer> procedureName, List<INetLogoModel> parameters)
        {
            return new ProcedureCall(procedureName.Value, parameters.Cast<IExpression>().ToList());
        }

        [Production("parameter : COLON[d] ID")]
        public INetLogoModel Parameter( Token<NetLogoLexer> id)
        {
            return new Parameter(id.Value);
        }

        [Production("command : COLOR[d] ID")]
        public INetLogoModel Color(Token<NetLogoLexer> id)
        {
            return new ColorInstruction(id.Value);
        }

        [Production("expression : NUMBER")]
        public INetLogoModel Number(Token<NetLogoLexer> number)
        {
            return new Number(number.DoubleValue);
        }

        [Production("expression : parameter")]
        public INetLogoModel ParameterRef(INetLogoModel expression) => expression;
    }