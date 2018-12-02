using OwnableCI.TestDataObjs;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace OwnableCI.XMLParsers
{
    public class XMLParseTestUsers
    {
      
        public List<TestUser> GetUsersTamplate()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            if (!config.HasFile)
            { throw new FileNotFoundException("Missing configuration file for test dll"); }
            var userPathSetting = config.AppSettings.Settings["TestUsersXMLPath"].Value;
            List<TestUser> parsedUsers = new List<TestUser>();
            XDocument usersXml = XDocument.Load(userPathSetting);
            var xmlUsers = usersXml.Root.Elements("user");
            foreach (XElement elem in xmlUsers)
            {
                TestUser userToAdd = new TestUser
                {
                    Email = elem.Element("Email").Value,
                    Password = elem.Element("Password").Value,
                    FirstName = elem.Element("FirstName").Value,
                    LastName = elem.Element("LastName").Value,
                    Adress = elem.Element("Adress").Value,
                    City = elem.Element("City").Value,
                    State = elem.Element("State").Value,
                    ZipCode = elem.Element("ZipCode").Value,
                    Mobile = elem.Element("Mobile").Value,
                    BirthDate = elem.Element("Birthdate").Value,
                    MonthlyIncome = elem.Element("MonthlyIncome").Value,
                    Company = elem.Element("Company").Value,
                    YearsEmployed = elem.Element("YearsEmployed").Value,
                    LastDigitsOFSocial = elem.Element("LastDigitsOFSocial").Value,
                    TestRole = elem.Element("TestRole").Value,
                    TestGroup = elem.Element("TestGroup").Value,
                    ExpResult = elem.Element("ExpectedResult").Value
                };
                parsedUsers.Add(userToAdd);
            }
            return parsedUsers;

        }
    }
}
