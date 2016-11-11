using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParseIni
{
    internal class ParseStateExpectEqualSign : IParseStatePattern
    {
        public void EvaluateState(ParseStatePatternHandle statePatternHandle)
        {
            try
            {
                switch (statePatternHandle.CurrentToken.TokenType)
                {
                    case LexerTokenNode.Token.EqualSign:
                        break;
                    default:
                        throw new Exception("While expecting an equal sign, received an unexpected character.");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
