using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Common;
using LIB.Compiler.Common;
using LIB.VirtualMachines;

namespace LIB.Compiler.AST
{
    public enum NodeTypes //moguce vrste cvorova u prvoj liniji nasljedivanja
    {
        IDN,
        Datatype,
        Expression,
        Instruction
    }

    public abstract class Node
    {
        protected NodeTypes nodeType;
        protected Coordinate coord;

        public Node(NodeTypes nodeType, Coordinate coord)
        {
            this.nodeType = nodeType;
            this.coord = coord;
        }

        public NodeTypes NodeType
        {
            get
            {
                return this.nodeType;
            }
        }

        public Coordinate Coord
        {
            get
            {
                return this.coord;
            }
        }

        public virtual bool Interpret(IdnTable idnTable, Memmory memmory, List<ErrorBase> errors) //virtual oznacava da u klasama koje budu nasljedivale ovaj cvor mozemo overvritati
        { // vraca bool ako je interpretacija uspjesna vraca se istina a ako nije onda nemozemo nastaviti sa interpretiranjem stabla 
            throw new NotImplementedException(this.GetType().ToString());
        }
    }
}
