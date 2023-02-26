using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Compiler.Common;

namespace LIB.Compiler.AST
{
    public class Variable : Identifier
    {
        public Variable(string name)
            :this(name, null)
        {}

        public Variable(string name, Coordinate coord):base(IdentifierTypes.Variable, name, coord)
        {}
    }
}
