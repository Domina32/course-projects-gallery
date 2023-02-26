using System;
using System.Collections.Generic;
using System.Text;

namespace LIB.Compiler.LexicalAnalysers
{
    public abstract class LexerEngineBase
    {
        protected bool caseSensitive;
        protected string filePath;
        protected string sourceText;
        protected string restText;

        protected int currentPosition;
        protected int row;
        protected int col;
        protected LexUnit lookAhead;

        public LexerEngineBase(bool caseSensitive)
        {
            this.caseSensitive = caseSensitive;
        }

        public string SourceText
        {
            get
            {
                return this.sourceText;
            }
            set
            {
                this.filePath = "None";
                if (value.EndsWith("\nEOF"))
                {
                    this.sourceText = value;
                }
                else
                {
                    this.sourceText = string.Format("{0}\nEOF", value);
                }
                this.restText = this.sourceText;
                this.Reset();

            }
        }

        public virtual void Reset()
        {
            this.currentPosition = 0;
            this.row = 1;
            this.col = 1;
        }

		public abstract LexUnitStream GetStream();
    }
}
