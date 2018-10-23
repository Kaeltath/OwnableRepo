﻿using OwnableCI.TestDataObjs;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            var appSettings = ConfigurationManager.AppSettings;
            List<TestUser> parsedUsers = new List<TestUser>();
            XDocument usersXml = XDocument.Load(appSettings["TestUsersXMLPath"]);
            var xmlUsers = usersXml.Root.Elements("user");
            foreach (XElement elem in xmlUsers)
            {
                TestUser userToAdd = new TestUser
                {
                    FirstName = elem.Element("FirstName").Value,
                    LastName = elem.Element("LastName").Value,
                    Adress = elem.Element("Adress").Value,
                    Email = elem.Element("Email").Value,
                    Password = elem.Element("Password").Value,
                    City = elem.Element("City").Value,
                    State = elem.Element("State").Value,
                    Zip = elem.Element("Zip").Value,
                    Mobile = elem.Element("Mobile").Value,
                    BirthDate = elem.Element("BirthDate").Value,
                    LastDigitsOFSocial = elem.Element("LastDigitsOFSocial").Value,
                    ExpResult = elem.Element("ExpectedResult").Value
                };
                parsedUsers.Add(userToAdd);
            }
            return parsedUsers;

        }
    }
}
