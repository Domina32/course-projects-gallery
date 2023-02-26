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
    public class IntLiteral : Expression
    {
        public IntLiteral(int n)
            :this(n, null)
        {}

        public IntLiteral(int n, Coordinate coord)
            :base(ExpressionTypes.Literal, coord)
        {
            this.dType = IntDataType.Instance;
            this.val = n; //prenese vrijednost i zapise u objekt val (expression)
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
