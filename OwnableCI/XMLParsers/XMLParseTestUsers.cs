using OwnableCI.TestDataObjs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OwnableCI.XMLParsers
{
    class XMLParseTestUsers
    {
      
        public List<TestUser> UsersForTests()
        {
            List<TestUser> parsedUsers = new List<TestUser>();
            XDocument usersXml = XDocument.Load(@"XMLTestAsserts\TestUsers.xml");
            var xmlUsers = usersXml.Root.Elements("user");
            foreach (XElement elem in xmlUsers)
            {
                TestUser userToAdd = new TestUser();
                userToAdd.FirstName = elem.Element("FirstName").Value;
                userToAdd.LastName = elem.Element("LastName").Value;
                userToAdd.Adress = elem.Element("Adress").Value;
                userToAdd.City = elem.Element("City").Value;
                userToAdd.State = elem.Element("State").Value;
                userToAdd.Zip = elem.Element("Zip").Value;
                userToAdd.Mobile = elem.Element("Mobile").Value;
                userToAdd.BirthDate = elem.Element("BirthDate").Value;
                userToAdd.LastDigitsOFSocial = elem.Element("LastDigitsOFSocial").Value;
                userToAdd.ExpResult = elem.Element("ExpectedResult").Value;
                parsedUsers.Add(userToAdd);
            }
            return parsedUsers;

        }
    }
}
