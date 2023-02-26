using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using LIB.Common;
using LIB.Compiler.Common;
using LIB.Compiler.LexicalAnalysers;
using LIB.Compiler.LexicalAnalysers.Simple;
using LIB.Compiler.AST;
using LIB.Compiler.AST.DataTypes;
using LIB.Compiler.AST.Expressions;
using LIB.Compiler.AST.Instructions;
using LIB.VirtualMachines;
using LIB.Compiler.SyntaxAnalysers.RecursiveDescent;

namespace TestConsole
{
    public abstract class Ptica
    {
        public virtual void Pjevaj()
        {
            Console.WriteLine("Ptica je apstraktna i ne može pjevati");
        }
    }
    public class Slavuj : Ptica
    {
        public override void Pjevaj()
        {
            base.Pjevaj();
            Console.WriteLine("Slavuj bigliše (lijepo)!");
        }
    }

    public class Svraka : Ptica
    {
        public override void Pjevaj()
        {
            Console.WriteLine("Svraka grakće (ružno)!");
        }
    }

    class Program
    {
        private static void test_lexer(string code)
        {
            
            Config x = new Config();
            Console.WriteLine(x.Lexer);
            Console.WriteLine(Config.Instance.Lexer);

            SimpleLexerEngine lex = LexerBuilder.LexerEngineFromXml(Config.Instance.Lexer);
            lex.SourceText = code;

            LexUnitStream stream = lex.GetStream(); //objekt kao lista leksickih jedinki
            while (!stream.IsEof)
            {
                LexUnit unit = stream.Read();
                Console.WriteLine(unit.ToString());
            }
        }

        private static void test_AST()
        { 
            //kod poziva metode interpret moramo imati:
            IdnTable it = new IdnTable();
            Memmory mem = new Memmory(5);
            List<ErrorBase> errors; //listu gresaka
                        
            //cvorovi stabla
            IntLiteral dva = new IntLiteral(2); //radimo objekt cvora literala koji ima vrijednost 2
            IntLiteral tri = new IntLiteral(3);
            IntLiteral cetiri = new IntLiteral(4);

            //radimo stablo
            // (2+3)*4
            Add op1 = new Add(dva, tri);
            Mul root = new Mul(op1, cetiri);
            errors = new List<ErrorBase>();
            root.Interpret(it, mem, errors);
            Console.WriteLine("{0} = {1} : {2}", root.ToString(), root.Val, root.DType.DataTypeCode);
            Console.WriteLine("<ENTER> za nastavak.");
            Console.ReadLine();

            // 2 + 3*4
            Expression r = new Add(dva, new Mul(tri, cetiri));
            errors = new List<ErrorBase>();
            r.Interpret(it, mem, errors);
            Console.WriteLine("{0} = {1} : {2}", r.ToString(), r.Val, r.DType.DataTypeCode);
            Console.WriteLine("<ENTER> za nastavak.");
            Console.ReadLine();

            // Program u jeziku J2:
            // int x, y;
            // učitati(x);
            // y = x + 1;
            // ispisati("Y=");
            // ispisati(y);

            Variable x = new Variable("x");
            Variable y = new Variable("y");
            it.Append(x, IntDataType.Instance);
            it.Append(y, IntDataType.Instance);

            Block b = new Block();
            b.Append(new Input(x));
            Expression rsd = new Add(new ReadVariable(x), new IntLiteral(1));
            b.Append(new Assign(y, rsd));
            b.Append(new Output(new StringLiteral("Y=")));
            b.Append(new Output(new ReadVariable(y)));
            b.Append(new Output(new StringLiteral("\n\r")));

            Console.WriteLine(b.ToString());
            errors = new List<ErrorBase>();
            b.Interpret(it, mem, errors);
        }

        static void Main(string[] args)
        {
            string code = System.IO.File.ReadAllText(Config.Instance.SampleFile);
            Console.WriteLine(code);
            test_lexer(code);

            //test_AST();

            SimpleLexerEngine lex = LexerBuilder.LexerEngineFromXml(Config.Instance.Lexer);
            RDParser parser = new RDParser(true);

            lex.SourceText = code; //lexeru proslijedimo izvorni kod naseg programa
            Prog p = parser.Analyze(lex.GetStream()); //pozivamo metodu analyze sa listom leksickih jedinki -  metoda analyze vraca stablo koje predstavlja program
            if( parser.Errors.Count == 0) //ako nema gresaka pristupamo interpretaciji
            {
                Console.WriteLine("Nema sintaksnih pogrešaka. Ispis programa iz AST:");
                Console.WriteLine(p.ToString());
                Memmory ram = new Memmory(10);
                p.Interpret(parser.IdentifierTable, ram, parser.Errors); //metoda interpretacije stala zahtjeva tablicu idn, a nju je izgradio parser, zatim zahtjeva memoriju, te listu gresaka
            }
            else
            {
                Console.WriteLine("Program ima {0} sintaksnih pogrešaka", parser.Errors.Count);
                for( int i=0; i <  parser.Errors.Count; i++)
                {
                    Console.WriteLine("{0}. {1}", i, parser.Errors[i].ToString());
                }
            }

            Console.WriteLine();
            Console.WriteLine("<ENTER> za kraj.");
            Console.ReadLine();
        }
    }
}
