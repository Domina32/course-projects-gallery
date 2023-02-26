using System;
using System.Collections.Generic;
using System.Text;

namespace General
{
    public class MyStack<T>
    {
        protected T[] content;

        protected int next;

        public MyStack(int capacity)
        {
            this.content = new T[capacity];
            this.next = 0;
        }

        public int Count
        {
            get
            {
                return this.next;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return this.next <= 0;
            }
        }

        public T Top
        {
            get
            {
                if (this.next > 0)
                {
                    return this.content[this.next - 1];
                }
                else
                {
                    throw new Exception("Stack is empty.");
                }
            }
        }

        public void Clear()
        {
            this.next = 0;
        }

        public void Rollback(int n)
        {
            this.next -= n;
        }

        public void Push(T item)
        {
            if (this.next < this.content.Length)
            {
                this.content[this.next] = item;
                this.next++;
            }
            else
            {
                throw new Exception("Stack is full");
            }
        }

        public T Pop()
        {
            if (this.next > 0)
            {
                this.next--;
                return this.content[this.next];
            }
            else
            {
                throw new Exception("Stack is empty");
            }
        }

        public void Swap()
        {
            T temp1 = this.Pop();
            T temp2 = this.Pop();
            this.Push(temp1);
            this.Push(temp2);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            int start = this.next + 2;
            if (start > this.content.Length - 1)
                start = this.content.Length - 1;

            for (int i = start; i >= 0 ; i--)
            {
                if (i == this.next)
                {
                    sb.Append("->");
                }
                else
                {
                    sb.Append("  ");
                }
                if (this.content[i] != null)
                {
                    sb.Append(this.content[i].ToString());
                }
                else
                {
                    sb.Append("...");
                }
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
