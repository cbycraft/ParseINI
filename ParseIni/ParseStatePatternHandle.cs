using System;
using System.Collections.Generic;

namespace ParseIni
{
    public class ParseStatePatternHandle
    {
        public ParseStatePatternHandle(Dictionary<string, Dictionary<string, string>> iniFileDictionary)
        {
            this.IniFileDictionary = iniFileDictionary;
            StringBuffer = "";
            CurrentPrimaryKey = "";
            CurrentSecondaryKey = "";
        }

        public string StringBuffer { set; get; }
        public string CurrentPrimaryKey { set; get; }
        public string CurrentSecondaryKey { set; get; }
        public IParseStatePattern CurrentState { set; get; }
        public LexerTokenNode CurrentToken { get; set; }
        public Dictionary<string, Dictionary<string, string>> IniFileDictionary { get; }

        public void StateRequest(LexerTokenNode currentToken)
        {
            this.CurrentToken = currentToken;
            try
            {
                this.CurrentState.EvaluateState(this);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
    }
}
