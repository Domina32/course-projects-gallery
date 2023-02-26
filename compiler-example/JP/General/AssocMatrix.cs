using System;
using System.Collections.Specialized;
using System.Text;
using System.IO;

namespace General
{
    public class AssocMatrix<T> : AssocArray<T>
    {
        protected Set<StringCollection> rows;
        protected Set<StringCollection> cols;
        public AssocMatrix()
            : base(2)
        {
            this.rows = new Set<StringCollection>("Defined columns");
            this.cols = new Set<StringCollection>("Definded rows");
        }

        public string[,] ToStringArray()
        {
            string[,] ret = new string[base.indexes[0].Count+1, base.indexes[1].Count+1];

            string[] xs = base.indexes[0].IdToStringArray();
            string[] ys = base.indexes[1].IdToStringArray();
            Set<string>.Sort(xs);
            Set<string>.Sort(ys);

            for (int i = 0; i < ys.Length; i++)
            {
                ret[0, i + 1] = ys[i];
            }

            for (int i = 0; i < xs.Length; i++)
            {
                string x = xs[i];
                ret[i + 1, 0] = x;
                for (int j = 0; j < ys.Length; j++)
                {
                    string y = ys[j];
                    string key = string.Format("{0}.{1}", x, y);
                    if (base.content[key] != null)
                        ret[i + 1, j + 1] = base.content[key].ToString();
                    else
                        ret[i + 1, j + 1] = "-";
                }
            }

            return ret;
        }

        public string[] GetColumnIndexesOf(string row)
        {
            string[] ret;
            StringCollection def = this.rows[row];

            if (def != null)
            {
                ret = new string[def.Count];
                for (int i = 0; i < def.Count; i++)
                {
                    ret[i] = def[i];
                }
            }
            else
            {
                ret = new string[0];
            }
            
            return ret;
        }

        public string[] GetRowIndexesOf(string col)
        {
            string[] ret;
            StringCollection def = this.cols[col];

            if (def != null)
            {
                ret = new string[def.Count];
                for (int i = 0; i < def.Count; i++)
                {
                    ret[i] = def[i];
                }
            }
            else
            {
                ret = new string[0];
            }

            return ret;
        }

        public override T this[params string[] indexes]
        {
            get
            {
                return base[indexes];
            }
            set
            {
                StringCollection definedColsForRow = this.rows[indexes[0]];
                if (definedColsForRow == null)
                {
                    definedColsForRow = new StringCollection();
                    this.rows.Add(indexes[0], definedColsForRow);
                    definedColsForRow.Add(indexes[1]);
                }
                else
                {
                    if (!definedColsForRow.Contains(indexes[1]))
                    {
                        definedColsForRow.Add( indexes[1]);
                    }
                }
                
                StringCollection definedRowsForCol = this.cols[indexes[1]];
                if( definedRowsForCol == null)
                {
                    definedRowsForCol = new StringCollection();
                    this.cols.Add(indexes[1], definedRowsForCol);
                    definedRowsForCol.Add(indexes[0]);
                }
                else
                {
                    if (!definedRowsForCol.Contains(indexes[0]))
                    {
                        definedRowsForCol.Add(indexes[0]);
                    }
                }

                base[indexes] = value;
            }
        }

        public void Write(string filePath)
        {
            StreamWriter writer = new StreamWriter(filePath, false, Encoding.ASCII);
            StringBuilder sb = new StringBuilder();
            string[,] data = this.ToStringArray();
            int rows = data.GetUpperBound(0) + 1;
            int cols = data.GetUpperBound(1) + 1;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    sb.Append( "\"");
                    sb.Append( data[row,col]);
                    sb.Append("\",");

                }
                if (sb.ToString().EndsWith(","))
                    sb.Length--;

                writer.WriteLine(sb.ToString());
                sb.Length = 0;
            }
            writer.Flush();
            writer.Close();
            writer.Dispose();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string[,] data = this.ToStringArray();
            int rows = data.GetUpperBound(0) + 1;
            int cols = data.GetUpperBound(1) + 1;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    sb.Append(CenterText(data[row, col], 5));
                    sb.Append("|");
                }
                sb.Append(Environment.NewLine);
                sb.Append(string.Empty.PadRight(cols * 6, '-'));
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        private string CenterText(string text, int size)
        {
            if (text == null)
                text = ".";

            if (text.Length >= size)
                return text;

            int nspace = size - text.Length;
            int leftspace = nspace / 2;
            int rightspace = nspace - leftspace;

            string ret = text.PadLeft(leftspace + text.Length).PadRight(size);

            return ret;
        }
    }
}
