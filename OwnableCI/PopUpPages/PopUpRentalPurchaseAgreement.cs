using OwnableCI.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace OwnableCI.PopUpPages
{
    public class PopUpRentalPurchaseAgreement : BasePopeUpPage
    {
        #region
        [FindsBy(How = How.XPath, Using = "//div[@class='modal-footer rental-footer ng-star-inserted']")]
        public IWebElement btnAgree;

        [FindsBy(How = How.XPath, Using = "//div[@name='signature']")]
        public IWebElement signatureField;

        #endregion

        public PopUpRentalPurchaseAgreement(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            logger = log4net.LogManager.GetLogger(typeof(PromoCodePopUp));
            PageFactory.InitElements(driver, this);
        }
    }
}
