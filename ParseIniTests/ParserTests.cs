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

        [TestMethod()]
        public void ParserTestSubkey()
        {
            string[] exampleIniFile = new string[]
            {
                "; last modified 1 April 2001 by John Doe"+System.Environment.NewLine,
                "[owner]"+System.Environment.NewLine,
                "MySub=1.0E-6"
            };

            Parser iniFileParser = new Parser(exampleIniFile);
            bool containsSubkey = iniFileParser.IniFileDictionary["owner"].ContainsKey("MySub");
            Assert.AreEqual(true, containsSubkey);
            int subkeyCount = iniFileParser.IniFileDictionary["owner"].Count;
            Assert.AreEqual(1, subkeyCount);
        }

        [TestMethod()]
        public void ParserTestSubkeyValue()
        {
            string[] exampleIniFile = new string[]
            {
                "; last modified 1 April 2001 by John Doe"+System.Environment.NewLine,
                "[owner]"+System.Environment.NewLine,
                "MySub=1.0E-6"
            };

            Parser iniFileParser = new Parser(exampleIniFile);
            string returnValue = iniFileParser.IniFileDictionary["owner"]["MySub"];
            Assert.AreEqual("1.0E-6", returnValue);
        }


        [TestMethod()]
        public void ParseTestTwoPrimaryKeys()
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

            Parser iniFileParser = new Parser(exampleIniFile);
            bool containsKeys = false;

            if( iniFileParser.IniFileDictionary.ContainsKey("owner") &&
                iniFileParser.IniFileDictionary.ContainsKey("database.dat"))
            {
                containsKeys = true;
            }
            else
            {
                containsKeys = false;
            }

            Assert.AreEqual(true, containsKeys);
            Assert.AreEqual(2, iniFileParser.IniFileDictionary.Count);
        }

        [TestMethod()]
        public void ParseTestCheckNameSubkey()
        {
            string[] exampleIniFile = new string[]
            {
                "; last modified 1 April 2001 by John Doe"+System.Environment.NewLine,
                "[owner]"+System.Environment.NewLine,
                "name=John Doe"+System.Environment.NewLine,
                "organization=Acme Widgets Inc."+System.Environment.NewLine,
                "[database.dat]"+System.Environment.NewLine,
                "; use IP address in case network name resolution is not working"+System.Environment.NewLine,
                "server=192.0.2.62"+System.Environment.NewLine,
                "port=143"+System.Environment.NewLine,
                "file=\"payroll.dat\""
            };

            Parser iniFileParser = new Parser(exampleIniFile);
            Assert.AreEqual(true, iniFileParser.IniFileDictionary["owner"].ContainsKey("name"));
        }

        [TestMethod()]
        public void ParseTestCheckNameSubkeyValue()
        {
            string[] exampleIniFile = new string[]
            {
                "; last modified 1 April 2001 by John Doe"+System.Environment.NewLine,
                "[owner]"+System.Environment.NewLine,
                "name=John Doe"+System.Environment.NewLine,
                "organization=Acme Widgets Inc."+System.Environment.NewLine,
                "[database.dat]"+System.Environment.NewLine,
                "; use IP address in case network name resolution is not working"+System.Environment.NewLine,
                "server=192.0.2.62"+System.Environment.NewLine,
                "port=143"+System.Environment.NewLine,
                "file=\"payroll.dat\""
            };

            Parser iniFileParser = new Parser(exampleIniFile);
            Assert.AreEqual("John Doe", iniFileParser.IniFileDictionary["owner"]["name"]);
        }

        [TestMethod()]
        public void ParseTestCheckOrganizationSubkey()
        {
            string[] exampleIniFile = new string[]
            {
                "; last modified 1 April 2001 by John Doe"+System.Environment.NewLine,
                "[owner]"+System.Environment.NewLine,
                "name=John Doe"+System.Environment.NewLine,
                "organization=Acme Widgets Inc."+System.Environment.NewLine,
                "[database.dat]"+System.Environment.NewLine,
                "; use IP address in case network name resolution is not working"+System.Environment.NewLine,
                "server=192.0.2.62"+System.Environment.NewLine,
                "port=143"+System.Environment.NewLine,
                "file=\"payroll.dat\""
            };

            Parser iniFileParser = new Parser(exampleIniFile);
            Assert.AreEqual(true, iniFileParser.IniFileDictionary["owner"].ContainsKey("organization"));
        }

        [TestMethod()]
        public void ParseTestCheckOrganizationSubkeyValue()
        {
            string[] exampleIniFile = new string[]
            {
                "; last modified 1 April 2001 by John Doe"+System.Environment.NewLine,
                "[owner]"+System.Environment.NewLine,
                "name=John Doe"+System.Environment.NewLine,
                "organization=Acme Widgets Inc."+System.Environment.NewLine,
                "[database.dat]"+System.Environment.NewLine,
                "; use IP address in case network name resolution is not working"+System.Environment.NewLine,
                "server=192.0.2.62"+System.Environment.NewLine,
                "port=143"+System.Environment.NewLine,
                "file=\"payroll.dat\""
            };

            Parser iniFileParser = new Parser(exampleIniFile);
            Assert.AreEqual("Acme Widgets Inc.", iniFileParser.IniFileDictionary["owner"]["organization"]);
        }

        [TestMethod()]
        public void ParseTestServerSubkey()
        {
            string[] exampleIniFile = new string[]
            {
                "; last modified 1 April 2001 by John Doe"+System.Environment.NewLine,
                "[owner]"+System.Environment.NewLine,
                "name=John Doe"+System.Environment.NewLine,
                "organization=Acme Widgets Inc."+System.Environment.NewLine,
                "[database.dat]"+System.Environment.NewLine,
                "; use IP address in case network name resolution is not working"+System.Environment.NewLine,
                "server=192.0.2.62"+System.Environment.NewLine,
                "port=143"+System.Environment.NewLine,
                "file=\"payroll.dat\""
            };

            Parser iniFileParser = new Parser(exampleIniFile);
            Assert.AreEqual(true, iniFileParser.IniFileDictionary["database.dat"].ContainsKey("server"));
        }

        [TestMethod()]
        public void ParseTestServerSubkeyValue()
        {
            string[] exampleIniFile = new string[]
            {
                "; last modified 1 April 2001 by John Doe"+System.Environment.NewLine,
                "[owner]"+System.Environment.NewLine,
                "name=John Doe"+System.Environment.NewLine,
                "organization=Acme Widgets Inc."+System.Environment.NewLine,
                "[database.dat]"+System.Environment.NewLine,
                "; use IP address in case network name resolution is not working"+System.Environment.NewLine,
                "server=192.0.2.62"+System.Environment.NewLine,
                "port=143"+System.Environment.NewLine,
                "file=\"payroll.dat\""
            };

            Parser iniFileParser = new Parser(exampleIniFile);
            Assert.AreEqual("192.0.2.62", iniFileParser.IniFileDictionary["database.dat"]["server"]);
        }

        [TestMethod()]
        public void ParseTestPortSubkey()
        {
            string[] exampleIniFile = new string[]
            {
                "; last modified 1 April 2001 by John Doe"+System.Environment.NewLine,
                "[owner]"+System.Environment.NewLine,
                "name=John Doe"+System.Environment.NewLine,
                "organization=Acme Widgets Inc."+System.Environment.NewLine,
                "[database.dat]"+System.Environment.NewLine,
                "; use IP address in case network name resolution is not working"+System.Environment.NewLine,
                "server=192.0.2.62"+System.Environment.NewLine,
                "port=143"+System.Environment.NewLine,
                "file=\"payroll.dat\""
            };

            Parser iniFileParser = new Parser(exampleIniFile);
            Assert.AreEqual(true, iniFileParser.IniFileDictionary["database.dat"].ContainsKey("port"));
        }

        [TestMethod()]
        public void ParseTestPortSubkeyValue()
        {
            string[] exampleIniFile = new string[]
            {
                "; last modified 1 April 2001 by John Doe"+System.Environment.NewLine,
                "[owner]"+System.Environment.NewLine,
                "name=John Doe"+System.Environment.NewLine,
                "organization=Acme Widgets Inc."+System.Environment.NewLine,
                "[database.dat]"+System.Environment.NewLine,
                "; use IP address in case network name resolution is not working"+System.Environment.NewLine,
                "server=192.0.2.62"+System.Environment.NewLine,
                "port=143"+System.Environment.NewLine,
                "file=\"payroll.dat\""
            };

            Parser iniFileParser = new Parser(exampleIniFile);
            Assert.AreEqual("143", iniFileParser.IniFileDictionary["database.dat"]["port"]);
        }

        [TestMethod()]
        public void ParseTestFileSubkey()
        {
            string[] exampleIniFile = new string[]
            {
                "; last modified 1 April 2001 by John Doe"+System.Environment.NewLine,
                "[owner]"+System.Environment.NewLine,
                "name=John Doe"+System.Environment.NewLine,
                "organization=Acme Widgets Inc."+System.Environment.NewLine,
                "[database.dat]"+System.Environment.NewLine,
                "; use IP address in case network name resolution is not working"+System.Environment.NewLine,
                "server=192.0.2.62"+System.Environment.NewLine,
                "port=143"+System.Environment.NewLine,
                "file=\"payroll.dat\""
            };

            Parser iniFileParser = new Parser(exampleIniFile);
            Assert.AreEqual(true, iniFileParser.IniFileDictionary["database.dat"].ContainsKey("file"));
        }

        [TestMethod()]
        public void ParseTestFileSubkeyValue()
        {
            string[] exampleIniFile = new string[]
            {
                "; last modified 1 April 2001 by John Doe"+System.Environment.NewLine,
                "[owner]"+System.Environment.NewLine,
                "name=John Doe"+System.Environment.NewLine,
                "organization=Acme Widgets Inc."+System.Environment.NewLine,
                "[database.dat]"+System.Environment.NewLine,
                "; use IP address in case network name resolution is not working"+System.Environment.NewLine,
                "server=192.0.2.62"+System.Environment.NewLine,
                "port=143"+System.Environment.NewLine,
                "file=\"payroll.dat\""
            };

            Parser iniFileParser = new Parser(exampleIniFile);
            Assert.AreEqual("\"payroll.dat\"", iniFileParser.IniFileDictionary["database.dat"]["file"]);
        }
    }
}
