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

namespace OwnableCI.Tests
{   
    //[TestFixture]
    class UserCreationTest : BaseTest
    {
        [TestCaseSource("users")]
        [Test]
        public void FirsOne(TestUser user)
        {
            SignUpPage page = new SignUpPage(chrome);
            page.inputEmail.SendKeys("testtest@test.test");
            page.inputPassword.SendKeys("123qweQWE!@#");
            page.chkIAgreeToTheTerms.Click();
            page.btnLogIn.Click();
            MemberCreation_PersonalInfoPage memberinput = new MemberCreation_PersonalInfoPage(chrome);            

        }

        
    }
}
