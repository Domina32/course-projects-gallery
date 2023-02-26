using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Compiler.Common;

namespace LIB.Compiler.AST.DataTypes
{
    public class ErrorDataType : DataType
    {
        protected static ErrorDataType instance;

        public ErrorDataType()
            : this(null)
        { }

        public ErrorDataType(Coordinate coord) : 
            base(DataTypeCodes.Error, coord)
        { }

        public static ErrorDataType Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ErrorDataType();
                }
                return instance;
            }
        }
    }
}
