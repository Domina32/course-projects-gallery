using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Common;
using LIB.Compiler.Common;
using LIB.Compiler.AST.DataTypes;
using LIB.Compiler.AST.Expressions;
using LIB.VirtualMachines;

namespace LIB.Compiler.AST.Instructions
{
    public class Assign : Instruction
    {
        protected Variable idn;
        protected Expression expr;

        public Assign(Variable idn, Expression expr)
            :this(idn, expr, null)
        { }

        public Assign(Variable idn, Expression expr, Coordinate coord) //konstruktor prima varijablu kojoj ptreba pridjeliti izraz, pa sam taj izraz (literal, binarna operacija ...), kordinate
            :base(InstructionTypes.Assignment, coord) //kordinate koje oznacavaju redak i kolonu gdje smo mi tu operaciju pridjeljivanja pronasli
        {
            this.idn = idn;
            this.expr = expr;
        }

        public override bool Interpret(IdnTable idnTable, Memmory memmory, List<ErrorBase> errors)
        {
            if(!this.expr.Interpret(idnTable, memmory, errors)) //prvo se interpretira izraz - ako nastupi greska prekidamo interpretaciju tj izvrsavanje programa 
            {
                return false;
            }

            IdnRow alloc = idnTable.Find(this.idn); //moramo doci do memorijske lokacije ove varijable, uz pomoc tablice indentifikatora
            if( alloc == null) // c# zahtjeva eksplicitnu deklaraciju, a neki drugi jezici npr python implicitno na temelju literala koji se dodjeljuje varijabli (jeli to cijeli broj, realni, bool)
            {
                errors.Add(new ErrorBase(ErrorTypes.Semantic, string.Format("Varijabla {0} nije deklarirana", this.idn.Name)));
                return false;
            }

            memmory.Write(alloc.Address, alloc.DType, this.expr.Val); //u memoriju na lokaciju adrese zapise vrijednost izraza

            return true;
        }

        public override string ToString()
        {
            return string.Format("{0} = {1};", this.idn.Name, this.expr.ToString());
        }
    }
}
