using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using RecurlyCleanUpUtility.Entities;

namespace RecurlyCleanUpUtility
{
    public class XmlParser
    {
        public static List<User> GetUsers(log4net.ILog Logger)
        {
            List<User> users = new List<User>();
            try
            {
                XDocument usersXml = XDocument.Load(@"XmlInputs\UsersToDelate.xml");
                var xmlUsers = usersXml.Root.Elements("user");
                foreach (XElement elem in xmlUsers)
                {
                    User userToAdd = new User()
                    {
                        FirstName = elem.Element("FirstName").Value,
                        LastName = elem.Element("LastName").Value,
                        Adress = elem.Element("Adress").Value,
                        City = elem.Element("City").Value,
                        State = elem.Element("State").Value,
                        Zip = elem.Element("Zip").Value,
                        Mobile = elem.Element("Mobile").Value,
                        BirthDate = elem.Element("BirthDate").Value,
                        LastDigitsOFSocial = elem.Element("LastDigitsOFSocial").Value
                    };
                    users.Add(userToAdd);                    
                }
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
