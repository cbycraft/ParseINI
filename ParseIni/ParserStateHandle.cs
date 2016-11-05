using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseIni
{
    internal class ParserStateHandle
    {
        private List<LexerTokenNode> tokenLine;
        private List<LexerTokenNode[]> tokenLines;

        internal ParserStateHandle(IParseState startingState)
        {
            this.CurrentState = startingState;
            this.tokenLine = new List<LexerTokenNode>();
            this.tokenLines = new List<LexerTokenNode[]>();
            this.CurrentCharacter = '\0';
            this.LineNumber = 0;
            this.CharacterNumber = 0;
        }

        internal IParseState CurrentState { get; set; }

        internal char CurrentCharacter { get; set; }

        internal int LineNumber { get; set; }

        internal int CharacterNumber { get; set; }

        internal string WordBuffer { get; set; }

        internal List<LexerTokenNode> TokenLine
        {
            get
            {
                return this.tokenLine;
            }
        }

        internal List<LexerTokenNode[]> TokenLines
        {
            get
            {
                return this.tokenLines;
            }
        }

        internal void StateRequest(char currentCharacter)
        {
            this.CharacterNumber = this.CharacterNumber + 1;
            this.CurrentCharacter = currentCharacter;
            this.CurrentState.StateChange(this);
        }
    }
}
