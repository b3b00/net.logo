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
        public INetLogoModel program_instruction_(List<INetLogoModel> p0)
        {
            return default(INetLogoModel);
        }

        [Production("instruction : [ command | repeat | procedure_definition | procedure_call ]")]
        public INetLogoModel instruction_command_repeat_proceduredefinition_procedurecall_(INetLogoModel instruction)
        {
            return default(INetLogoModel);
        }

        [Production("command : [ AV | RE | TD | TG ] expression")]
        public INetLogoModel MoveCommand(Token<NetLogoLexer> command, object argument)
        {
            return default(INetLogoModel);
        }

        [Production("command : [ BC | LC ]")]
        public INetLogoModel PenCommand(Token<NetLogoLexer> command)
        {
            return default(INetLogoModel);
        }

        [Production("command : [ CLEAN | HOME ]")]
        public INetLogoModel GlobalCommand(Token<NetLogoLexer> command)
        {
            return default(INetLogoModel);
        }

        [Production("repeat : REPEAT[d] NUMBER RBRACK[d] instruction * RBRACK[d]")]
        public INetLogoModel repeat_REPEAT_NUMBER_instruction_(Token<NetLogoLexer> count,  List<INetLogoModel> instructions)
        {
            return default(INetLogoModel);
        }

        [Production("procedure_definition : PO[d] ID parameter * instruction * END[d]")]
        public INetLogoModel ProcedureDefinition(Token<NetLogoLexer> id, List<object> erguments, List<object> instructions)
        {
            return default(INetLogoModel);
        }

        [Production("procedure_call : ID expression *")]
        public INetLogoModel ProcedureCall(Token<NetLogoLexer> procedureName, List<INetLogoModel> parameters)
        {
            return default(INetLogoModel);
        }

        [Production("parameter : COLON[d] ID")]
        public INetLogoModel Parameter( Token<NetLogoLexer> id)
        {
            return default(INetLogoModel);
        }

        [Production("expression : NUMBER")]
        public INetLogoModel Number(Token<NetLogoLexer> number)
        {
            return default(INetLogoModel);
        }

        [Production("expression : parameter")]
        public INetLogoModel ParameterRef(object p0)
        {
            return default(INetLogoModel);
        }
    }