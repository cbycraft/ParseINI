using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParseIni
{
    internal class ParseStateReadSubkeyValue : IParseStatePattern
    {
        public void EvaluateState(ParseStatePatternHandle statePatternHandle)
        {
            try
            {
                if(statePatternHandle.CurrentToken.TokenType == LexerTokenNode.Token.EndOfLine ||
                    statePatternHandle.CurrentToken.TokenType == LexerTokenNode.Token.EndOfFile)
                {
                    if (statePatternHandle.IniFileDictionary.ContainsKey(statePatternHandle.CurrentPrimaryKey))
                    {
                        if (statePatternHandle.IniFileDictionary[statePatternHandle.CurrentPrimaryKey].ContainsKey(statePatternHandle.CurrentSecondaryKey))
                        {
                            statePatternHandle.IniFileDictionary[statePatternHandle.CurrentPrimaryKey][statePatternHandle.CurrentSecondaryKey] = statePatternHandle.StringBuffer;
                            statePatternHandle.StringBuffer = "";
                            statePatternHandle.CurrentState = new ParseStateLineStart();
                        }
                        else
                        {
                            throw new Exception("Cannot find secondary key " + statePatternHandle.CurrentSecondaryKey + ".");
                        }
                    }
                    else
                    {
                        throw new Exception("Cannot find primary key " + statePatternHandle.CurrentPrimaryKey + ".");
                    }
                }
                else
                {
                    statePatternHandle.StringBuffer = statePatternHandle.StringBuffer + statePatternHandle.CurrentToken.TokenValue;
                    statePatternHandle.CurrentState = new ParseStateReadSubkeyValue();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
