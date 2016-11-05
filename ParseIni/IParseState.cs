using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseIni
{
    internal interface IParseState
    {
        void StateChange(ParserStateHandle lexerStateHandle);
    }
}
