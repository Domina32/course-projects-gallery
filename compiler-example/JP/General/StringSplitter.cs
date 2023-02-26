using System;
using System.Text;


namespace General
{
	public class StringSplitter
	{
		protected char[] delimiters;
		protected string keyword;
		protected string[] parameters;

		public StringSplitter(params char[] delimiters)
		{
            this.delimiters = delimiters;
		}

		public void Split( string text )
		{
			this.keyword = StringExtractor.ExtractTo( text, "(" );
			string ps = StringExtractor.ExtractBetween( text, "(", ")").Trim();
			this.parameters = ps.Split( this.delimiters );

			for( int i=0; i<this.parameters.Length; i++)
			{
				this.parameters[i] = this.parameters[i].Trim();
			}
		}

		public string Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		public string[] Parameters
		{
			get
			{
				return this.parameters;
			}
		}

        public string Concatenate(int from, int to, string separator)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = from; i < to && i < this.parameters.Length; i++)
            {
                sb.Append(this.parameters[i]);
                sb.Append(separator);
            }

            if (sb.ToString().EndsWith( separator ))
            {
                sb.Length -= separator.Length;
            }

            return sb.ToString();
        }
    }
}