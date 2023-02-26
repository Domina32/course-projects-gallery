using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Compiler.Common;
using LIB.Compiler.AST.DataTypes;

namespace LIB.Compiler.AST
{
    public enum IdentifierTypes
    {
        Undefined,
        Variable,
        Function
    }

    public class Identifier : Node
    {
        protected IdentifierTypes identifierType;
        protected string name;

        

        public Identifier(IdentifierTypes identifierType, string name, Coordinate coord)
            :base(NodeTypes.IDN, coord)
        {
            this.identifierType = identifierType;
            this.name = name;
        }

        public IdentifierTypes IdentifierType
        {
            get
            {
                return this.identifierType;
            }
            set
            {
                this.identifierType = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }
    }
}
