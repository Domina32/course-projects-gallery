using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Compiler.AST.DataTypes;

namespace LIB.VirtualMachines
{
    public class Memmory
    {
        protected MemmoryLocation[] store;
        public Memmory(int capacity)
        {
            this.store = new MemmoryLocation[capacity];
            for( int i=0; i < this.store.Length; i++)
            {
                this.store[i] = new MemmoryLocation();
            }
        }

        public MemmoryLocation Read(int address)
        {
            if( address >= 0 && address < this.store.Length)
            {
                return this.store[address];
            }
            else
            {
                return null;
            }
        }

        public bool Write(int address, DataType dType, object val)
        {
            if (address >= 0 && address < this.store.Length)
            {
                this.store[address].DType = dType;
                this.store[address].Val = val;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
