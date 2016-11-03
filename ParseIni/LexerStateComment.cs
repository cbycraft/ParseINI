using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseIni
{
    internal class LexerStateComment : ILexerState
    {
        char currentCharacter;

        public void StateChange(LexerStateHandle stateContext)
        {

        }
    }
}
