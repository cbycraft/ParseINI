using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseIni
{
    internal struct LexerTokenNode
    {
        private Token tokenType;
        private string tokenValue;
        private int lineNumber;
        private int characterNumber;

        internal LexerTokenNode(Token tokenType, string tokenValue, int lineNumber, int characterNumber)
        {
            this.tokenType = tokenType;
            this.tokenValue = tokenValue;
            this.lineNumber = lineNumber;
            this.characterNumber = characterNumber;
        }

        internal Token TokenType
        {
            get
            {
                return this.tokenType;
            }
        }

        internal string TokenValue
        {
            get
            {
                return this.tokenValue;
            }
        }

        internal int LineNumber
        {
            get
            {
                return this.lineNumber;
            }
        }

        internal int CharacterNumber
        {
            get
            {
                return this.characterNumber;
            }
        }

        internal enum Token
        {
            String,
            OpenSquareBrace,
            CloseSquareBrace,
            EqualSign,
            EndOfLine,
            Unknown
        }
    }
}
