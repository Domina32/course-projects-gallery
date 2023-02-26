using System;
using System.Collections.Generic;
using System.Text;

using LIB.Compiler.Common;

namespace LIB.Compiler.LexicalAnalysers
{
    public class LexUnit
    {
        protected Coordinate coord;
        protected string unitClass;
        protected string unitAttribute;
        protected string unitText;

        public LexUnit(string unitClass, string unitAttribute, string unitText, Coordinate coord)
        {
            this.unitClass = unitClass;
            this.unitAttribute = unitAttribute;
            this.unitText = unitText;
            this.coord = coord;
        }

        public string UnitClass
        {
            get
            {
                return this.unitClass;
            }
        }

        public string UnitAttribute
        {
            get
            {
                return this.unitAttribute;
            }
            set
            {
                this.unitAttribute = value;
            }
        }

        public string UnitText
        {
            get
            {
                return this.unitText;
            }
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
            StringBuilder sb = new StringBuilder();
            sb.Append("class=");
            sb.Append(this.unitClass);
            sb.Append(", attr=");
            sb.Append(this.unitAttribute);
            sb.Append(", text='");
            sb.Append(this.unitText);
            sb.Append("' at ");
            sb.Append(this.coord.ToString());

            return sb.ToString();
        }
    }
}
