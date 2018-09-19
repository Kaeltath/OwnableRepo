using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using OwnableCI_TestLib.Constants;
using System.Threading;

namespace OwnableCI_TestLib.Pages
{
    class LoginPage : BasePage
    {

        //
        // todo all neede elements
        //

        #region IWebelements        
        private IWebElement SignUpButton;
        private IWebElement EmailField;
        private IWebElement PassField;



        #endregion



        // <summary>
        /// Call the base class constructor
        /// </summary>
        /// <param name="browser"></param>
        public LoginPage(IWebDriver browser) : base(browser) { }

        
        public void Login()
        {
            SignUpButton = driver.FindElement(By.XPath("//button[text() = ' Sign Up ']"));
            SignUpButton.Click();
            EmailField = driver.FindElement(By.XPath("//input[@type = 'email']"));            
            PassField= driver.FindElement(By.XPath("//input[@type = 'password']"));
            EmailField.SendKeys("user@user.user");
            PassField.SendKeys("Qq1!qwe123@");
            Thread.Sleep(50000);

        }

        public override void NavigateToPage(string parameter = "http://dev.ownable.us/app/home")
        {
            this.driver.Navigate().GoToUrl(parameter);
        }
    }
}
