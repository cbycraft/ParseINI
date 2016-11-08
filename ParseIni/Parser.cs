using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParseIni
{
    public class Parser
    {
        private Dictionary<string, Dictionary<string, string>> iniFileDictionary;
        private string fileName = "";

        public Parser(string fullAbsolutePathToIniFile)
        {
            try
            {
                this.fileName = fullAbsolutePathToIniFile;
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
            statePatternHandle.CurrentState = new ParseStateFileStart();

            foreach(LexerTokenNode currentToken in lexedFile.Tokens)
            {
                try
                {
                    statePatternHandle.StateRequest(currentToken);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Exception in file - " + fileName);
                    Console.WriteLine("INI Parser Exception - " + exception.Message);
                    throw exception;
                }
            }

            IniFileDictionary = statePatternHandle.IniFileDictionary;
        }

        public Dictionary<string, Dictionary<string, string>> IniFileDictionary { get; }
    }
}
