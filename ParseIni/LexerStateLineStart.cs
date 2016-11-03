using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseIni
{
    internal class LexerStateLineStart : ILexerState
    {
        public void StateChange(LexerStateHandle stateContext)
        {
            switch (stateContext.CurrentCharacter)
            {
                case ';':
                    stateContext.CurrentState = new LexerStateComment();
                    break;
                default:
                    break;
            }
        }
    }
}
