using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OwnableCI_TestLib.Pages;

namespace OwnableCI.Pages
{
    class CheckOutFinalPage : HomePage
    {
        #region
        [FindsBy(How = How.XPath, Using = "//a[text()='CONTINUE SHOPPING']")]
        public IWebElement btnContinueShoping;


        [FindsBy(How = How.XPath, Using = "//a[text()='See your Pay Calendar']")]
        public IWebElement lnkPaymentCalendar;
        #endregion

        public CheckOutFinalPage(IWebDriver browser) : base(browser)
        {
            logger = log4net.LogManager.GetLogger(typeof(CheckOutFirstPage));

        }
    }
}
