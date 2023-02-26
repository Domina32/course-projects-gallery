using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Compiler.Common;

namespace LIB.Compiler.AST.DataTypes
{
    public enum DataTypeCodes
    {
        Int,
        Real,
        Bool,
        Char,
        String,
        Void,
        Error
    }

    public abstract class DataType : Node
    {
        protected DataTypeCodes dataTypeCode;

        public DataType(DataTypeCodes dataTypeCode, Coordinate coord)
            :base(NodeTypes.Datatype, coord)
        {
            this.dataTypeCode = dataTypeCode;
        }

        public DataTypeCodes DataTypeCode
        {
            get
            {
                return this.dataTypeCode;
            }
        }
    }
}
