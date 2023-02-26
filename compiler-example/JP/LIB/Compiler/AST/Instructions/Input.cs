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
    public class Input : Instruction
    {
        protected Variable idn;

        public Input(Variable idn)
            :this(idn, null)
        { }

        public Input(Variable idn, Coordinate coord)
            :base(InstructionTypes.Input, coord)
        {
            this.idn = idn;
        }

        public override bool Interpret(IdnTable idnTable, Memmory memmory, List<ErrorBase> errors)
        {
            IdnRow alloc = idnTable.Find(this.idn);
            if (alloc == null)
            {
                errors.Add(new ErrorBase(ErrorTypes.Semantic, string.Format("Varijabla {0} nije deklarirana", this.idn.Name)));
                return false;
            }

            Console.Write("{0} ?=", this.idn.Name);
            string input = Console.ReadLine();
            bool ok = true;
            switch(alloc.DType.DataTypeCode)
            {
                case DataTypes.DataTypeCodes.Int:
                    int n;
                    if( int.TryParse(input, out n))
                    {
                        memmory.Write(alloc.Address, alloc.DType, n);
                    }
                    else
                    {
                        errors.Add(new ErrorBase(ErrorTypes.Semantic, "Input: učekivan je ulaz tipa Int"));
                        ok = false;
                    }
                    break;
                case DataTypes.DataTypeCodes.Real:
                    double x;
                    if (double.TryParse(input, out x))
                    {
                        memmory.Write(alloc.Address, alloc.DType, x);
                    }
                    else
                    {
                        errors.Add(new ErrorBase(ErrorTypes.Semantic, "Input: učekivan je ulaz tipa Real"));
                        ok = false;
                    }
                    break;
            }

            return ok;
        }

        public override string ToString()
        {
            return string.Format("učitati({0});", this.idn.Name);
        }
    }
}
