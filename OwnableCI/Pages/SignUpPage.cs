using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using OwnableCI.ServiceClasses;
using OwnableCI.TestDataObjs;
using OwnableCI_TestLib.Pages;
using System;
using System.Threading;

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


        //[FindsBy(How = How.XPath, Using = "//div[@class='auth0-lock-view-content']//label")] //obsolete control
        //public IWebElement chkIAgreeToTheTerms;


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

        public SignUpPage(IWebDriver browser, bool navigate) : base(browser)
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
            new HomePage(browser).btnSignUp.Click();
            InitPage(this);
        }

        public override bool Login(TestUser user)
        {
            throw new NotImplementedException();
        }

        public bool ValidateSignUp()
        {
            try
            {
                var failedMessage = driver.FindElement(By.XPath("//span[@class='animated fadeInUp']"));
            }
            catch {
                return true;
            }           
            return false;            
        }

        public bool UserSignUp(TestUser user)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//a[text()='Sign Up']")));//ToDo: need to wait 'tabSignUp' here
            Thread.Sleep(1000);
            TestHelper.JSexecutorClick(tabSignUp, driver);
            inputEmail.SendKeys(user.Email);
            inputPassword.SendKeys(user.Password);
            //pageSignUp.chkIAgreeToTheTerms.Click(); //removed from current version
            TestHelper.JSexecutorClick(btnLogIn, driver);
            Thread.Sleep(2000);
            try
            {
                var failedMessage = driver.FindElement(By.XPath("//span[contains(text(),'something went wrong when attempting to sign up') or contains(text(),'The user already exists')]")); //different messages on Dev and Stag
                return false;
            }
            catch
            {
                return true;
            }
        }

        public static implicit operator SignUpPage(SignInPage v)
        {
            throw new NotImplementedException();
        }
    }
}
