using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Common;
using LIB.Compiler.Common;
using LIB.Compiler.LexicalAnalysers;
using LIB.Compiler.AST;
using LIB.Compiler.AST.DataTypes;
using LIB.Compiler.AST.Expressions;
using LIB.Compiler.AST.Instructions;

namespace LIB.Compiler.SyntaxAnalysers.RecursiveDescent
{
    /*
    ==================================================
        Language J2 Grammar
    ==================================================
    Program -> Blok 'EOF'
        ;
    Blok -> InstructionList
        ;
    InstructionList -> Instruction InstructionList
        | 'eps'
        ;
    Instruction -> Assign
        | Input
        | Output
        | Ako
        ;
    Assign -> IDN '=' Expression ';'
        ;
    Input -> 'učitati' '(' Variable ')' ';
    ;
    Output -> 'ispisati' '(' Expression ')' ';'
    ;
    Ako -> 'ako' '(' Expression ')' 'onda' '{' InstructionList '}' ';'
    ;
    Expression -> Term TermList
        ;
    TermList -> '+' Term TermList | '-' Term TermList
        | 'eps'
        ;
    Term -> Factor FactorList
        ;
    FactorList -> '*' Factor FactorList | '/' Term TermList
        | 'eps'
        ;
    Factor -> ReadVariable
        | 'INT'
        | '(' Expression ')'
        | 'EOL'
        ; 
    ReadVariable -> Variable
        ;
    Variable -> IDN
        ;
    IDN -> 'idn'
        ;
    */
    public class RDParser
    {
        protected bool isAutoDeclare;
        protected IdnTable identifierTable;
        protected LexUnitStream stream;

        protected List<ErrorBase> errors;

        public RDParser(bool isAutoDeclare)
        {
            this.isAutoDeclare = isAutoDeclare;
            this.identifierTable = new Common.IdnTable();
            this.errors = new List<ErrorBase>();
        }

        public List<ErrorBase> Errors
        {
            get
            {
                return this.errors;
            }
        }

        public IdnTable IdentifierTable
        {
            get
            {
                return this.identifierTable;
            }
        }

        protected void WriteError(string text)
        {
            this.errors.Add(new ErrorBase(ErrorTypes.Syntax, text));
        }

        protected void WriteError(LexUnit found, string expected)
        {
            this.errors.Add(new Error(ErrorTypes.Syntax, string.Format("Očekivano {0}, pronađeno {1}", expected, found.UnitText), found.Coord));
        }

        protected bool Expect(string symbol)
        {
            return this.stream.Next.UnitAttribute == symbol; //provjeravamo jeli atribut sljedece leks jednke jednak simbolu
        }

        public Prog Analyze(LexUnitStream stream) //stream je lista leksickih jedinki - sadrzi svojstvo next(sljedeca leks jedinka), prethodna, te koliko leks jedinki ima
        {
            this.errors.Clear();
            this.identifierTable.Clear();
            this.stream = stream;

            return this.GetProg(); //
        }

        // Program -> Blok 'EOF'          
        // blok je nezavrsni znak a eof je zavrsni znak da je kraj datoteke
        protected Prog GetProg()
        {
            Block block = this.GetBlock();
            if( block != null) //provjeravamo jeli sljedeci znak eof , ako je -> sve u redu, ako ne ->imamo gresku
            {
                if( Expect("EOF"))
                {
                    stream.Read();
                    return new Prog(block); // vracamo objekt koji predstavlja cvor programa, a taj cvor programa sadrzi blok instrukcija (ucitati, ispisati, pridjeliti)
                }
                else
                {
                    WriteError(stream.Next, "EOF");
                }
            }

            return null;
        }

        // Blok -> InstructionList
        //prema nasim gramatickim pravilima blok povlaci instrukcijsku listu
        protected Block GetBlock()
        {
            List<Instruction> list = GetInstructionList();
            
            if (list.Count > 0)
            {
                return new Block(list, list[0].Coord);
            }
            else
            {
                WriteError("Program ne sadrži niti jednu instrukciju");
            }

            return null;
        }

        // InstructionList -> Instruction InstructionList         gramaticko pravilo kaze da instrukcijska lista razvija jednu od 2 instrukcijske opcije -> 1. kada imamo nanizane instrukcije u instrukc bloku
        // | 'eps' 2. opcija prazan znak-> lijeva strana ne razvija nista (kada zavrsimo listu)
        protected List<Instruction> GetInstructionList()
        {
            List<Instruction> list = new List<Instruction>();

            Instruction item = GetInstruction();
            while( item != null)
            {
                list.Add(item);
                item = GetInstruction();
            }

            return list;
        }
        //3 vrste instrukcija
        // Instruction -> Assign 
        // | Input
        // | Output
        // | Ako
        protected Instruction GetInstruction()
        {   //na temelju sljedecega znaka u listi leks jedinki mi znamo koju instrukciju trebamo pozvati
            if (Expect("idn"))
            {
                return GetAssign(); //radi pridjeljivanje jer je u pitanju varijabla -> getAssig vraca objekt abstraktnog sintaksnog stabla
            }
            else if (Expect("učitati"))
            {
                return GetInput(); //obradujemo ulaz
            }
            else if( Expect("ispisati"))
            {
                return GetOutput(); //obradujemo izlaz
            }
            else if (Expect("ako"))
            {
                return GetAko();
            }
            else if( Expect("EOF"))
            {
                //nista necemo pozivati
            }
            else //sintaksna greska
            {
                WriteError(stream.Next, "idn, učitati ili ispisati ili ako"); //ispisujemo gresku koju smo pronasli, i sto smo ocekivali
            }

            return null;
        }

        // Assign -> IDN '=' Expression ';'
        protected Assign GetAssign() //vraca cvor apstraktnog sintaksnog stabla koji predstavlja pridjeljivanje
        {
            LexUnit idn = stream.Read(); //pozivamo leksicku jedinku koja mora biti indentifikator

            if( Expect("="))
            {
                stream.Read();
                Expression expr = GetExpression(); //pozivamo funkciju za analizu izraza
                if (expr != null)
                {
                    if (Expect(";"))
                    {
                        stream.Read();
                        Variable lsd = new Variable(idn.UnitText, idn.Coord); //sada izgradujemo cvor koji predstavlja funkciju pridjeljivanja jer imamo idn pa = pa izraz pa ; i njeno ime je prema tekstu u izovrnom kodu
                        return new Assign(lsd, expr, lsd.Coord); //pozivamo fun pridjlejivanja (left side varijabla, izraz, koord)
                    }
                    else
                    {
                        WriteError(stream.Next, ";");
                    }
                }
            }
            else
            {
                WriteError(stream.Next, "=");
            }

            return null;
        }

        // Input -> 'učitati' '(' Variable ')' ';'         -> gramaticko pravilo leks jedinka "ucitati"
        protected Input GetInput()
        {
            LexUnit unit = stream.Read(); //znamo da smo dosli do kljucne rijeci "ucitati" pa ju netrebamo provjeravati vec mozemo napredovati
            if( Expect("("))
            {
                stream.Read(); //procitali smo zagradu pa napredujemo dalje
                if( Expect("idn"))
                {
                    Variable idn = GetVariable(); //vracamo cvor koji predstavlja cvor apstraktnog sint stabla -varijabla sa imenom i kordinatama
                    if (idn != null)
                    {
                        if (Expect(")"))
                        {
                            stream.Read();
                            if (Expect(";"))
                            {
                                stream.Read();
                                return new Input(idn, unit.Coord); //napravimo novi cvor ulaza - funkcije Input koja prima samo idn (samo 1 varijablu sa ulaza mozemo) i koord
                            }
                            else
                            {
                                WriteError(stream.Next, ";");
                            }
                        }
                        else
                        {
                            WriteError(stream.Next, ")");
                        }
                    }
                }
                else
                {
                    WriteError(stream.Next, "varijabla");
                }
            }
            else
            {
                WriteError(stream.Next, "(");
            }

            return null;
        }

        //  Izlaz -> 'ispisati' '(' Izraz ')' ';'
        protected Output GetOutput()
        {
            LexUnit unit = stream.Read();
            if (Expect("("))
            {
                stream.Read();
                Expression expr = GetExpression();
                if (expr != null)
                {
                    if (Expect(")"))
                    {
                        stream.Read();
                        if (Expect(";"))
                        {
                            stream.Read();
                            return new Output(expr, unit.Coord);
                        }
                        else
                        {
                            WriteError(stream.Next, ";");
                        }
                    }
                    else
                    {
                        WriteError(stream.Next, ")");
                    }
                }
            }
            else
            {
                WriteError(stream.Next, "(");
            }

            return null;
        }

        // Input -> 'ako' '(' Expression ')' 'onda' '{' InstructionList '}' ';'
        protected Output GetAko()
        {
            LexUnit unit = stream.Read();
            if (Expect("("))
            {
                stream.Read();
                Expression expr = GetExpression();
                if (expr != null)
                {
                    if (Expect(")"))
                    {
                        stream.Read();

                        if (Expect("onda"))
                        {
                            stream.Read();

                            if (Expect("{"))
                            {
                                stream.Read();
                                Instruction instr = GetInstruction();
                                if(instr != null)
                                {
                                    if (Expect("}"))
                                    {
                                        
                                        if (Expect(";"))
                                        {
                                            
                                        }
                                        else
                                        {
                                            WriteError(stream.Next, "}");
                                        }
                                    }
                                    else
                                    {
                                        WriteError(stream.Next, "}");
                                    }
                                }
                            }
                            else
                            {
                                WriteError(stream.Next, "{");
                            }
                        }
                        else
                        {
                            WriteError(stream.Next, "onda");
                        }
                    }
                    else
                    {
                        WriteError(stream.Next, ")");
                    }
                }
            }
            else
            {
                WriteError(stream.Next, "(");
            }

            return null;
        }

        // Expression -> Term TermList      izraz razvija pribrojnika pa eventualno listu pribrojnika
        protected Expression GetExpression()
        {
            Expression left = GetTerm(); //"npr pribrojnik" sa lijeve strane
            if (left != null)
            {
                string symbol = stream.Next.UnitAttribute;
                Expression right = GetTermList();
                while (right != null)
                {
                    switch(symbol)
                    {
                        case "+":
                            left = new Add(left, right);
                            break;
                        case "-":
                            left = new Subtract(left, right);
                            break;
                        default:
                            throw new Exception("Pogrešan simbol");
                    }
                    symbol = stream.Next.UnitAttribute;
                    right = GetTermList();
                }
            }

            return left;
        }

        // ListaTerma -> '+' Term ListaTerma
        //             | '-' Term ListaTerm
        // | 'eps'
        protected Expression GetTermList()
        {
            if (Expect("+") || Expect("-"))
            {
                stream.Read();
                return GetTerm();
            }

            return null;
        }
        // Term -> Factor '*' FactorsList
        // | 'eps'
        // engl. Term: Parts of an expression or series separated by + or – signs,
        // or the parts of a sequence separated by commas.

        protected Expression GetTerm()
        {
            Expression left = GetFactor();
            if (left != null)
            {
                string symbol = stream.Next.UnitAttribute;
                Expression right = GetFactorList();
                while (right != null)
                {
                    switch(symbol)
                    {
                        case "*":
                            left = new Mul(left, right, left.Coord);
                            break;
                        case "/"://moj kod
                            left = new Divide(left, right, left.Coord);
                            break;
                        case "%"://moj kod
                            left = new Mod(left, right, left.Coord);
                            break;
                        case "=="://moj kod
                            left = new IsEqual(left, right, left.Coord);
                            break;
                        case "!="://moj kod
                            left = new IsNotEqual(left, right, left.Coord);
                            break;
                        case "<="://moj kod
                            left = new IsLessOrEqual(left, right, left.Coord);
                            break;
                        case ">="://moj kod
                            left = new IsGreaterOrEqual(left, right, left.Coord);
                            break;
                        case "<"://moj kod
                            left = new IsLess(left, right, left.Coord);
                            break;
                        case ">"://moj kod
                            left = new IsGreater(left, right, left.Coord);
                            break;
                        case "o"://moj kod
                            left = new LogicalOr(left, right, left.Coord);
                            break;
                        case "a"://moj kod
                            left = new LogicalAnd(left, right, left.Coord);
                            break;
                        case "!"://moj kod
                            left = new LogicalNot(left, left.Coord);
                            break;
                    }
                    
                    symbol = stream.Next.UnitAttribute;
                    right = GetFactorList();
                }
            }
            return left;
        }

        // Factor -> ReadVariable       faktor prema gram pravilu poziva ili metodu za citanje varijable ili
        // | 'INT'                      ili moze predstavljati literal iz skupa cijelih brojeva
        // | '(' Izraz ')'              faktor moze povlaciti otvorenu zagradu pa izraz pa zatvorenu
        // | 'EOL'                      a moze biti i oznaka za kraj linije
        protected Expression GetFactor()
        {
            if( Expect("idn"))
            {
                return GetReadVariable();
            }
            else if( Expect("INT"))
            {
                return GetIntLiteral();
            }
            else if( Expect("("))
            {
                stream.Read();
                Expression expr = GetExpression();
                if( Expect(")"))
                {
                    stream.Read();
                    return expr;
                }
                else
                {
                    WriteError(stream.Next, ")");
                }
            }
            else if( Expect("EOL"))
            {
                return GetEndOfLine();
            }
            else
            {
                WriteError(stream.Next, "identifikator, cijeli broj ili (");
            }
            return null;
        }

        // FactorList -> '*' Factor FactorList | '/' | '%' | == | != | >= | <= | > | < | or | and | !
        // | 'eps'
        protected Expression GetFactorList()
        {
            if( Expect("*") || Expect("/") ||  Expect("%") || Expect("==") || Expect("!=") || Expect(">=") || Expect("<=") || Expect(">") || Expect("<") || Expect("!") || Expect("o") || Expect("a")) // moj kod
            {
                stream.Read();
                return GetFactor();
            }

            return null;
        }

        // Variable -> IDN
        protected Variable GetVariable()
        {
            Identifier idn = GetIdentifier(); //ovdje vracamo objekt koji predstavlja cvor indentifikatora u apstraktnom sintaksnom stablu
            IdnRow alloc = identifierTable.Find(idn); //indentifikator dodajemo u tablicu indentifikatora 
            if( alloc == null) //alokacijski redak iz tablice indentifikatora
            {
                if( this.isAutoDeclare) //u nasem programskom jeziku je dopusteno automatsko upisivanje indentifikatora u tablicu indentifikatora - autodeklaracija 
                {
                    idn.IdentifierType = IdentifierTypes.Variable; //stvorit cemo indentifikator tipa varijabla
                    identifierTable.Append(idn, RealDataType.Instance); //dodajemo u tablicu indentifkitaora, tipa realnog broja
                    return new Variable(idn.Name, idn.Coord); //vracamo cvor koji predstavlja cvor apstraktnog sint stabla - varijabla sa imenom i kordinatama
                } //deklaracija prijava imena i podatkovnog tipa varijable i definicija- pridjeljivanje kontkretne vrijednosti/broja varijabli
                else
                {
                    WriteError(string.Format("Identifikator {0} na poziciji {1} nije deklariran", idn.Name, idn.Coord));
                }
            }
            else
            {
                if (alloc.Idn.IdentifierType == IdentifierTypes.Variable) //indentifikator je varijabla
                {
                    return new Variable(idn.Name, idn.Coord); //pravimo cvor apstraktnog sintaksnog stabla koji je varijabla
                }
                else
                {
                    WriteError(string.Format("Identifikator {0} na poziciji {1} je {2}, a koristi se kao varijabla", idn.Name, idn.Coord, idn.IdentifierType));
                }
            }

            return null;
        }

        // ReadVariable -> Variable
        protected ReadVariable GetReadVariable()
        {
            Variable v = GetVariable();
            if( v != null)
            {
                return new ReadVariable(v, v.Coord);
            }

            return null;
        }

        // 'INT'
        protected IntLiteral GetIntLiteral()
        {
            LexUnit unit = stream.Read();
            int n = int.Parse(unit.UnitText);

            return new IntLiteral(n, unit.Coord);
        }

        // IDN -> 'idn'
        protected Identifier GetIdentifier()
        {
            LexUnit unit = stream.Read();

            return new Identifier(IdentifierTypes.Undefined, unit.UnitText, unit.Coord);  //radimo novi cvor koji predstavlja indentifikator 
        }

        // 'EOL'
        protected StringLiteral GetEndOfLine()
        {
            LexUnit unit = stream.Read();

            return new AST.Expressions.StringLiteral("\n\r", unit.Coord);
        }
        
    }
}
