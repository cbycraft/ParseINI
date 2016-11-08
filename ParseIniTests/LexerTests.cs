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

        //[TestMethod()]
        //public void LexerTestSimple()
        //{
        //    string[] exampleIniFile = new string[]
        //    {
        //        "; comment in an ini file"+System.Environment.NewLine,
        //        ";followed by another comment"+System.Environment.NewLine,
        //        "[FollowedByAKey]"+System.Environment.NewLine
        //    };

        //    LexerTokenNode[] expectedOutputLines = new LexerTokenNode[]
        //    {
        //        new LexerTokenNode(LexerTokenNode.Token.OpenSquareBrace, "[", 3, 1),
        //        new LexerTokenNode(LexerTokenNode.Token.String, "FollowedByAKey", 3, 2),
        //        new LexerTokenNode(LexerTokenNode.Token.CloseSquareBrace, "]", 3, 16),
        //        new LexerTokenNode(LexerTokenNode.Token.EndOfLine, System.Environment.NewLine, 3, 18)
        //    };

        //    Lexer lexer = new Lexer(exampleIniFile);
        //    LexerTokenNode[] actualOutput = lexer.Tokens;
        //    LexerTokenNode[] expectedOutput = expectedOutputLines;

        //    Assert.AreEqual(expectedOutput.Length, actualOutput.Length);
        //    for(int index = 0; index < actualOutput.Length; index++)
        //    {
        //        Assert.AreEqual(expectedOutput[index].TokenType, actualOutput[index].TokenType);
        //        Assert.AreEqual(expectedOutput[index].TokenValue, actualOutput[index].TokenValue);
        //        Assert.AreEqual(expectedOutput[index].LineNumber, actualOutput[index].LineNumber);
        //        Assert.AreEqual(expectedOutput[index].CharacterNumber, actualOutput[index].CharacterNumber);
        //    }
        //}

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
                "file=\"payroll.dat\""
            };

            LexerTokenNode[] expectedOutputLines = new LexerTokenNode[]
            {
                //; last modified 1 April 2001 by John Doe
                //[owner]

                new LexerTokenNode(LexerTokenNode.Token.OpenSquareBrace, "[", 2, 1),
                new LexerTokenNode(LexerTokenNode.Token.String, "owner", 2, 2),
                new LexerTokenNode(LexerTokenNode.Token.CloseSquareBrace, "]", 2, 7),
                new LexerTokenNode(LexerTokenNode.Token.EndOfLine, System.Environment.NewLine, 2, 8),

                //name = John Doe
                new LexerTokenNode(LexerTokenNode.Token.String, "name", 3, 1),
                new LexerTokenNode(LexerTokenNode.Token.WhiteSpace, " ", 3, 5 ),
                new LexerTokenNode(LexerTokenNode.Token.EqualSign, "=", 3, 6),
                new LexerTokenNode(LexerTokenNode.Token.WhiteSpace, " ", 3, 7),
                new LexerTokenNode(LexerTokenNode.Token.String, "John", 3, 8),
                new LexerTokenNode(LexerTokenNode.Token.WhiteSpace, " ", 3, 12),
                new LexerTokenNode(LexerTokenNode.Token.String, "Doe", 3, 13),
                new LexerTokenNode(LexerTokenNode.Token.EndOfLine, System.Environment.NewLine, 3, 16),

                //organization=Acme Widgets Inc
                new LexerTokenNode(LexerTokenNode.Token.String, "organization", 4, 1),
                new LexerTokenNode(LexerTokenNode.Token.EqualSign, "=", 4, 13),
                new LexerTokenNode(LexerTokenNode.Token.String, "Acme", 4, 14),
                new LexerTokenNode(LexerTokenNode.Token.WhiteSpace, " ", 4, 18),
                new LexerTokenNode(LexerTokenNode.Token.String, "Widgets", 4, 19),
                new LexerTokenNode(LexerTokenNode.Token.WhiteSpace, " ", 4, 26),
                new LexerTokenNode(LexerTokenNode.Token.String, "Inc.", 4, 27),
                new LexerTokenNode(LexerTokenNode.Token.EndOfLine, System.Environment.NewLine, 4, 31),

                //[database.dat]
                new LexerTokenNode(LexerTokenNode.Token.OpenSquareBrace, "[", 5, 1),
                new LexerTokenNode(LexerTokenNode.Token.String, "database.dat", 5, 2),
                new LexerTokenNode(LexerTokenNode.Token.CloseSquareBrace, "]", 5, 14), 
                new LexerTokenNode(LexerTokenNode.Token.EndOfLine, System.Environment.NewLine, 5, 15),

                //; use IP address in case network name resolution is not working
                //server= 192.0.2.62
                new LexerTokenNode(LexerTokenNode.Token.String, "server", 7, 1),
                new LexerTokenNode(LexerTokenNode.Token.EqualSign, "=", 7, 7),
                new LexerTokenNode(LexerTokenNode.Token.WhiteSpace, " ", 7, 8),
                new LexerTokenNode(LexerTokenNode.Token.String, "192.0.2.62", 7, 9),
                new LexerTokenNode(LexerTokenNode.Token.EndOfLine, System.Environment.NewLine, 7, 19),

                //port =143
                new LexerTokenNode(LexerTokenNode.Token.String, "port", 8, 1),
                new LexerTokenNode(LexerTokenNode.Token.WhiteSpace, " ", 8, 5),
                new LexerTokenNode(LexerTokenNode.Token.EqualSign, "=", 8, 6), 
                new LexerTokenNode(LexerTokenNode.Token.String, "143", 8, 7),
                new LexerTokenNode(LexerTokenNode.Token.EndOfLine, System.Environment.NewLine, 8, 10),

                //file=\"payroll.dat\"
                new LexerTokenNode(LexerTokenNode.Token.String, "file", 9, 1),
                new LexerTokenNode(LexerTokenNode.Token.EqualSign, "=", 9, 5),
                new LexerTokenNode(LexerTokenNode.Token.String, "\"payroll.dat\"", 9, 6),

                //End of the INI file
                new LexerTokenNode(LexerTokenNode.Token.EndOfFile, "0", 9, 19)
            };

            Lexer lexer = new Lexer(exampleIniFile);
            LexerTokenNode[] actualOutput = lexer.Tokens;
            LexerTokenNode[] expectedOutput = expectedOutputLines;

            int minimumOfBothSizes = System.Math.Min(actualOutput.Length, expectedOutput.Length); //Take the smallest; these should be equal, but if not,
                                                                                                  //this will make debug qucker since it'll stop at index
            for (int index = 0; index < minimumOfBothSizes; index++)
            {
                Assert.AreEqual(expectedOutput[index].TokenType, actualOutput[index].TokenType);
                Assert.AreEqual(expectedOutput[index].TokenValue, actualOutput[index].TokenValue);
                Assert.AreEqual(expectedOutput[index].LineNumber, actualOutput[index].LineNumber);
                Assert.AreEqual(expectedOutput[index].CharacterNumber, actualOutput[index].CharacterNumber);
            }
        }

        [TestMethod()]
        public void ParserTestFileStartException()
        {
            string[] exampleIniFile = new string[]
            {
                "; last modified 1 April 2001 by John Doe"+System.Environment.NewLine,
                " [owner]"+System.Environment.NewLine,
                "name = John Doe"+System.Environment.NewLine,
                "organization=Acme Widgets Inc."+System.Environment.NewLine,
                "[database.dat]"+System.Environment.NewLine,
                "; use IP address in case network name resolution is not working"+System.Environment.NewLine,
                "server= 192.0.2.62"+System.Environment.NewLine,
                "port =143"+System.Environment.NewLine,
                "file=\"payroll.dat\""
            };

            try
            {
                Parser parser = new Parser(exampleIniFile);
            }
            catch (Exception exception)
            {
                Assert.AreEqual("First non-white space or comment in INI file must be an open square brace.", exception.Message);
                return;
            }
            Assert.Fail();
        }
    }
}