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
    [TestFixture]
    public class BaseTest
    {
        internal IWebDriver chrome;
        internal BasePage page;
        internal static List<TestUser> users = new XMLParseTestUsers().UsersForTests();
        internal static List<CreditCard> cards = new XMLParseCreditCards().CardsForTests();
        internal static List<CodeAndState> statesCodes = new XMLParseStatesAndCodes().CardsForTests();

        [OneTimeSetUp]
        public void CreateBrowser()
        {
            chrome = new ChromeDriver("D:\\SelTestLib\\OwnableCI_TestLib\\OwnableCI_TestLib\\Drivers");
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            chrome.Close();
            chrome.Quit();
        }

    }
}