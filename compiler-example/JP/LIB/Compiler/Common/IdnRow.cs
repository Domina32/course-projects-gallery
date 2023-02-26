using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Compiler.AST;
using LIB.Compiler.AST.DataTypes;

namespace LIB.Compiler.Common
{
    public enum LifeTimeTypes
    {
        Automatic,
        Static
    }

    public class IdnRow
    {
        protected string function;
        protected Identifier idn;
        protected DataType dType;
        protected LifeTimeTypes lifeTime;
        protected int address;

        public IdnRow(string function, Identifier idn, DataType dType, LifeTimeTypes lifeTime, int address)
        {
            this.function = function;
            this.idn = idn;
            this.dType = dType;
            this.address = address;
        }

        public string Function
        {
            get
            {
                return this.function;
            }
        }
        public Identifier Idn
        {
            get
            {
                return this.idn;
            }
        }

        public DataType DType
        {
            get
            {
                return this.dType;
            }
        }

        public LifeTimeTypes LifeTime
        {
            get
            {
                return this.lifeTime;
            }
        }

        public int Address
        {
            get
            {
                return this.address;
            }
        }
    }
}
