using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParseIni
{
    public interface IParseStatePattern
    {
        void EvaluateState(ParseStatePatternHandle lexerStateHandle);
    }
}
