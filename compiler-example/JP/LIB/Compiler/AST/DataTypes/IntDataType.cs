using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Compiler.Common;

namespace LIB.Compiler.AST.DataTypes
{
    public class IntDataType : DataType
    {
        protected static IntDataType instance;

        public IntDataType()
            :base(DataTypeCodes.Int, null)
        {

        }

        public static IntDataType Instance
        {
            get
            {
                if( instance == null)
                {
                    instance = new IntDataType();
                }
                return instance;
            }
        }

        public override string ToString()
        {
            return "Int";
        }
    }
}
