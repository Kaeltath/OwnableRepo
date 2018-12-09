using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OwnableCI_TestLib.Pages;


namespace OwnableCI.Pages
{
    public class CheckOutSecondPage : HomePage
    {
        #region IWebelements
        [FindsBy(How = How.XPath, Using = "//p[@class='warning']")]
        public IWebElement AmericanExpressWanringLabel;


        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Card Number']")]
        public IWebElement CardNumber;


        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Month (mm)']")]
        public IWebElement ExpirationDateMounth;


        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Year (yy)']")]
        public IWebElement ExpirationDateYear;


        [FindsBy(How = How.XPath, Using = "//input[@placeholder='CVV']")]
        public IWebElement CVV;

        
        [FindsBy(How = How.XPath, Using = "//label[@class='form-check-label']")]
        public IWebElement BilingAdressCheckBox;


        [FindsBy(How = How.XPath, Using = "//button[text()=' Next ']")]
        public IWebElement btnNext;


        [FindsBy(How = How.XPath, Using = "//button[text()=' Back ']")]
        public IWebElement btnBack;
        #endregion

        public CheckOutSecondPage(IWebDriver browser) : base(browser)
        {
            logger = log4net.LogManager.GetLogger(typeof(CheckOutSecondPage));
        }

        public void FillForm()
        {

        }
    }
}
