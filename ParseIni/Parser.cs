using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseIni
{
    public class Parser
    {
        private Dictionary<string, Dictionary<string, string>> iniFileDictionary;

        public Parser(string fullAbsolutePathToIniFile)
        {
            try
            {
                string[] fileAsArrayOfStrings = System.IO.File.ReadAllLines(fullAbsolutePathToIniFile);
                Parser passInFile = new Parser(fileAsArrayOfStrings);
            }

            catch (Exception fileException)
            {
                if (fileException.Source != null)
                {
                    Console.WriteLine(fileException.Source);
                }
                throw;
            }
        }

        public Parser(string[] fileAsStringArray)
        {
            Lexer lexedFile = new Lexer(fileAsStringArray);
            this.iniFileDictionary = new Dictionary<string, Dictionary<string, string>>();
            Stack<LexerTokenNode> tokenStack = new Stack<LexerTokenNode>(lexedFile.Tokens);
            ParseStatePatternHandle statePatternHandle = new ParseStatePatternHandle(iniFileDictionary);
            statePatternHandle.CurrentState = new ParseStateLineStart();

            foreach(LexerTokenNode currentToken in lexedFile.Tokens)
            {
                statePatternHandle.StateRequest(currentToken);
            }
        }

        public Dictionary<string, Dictionary<string, string>> IniFileDictionary { get; }
    }
}
