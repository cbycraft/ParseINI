using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseIni
{
    public interface IParseStatePattern
    {
        void EvaluateState(ParseStatePatternHandle lexerStateHandle);
    }
}
