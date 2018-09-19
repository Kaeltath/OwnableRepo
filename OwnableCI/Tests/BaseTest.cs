
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

namespace OwnableCI_TestLib.Tests
{
    public class BaseTest
    {
        private IWebDriver chrome;
        private LoginPage page;

        [SetUp]
        public void Init()
        {
            chrome = new ChromeDriver("D:\\SelTestLib\\OwnableCI_TestLib\\OwnableCI_TestLib\\Drivers");
        }

       [Test]
       public void FirsOne()
        {
           
            page = new LoginPage(chrome);            
            page.NavigateToPage();
            page.Login();
            chrome.Close();
            chrome.Quit();

        }
        
    }
}