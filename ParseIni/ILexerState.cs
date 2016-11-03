using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseIni
{
    internal interface ILexerState
    {
        void StateChange(LexerStateHandle lexerStateHandle);
    }
}
