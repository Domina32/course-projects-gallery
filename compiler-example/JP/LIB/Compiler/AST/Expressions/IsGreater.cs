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
    public class IsGreater : BinarOp
    {
        public IsGreater(Expression left, Expression right) //prenese 2 podcvora - lijevi i desni operand
            : this(left, right, null)
        { }

        public IsGreater(Expression left, Expression right, Coordinate coord)
            : base(ExpressionTypes.Add, left, right, coord)
        { }

        public override bool Interpret(IdnTable idnTable, Memmory memmory, List<ErrorBase> errors)
        {
            if (!base.Interpret(idnTable, memmory, errors))
            {
                return false;
            }

            if (this.dType.DataTypeCode == DataTypeCodes.Error)
            {
                errors.Add(new ErrorBase(ErrorTypes.Semantic, string.Format("Zbrajanje tipova {0} i {1} nije moguće", this.left.DType.ToString(), this.right.ToString())));
                return false;
            }

            if (this.dType.DataTypeCode == DataTypeCodes.Int)
            {
                int L = Convert.ToInt32(this.left.Val);
                int R = Convert.ToInt32(this.right.Val);
                this.val = L > R;
            }
            else if (this.dType.DataTypeCode == DataTypeCodes.Real)
            {
                double L = Convert.ToDouble(this.left.Val);
                double R = Convert.ToDouble(this.right.Val);
                this.val = L > R;
            }

            return true;
        }

        public override string ToString()
        {
            return string.Format("({0}>{1})", this.left.ToString(), this.right.ToString());
        }
    }
}
