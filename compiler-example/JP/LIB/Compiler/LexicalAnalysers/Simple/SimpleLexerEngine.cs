using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using LIB.Compiler.Common;

namespace LIB.Compiler.LexicalAnalysers.Simple
{
    public class SimpleLexerEngine : LexerEngineBase
    {
        protected List<RegexItem> items;
                
        public SimpleLexerEngine(bool caseSensitive):base(caseSensitive)
        {
            RegexItem.CaseSensitive = caseSensitive;
            this.items = new List<RegexItem>();
            this.lookAhead = null;
        }

        public List<RegexItem> Items
        {
            get
            {
                return this.items;
            }
        }

        public override LexUnitStream GetStream()
        {
            LexUnitStream ret = new LexUnitStream();

            LexUnit next = this.NextUnit();
            while (next != null)
            {
                ret.Add(next);
                next = this.NextUnit();
            }

            return ret;
        }

        public virtual LexUnit NextUnit()
        {
            LexUnit ret = null;

            if (this.currentPosition >= this.sourceText.Length) //preko ovoga govorimo programu do koje linije smo dosli, i racunamo kolonu i redak programa
            { //ako je tekuca pozicija premasila tekst onda se vracamo, u protivnom napredujemo tekstom
                return ret;
            }

            RegexItem maxItem = null;  //varijabla najbolje
            string foundText = null; //prepoznato
            string getText = null;  

            for (int i = 0; i < this.items.Count; i++)
            {
                RegexItem nextItem = this.items[i];
                Regex reg = nextItem.RegexStd;
                Match max = null;

                MatchCollection founds = reg.Matches(this.restText);
                if (founds.Count > 0)
                {
                    max = founds[0];
                    for (int j = 1; j < founds.Count; j++)
                    {
                        Match next = founds[j];
                        if (next.Length > max.Length)
                        {
                            max = next;
                        }
                    }
                }
                if (max != null)
                {
                    if (foundText != null)
                    {
                        if (foundText.Length < max.Length)
                        {
                            maxItem = nextItem;
                            foundText = max.Value;
                            if (max.Groups["get"].Value != string.Empty)
                            {
                                getText = max.Groups["get"].Value;
                            }
                            else
                            {
                                getText = foundText;
                            }
                        }
                    }
                    else
                    {
                        maxItem = nextItem;
                        foundText = max.Value;
                        if (max.Groups["get"].Value != string.Empty)
                        {
                            getText = max.Groups["get"].Value;
                        }
                        else
                        {
                            getText = foundText;
                        }
                    }
                }
            }
            Coordinate coord = new Coordinate(this.filePath, this.row, this.col, this.currentPosition, foundText != null ? foundText.Length : 0);

            if (maxItem != null) //mijenjamo tekucu poziciju za duzinu prepoznatoga teksta i radimo leksicku jedinku
            {
                this.currentPosition += foundText.Length;
                string attribute = maxItem.ItemAttribute;
                if (attribute == "this")
                {
                    attribute = getText;
                }
                ret = new LexUnit(maxItem.ItemClass, attribute, getText, coord);
                this.NewLineCount(foundText); //brojimo nove linije
            }
            else //nista nismo prepoznali
            {
                int len = this.sourceText.Length - this.currentPosition;
                if( len < 0 )
                {
                    len = 0;
                }
                else if( len > 5 )
                {
                    len = 5;
                }
                string errorText = this.sourceText.Substring(this.currentPosition, len );
                ret = new LexUnit("error", errorText, errorText, coord);    //leksicka jedinka pripada klasi error (greška) i dodajemo u listu izlaznih jedinki
                this.currentPosition++;
            }
            this.restText = this.sourceText.Substring(this.currentPosition, this.sourceText.Length - this.currentPosition); //ostatak teksta - pomice se prema naprijed za duzinu prepoznatog znaka
            
            if (ret.UnitClass == "ignore")
            {
                ret = this.NextUnit(); //idemo na prepoznavanje sljedecega znaka
            }
            return ret;
        }

        protected int NewLineCount(string text)
        {
            int n = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '\n')
                {
                    n++;
                    this.row++;
                    this.col = 1;
                }
                else if (text[i] == '\t')
                {
                    this.col += 5;
                }
                else
                {
                    this.col++;
                }
            }
            
            return n;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.items.Count; i++)
            {
                sb.Append(i + 1);
                sb.Append(". ");
                sb.Append(this.items[i].ToString());
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
