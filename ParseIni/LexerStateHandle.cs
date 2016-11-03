using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseIni
{
    internal class LexerStateHandle
    {
        private List<LexerTokenNode> tokenLine;
        private List<LexerTokenNode[]> tokenLines;

        internal LexerStateHandle(ILexerState startingState)
        {
            this.CurrentState = startingState;
            this.tokenLine = new List<LexerTokenNode>();
            this.tokenLines = new List<LexerTokenNode[]>();
            this.CurrentCharacter = '\0';
        }

        internal ILexerState CurrentState { get; set; }

        internal char CurrentCharacter { get; set; }

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
            this.CurrentCharacter = currentCharacter;
            this.CurrentState.StateChange(this);
        }
    }
}
