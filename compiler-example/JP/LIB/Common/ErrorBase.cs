using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.Common
{
    public enum ErrorTypes
    {
        Syntax,
        Semantic
    }

    public class ErrorBase
    {
        protected ErrorTypes errorType;
        protected string text;

        public ErrorBase(ErrorTypes errorType, string text)
        {
            this.errorType = errorType;
            this.text = text;
        }

        public ErrorTypes ErrorType
        {
            get
            {
                return this.errorType;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", this.errorType, this.text);
        }
    }
}
