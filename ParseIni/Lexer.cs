using System;
using System.IO;

namespace ParseIni
{
    public class Lexer
    {
        public Lexer(string fullAbsolutePathToIniFile)
        {

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

        private class IniFile
        {
            private string[] fileAsArrayOfStrings = null;

            public IniFile(string fullAbsolutePathToIniFile)
            {
                try
                {
                    this.fileAsArrayOfStrings = File.ReadAllLines(fullAbsolutePathToIniFile);
                }
                catch (ArgumentException argumentException)
                {
                    Console.WriteLine(argumentException.Message);
                }
                catch (IOException ioException)
                {
                    Console.WriteLine(ioException.Message);
                }
                catch (UnauthorizedAccessException unauthorizedAccessException)
                {
                    Console.WriteLine(unauthorizedAccessException.Message);
                }
                catch (NotSupportedException notSupportedException)
                {
                    Console.WriteLine(notSupportedException.Message);
                }
                catch (System.Security.SecurityException securityException)
                {
                    Console.WriteLine(securityException.Message);
                }
            }

            public string[] FileAsArrayOfStrings
            {
                get
                {
                    return this.fileAsArrayOfStrings;
                }
            }
        }
    }
}
