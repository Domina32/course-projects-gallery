using System;
using System.Collections.Generic;
using System.Text;

namespace LIB.Compiler.LexicalAnalysers
{
    public class LexUnitStream
    {
        protected int next;
        protected List<LexUnit> tokens;

        public LexUnitStream()
        {
            this.next = 0;
            this.tokens = new List<LexUnit>();
        }

        public void Add(LexUnit token)
        {
            this.tokens.Add(token);
        }

        public bool IsBof
        {
            get
            {
                return this.next == 0;
            }
        }

        public bool IsEof
        {
            get
            {
                if (this.next < this.tokens.Count && this.tokens[this.next].UnitClass == "eof")
                    return true;

                return this.next == this.tokens.Count;   
            }
        }

        public LexUnit Next
        {
            get
            {
                return this.tokens[this.next];
            }
        }

        public LexUnit Previous
        {
            get
            {
                if (!this.IsBof)
                {
                    return this.tokens[this.next - 1];
                }
                else
                {
                    return null;
                }
            }
        }

        public LexUnit LookAhead(int index)
        {
            return this.tokens[this.next + index];
        }

        public void Reset()
        {
            this.next = 0;
        }

        public LexUnit Read()
        {
            LexUnit ret = this.Next;

            if (!this.IsEof)
                this.next++;

            return ret;
        }
    }
}
