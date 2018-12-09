using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace OwnableCI.PopUpPages
{
    class PopUpAgreementConfirmation : PopUpRentalPurchaseAgreement
    {
        public PopUpAgreementConfirmation(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            logger = log4net.LogManager.GetLogger(typeof(PromoCodePopUp));
            PageFactory.InitElements(driver, this);
        }
    }
}
