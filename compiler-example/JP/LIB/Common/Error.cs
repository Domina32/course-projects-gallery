using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Compiler.Common;

namespace LIB.Common
{
    public class Error : ErrorBase
    {
        protected Coordinate coord;

        public Error( ErrorTypes errorType, string text, Coordinate coord)
            :base(errorType, text)
        {
            this.coord = coord;
        }

        public Coordinate Coord
        {
            get
            {
                return this.coord;
            }
        }

        public override string ToString()
        {
            if (this.coord != null)
            {
                return string.Format("{0} : [{1}, {2}]", this.text, this.coord.Row, this.coord.Col );
            }
            else
            {
                return string.Format("{0}", this.text);
            }
        }
    }
}
