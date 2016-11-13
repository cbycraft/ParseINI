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
                    case LexerTokenNode.Token.EndOfFile:
                        statePatternHandle.CurrentState = null;
                        break;
                    case LexerTokenNode.Token.EndOfLine:
                        statePatternHandle.CurrentState = new ParseStateLineStart();
                        break;
                    case LexerTokenNode.Token.WhiteSpace:
                        if (statePatternHandle.StringBuffer == "")
                        {
                            statePatternHandle.CurrentState = new ParseStateLineStart();
                        }
                        else
                        {
                            statePatternHandle.StringBuffer = statePatternHandle.StringBuffer + statePatternHandle.CurrentToken.TokenValue;
                            statePatternHandle.CurrentState = new ParseStateLineStart();
                        }
                        break;
                    case LexerTokenNode.Token.OpenSquareBrace:
                        statePatternHandle.CurrentState = new ParseStateReadPrimeKey();
                        break;
                    case LexerTokenNode.Token.String:
                        statePatternHandle.StringBuffer = statePatternHandle.CurrentToken.TokenValue;
                        statePatternHandle.CurrentState = new ParseStateLineStart();
                        break;
                    case LexerTokenNode.Token.EqualSign:
                        if (statePatternHandle.StringBuffer == "")
                        {
                            throw new Exception("Subkey must contain at least one non-whitespace character.");
                        }
                        else
                        {
                            if (statePatternHandle.IniFileDictionary.ContainsKey(statePatternHandle.CurrentPrimaryKey))
                            {
                                statePatternHandle.CurrentSecondaryKey = statePatternHandle.StringBuffer;
                                statePatternHandle.IniFileDictionary[statePatternHandle.CurrentPrimaryKey].Add(statePatternHandle.StringBuffer, "");
                                statePatternHandle.StringBuffer = "";
                                statePatternHandle.CurrentState = new ParseStateReadSubkeyValue();
                            }
                            else
                            {
                                throw new Exception("Cannot find primary key " + statePatternHandle.CurrentPrimaryKey + " in dictionary.");
                            }
                        }
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
