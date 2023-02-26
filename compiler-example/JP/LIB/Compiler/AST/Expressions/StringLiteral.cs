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
    public class StringLiteral : Expression
    {
        public StringLiteral(string s)
            :this(s, null)
        { }

        public StringLiteral(string s, Coordinate coord)
            :base(ExpressionTypes.Literal, coord)
        {
            this.dType = StringDataType.Instance;
            this.val = s;
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
