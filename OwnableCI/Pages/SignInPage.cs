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
        #region IWebelements

        [FindsBy(How = How.XPath, Using = "//span[@class='auth0-lock-close-button']")]
        public IWebElement btnClose;


        [FindsBy(How = How.XPath, Using = "//a[text()='Log In']")]
        public IWebElement tabLogIn;


        [FindsBy(How = How.XPath, Using = "//a[text()='Sign Up']")]
        public IWebElement tabSignUp;


        [FindsBy(How = How.XPath, Using = "//div[text()='Log in with Facebook']")]
        public IWebElement btnLogInWithFacebook;


        [FindsBy(How = How.XPath, Using = "//div[text()='Log in with Google']")]
        public IWebElement btnLogInWithGoogle;


        [FindsBy(How = How.XPath, Using = "//input[@name='email']")]
        public IWebElement inputEmail;


        [FindsBy(How = How.XPath, Using = "//input[@name='password']")]
        public IWebElement inputPassword;


        [FindsBy(How = How.XPath, Using = "//a[@class='auth0-lock-alternative-link']")]
        public IWebElement lnkDontRememberYourPassword;


        [FindsBy(How = How.XPath, Using = "//button[@name='submit']/span[@class='auth0-label-submit']")]
        public IWebElement btnLogIn;

        #endregion

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
