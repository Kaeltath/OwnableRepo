using OwnableCI.Enums;
using OwnableCI.TestDataObjs;
using OwnableCI.XMLParsers;
using OwnableCI_TestLib.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using System.Linq;

namespace OwnableCI.SeviceClasses
{
    public class TestSourceProvider
    {
        internal log4net.ILog log = log4net.LogManager.GetLogger(typeof(TestSourceProvider));
        private Dictionary<TestUser, TestUser> usersIncrementTracker;

        public List<KeyValuePair<TestUser, BrowserType>> GetTestSource(TestGroup group)
        {
            List<TestUser> usersTemplates = new XMLParseTestUsers().GetUsersTamplate();
            List<TestUser> targetTestUsers = GetTargetTestGroupUsers(usersTemplates, group);
            List<KeyValuePair<TestUser, BrowserType>> testSource = CreateIncrementedTestSource(targetTestUsers, TestGroup.NewUserTests);
            UpdateUsersTamplateXML();
            return testSource;
        }

        private List<TestUser> GetTargetTestGroupUsers(List<TestUser> users, TestGroup group)
        {
            List<TestUser> targetUsers = new List<TestUser>();
            foreach (var user in users)
            {
                if (user.TestGroup == group.ToString())
                    targetUsers.Add(user);
            }
            return targetUsers;
        }

        private List<KeyValuePair<TestUser, BrowserType>> CreateIncrementedTestSource(List<TestUser> users, TestGroup group)
        {
            List<KeyValuePair<TestUser, BrowserType>> incrementedTestSource = new List<KeyValuePair<TestUser, BrowserType>>();
            usersIncrementTracker = new Dictionary<TestUser, TestUser>();
            TestUser currentUser;
            foreach (var user in users)
            {
                currentUser = user;
                foreach (BrowserType browser in Enum.GetValues(typeof(BrowserType)))
                {
                    incrementedTestSource.Add(new KeyValuePair<TestUser, BrowserType>(currentUser, browser));
                    if (user.TestRole == "OneTime")
                        currentUser = IncrementUser(currentUser);
                }

                if (user.TestRole == "OneTime")
                {
                    usersIncrementTracker.Add(user, currentUser);
                }
            }
            return incrementedTestSource;
        }

        private TestUser IncrementUser(TestUser user)
        {
            TestUser incrementedUser = new TestUser
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Adress = user.Adress,
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode,
                Mobile = user.Mobile,
                BirthDate = user.BirthDate,
                MonthlyIncome = user.MonthlyIncome,
                Company = user.Company,
                YearsEmployed = user.YearsEmployed,
                LastDigitsOFSocial = user.LastDigitsOFSocial,
                TestRole = user.TestRole,
                TestGroup = user.TestGroup,
                ExpResult = user.ExpResult
            };

            if (Regex.IsMatch(user.Email, @"(\d+)"))
            {
                var result = Regex.Replace(user.Email, @"(\d+)", Incrementor, RegexOptions.Multiline);
                incrementedUser.Email = result;
            }
            return incrementedUser;
        }

        private string Incrementor(Match match)
        {
            return (Convert.ToInt32(match.Value) + 1).ToString();
        }

        private void UpdateUsersTamplateXML()
        {
            if (usersIncrementTracker.Count == 0)
            {
                log.Debug("No user that requieres updates in XML");
                return;
            }
            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            string tamplateFileLocation = config.AppSettings.Settings["TestUsersXMLPath"].Value;
            XmlDocument usersTamplate = new XmlDocument();
            usersTamplate.Load(tamplateFileLocation);
            foreach (var iterationTracker in usersIncrementTracker)
            {
                var fieldToUpdate = usersTamplate.SelectNodes("/Users/user/Email[text()='" + iterationTracker.Key.Email + "']");
                if (fieldToUpdate != null)
                    fieldToUpdate[0].InnerText = iterationTracker.Value.Email;
            }
            usersTamplate.Save(tamplateFileLocation);
        }
    }
}
