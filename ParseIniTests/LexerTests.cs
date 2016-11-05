using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParseIni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ParseIni.Tests
{
    [TestClass()]
    public class LexerTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyPathToLexer()
        {
            Lexer lexer = new Lexer("");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullPathToLexer()
        {
            string nullString = null;
            Lexer lexer = new Lexer(nullString);
        }

        [TestMethod()]
        [ExpectedException(typeof(FileNotFoundException))]
        public void InvalidPathToLexer()
        {
            Lexer lexer = new Lexer(@"C:\ThisDoesNotExistEver.ini");
        }

        [TestMethod()]
        public void CommentFollowedByPrimaryKey()
        {
            List<string> exampleIniFile = new List<string>
            {
                "; comment in an ini file"+System.Environment.NewLine,
                ";followed by another comment"+System.Environment.NewLine,
                "[FollowedByAKey]"+System.Environment.NewLine
            };

            List<LexerTokenNode> expectedOutputLines = new List<LexerTokenNode>
            {
                new LexerTokenNode(LexerTokenNode.Token.OpenSquareBrace, "[", 3, 1),
                new LexerTokenNode(LexerTokenNode.Token.String, "FollowedByAKey", 3, 2),
                new LexerTokenNode(LexerTokenNode.Token.CloseSquareBrace, "]", 3, 16),
                new LexerTokenNode(LexerTokenNode.Token.EndOfLine, System.Environment.NewLine, 3, 18)
            };

            Lexer lexer = new Lexer(exampleIniFile.ToArray());
            LexerTokenNode[] actualOutput = lexer.Tokens;
            LexerTokenNode[] expectedOutput = expectedOutputLines.ToArray();

            Assert.AreEqual(expectedOutput.Length, actualOutput.Length);
            for(int index = 0; index < actualOutput.Length; index++)
            {
                Assert.AreEqual(expectedOutput[index].TokenType, actualOutput[index].TokenType);
                Assert.AreEqual(expectedOutput[index].TokenValue, actualOutput[index].TokenValue);
                Assert.AreEqual(expectedOutput[index].LineNumber, actualOutput[index].LineNumber);
                Assert.AreEqual(expectedOutput[index].CharacterNumber, actualOutput[index].CharacterNumber);
            }
        }
    }
}