using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Common;
using LIB.Compiler.Common;
using LIB.VirtualMachines;

namespace LIB.Compiler.AST.Expressions
{
    public class ReadVariable : Expression
    {
        protected Variable idn;

        public ReadVariable(Variable idn)
            : this(idn, null)
        { }

        public ReadVariable(Variable idn, Coordinate coord)
            :base(ExpressionTypes.ReadVariable, coord)
        {
            this.idn = idn;
        }

        public override bool Interpret(IdnTable idnTable, Memmory memmory, List<ErrorBase> errors)
        {
            IdnRow alloc = idnTable.Find(this.idn);
            if (alloc == null)
            {
                errors.Add(new ErrorBase(ErrorTypes.Semantic, string.Format("Varijabla {0} nije deklarirana", this.idn.Name)));
                return false;
            }

            MemmoryLocation loc = memmory.Read(alloc.Address);
            this.dType = loc.DType;
            this.val = loc.Val;

            return true;
        }

        public override string ToString()
        {
            return this.idn.Name;
        }
    }
}
