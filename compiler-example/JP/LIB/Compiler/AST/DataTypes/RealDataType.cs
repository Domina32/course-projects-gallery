using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Compiler.Common;

namespace LIB.Compiler.AST.DataTypes
{
    public class RealDataType : DataType
    {
        protected static RealDataType instance;

        public RealDataType()
            :base(DataTypeCodes.Real, null)
        { }

        public static RealDataType Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RealDataType();
                }
                return instance;
            }
        }

        public override string ToString()
        {
            return "Real";
        }
    }
}
