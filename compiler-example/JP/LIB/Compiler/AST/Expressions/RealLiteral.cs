using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Common;
using LIB.VirtualMachines;
using LIB.Compiler.Common;
using LIB.Compiler.AST.DataTypes;

namespace LIB.Compiler.AST.Expressions
{
    public class RealLiteral : Expression
    {
        public RealLiteral(double x)
            :this(x, null)
        { }

        public RealLiteral(double x, Coordinate coord)
            :base(ExpressionTypes.Literal, coord)
        {
            this.dType = RealDataType.Instance;
            this.val = x;
        }

        public override bool Interpret(IdnTable idnTable, Memmory memmory, List<ErrorBase> errors)
        {
            return true;
        }

        public override string ToString()
        {
            return this.val.ToString();
        }
    }
}
