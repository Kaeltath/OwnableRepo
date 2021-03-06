﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OwnableCI.ServiceClasses;
using OwnableCI.TestDataObjs;
using OwnableCI_TestLib.Pages;
using System.Threading;

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


        [FindsBy(How = How.XPath, Using = "//button[@name='submit']/span[text()='Log In']")]
        public IWebElement btnLogIn;

        #endregion

        private IWebDriver browser;
        public SignInPage(IWebDriver browser) : base(browser)
        {
            this.browser = browser;
            PageFactory.InitElements(driver, this);
            NavigateToPage();
        }

        public SignInPage(IWebDriver browser, bool navigate) : base(browser)
        {
            this.browser = browser;
            if (navigate)
            {
                PageFactory.InitElements(driver, this);
                NavigateToPage();
            }
        }

        public override void NavigateToPage(string parameter = "")
        {
            new HomePage(browser).btnSignIn.Click();
            InitPage(this);
        }

        public override bool Login(TestUser user)
        {
            inputEmail.SendKeys(user.Email);
            inputPassword.SendKeys(user.Password);
            TestHelper.JSexecutorClick(btnLogIn, driver);
            Thread.Sleep(2000);
            try
            {
                var failedMessage = driver.FindElement(By.XPath("//div[@id='auth0-lock-container-1']//span[text()='Wrong email or password.']"));
            }
            catch
            {
                return true;
            }
            return false;
        }
    }
}
