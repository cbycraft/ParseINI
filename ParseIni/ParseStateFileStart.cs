using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParseIni
{
    internal class ParseStateFileStart : IParseStatePattern
    {
        public void EvaluateState(ParseStatePatternHandle statePatternHandle)
        {
            try
            {
                switch (statePatternHandle.CurrentToken.TokenType)
                {
                    case LexerTokenNode.Token.OpenSquareBrace:
                        if (statePatternHandle.CurrentToken.CharacterNumber == 1)
                        {

                        }
                        else
                        {
                            throw new Exception("First non-white space or comment in INI file must be an open square brace.");
                        }
                        break;
                    case LexerTokenNode.Token.WhiteSpace: //Ignore white space at the top - keep reading
                        statePatternHandle.CurrentState = new ParseStateFileStart();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
