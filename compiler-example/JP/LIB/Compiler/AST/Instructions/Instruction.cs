using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Common;
using LIB.Compiler.Common;
using LIB.VirtualMachines;

namespace LIB.Compiler.AST.Instructions
{
    public enum InstructionTypes
    {
        Input,
        Output,
        Assignment,
        Block,
        Program,
        Ako
    }

    public abstract class Instruction: Node
    {
        protected InstructionTypes instructionType;

        public Instruction(InstructionTypes instructionType, Coordinate coord)
            :base(NodeTypes.Instruction, coord)
        {
            this.instructionType = instructionType;
        }

        public InstructionTypes InstructionType
        {
            get
            {
                return this.instructionType;
            }
        }

        public override bool Interpret(IdnTable idnTable, Memmory memmory, List<ErrorBase> errors )
        {
            throw new NotImplementedException(this.GetType().ToString());
        }
    }
}
