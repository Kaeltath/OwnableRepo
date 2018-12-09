using OwnableCI_TestLib.Pages;
using System;
using OpenQA.Selenium;
using OwnableCI.TestDataObjs;
using OpenQA.Selenium.Support.PageObjects;

namespace OwnableCI.Pages
{
    public class BasePopeUpPage : BasePage
    {
        public BasePopeUpPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            logger = log4net.LogManager.GetLogger(typeof(BasePage));
            PageFactory.InitElements(driver, this);
        }

        public override bool Login(TestUser user)
        {
            throw new NotSupportedException();
        }

        public override void NavigateToPage(string parameter = "")
        {
            throw new NotSupportedException();
        }
    }
}
