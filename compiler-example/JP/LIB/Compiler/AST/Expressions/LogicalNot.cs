using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Common;
using LIB.Compiler.Common;
using LIB.Compiler.AST.DataTypes;
using LIB.VirtualMachines;

namespace LIB.Compiler.AST.Expressions
{
    public class LogicalNot : UnarOp
    {
        public LogicalNot(Expression left) //prenese 1 podcvor - lijevi operand
            : this(left, null)
        { }

        public LogicalNot(Expression left, Coordinate coord)
            : base(ExpressionTypes.Add, left, coord)
        { }

        public override bool Interpret(IdnTable idnTable, Memmory memmory, List<ErrorBase> errors)
        {
            if (!base.Interpret(idnTable, memmory, errors))
            {
                return false;
            }

            if (this.dType.DataTypeCode == DataTypeCodes.Error)
            {
                errors.Add(new ErrorBase(ErrorTypes.Semantic, string.Format("Negiranje tipa {0}  nije moguće", this.left.DType.ToString())));
                return false;
            }

            if (this.dType.DataTypeCode == DataTypeCodes.Bool)
            {
                bool L = Convert.ToBoolean(this.left.Val);
                this.val = !L;
            }

            return true;
        }

        public override string ToString()
        {
            return string.Format("(!{0})", this.left.ToString());
        }
    }
}
