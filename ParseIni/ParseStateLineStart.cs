using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParseIni
{
    internal class ParseStateLineStart : IParseStatePattern
    {
        public void EvaluateState(ParseStatePatternHandle statePatternHandle)
        {
            try
            {
                switch (statePatternHandle.CurrentToken.TokenType)
                {
                    case LexerTokenNode.Token.WhiteSpace:
                        statePatternHandle.CurrentState = new ParseStateLineStart();
                        break;
                    case LexerTokenNode.Token.OpenSquareBrace:
                        statePatternHandle.CurrentState = new ParseStateReadPrimeKey();
                        break;
                    case LexerTokenNode.Token.String:
                        statePatternHandle.StringBuffer = statePatternHandle.CurrentToken.TokenValue;
                        statePatternHandle.CurrentState = new ParseStateExpectEqualSign();
                        break;
                    default:
                        throw new Exception("Unexpected character at the start of a line.");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
