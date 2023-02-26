using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Compiler.Common;
using LIB.Compiler.AST.DataTypes;

namespace LIB.Compiler.AST.Expressions
{
    public enum ExpressionTypes
    {
        Literal,
        ReadVariable,
        Add,
        Mul,
        Mod,
        Subtract,
        Divide,
        IsEqual,
        IsNotEqual,
        IsLessOrEqual,
        IsGreaterOrEqual,
        IsLess,
        IsGreater
    }

    public abstract class Expression : Node
    {
        protected ExpressionTypes expressionType;
        protected DataType dType;
        protected object val;

        public Expression(ExpressionTypes expressionType, Coordinate coord)
            :base(NodeTypes.Expression, coord)
        {
            this.expressionType = expressionType;
        }

        public ExpressionTypes ExpressionType
        {
            get
            {
                return this.expressionType;
            }
        }

        public DataType DType
        {
            get
            {
                return this.dType;
            }
            set
            {
                this.dType = value;
            }
        }

        public object Val
        {
            get
            {
                return this.val;
            }
            set
            {
                this.val = value;
            }
        }

        public virtual DataType GetResultDataType()
        {
            throw new NotFiniteNumberException();
        }
    }
}
