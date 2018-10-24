using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OwnableCI_TestLib.Pages;

namespace OwnableCI.Pages
{
    class MemberCreationFifthPage : HomePage   //"Great News!" finish registration page
    {
        #region IWebelements 
        [FindsBy(How = How.XPath, Using = "//h1[starts-with(text(),' $')]")]
        public IWebElement txtRentalCapValue;


        [FindsBy(How = How.XPath, Using = "//button[text()='START SHOPPING!']")]
        public IWebElement btnStartShopping;
        #endregion

        public MemberCreationFifthPage(IWebDriver usedBrowser): base(usedBrowser, false)
        {
            driver = usedBrowser;
            PageFactory.InitElements(driver, this);
            
        }
    }
}
