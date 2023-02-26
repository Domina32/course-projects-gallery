using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using LIB.Compiler.LexicalAnalysers.Simple;

namespace LIB.Compiler.LexicalAnalysers
{
    public class LexerBuilder
    {
        public static SimpleLexerEngine LexerEngineFromXml(string path)
        {
            XDocument doc = XDocument.Load(path);

            SimpleLexerEngine ret = new SimpleLexerEngine(true);

            var rules = from r in doc.Descendants("rule")
                        select new RegexItem(
                            r.Attribute("class").Value,
                            r.Attribute("attribute").Value,
                            r.Attribute("regex").Value);
                               

            foreach (var rule in rules)
            {
                ret.Items.Add(rule);
            }

            return ret;
        }
    }
}
