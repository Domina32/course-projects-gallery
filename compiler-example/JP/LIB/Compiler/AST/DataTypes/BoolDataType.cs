using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Compiler.Common;

namespace LIB.Compiler.AST.DataTypes
{
    public class BoolDataType : DataType
    {
        protected static BoolDataType instance;

        public BoolDataType()
            : base(DataTypeCodes.Bool, null)
        { }

        public static BoolDataType Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BoolDataType();
                }
                return instance;
            }
        }

        public override string ToString()
        {
            return "Bool";
        }
    }
}
