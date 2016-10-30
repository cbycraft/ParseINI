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

        }

        internal enum Tokens
        {
            String,
            OpenSquareBrace,
            CloseSquareBrace,
            EqualSign,
            Unknown
        }
    }
}
