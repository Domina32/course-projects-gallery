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
    public class Ako : Instruction
    {
        protected Expression idn;
        

        public Ako(Expression idn)
            : this(idn, null)
        {
            
        }

        public Ako(Expression idn, Coordinate coord)
            : base(InstructionTypes.Ako, coord)
        {
            this.idn = idn;
            
        }

        public override bool Interpret(IdnTable idnTable, Memmory memmory, List<ErrorBase> errors)
        {
            //IdnRow alloc = idnTable.Find(this.idn);


            //Console.Write("{0} ?=", this.idn.Name);

            

            Console.Write("ako {0} ", this.idn.Val);
            string input = Console.ReadLine();
            bool ok = true;
            


            return ok;
        }

        public override string ToString()
        {
            return string.Format("ako({0})", this.idn.Val);
        }
    }
}
