using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.Compiler.AST.DataTypes
{
    public class StringDataType : DataType
    {
        protected static StringDataType instance;

        public StringDataType()
            :base(DataTypeCodes.String, null)
        {

        }

        public static StringDataType Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StringDataType();
                }
                return instance;
            }
        }

        public override string ToString()
        {
            return "String";
        }
    }
}
