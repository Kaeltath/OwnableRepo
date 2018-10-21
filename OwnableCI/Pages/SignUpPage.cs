using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OwnableCI.TestDataObjs;
using OwnableCI_TestLib.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnableCI.Pages
{
    class SignUpPage : BasePage
    {
        #region IWebelements

        [FindsBy(How = How.XPath, Using = "//span[@class='auth0-lock-close-button']")]
        public IWebElement btnClose;


        [FindsBy(How = How.XPath, Using = "//a[text()='Log In']")]
        public IWebElement tabLogIn;


        [FindsBy(How = How.XPath, Using = "//a[text()='Sign Up']")]
        public IWebElement tabSignUp;


        [FindsBy(How = How.XPath, Using = "//div[text()='Sign up with Facebook']")]
        public IWebElement btnSignUpWithFacebook;


        [FindsBy(How = How.XPath, Using = "//div[text()='Sign up with Google']")]
        public IWebElement btnSignUpWithGoogle;


        [FindsBy(How = How.XPath, Using = "//input[@name='email']")]
        public IWebElement inputEmail;


        [FindsBy(How = How.XPath, Using = "//input[@name='password']")]
        public IWebElement inputPassword;


        [FindsBy(How = How.XPath, Using = "//input[@type='checkbox']")]
        public IWebElement chkIAgreeToTheTerms;


        [FindsBy(How = How.XPath, Using = "//button[@name='submit']/span[text()='Sign Up']")]
        public IWebElement btnLogIn;

        #endregion

        private IWebDriver browser;
        public SignUpPage(IWebDriver browser) : base(browser)
        {
            this.browser = browser;
            PageFactory.InitElements(driver, this);
            NavigateToPage();
        }

        public override void NavigateToPage(string parameter = "")
        {
            new HomePage(browser).btnSignUp.Click();
            InitPage(this);
        }

        public override void Login(TestUser user)
        {
            throw new NotImplementedException();
        }

        public static implicit operator SignUpPage(SignInPage v)
        {
            throw new NotImplementedException();
        }
    }
}
