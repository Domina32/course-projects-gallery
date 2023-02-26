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
    public class Prog : Instruction
    {
        protected Block main;

        public Prog( Block main) : base(InstructionTypes.Program, null)
        {
            this.main = main;
        }

        public Block Main
        {
            get
            {
                return this.main;
            }
        }

        public override bool Interpret(IdnTable idnTable, Memmory memmory, List<ErrorBase> errors)
        {
            return this.main.Interpret(idnTable, memmory, errors);
        }

        public override string ToString()
        {
            return this.main.ToString();
        }
    }
}
