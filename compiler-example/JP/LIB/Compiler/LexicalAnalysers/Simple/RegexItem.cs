using System;
using System.Text;
using System.Text.RegularExpressions;

namespace LIB.Compiler.LexicalAnalysers.Simple
{
    public class RegexItem
    {
        protected static bool caseSensitive = true;

        protected string itemClass;
        protected string itemAttribute;
        protected string regexText;

        protected Regex regexStd;

        public RegexItem(string itemClass, string itemAttribute, string regexText)
        {
            this.itemClass = itemClass;
            this.itemAttribute = itemAttribute;
            this.regexText = regexText;

            string pattern = string.Format(@"\A{0}", regexText);
            if (regexText.IndexOf("\\|") >= 0)
            {
                pattern = pattern.Replace("\\|", "%%##%%");
                pattern = pattern.Replace("|", @"|\A");
                pattern = pattern.Replace("%%##%%", "\\|");
            }
            else
            {
                pattern = pattern.Replace("|", @"|\A");
            }
            RegexOptions opt = RegexOptions.Multiline | RegexOptions.Singleline;
            if (!caseSensitive)
            {
                opt |= RegexOptions.IgnoreCase;
            }
            this.regexStd = new Regex(pattern, opt);
        }

        public static bool CaseSensitive
        {
            get
            {
                return caseSensitive;
            }
            set
            {
                caseSensitive = value;
            }
        }

        public string ItemClass
        {
            get
            {
                return this.itemClass;
            }
        }

        public string ItemAttribute
        {
            get
            {
                return this.itemAttribute;
            }
        }

        public Regex RegexStd
        {
            get
            {
                return this.regexStd;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            sb.Append(this.itemClass);
            sb.Append(", ");
            sb.Append(this.itemAttribute);
            sb.Append(")->");
            sb.Append(this.regexText);

            return sb.ToString();
        }
    }
}
