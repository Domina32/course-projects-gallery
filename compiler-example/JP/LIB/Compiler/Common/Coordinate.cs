using System;
using System.Text;

namespace LIB.Compiler.Common
{
    public class Coordinate
    {
        protected string filePath;
        protected int row;
        protected int col;
        protected int pos;
        protected int length;

        public Coordinate(string filePath, int row, int col, int pos, int length)
        {
            this.filePath = filePath;
            this.row = row;
            this.col = col;
            this.pos = pos;
            this.length = length;
        }

        public Coordinate(int row, int col, int pos, int length)
            : this("Noname", row, col, pos, length)
        { }

        public Coordinate(Coordinate coord)
        {
            this.Copy(coord);
        }

        public string FilePath
        {
            get
            {
                return this.filePath;
            }
        }

        public int Row
        {
            get
            {
                return this.row;
            }
        }

        public int Col
        {
            get
            {
                return this.col;
            }
        }

        public int Pos
        {
            get
            {
                return this.pos;
            }
            set
            {
                this.pos = value;
            }
        }

        public int Length
        {
            get
            {
                return this.length;
            }
            set
            {
                this.length = value;
            }
        }

        public void MakeToCoordinate()
        {
            this.col += this.length;
            this.pos += this.length;
        }

        public void Copy(Coordinate coord)
        {
            this.filePath = coord.filePath;
            this.row = coord.row;
            this.col = coord.col;
            this.pos = coord.pos;
            this.length = coord.length;
        }

        public string ToHtml()
        {
            return string.Format("[{0},{1}]", this.row, this.col);
        }

        public override string ToString()
        {
            return string.Format("[{0},{1}]", this.row, this.col);
        }
    }
}
