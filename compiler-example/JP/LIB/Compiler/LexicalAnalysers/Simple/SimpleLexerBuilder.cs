using System;
using System.Collections.Generic;
using System.Text;

namespace LIB.Compiler.LexicalAnalysers.Simple
{
    public class SimpleLexerBuilder
    {
        public SimpleLexerBuilder()
        { }

		public static SimpleLexerEngine GetTreeLexer()
		{
			SimpleLexerEngine reng = new SimpleLexerEngine(false);

			RegexItem item = new RegexItem("node_delimiter", "this", "[()]");
			reng.Items.Add(item);

			item = new RegexItem("label", "label", @"[a-zA-Z0-9]+|[+\-\*/%=><!\(\)]=?");
			reng.Items.Add(item);

			item = new RegexItem("eof", "EOF", "EOF");
			reng.Items.Add(item);

			item = new RegexItem("ignore", "white", "[\n\r\t ]+");
			reng.Items.Add(item);

			return reng;
		}

        public static SimpleLexerEngine GetSimpleBasicLexer()
        {
            SimpleLexerEngine reng = new SimpleLexerEngine(false);

            RegexItem item = new RegexItem("number", "DecLiteral", "[1-9][0-9]*");
            reng.Items.Add(item);
            item = new RegexItem("number", "OctLiteral", "[0][0-9]*");
            reng.Items.Add(item);
            item = new RegexItem("literal", "HexLiteral", "0x[0-9A-Fa-f]+");
            reng.Items.Add(item);
            item = new RegexItem("literal", "floatLiteral", @"[0-9]*\.[0-9]+");
            reng.Items.Add(item);
            item = new RegexItem("literal", "charLiteral", @"'(?<get>\\\\)'|'(?<get>[^\\])'|'(?<get>\\'')");
            reng.Items.Add(item);
            item = new RegexItem("literal", "stringLiteral", @"""(?<get>([^""]|\"")*)""");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "this", "if|then|else|endif|input|print|end");
            reng.Items.Add(item);

            item = new RegexItem("eof", "EOF", "EOF");
            reng.Items.Add(item);

            item = new RegexItem("idn", "IDN", "[_a-zA-Z][_a-zA-Z0-9]*");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"[+\-\*/%=><!\(\)]=?");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"\+\+|\-\-");
            reng.Items.Add(item);

            item = new RegexItem("comment", "comment", @"/\*.*\*/");
            reng.Items.Add(item);
            item = new RegexItem("ignore", "white", "[\n\t ]+");
            reng.Items.Add(item);

            return reng;
        }

        public static SimpleLexerEngine GetBNFSimpleLexer()
        {
            SimpleLexerEngine reng = new SimpleLexerEngine(false);

            RegexItem item;
            item = new RegexItem("operator", "this", @"->|=|\|");
            reng.Items.Add(item);

            item = new RegexItem("terminal", "con", @"'(?<get>([^\\']|\\\\|\\')+)'");
            reng.Items.Add(item);
            
            item = new RegexItem("eof", "EOF", "EOF");
            reng.Items.Add(item);

            item = new RegexItem("nonterminal", "var", "[a-zA-Z][_a-zA-Z0-9]*");
            reng.Items.Add(item);

            item = new RegexItem("action", "act", @"{(?<get>[^{}]*)}");
            reng.Items.Add(item);

            item = new RegexItem("literal", "str", @"""(?<get>([^""]|\"")*)""");
            reng.Items.Add(item);

            item = new RegexItem("Syntatic", "this", ";");
            reng.Items.Add(item);

            item = new RegexItem("comment", "rem", @"!.*\n");
            reng.Items.Add(item);

            item = new RegexItem("ignore", "white", "[\r\n\t ]+");
            reng.Items.Add(item);

            return reng;
        }

        public static SimpleLexerEngine GetBNFLexer()
        {
            SimpleLexerEngine reng = new SimpleLexerEngine(false);

            RegexItem item;
            item = new RegexItem("operator", "this", @"->|=|\|");
            reng.Items.Add(item);

            item = new RegexItem("terminal", "con", @"'(?<get>([^\\']|\\\\|\\')+)'");
            reng.Items.Add(item);

            item = new RegexItem("eof", "EOF", "EOF");
            reng.Items.Add(item);

            item = new RegexItem("nonterminal", "var", "[a-zA-Z][_a-zA-Z0-9]*");
            reng.Items.Add(item);

            item = new RegexItem("parametar", "ps", @"\((?<get>[^\(\)]*)\)");
            reng.Items.Add(item);

            item = new RegexItem("parametar", "p", @"\$(?<get>[^\$ )]+)");
            reng.Items.Add(item);

            item = new RegexItem("action", "act", @"{(?<get>[^{}]*)}");
            reng.Items.Add(item);

            item = new RegexItem("literal", "str", @"""(?<get>([^""]|\"")*)""");
            reng.Items.Add(item);

            item = new RegexItem("Syntatic", "this", ";");
            reng.Items.Add(item);

            item = new RegexItem("comment", "rem", @"!.*\n");
            reng.Items.Add(item);

            item = new RegexItem("ignore", "white", "[\r\n\t ]+");
            reng.Items.Add(item);

            return reng;
        }

        public static SimpleLexerEngine GetCLangLexer()
        {
            SimpleLexerEngine reng = new SimpleLexerEngine(false);

            RegexItem item = new RegexItem("number", "DecLiteral", "[1-9][0-9]*");
            reng.Items.Add(item);
            item = new RegexItem("literal", "BoolLiteral", "true|false");
            reng.Items.Add(item);
            item = new RegexItem("literal", "OctLiteral", "[0][0-7]*");
            reng.Items.Add(item);
            item = new RegexItem("literal", "HexLiteral", "0x[0-9A-Fa-f]+");
            reng.Items.Add(item);
            item = new RegexItem("literal", "FloatLiteral", @"[0-9]*\.[0-9]+");
            reng.Items.Add(item);
            item = new RegexItem("literal", "CharLiteral", @"'(?<get>\\\\)'|'(?<get>[^\\'])'|'(?<get>\\'')|'(?<get>\\[ntr])'");
            reng.Items.Add(item);
            item = new RegexItem("literal", "StringLiteral", @"""(?<get>([^""]|\"")*)""");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "this", "const|extern|static|register|auto|volatile|signed|unsigned|char|int|short|long|float|double|bool|string|void|struct|union|enum|typedef|if|else|for|while|do|switch|goto|break|continue|return|case|default|out|in|defgoal|defexpression|goal|function|is|variant|good|bugy|style|expression");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "short int", "short[ \t]+int");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "long int", "long[ \t]+int");
            reng.Items.Add(item);

            item = new RegexItem("eof", "EOF", "EOF");
            reng.Items.Add(item);

            item = new RegexItem("idn", "idn", "[_a-zA-Z][_a-zA-Z0-9]*");
            reng.Items.Add(item);

            item = new RegexItem("comment", "comment", @"//[^/\n]*\n");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"[\+\-\*/%\|&^~=><!]=?|[\[\]\?\(\),:]");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"\|\||&&|(<<|>>)=?");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"\+\+|\-\-|\.|->");
            reng.Items.Add(item);

            item = new RegexItem("Syntatic", "this", @"[;{}]");
            reng.Items.Add(item);

            item = new RegexItem("comment", "comment", @"/\*[^/\*]*(\*)+/");
            reng.Items.Add(item);

            item = new RegexItem("comment", "commentStart", @"/\*[^\n]*\n");
            reng.Items.Add(item);

            item = new RegexItem("comment", "commentEnd", @"[^\*/]*\*+/");
            reng.Items.Add(item);

            item = new RegexItem("ignore", "white", "[\r\n\t ]+");
            reng.Items.Add(item);

            return reng;
        }

        public static SimpleLexerEngine GetPseudoLexer()
        {
            SimpleLexerEngine reng = new SimpleLexerEngine(false);

            RegexItem item = new RegexItem("number", "DecLiteral", "[1-9][0-9]*");
            reng.Items.Add(item);
            item = new RegexItem("literal", "BoolLiteral", "istinito|lažno");
            reng.Items.Add(item);
            item = new RegexItem("literal", "OctLiteral", "[0][0-7]*");
            reng.Items.Add(item);
            item = new RegexItem("literal", "HexLiteral", "0x[0-9A-Fa-f]+");
            reng.Items.Add(item);
            item = new RegexItem("literal", "FloatLiteral", @"[0-9]*\.[0-9]+");
            reng.Items.Add(item);
            item = new RegexItem("literal", "CharLiteral", @"'(?<get>\\\\)'|'(?<get>[^\\])'|'(?<get>\\'')");
            reng.Items.Add(item);
            item = new RegexItem("literal", "StringLiteral", @"""(?<get>([^""]|\"")*)""");
            reng.Items.Add(item);

            item = new RegexItem("nlp", "nlp", "#(?<get>[^#]*)#");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "var", "varijabl[ae]");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "this", "znakovni|cjelobrojni|realni|bool|tekst");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "this", "defgoal|defexpression|goal|function|is|variant|good|bugy|style|expression");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "this", "početak|kraj.|inicirati|izračunati|učitati|ispisati|ako|onda|inače|dok|činiti|za indeks");
            reng.Items.Add(item);

            item = new RegexItem("eof", "EOF", "EOF");
            reng.Items.Add(item);

            item = new RegexItem("idn", "idn", "[_a-zA-ZčČćĆđĐšŠžŽ][_a-zA-Z0-9čČćĆđĐšŠžŽ]*");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"\.\.|\|\||&&");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"[=><!]=?");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"[\+\-\*/%^\(\),:\[\]]");
            reng.Items.Add(item);

            item = new RegexItem("Syntatic", "this", @"[;{}]");
            reng.Items.Add(item);

            item = new RegexItem("comment", "comment", @"/\*.*\*/");
            reng.Items.Add(item);
            item = new RegexItem("ignore", "white", "[\r\n\t ]+");
            reng.Items.Add(item);

            return reng;
        }

        public static SimpleLexerEngine GetQuestionLexer()
        {
            SimpleLexerEngine reng = new SimpleLexerEngine(false);

            RegexItem item = new RegexItem("literal", "StringLiteral", @"'(?<get>([^'])*)'");
            reng.Items.Add(item);

            item = new RegexItem("idn", "idn", "[_a-zA-ZčČćĆđĐšŠžŽ][_a-zA-Z0-9čČćĆđĐšŠžŽ]*");
            reng.Items.Add(item);

            item = new RegexItem("ignore", "white", "[\r\n\t ]+");
            reng.Items.Add(item);

            return reng;
        }

        public static SimpleLexerEngine GetNlpLexer()
        {
            SimpleLexerEngine reng = new SimpleLexerEngine(false);

            RegexItem item = new RegexItem("literal", "DecLiteral", "[0-9][0-9]*");
            reng.Items.Add(item);
            item = new RegexItem("literal", "BoolLiteral", "istinito|lažno");
            reng.Items.Add(item);
            item = new RegexItem("literal", "OctLiteral", "[0][0-7]*");
            reng.Items.Add(item);
            item = new RegexItem("literal", "HexLiteral", "0x[0-9A-Fa-f]+");
            reng.Items.Add(item);
            item = new RegexItem("literal", "FloatLiteral", @"[0-9]*\.[0-9]+");
            reng.Items.Add(item);
            item = new RegexItem("literal", "CharLiteral", @"'(?<get>\\\\)'|'(?<get>[^\\])'|'(?<get>\\'')");
            reng.Items.Add(item);
            item = new RegexItem("literal", "StringLiteral", @"""(?<get>([^""]|\"")*)""");
            reng.Items.Add(item);

            item = new RegexItem("nlp", "nlp", "#(?<get>[^#]*)#");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "var", "varijabl[ae]");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "this", "pravilo zaključivanja|interpretirati|realni|cjelobrojni|znakovni|ako|onda|dakle|što");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "koji", "koj[iae]");
            reng.Items.Add(item);

            item = new RegexItem("eof", "EOF", "EOF");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"ne|od|da");
            reng.Items.Add(item);

            item = new RegexItem("operator", "postoji", @"postoji|barem jed[an][ao]");
            reng.Items.Add(item);

            item = new RegexItem("operator", "svaki", @"svak[iaoe]g?|bilo koj[iae][gmh]?|svi[h]?");
            reng.Items.Add(item);

            item = new RegexItem("operator", ">", @"već[iae][ ]+je|je[ ]+već[iae]");
            reng.Items.Add(item);

            item = new RegexItem("operator", ">", @"već[iae] su|su već[iae]");
            reng.Items.Add(item);

            item = new RegexItem("operator", "<", @"manj[iae] je|je manj[iae]");
            reng.Items.Add(item);

            item = new RegexItem("operator", "s", @"sa?");
            reng.Items.Add(item);

            item = new RegexItem("operator", ",", @"i");
            reng.Items.Add(item);

            item = new RegexItem("operator", "==", @"jednak([ao])?[ ]+je|je[ ]+jednak([ao])?");
            reng.Items.Add(item);

            item = new RegexItem("operator", "::=", @"jest|jesu");
            reng.Items.Add(item);

            item = new RegexItem("operator", "==", @"jednak([iea])?[ ]+su|su[ ]+jednak([iea])?");
            reng.Items.Add(item);

            item = new RegexItem("operator", "!=", @"nije([ ])+jednak([oa])?");
            reng.Items.Add(item);

            item = new RegexItem("idn", "nlp", "[_a-zA-ZčČćĆđĐšŠžŽ][_a-zA-Z0-9čČćĆđĐšŠžŽ]*");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"\.\.|&&|\|\|");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"[=><!]=?");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"[\+\-\*/%^\(\),:\[\]]");
            reng.Items.Add(item);

            item = new RegexItem("Syntatic", "this", @"[;{}\?]");
            reng.Items.Add(item);

            item = new RegexItem("comment", "comment", @"/\*.*\*/");
            reng.Items.Add(item);
            item = new RegexItem("ignore", "white", "[\r\n\t ]+");
            reng.Items.Add(item);

            return reng;
        }

        public static SimpleLexerEngine GetSemanticLexer()
        {
            SimpleLexerEngine reng = new SimpleLexerEngine(false);

            RegexItem item = new RegexItem("eof", "EOF", "EOF");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"\.\.");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"[=><!]=?");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"[\+\-\*/%^\(\),:\[\]]");
            reng.Items.Add(item);

            item = new RegexItem("Syntatic", "this", @"[;{}]");
            reng.Items.Add(item);

            item = new RegexItem("comment", "comment", @"/\*.*\*/");
            reng.Items.Add(item);
            item = new RegexItem("ignore", "white", "[\r\n\t ]+");
            reng.Items.Add(item);

            return reng;
        }

        public static SimpleLexerEngine GetNetSqlLexer()
        {
            SimpleLexerEngine reng = new SimpleLexerEngine(true);

            RegexItem item = new RegexItem("number", "DecLiteral", "[1-9][0-9]*");
            reng.Items.Add(item);
            item = new RegexItem("literal", "FloatLiteral", @"[0-9]*\.[0-9]+");
            reng.Items.Add(item);
            item = new RegexItem("literal", "CharLiteral", @"'(?<get>\\\\)'|'(?<get>[^\\])'|'(?<get>\\'')");
            reng.Items.Add(item);
            item = new RegexItem("literal", "StringLiteral", @"""(?<get>([^""]|\"")*)""");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "this", "select|from|join|where|orderby|groupby|id|value|domain|count");
            reng.Items.Add(item);

            item = new RegexItem("eof", "EOF", "EOF");
            reng.Items.Add(item);

            item = new RegexItem("var", "var", "[A-Z][_a-zA-Z0-9]*");
            reng.Items.Add(item);

            item = new RegexItem("term", "term", "[a-zčćđšž][_a-zA-Z0-9čČćĆđĐšŠžŽ]*");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"[+\-\*/%=><!\(\)]=?");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"\+\+|\-\-|,");
            reng.Items.Add(item);

            item = new RegexItem("comment", "comment", @"/\*.*\*/");
            reng.Items.Add(item);
            item = new RegexItem("ignore", "white", "[\r\n\t ]+");
            reng.Items.Add(item);

            return reng;
        }

        public static SimpleLexerEngine GetFunctionLexer()
        {
            SimpleLexerEngine reng = new SimpleLexerEngine(true);

            RegexItem item = new RegexItem("literal", "DecLiteral", "[1-9][0-9]*");
            reng.Items.Add(item);
            item = new RegexItem("literal", "FloatLiteral", @"[0-9]*\.[0-9]+");
            reng.Items.Add(item);
            item = new RegexItem("literal", "CharLiteral", @"'(?<get>\\\\)'|'(?<get>[^\\])'|'(?<get>\\'')");
            reng.Items.Add(item);
            item = new RegexItem("literal", "StringLiteral", @"""(?<get>([^""]|\"")*)""");
            reng.Items.Add(item);

            item = new RegexItem("term", "nlp", "[_a-zA-ZčČćĆđĐšŠžŽ][_a-zA-Z0-9čČćĆđĐšŠžŽ]*");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"\.\.|&&|\|\|");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"[=><!]=?");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"[\+\-\*/%^\(\),:\[\]]");
            reng.Items.Add(item);

            item = new RegexItem("comment", "comment", @"/\*.*\*/");
            reng.Items.Add(item);
            item = new RegexItem("ignore", "white", "[\r\n\t ]+");
            reng.Items.Add(item);

            return reng;
        }

        public static SimpleLexerEngine GetCPrettyLexer()
        {
            SimpleLexerEngine reng = new SimpleLexerEngine(false);

            RegexItem item = new RegexItem("number", "DecLiteral", "[1-9][0-9]*");
            reng.Items.Add(item);
            item = new RegexItem("literal", "BoolLiteral", "true|false");
            reng.Items.Add(item);
            item = new RegexItem("literal", "OctLiteral", "[0][0-7]*");
            reng.Items.Add(item);
            item = new RegexItem("literal", "HexLiteral", "0x[0-9A-Fa-f]+");
            reng.Items.Add(item);
            item = new RegexItem("literal", "FloatLiteral", @"[0-9]*\.[0-9]+");
            reng.Items.Add(item);
            item = new RegexItem("literal", "CharLiteral", @"'(?<get>\\\\)'|'(?<get>[^\\'])'|'(?<get>\\'')|'(?<get>\\[ntr])'");
            reng.Items.Add(item);
            item = new RegexItem("literal", "StringLiteral", @"""([^""]|\"")*""");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "this", "const|extern|static|register|auto|volatile|signed|unsigned|char|int|short|long|float|double|bool|string|void|struct|union|enum|typedef|if|else|for|while|do|switch|goto|break|continue|return|case|default|out|in|defgoal|defexpression|goal|function|is|variant|good|bugy|style|expression");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "short int", "short[ \t]+int");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "long int", "long[ \t]+int");
            reng.Items.Add(item);

            item = new RegexItem("eof", "EOF", "EOF");
            reng.Items.Add(item);

            item = new RegexItem("idn", "idn", "[_a-zA-Z][_a-zA-Z0-9]*");
            reng.Items.Add(item);

            item = new RegexItem("comment", "comment", @"//[^/\n]*\n");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"[\+\-\*/%\|&^~=><!]=?|[\[\]\?\(\),:]");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"\|\||&&|(<<|>>)=?");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"\+\+|\-\-|\.|->");
            reng.Items.Add(item);

            item = new RegexItem("Syntatic", "this", @"[;{}]");
            reng.Items.Add(item);

            item = new RegexItem("comment", "comment", @"/\*[^/\*]*(\*)+/");
            reng.Items.Add(item);

            item = new RegexItem("comment", "commentStart", @"/\*[^\n]*\n");
            reng.Items.Add(item);

            item = new RegexItem("comment", "commentEnd", @"[^\*/]*\*+/");
            reng.Items.Add(item);

            item = new RegexItem("ret", "white", "[\r\n]");
            reng.Items.Add(item);

            item = new RegexItem("tab", "white", "[\t]");
            reng.Items.Add(item);

            item = new RegexItem("blank", "white", "[ ]");
            reng.Items.Add(item);


            return reng;
        }

        public static SimpleLexerEngine GetPseudoPrettyLexer()
        {
            SimpleLexerEngine reng = new SimpleLexerEngine(false);

            RegexItem item = new RegexItem("number", "DecLiteral", "[1-9][0-9]*");
            reng.Items.Add(item);
            item = new RegexItem("literal", "BoolLiteral", "istinito|lažno");
            reng.Items.Add(item);
            item = new RegexItem("literal", "OctLiteral", "[0][0-7]*");
            reng.Items.Add(item);
            item = new RegexItem("literal", "HexLiteral", "0x[0-9A-Fa-f]+");
            reng.Items.Add(item);
            item = new RegexItem("literal", "FloatLiteral", @"[0-9]*\.[0-9]+");
            reng.Items.Add(item);
            item = new RegexItem("literal", "CharLiteral", @"'(?<get>\\\\)'|'(?<get>[^\\])'|'(?<get>\\'')");
            reng.Items.Add(item);
            item = new RegexItem("literal", "StringLiteral", @"""([^""]|\"")*""");
            reng.Items.Add(item);

            item = new RegexItem("nlp", "nlp", "#(?<get>[^#]*)#");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "var", "varijabl[ae]");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "this", "znakovni|cjelobrojni|realni|bool|tekst");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "this", "defgoal|defexpression|goal|function|is|variant|good|bugy|style|expression");
            reng.Items.Add(item);

            item = new RegexItem("keyword", "this", "početak|kraj.|inicirati|izračunati|učitati|ispisati|ako|onda|inače|dok|činiti|za indeks");
            reng.Items.Add(item);

            item = new RegexItem("eof", "EOF", "EOF");
            reng.Items.Add(item);

            item = new RegexItem("idn", "idn", "[_a-zA-ZčČćĆđĐšŠžŽ][_a-zA-Z0-9čČćĆđĐšŠžŽ]*");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"\.\.");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"[=><!]=?");
            reng.Items.Add(item);

            item = new RegexItem("operator", "this", @"[\+\-\*/%^\(\),:\[\]]");
            reng.Items.Add(item);

            item = new RegexItem("Syntatic", "this", @"[;{}]");
            reng.Items.Add(item);

            item = new RegexItem("comment", "comment", @"/\*.*\*/");
            reng.Items.Add(item);

            item = new RegexItem("ret", "white", "[\r\n]");
            reng.Items.Add(item);

            item = new RegexItem("tab", "white", "[\t]");
            reng.Items.Add(item);

            item = new RegexItem("blank", "white", "[ ]");
            reng.Items.Add(item);

            return reng;
        }
    }
}
