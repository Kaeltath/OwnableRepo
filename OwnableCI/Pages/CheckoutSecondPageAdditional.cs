using OpenQA.Selenium;
using OwnableCI.TestDataObjs;


namespace OwnableCI.Pages
{
    public class CheckoutSecondPageAdditional : CheckOutFirstPage
    {
        #region IWebelements     
        #endregion

        public CheckoutSecondPageAdditional(IWebDriver browser) : base(browser)
        {
            logger = log4net.LogManager.GetLogger(typeof(CheckoutSecondPageAdditional));
        }

        public override void FillForm(TestUser user)
        {

        }
    }
}
