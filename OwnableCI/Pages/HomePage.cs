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
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;



namespace OwnableCI_TestLib.Pages
{
    class HomePage : BasePage
    {

        //
        // todo all neede elements
        //

        #region IWebelements        

        [FindsBy(How = How.XPath, Using = "//button[text() = ' Sign Up ']")]
        private IWebElement SignUpButton;

        [FindsBy(How = How.XPath, Using = "//input[@type = 'email']")]
        private IWebElement EmailField;

        [FindsBy(How = How.XPath, Using = "//input[@type = 'password']")]
        private IWebElement PassField;


        #endregion



        // <summary>
        /// Call the base class constructor
        /// </summary>
        /// <param name="browser"></param>
        public HomePage(IWebDriver browser) : base(browser) {

            PageFactory.InitElements(driver, this);
            NavigateToPage();            
        }

        
        public void Login( string username = "123", string pass = "321")
        {
            ChromeOptions options = new ChromeOptions();
            SignUpButton.Click();            
            EmailField.SendKeys(username);
            PassField.SendKeys(pass);
            Thread.Sleep(50000);

        }

        public override void NavigateToPage(string parameter = "http://dev.ownable.us/app/home")
        {
            this.driver.Navigate().GoToUrl(parameter);
            InitPage(this);
        }
    }
}
