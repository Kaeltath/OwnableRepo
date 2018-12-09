using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OwnableCI.Pages;

namespace OwnableCI.PopUpPages
{
    public class PromoCodePopUp : BasePopeUpPage
    {

        #region IWebelements
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Promo Code'")]
        public IWebElement iputPromoCode;


        [FindsBy(How = How.XPath, Using = "//button[text()='ADD PROMO CODE'")]
        public IWebElement btnAddPromoCode;


        [FindsBy(How = How.XPath, Using = "//button[text()='Cancel']")]
        public IWebElement btnBack;

        #endregion

        public PromoCodePopUp(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            logger = log4net.LogManager.GetLogger(typeof(PromoCodePopUp));
            PageFactory.InitElements(driver, this);
        }
    }
}
