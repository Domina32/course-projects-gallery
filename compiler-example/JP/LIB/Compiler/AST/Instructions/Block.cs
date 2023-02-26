using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Common;
using LIB.Compiler.Common;
using LIB.VirtualMachines;

namespace LIB.Compiler.AST.Instructions
{
    public class Block : Instruction
    {
        protected List<Instruction> items;

        public Block()
            : this(null)
        { }

        public Block(List<Instruction> items, Coordinate coord)
            :base(InstructionTypes.Block, coord)
        {
            this.items = items;
        }

        public Block(Coordinate coord):base(InstructionTypes.Block, coord)
        {
            this.items = new List<Instruction>();
        }

        public void Append(Instruction item)
        {
            this.items.Add(item);
        }

        public override bool Interpret(IdnTable idnTable, Memmory memmory, List<ErrorBase> errors)
        {
            foreach(Instruction item in this.items)
            {
                if(!item.Interpret(idnTable, memmory, errors))
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach(Instruction item in this.items)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }
    }
}
