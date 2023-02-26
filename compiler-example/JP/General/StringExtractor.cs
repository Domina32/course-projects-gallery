using System;
using System.Text;


namespace General
{
	public class StringExtractor
	{
		public static string ExtractBetween( string text, string from, string to )
		{
			string ret = string.Empty;
			int iFrom = text.IndexOf( from );
			int iTo;
			if( iFrom < text.Length )
			{
				iTo = text.IndexOf( to, iFrom + 1 );
			}
			else
			{
				iTo = iFrom;
			}
			if( iFrom >=0 && iTo >= 0 )
			{
				ret = text.Substring( iFrom + from.Length, iTo - (iFrom + from.Length ));
			}
			return ret.Trim();
		}

        public static string ReplaceBetween( string text, string from, string to, string with )
        {
            string pattern = ExtractBetween( text, from, to );
            string ret = text.Replace(pattern, with);

            return ret;
        }


		public static string ExtractTo( string text, string to )
		{
			string ret = text;
			int iTo = text.IndexOf( to );
			if( iTo > 0 )
			{
				ret = text.Substring( 0, iTo );
			}
			return ret.Trim();
		}

        public static string ExtractFrom(string text, string from)
        {
            string ret = text;
            int iFrom = text.IndexOf(from);
            if (iFrom >= 0)
            {
                ret = text.Substring(iFrom+1, ret.Length - (iFrom+1));
            }
            return ret.Trim();
        }
	}
}