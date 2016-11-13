using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParseIni
{
    public struct LexerTokenNode
    {
        private Token tokenType;
        private string tokenValue;
        private int lineNumber;
        private int characterNumber;

        public LexerTokenNode(Token tokenType, string tokenValue, int lineNumber, int characterNumber)
        {
            this.tokenType = tokenType;
            this.tokenValue = tokenValue;
            this.lineNumber = lineNumber;
            this.characterNumber = characterNumber;
        }

        public Token TokenType
        {
            get
            {
                return this.tokenType;
            }
        }

        public string TokenValue
        {
            get
            {
                return this.tokenValue;
            }
        }

        public int LineNumber
        {
            get
            {
                return this.lineNumber;
            }
        }

        public int CharacterNumber
        {
            get
            {
                return this.characterNumber;
            }
        }

        public enum Token
        {
            String,
            OpenSquareBrace,
            CloseSquareBrace,
            EqualSign,
            WhiteSpace,
            EndOfLine,
            EndOfFile,
            Unknown
        }
    }
}
