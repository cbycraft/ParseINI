using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParseIni;
using System;
using System.Collections.Generic;
using System.IO;

namespace ParseIniTests
{
    [TestClass()]
    public class ParserTests
    {
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

        [TestMethod()]
        public void ParserTestPrimaryKey()
        {
            string[] exampleIniFile = new string[]
            {
                "; last modified 1 April 2001 by John Doe"+System.Environment.NewLine,
                "[owner]"+System.Environment.NewLine,
                ""
            };

            Parser iniFileParser = new Parser(exampleIniFile);
            bool containsKey = iniFileParser.IniFileDictionary.ContainsKey("owner");
            Assert.AreEqual(true, containsKey);
            int keyCount = iniFileParser.IniFileDictionary.Count;
            Assert.AreEqual(1, keyCount);
        }
    }
}
