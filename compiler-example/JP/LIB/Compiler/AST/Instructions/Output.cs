using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Common;
using LIB.Compiler.Common;
using LIB.Compiler.AST.Expressions;
using LIB.VirtualMachines;

namespace LIB.Compiler.AST.Instructions
{
    public class Output : Instruction
    {
        protected Expression expr;

        public Output(Expression expr):
            this(expr, null)
        {

        }

        public Output(Expression expr, Coordinate coord)
            : base(InstructionTypes.Output, coord)
        {
            this.expr = expr;
        }

        public override bool Interpret(IdnTable idnTable, Memmory memmory, List<ErrorBase> errors)
        {
            if(!this.expr.Interpret(idnTable, memmory, errors))
            {
                return false;
            }

            Console.Write(this.expr.Val.ToString());

            return true;
        }

        public override string ToString()
        {
            if( this.expr.DType != null && this.expr.DType.DataTypeCode == DataTypes.DataTypeCodes.String)
            {
                string s = this.expr.Val.ToString();
                if( s == "\n\r")
                {
                    return "ispisati(<CR>)";
                }
            }
            
            return string.Format("ispisati({0});", this.expr.ToString());
        }
    }
}
