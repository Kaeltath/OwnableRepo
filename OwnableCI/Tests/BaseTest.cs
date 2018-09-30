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

namespace OwnableCI_TestLib.Tests
{
    public class BaseTest
    {
        private IWebDriver chrome;
        private HomePage page;
        private static List<TestUser> users = new XMLParseTestUsers().UsersForTests();
        private List<CreditCard> cards = new XMLParseCreditCards().CardsForTests();
        private List<CodeAndState> statesCodes = new XMLParseStatesAndCodes().CardsForTests();

        [SetUp]
        public void Init()
        {
            chrome = new ChromeDriver("D:\\SelTestLib\\OwnableCI_TestLib\\OwnableCI_TestLib\\Drivers");
        }

        [TestCaseSource("users")]
       [Test]
       public void FirsOne()
        {
            new XMLParseTestUsers();
            page = new HomePage(chrome);   
            page.Login();
            chrome.Close();
            chrome.Quit();

        }
        
    }
}