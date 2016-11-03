using System;
using System.IO;

namespace ParseIni
{
    public class Lexer
    {
        public Lexer(string fullAbsolutePathToIniFile)
        {
            try
            {
                string[] fileAsArrayOfStrings = File.ReadAllLines(fullAbsolutePathToIniFile);
                Lexer passInFile = new Lexer(fileAsArrayOfStrings);
            }
            catch (Exception fileException)
            {
                if(fileException.Source != null)
                {
                    Console.WriteLine(fileException.Source);
                }   
                throw;
            }
        }

        public Lexer(string[] fileAsStringArray)
        {
            LexerStateHandle context = new LexerStateHandle(new LexerStateLineStart());
            foreach(string currentLine in fileAsStringArray)
            {
                foreach(char currentCharacter in currentLine.ToCharArray())
                {
                    context.StateRequest(currentCharacter);
                }
            }
        }
    }
}
