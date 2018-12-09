using OwnableCI_TestLib.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OwnableCI.ServiceClasses;
using OwnableCI.TestDataObjs;

namespace OwnableCI.Pages
{
    public class CheckOutFirstPage : HomePage
    {
        #region IWebelements
        [FindsBy(How = How.XPath, Using = "//input[@id='firstName']")]
        public IWebElement inputFirstName;


        [FindsBy(How = How.XPath, Using = "//input[@id='lastName']")]
        public IWebElement inputLastName;


        [FindsBy(How = How.XPath, Using = "//input[@id='addressLine1']")]
        public IWebElement inputHomeAddress;


        [FindsBy(How = How.XPath, Using = "//input[@id='addressLine2']")]
        public IWebElement inputAddress2Optional;


        [FindsBy(How = How.XPath, Using = "//input[@id='city']")]
        public IWebElement inputCity;


        [FindsBy(How = How.XPath, Using = "//ng-select[@role='listbox']")]
        public IWebElement lstState;


        [FindsBy(How = How.XPath, Using = "//input[@id='zipCode']")]
        public IWebElement inputZipCode;


        [FindsBy(How = How.XPath, Using = "//input[@id='phoneNumber']")]
        public IWebElement inputMobile;


        [FindsBy(How = How.XPath, Using = "//button[text()='Next']")]
        public IWebElement btnNext;
        #endregion

        public CheckOutFirstPage(IWebDriver browser) : base(browser)
        {
            logger = log4net.LogManager.GetLogger(typeof(CheckOutFirstPage));
        }

        public virtual void FillForm(TestUser user)
        {
            inputFirstName.SendKeys(user.FirstName);
            inputLastName.SendKeys(user.LastName);
            inputHomeAddress.SendKeys(user.Adress);
            inputCity.SendKeys(user.City);
            lstState.Click();
            var element = driver.FindElement(By.XPath("//span[text()='" + user.State + "']"));
            TestHelper.JSexecutorClick(element, driver);
            inputZipCode.SendKeys(user.ZipCode);
            inputMobile.SendKeys(user.Mobile);
            btnNext.Click();
        }
    }
}
