using System;
using System.Text;

namespace General
{
    public class AssocArray<T>
    {
        protected int rank;
        protected Set<T> content;
        protected Set<string>[] indexes;

        public AssocArray(int rank )
        {
            this.rank = rank;
            this.content = new Set<T>("array-content");
            this.indexes = new Set<string>[this.rank];
            for (int i = 0; i < rank; i++)
            {
                this.indexes[i] = new Set<string>(string.Format("index {0}", i));
            }
        }

        public string[] GetIndexes(int index)
        {
            if (index < this.rank)
                return this.indexes[index].IdToStringArray();

            return new string[0];
        }

        public virtual T this[params string[] indexes]
        {
            get
            {
                if (indexes.Length == this.rank)
                {
                    string key = this.GetKey(indexes);
                    return (T)this.content[key];
                }
                else
                {
                    throw new Exception(string.Format("Number of indexes is not {0}.", this.rank));
                }
            }
            set
            {
                if (indexes.Length == this.rank)
                {
                    string key = this.GetKey(indexes);
                    this.content.Remove(key);
                    this.content.Add(key, value);
                    for (int i = 0; i < this.rank; i++ )
                    {
                        this.indexes[i].Add(indexes[i], indexes[i]);
                    }
                }
                else
                {
                    throw new Exception(string.Format("Number of indexes is not {0}.", this.rank));
                }
            }
        }

        protected string GetKey(string[] indexes)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < indexes.Length; i++)
            {
                sb.Append(indexes[i]);
                sb.Append(".");
            }

            if (sb.ToString().EndsWith("."))
                sb.Length--;

            return sb.ToString();
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Set<string> inds = new Set<string>("S0", this.indexes[0]);

            for (int i = 1; i < this.indexes.Length; i++)
            {
                inds = Set<string>.Carthesy(inds, this.indexes[i]);
            }

            string[] keys = inds.IdToStringArray();

            foreach (string ind in keys)
            {
                object obj = this.content[ind];
                sb.Append(ind);
                sb.Append(" = ");
                if (obj != null)
                    sb.Append(obj.ToString());
                else
                    sb.Append(" - ");

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
