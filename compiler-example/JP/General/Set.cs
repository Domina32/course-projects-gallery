using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace General
{
    public class Set<T> : IEnumerable
    {
        protected Hashtable content;
        protected StringCollection order;
        
        protected string name;

        public Set( string name )
        {
            this.name = name;
            this.content = new Hashtable();
            this.order = new StringCollection();
        }

        public Set(string name, Set<T> s1):this(name)
        {
            foreach (string id in s1.IdToStringArray())
            {
                this.Add(id, s1[id]);
            }
        }

        public int Count
        {
            get
            {
                return this.content.Count;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return this.Count == 0;
            }
        }

        public bool Contains(string elementId)
        {
            return this.content.Contains(elementId);
        }

        public bool Add(string elementId, T element)
        {
            if (!this.content.Contains(elementId))
            {
                this.content.Add(elementId, element);
                this.order.Add(elementId);
                return true;
            }

            return false;
        }

        public bool Remove(string elementId)
        {
            if( this.Contains( elementId ))
            {
                this.content.Remove(elementId);
                this.order.Remove(elementId);
                return true;
            }

            return false;
        }

        public override bool  Equals(object obj)
        {
            if (obj.GetType() != typeof(Set<T>))
                return false;

            Set<T> s = (Set<T>)obj;

            if (this.Count != s.Count)
                return false;

            foreach (string id in this.IdToStringArray())
            {
                if (!s.Contains(id))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void Clear()
        {
            this.content.Clear();
            this.order.Clear();
        }

        public bool IsProperSubsetOf(Set<T> s1)
        {
            if (this.Count >= s1.Count)
                return false;

            foreach (string id in this.IdToStringArray())
            {
                if (!s1.Contains(id))
                    return false;
            }

            return true;
        }

        public bool IsSubsetOf(Set<T> s1)
        {
            if (this.Count > s1.Count)
                return false;

            foreach (string id in this.IdToStringArray())
            {
                if (!s1.Contains(id))
                    return false;
            }

            return true;
        }

        public static Set<T> Union(Set<T> s1, Set<T> s2)
        {
            Set<T> ret = new Set<T>(string.Format("{0} U {1}", s1.name, s2.name), s1);
            foreach (string id in s2.IdToStringArray())
            {
                ret.Add(id, s2[id]);
            }
            return ret;
        }

        public static Set<T> Intersection(Set<T> s1, Set<T> s2)
        {
            Set<T> ret = new Set<T>(string.Format("{0} A {1}", s1.name, s2.name));

            Set<T> minor, major;
            if (s1.Count < s2.Count)
            {
                minor = s1;
                major = s2;
            }
            else
            {
                minor = s2;
                major = s1;
            }

            foreach (string id in minor.IdToStringArray())
            {
                if (major.Contains(id))
                {
                    ret.Add(id, minor[id]);
                }
            }

            return ret;
        }

        public static Set<T> Diff(Set<T> s1, Set<T> s2)
        {
            Set<T> ret = new Set<T>(string.Format("{0} \\ {1}", s1.name, s2.name));

            foreach (string id in s1.IdToStringArray())
            {
                if (!s2.Contains(id))
                {
                    ret.Add(id, s1[id]);
                }
            }

            return ret;
        }

        public static Set<string> Carthesy(Set<T> s1, Set<T> s2)
        {
            Set<string> ret = new Set<string>(string.Format("{0} x {1}", s1.name, s2.name));

            foreach (string id1 in s1.IdToStringArray())
            {
                foreach (string id2 in s2.IdToStringArray())
                {
                    ret.Add(string.Format("{0}.{1}", id1, id2), null);
                }
            }
            return ret;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < this.order.Count; i++)
            {
                yield return (T)this[this.order[i]];
            }
        }

        public T this[string elementId]
        {
            get
            {
                if( this.Contains(elementId))
                    return (T)this.content[elementId];

                return default(T);
            }
        }

        public T this[int index]
        {
            get
            {
                if (index >= 0 && index < this.order.Count)
                    return this[this.order[index]];

                return default(T);
            }
        }

        public string[] IdToStringArray()
        {
            string[] ret = new string[this.order.Count];

            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = this.order[i];
            }
            return ret;
        }

        public T[] ToArray()
        {
            T[] ret = new T[this.content.Count];

            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = (T)this.content[this.order[i]];
            }
            return ret;
        }

        public static void Sort( string[] s )
        {
            for (int i = 0; i < s.Length - 1; i++ )
            {
                for (int j = i + 1; j < s.Length; j++)
                {
                    string s1 = s[i];
                    string s2 = s[j];
                    int index = s1.CompareTo(s2);
                    if (index > 0)
                    {
                        string tmp = s[i];
                        s[i] = s[j];
                        s[j] = tmp;
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            string[] ids = this.IdToStringArray();
            Set<T>.Sort(ids);

            sb.Append("{");
            foreach( string id in ids )
            {
                sb.Append(id);
                sb.Append(", ");
            }
            if( sb.ToString().EndsWith(", "))
                sb.Length -= 2;

            sb.Append( "}");
            return sb.ToString();
        }
    }
}
