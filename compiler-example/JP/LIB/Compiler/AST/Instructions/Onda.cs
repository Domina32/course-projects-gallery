using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Common;
using LIB.Compiler.AST.Expressions;
using LIB.Compiler.Common;
using LIB.VirtualMachines;

namespace LIB.Compiler.AST.Instructions
{
    public class Onda : Instruction
    {
        protected Block blok;
        protected Expression idn;


        public Onda(Expression idn)
            : this(idn, null)
        {

        }

        public Onda(Expression idn, Coordinate coord)
            : base(InstructionTypes.Ako, coord)
        {
            this.idn = idn;

        }

        public override bool Interpret(IdnTable idnTable, Memmory memmory, List<ErrorBase> errors)
        {
            Console.Write("onda {{0}} ", this.blok.ToString());
            string input = Console.ReadLine();
            bool ok = true;

            return ok;
        }

        public override string ToString()
        {
            return string.Format("onda{{0}}", this.blok.ToString());
        }
    }
}
