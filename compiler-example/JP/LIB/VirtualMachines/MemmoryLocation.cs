using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Compiler.AST.DataTypes;

namespace LIB.VirtualMachines
{
    public class MemmoryLocation
    {
        protected DataType dType;
        protected object val;

        public DataType DType
        {
            get
            {
                return this.dType;
            }
            set
            {
                this.dType = value;
            }
        }

        public object Val
        {
            get
            {
                return this.val;
            }
            set
            {
                this.val = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", this.dType != null ? this.dType.DataTypeCode.ToString() : "?", this.val != null ? this.val : "?");
        }
    }
}
