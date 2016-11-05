using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseIni
{
    internal class LexerStateComment : ILexerState
    {
        public void StateChange(LexerStateHandle stateContext)
        {
            switch (stateContext.CurrentCharacter)
            {
                case '\n':
                    stateContext.CurrentState = new LexerStateLineStart();
                    break;
                default:
                    stateContext.CurrentState = new LexerStateComment();
                    break;
            }
        }
    }
}
