using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIB.Compiler.AST;
using LIB.Compiler.AST.DataTypes;

namespace LIB.Compiler.Common
{
    public class IdnTable
    {
        protected static int count;

        protected List<IdnRow> rows;

        public IdnTable()
        {
            count = 0;
            this.rows = new List<IdnRow>();
        }

        public void Clear()
        {
            this.rows.Clear();
            count = 0;
        }

        public IdnRow Find(Identifier idn)
        {
            return this.Find("global", idn); //u  listi indentifikatora ce pronaci (ako postoji) "globalni" indentifikator, jer imamo samo globalne varijable u nasem programu
        }

        public IdnRow Find(string function, Identifier idn)
        {
            foreach( IdnRow row in this.rows)
            {
                if( row.Function == function && row.Idn.Name == idn.Name )
                {
                    return row;
                }
            }

            return null;
        }

        public bool Append(Identifier idn, DataType dType)
        {
            IdnRow existing = this.Find(idn);
            if (existing == null)
            {
                this.rows.Add(new IdnRow("global", idn, dType, LifeTimeTypes.Static, count++));
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
