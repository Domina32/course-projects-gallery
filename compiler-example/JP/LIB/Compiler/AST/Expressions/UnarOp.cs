using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Compiler.Common;
using LIB.Compiler.AST.DataTypes;
using LIB.Common;
using LIB.VirtualMachines;

namespace LIB.Compiler.AST.Expressions
{
    public abstract class UnarOp : Expression
    {
        protected Expression left;

        public UnarOp(ExpressionTypes expressionType, Expression left, Coordinate coord)
            : base(expressionType, coord)
        {
            this.left = left;
        }

        public Expression Left
        {
            get
            {
                return this.left;
            }
        }

      
        public override bool Interpret(IdnTable idnTable, Memmory memmory, List<ErrorBase> errors)
        {
            bool ok = this.left.Interpret(idnTable, memmory, errors);
            

            if (!ok)
            {
                return false;
            }

            this.dType = this.GetResultDataType();

            return true;
        }

        
    }
}
