using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OwnableCI_TestLib.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnableCI.Pages
{
    class SignInPage : BasePage
    {
        private IWebDriver browser;
        public SignInPage(IWebDriver browser) : base(browser)
        {
            this.browser = browser;
            PageFactory.InitElements(driver, this);
            NavigateToPage();
        }

        public override void NavigateToPage(string parameter = "")
        {
            new HomePage(browser).ClickbtnSignIn();
            InitPage(this);
        }
    }
}
