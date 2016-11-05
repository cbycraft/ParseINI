using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseIni
{
    internal class LexerStateReturn : ILexerState
    {
        public void StateChange(LexerStateHandle stateContext)
        {
            switch (stateContext.CurrentCharacter)
            {
                case '\n':
                    stateContext.TokenLine.Add(new LexerTokenNode(LexerTokenNode.Token.EndOfLine, System.Environment.NewLine, stateContext.LineNumber, stateContext.CharacterNumber));
                    stateContext.CharacterNumber = 0;
                    stateContext.LineNumber = stateContext.LineNumber + 1;
                    stateContext.CurrentState = new LexerStateLineStart();
                    break;
                default:
                    break;
            }
        }
    }
}
