using System;
using System.IO;
using System.Collections.Generic;

namespace ParseIni
{
    public class Lexer
    {
        private LexerTokenNode[] tokens;
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
            List<LexerTokenNode> listOfTokens = new List<LexerTokenNode>();
            int lineNumber = 0;
            int characterNumber = 0;
            int stringBufferCharacterIndex = 0;
            string stringBuffer = "";
            bool currentLineIsAComment = false;

            foreach (string currentLine in fileAsStringArray)
            {
                lineNumber++;
                characterNumber = 0;
                stringBufferCharacterIndex = 0;

                foreach (char currentCharacter in currentLine.ToCharArray())
                {
                    characterNumber++;

                    if (currentLineIsAComment) //If this line is a comment, break and go to next line
                    {
                        currentLineIsAComment = false;
                        break;
                    }

                    if (stringBuffer == "") //Allows for error messages to index at start of string
                    {
                        stringBufferCharacterIndex = characterNumber;
                    }

                    switch (currentCharacter)
                    {
                        case ';':
                            if (characterNumber == 1) //If a comment, break out of reading string - throws away comment.
                            {
                                currentLineIsAComment = true;
                            }
                            else //If this isn't the first character, this must be part of a string.
                            {
                                stringBuffer = stringBuffer + currentCharacter.ToString();
                            }
                            break;
                        case '[':
                            ClearStringBufferIfNotEmpty(ref stringBuffer, listOfTokens, lineNumber, stringBufferCharacterIndex);
                            listOfTokens.Add(new LexerTokenNode(LexerTokenNode.Token.OpenSquareBrace, "[", lineNumber, characterNumber));
                            break;
                        case ']':
                            ClearStringBufferIfNotEmpty(ref stringBuffer, listOfTokens, lineNumber, stringBufferCharacterIndex);
                            listOfTokens.Add(new LexerTokenNode(LexerTokenNode.Token.CloseSquareBrace, "]", lineNumber, characterNumber));
                            break;
                        case '=':
                            ClearStringBufferIfNotEmpty(ref stringBuffer, listOfTokens, lineNumber, stringBufferCharacterIndex);
                            listOfTokens.Add(new LexerTokenNode(LexerTokenNode.Token.EqualSign, "=", lineNumber, characterNumber));
                            break;
                        case ' ':
                            ClearStringBufferIfNotEmpty(ref stringBuffer, listOfTokens, lineNumber, stringBufferCharacterIndex);
                            listOfTokens.Add(new LexerTokenNode(LexerTokenNode.Token.WhiteSpace, " ", lineNumber, characterNumber));
                            break;
                        case '\t': //Throw out white space
                            ClearStringBufferIfNotEmpty(ref stringBuffer, listOfTokens, lineNumber, stringBufferCharacterIndex);
                            listOfTokens.Add(new LexerTokenNode(LexerTokenNode.Token.WhiteSpace, "\t", lineNumber, characterNumber));
                            break;
                        case '\r': //For Windows style end of line, expect a carriage return
                            ClearStringBufferIfNotEmpty(ref stringBuffer, listOfTokens, lineNumber, stringBufferCharacterIndex);
                            break;
                        case '\n': //For Unix and Windows, expect a new line. Add as a token and clear the buffer in case Unix only.
                            ClearStringBufferIfNotEmpty(ref stringBuffer, listOfTokens, lineNumber, stringBufferCharacterIndex); //Added in case of Unix style line ending.
                            characterNumber--; //Don't count this character
                            listOfTokens.Add(new LexerTokenNode(LexerTokenNode.Token.EndOfLine, System.Environment.NewLine, lineNumber, characterNumber));
                            break;
                        default: //Any other character will form a string token
                            stringBuffer = stringBuffer + currentCharacter.ToString();
                            break;
                    }
                }
            }
            ClearStringBufferIfNotEmpty(ref stringBuffer, listOfTokens, lineNumber, stringBufferCharacterIndex);
            characterNumber++; //Need to count one past last character to be at end of file char
            listOfTokens.Add(new LexerTokenNode(LexerTokenNode.Token.EndOfFile, "0", lineNumber, characterNumber));
            tokens = listOfTokens.ToArray();
        }

        private static void ClearStringBufferIfNotEmpty(ref string stringBuffer, List<LexerTokenNode> tokenLine, int lineNumber, int characterNumber)
        {
            if (stringBuffer != "")
            {
                tokenLine.Add(new LexerTokenNode(LexerTokenNode.Token.String, stringBuffer, lineNumber, characterNumber));
                stringBuffer = "";
            }
        }

        public LexerTokenNode[] Tokens
        {
            get
            {
                return tokens;
            }
        }
    }
}
