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
        public void LexerTestEmptyFilePath()
        {
            Lexer lexer = new Lexer("");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LexerTestNullFile()
        {
            string nullString = null;
            Lexer lexer = new Lexer(nullString);
        }

        [TestMethod()]
        [ExpectedException(typeof(FileNotFoundException))]
        public void LexerTestNonExistentPath()
        {
            Lexer lexer = new Lexer(@"C:\ThisDoesNotExistEver.ini");
        }

        [TestMethod()]
        public void LexerTestSimple()
        {
            string[] exampleIniFile = new string[]
            {
                "; comment in an ini file"+System.Environment.NewLine,
                ";followed by another comment"+System.Environment.NewLine,
                "[FollowedByAKey]"+System.Environment.NewLine
            };

            LexerTokenNode[] expectedOutputLines = new LexerTokenNode[]
            {
                new LexerTokenNode(LexerTokenNode.Token.OpenSquareBrace, "[", 3, 1),
                new LexerTokenNode(LexerTokenNode.Token.String, "FollowedByAKey", 3, 2),
                new LexerTokenNode(LexerTokenNode.Token.CloseSquareBrace, "]", 3, 16),
                new LexerTokenNode(LexerTokenNode.Token.EndOfLine, System.Environment.NewLine, 3, 18)
            };

            Lexer lexer = new Lexer(exampleIniFile);
            LexerTokenNode[] actualOutput = lexer.Tokens;
            LexerTokenNode[] expectedOutput = expectedOutputLines;

            Assert.AreEqual(expectedOutput.Length, actualOutput.Length);
            for(int index = 0; index < actualOutput.Length; index++)
            {
                Assert.AreEqual(expectedOutput[index].TokenType, actualOutput[index].TokenType);
                Assert.AreEqual(expectedOutput[index].TokenValue, actualOutput[index].TokenValue);
                Assert.AreEqual(expectedOutput[index].LineNumber, actualOutput[index].LineNumber);
                Assert.AreEqual(expectedOutput[index].CharacterNumber, actualOutput[index].CharacterNumber);
            }
        }

        [TestMethod()]
        public void LexerTestWikipediaExample()
        {
            string[] exampleIniFile = new string[]
            {
                "; last modified 1 April 2001 by John Doe"+System.Environment.NewLine,
                "[owner]"+System.Environment.NewLine,
                "name = John Doe"+System.Environment.NewLine,
                "organization=Acme Widgets Inc."+System.Environment.NewLine,
                "[database.dat]"+System.Environment.NewLine,
                "; use IP address in case network name resolution is not working"+System.Environment.NewLine,
                "server= 192.0.2.62"+System.Environment.NewLine,
                "port =143"+System.Environment.NewLine,
                "file=\"payroll.dat\""+System.Environment.NewLine
            };

            LexerTokenNode[] expectedOutputLines = new LexerTokenNode[]
            {
                new LexerTokenNode(LexerTokenNode.Token.OpenSquareBrace, "[", 2, 1),
                new LexerTokenNode(LexerTokenNode.Token.String, "owner", 2, 2),
                new LexerTokenNode(LexerTokenNode.Token.CloseSquareBrace, "]", 2, 7),
                new LexerTokenNode(LexerTokenNode.Token.EndOfLine, System.Environment.NewLine, 2, 10),

                new LexerTokenNode(LexerTokenNode.Token.String, "name", 3, 1),
                new LexerTokenNode(LexerTokenNode.Token.WhiteSpace, " ", 3, 5 ),
                new LexerTokenNode(LexerTokenNode.Token.EqualSign, "=", 3, 6),
                new LexerTokenNode(LexerTokenNode.Token.WhiteSpace, " ", 3, 7),
                new LexerTokenNode(LexerTokenNode.Token.String, "John", 3, 8),
                new LexerTokenNode(LexerTokenNode.Token.WhiteSpace, " ", 3, 9),
                new LexerTokenNode(LexerTokenNode.Token.String, "Doe", 3, 10),
                new LexerTokenNode(LexerTokenNode.Token.EndOfLine, System.Environment.NewLine, 3, 18),

                //Todo: Implement rest
            };

            Lexer lexer = new Lexer(exampleIniFile);
            LexerTokenNode[] actualOutput = lexer.Tokens;
            LexerTokenNode[] expectedOutput = expectedOutputLines;

            Assert.AreEqual(expectedOutput.Length, actualOutput.Length);
            for (int index = 0; index < actualOutput.Length; index++)
            {
                Assert.AreEqual(expectedOutput[index].TokenType, actualOutput[index].TokenType);
                Assert.AreEqual(expectedOutput[index].TokenValue, actualOutput[index].TokenValue);
                Assert.AreEqual(expectedOutput[index].LineNumber, actualOutput[index].LineNumber);
                Assert.AreEqual(expectedOutput[index].CharacterNumber, actualOutput[index].CharacterNumber);
            }
        }
    }
}