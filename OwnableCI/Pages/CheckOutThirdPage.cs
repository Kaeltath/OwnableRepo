

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OwnableCI_TestLib.Pages;

namespace OwnableCI.Pages
{
    public class CheckOutThirdPage : HomePage
    {
        #region IWebelements
        [FindsBy(How = How.XPath, Using = "//button[text()='ADD PROMO CODE'")]
        public IWebElement btnAddPromoCode;


        [FindsBy(How = How.XPath, Using = "//button[text()='Place order']")]
        public IWebElement btnPlaceOrder;


        [FindsBy(How = How.XPath, Using = "//button[text()=' Back ']")]
        public IWebElement btnBack;
        #endregion

        public CheckOutThirdPage(IWebDriver browser) : base(browser)
        {
            logger = log4net.LogManager.GetLogger(typeof(CheckOutThirdPage));
        }

    }
}
