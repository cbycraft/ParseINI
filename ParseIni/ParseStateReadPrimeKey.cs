using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParseIni
{
    internal class ParseStateReadPrimeKey : IParseStatePattern
    {
        public void EvaluateState(ParseStatePatternHandle statePatternHandle)
        {
            try
            {
                switch (statePatternHandle.CurrentToken.TokenType)
                {
                    case LexerTokenNode.Token.CloseSquareBrace:
                        if (statePatternHandle.StringBuffer != "")
                        {
                            statePatternHandle.IniFileDictionary.Add(statePatternHandle.StringBuffer, null);
                            statePatternHandle.StringBuffer = "";
                            statePatternHandle.CurrentState = new ParseStateLineStart();
                        }
                        else
                        {
                            throw new Exception("Cannot have an empty primary key.");
                        }
                        break;
                    case LexerTokenNode.Token.String:
                        statePatternHandle.StringBuffer = statePatternHandle.CurrentToken.TokenValue;
                        statePatternHandle.CurrentState = new ParseStateReadPrimeKey();
                        break;
                    case LexerTokenNode.Token.WhiteSpace:
                        statePatternHandle.StringBuffer = statePatternHandle.CurrentToken.TokenValue;
                        statePatternHandle.CurrentState = new ParseStateReadPrimeKey();
                        break;
                    default:
                        throw new Exception("Unexpected character in primary key of INI file.");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
