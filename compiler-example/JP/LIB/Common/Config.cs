using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LIB.Common
{
    public class Config
    {
        private static Config instance;

        private string lexer;
        protected string sampleFile;

        public Config()
        {
            this.lexer = ConfigurationManager.AppSettings["lexer"];
            this.sampleFile = ConfigurationManager.AppSettings["sampleFile"];
        }

        public static Config Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Config();
                }
                return instance;
            }
        }

        public string Lexer
        {
            get
            {
                return this.lexer;
            }
        }

        public string SampleFile
        {
            get
            {
                return this.sampleFile;
            }
        }
    }
}
