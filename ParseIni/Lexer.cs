using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseIni
{
    public class Lexer
    {
        public Lexer(string fullAbsolutePathToIniFile)
        {

        }
        internal enum Tokens
        {
            String,
            OpenSquareBrace,
            CloseSquareBrace,
            EqualSign,
            Unknown
        }
    }
}
