using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseIni
{
    public class ParseStatePatternHandle
    {
        private Dictionary<string, Dictionary<string, string>> iniFileDictionary;

        public ParseStatePatternHandle(Dictionary<string, Dictionary<string, string>> iniFileDictionary)
        {
            this.iniFileDictionary = iniFileDictionary;
        }

        public IParseStatePattern CurrentState { set; get; }

        public LexerTokenNode CurrentToken { get; set; }

        public Dictionary<string, Dictionary<string, string>> IniFileDictionary { get; }

        public void StateRequest(LexerTokenNode currentToken)
        {
            this.CurrentToken = currentToken;
            this.CurrentState.EvaluateState(this);
        }
    }
}
