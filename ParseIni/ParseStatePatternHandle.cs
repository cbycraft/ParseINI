using System;
using System.Collections.Generic;

namespace ParseIni
{
    internal class ParseStatePatternHandle
    {
        private Dictionary<string, Dictionary<string, string>> iniFileDictionary;

        public ParseStatePatternHandle(Dictionary<string, Dictionary<string, string>> iniFileDictionary)
        {
            this.iniFileDictionary = iniFileDictionary;
            StringBuffer = "";
        }

        public string StringBuffer { set; get; }
        public string CurrentPrimaryKey { set; get; }
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
