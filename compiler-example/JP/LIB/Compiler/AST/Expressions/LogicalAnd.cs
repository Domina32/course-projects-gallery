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
    public class LogicalAnd : BinarOp
    {
        public LogicalAnd(Expression left, Expression right) //prenese 2 podcvora - lijevi i desni operand
            : this(left, right, null)
        { }

        public LogicalAnd(Expression left, Expression right, Coordinate coord)
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
                errors.Add(new ErrorBase(ErrorTypes.Semantic, string.Format("And tipova {0} i {1} nije moguće", this.left.DType.ToString(), this.right.ToString())));
                return false;
            }

            if (this.dType.DataTypeCode == DataTypeCodes.Bool)
            {
                bool L = Convert.ToBoolean(this.left.Val);
                bool R = Convert.ToBoolean(this.right.Val);
                this.val = L && R;
            }
            
            return true;
        }

        public override string ToString()
        {
            return string.Format("({0}a{1})", this.left.ToString(), this.right.ToString());
        }
    }
}
