using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseIni
{
    internal class LexerStateBuildString : ILexerState
    {
        public void StateChange(LexerStateHandle stateContext)
        {
            switch (stateContext.CurrentCharacter)
            {
                case ']':
                    break;
                case '\r':
                    break;
                case '=':
                    break;
                case ' ':
                    break;
                case '\t':
                    break;
                default:
                    stateContext.WordBuffer = stateContext.WordBuffer + stateContext.CurrentCharacter.ToString();
                    break;
            }
        }
    }
}
