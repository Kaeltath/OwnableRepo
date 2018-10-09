using OpenQA.Selenium;
using OwnableCI_TestLib.Pages;
using OwnableCI_TestLib.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using NUnit.Framework;
using System.Threading;
using OwnableCI.XMLParsers;
using OwnableCI.TestDataObjs;
using OwnableCI.Pages;
using OwnableCI_TestLib.Tests;
using OwnableCI;

namespace OwnableCI.Tests
{
    [TestFixtureSource(typeof(TestProperties), "users")]
    [TestFixture]
    class UserCreationTest : BaseTest
    {
        private TestUser user;
        
        public UserCreationTest(TestUser inpUser)
        {
            this.user = inpUser;
        }

        
        [Test]
        public void FirsOne()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName+ user.LastName + ";");
            string birth = user.BirthDate;
            int i = 1 + 1;
            SignUpPage page = new SignUpPage(driverForRun);
            page.inputEmail.SendKeys("testtest@test.test");
            page.inputPassword.SendKeys("123qweQWE!@#");
            page.chkIAgreeToTheTerms.Click();
            page.btnLogIn.Click();
            MemberCreationFirstPage memberInput = new MemberCreationFirstPage(driverForRun);            

        }

        
    }
}
