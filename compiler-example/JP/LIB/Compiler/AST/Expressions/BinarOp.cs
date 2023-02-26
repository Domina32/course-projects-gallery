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
    public abstract class BinarOp : Expression
    {
        protected Expression left;
        protected Expression right;

        public BinarOp(ExpressionTypes expressionType, Expression left, Expression right, Coordinate coord)
            : base(expressionType, coord)
        {
            this.left = left;
            this.right = right;
        }

        public Expression Left
        {
            get
            {
                return this.left;
            }
        }

        public Expression Right
        {
            get
            {
                return this.right;
            }
        }

        public override bool Interpret(IdnTable idnTable, Memmory memmory, List<ErrorBase> errors)
        {
            bool ok = this.left.Interpret(idnTable, memmory, errors);
            if (ok)
            {
                ok = this.right.Interpret(idnTable, memmory, errors);
            }

            if (!ok)
            {
                return false;
            }

            this.dType = this.GetResultDataType();

            return true;
        }

        public override DataType GetResultDataType()
        {
            if( this.left.DType.DataTypeCode == this.right.DType.DataTypeCode)
            {
                if(this.left.DType.DataTypeCode == DataTypeCodes.Int
                    || this.left.DType.DataTypeCode == DataTypeCodes.Real)
                {
                    return this.left.DType;
                }
                else
                {
                    return ErrorDataType.Instance;
                }
            }
            else if( this.left.DType.DataTypeCode == DataTypeCodes.Int 
                && this.right.DType.DataTypeCode == DataTypeCodes.Real)
            {
                return this.right.DType;
            }

            else if (this.left.DType.DataTypeCode == DataTypeCodes.Real
                && this.right.DType.DataTypeCode == DataTypeCodes.Int)
            {
                return this.left.DType;
            }
            else
            {
                return ErrorDataType.Instance;
            }
        }
    }
}
