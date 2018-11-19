using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OwnableCI_TestLib.Pages;

namespace OwnableCI.Pages
{
    class MemberCreationFifthPage : HomePage   //"Great News!" finish registration page
    {
        #region IWebelements 
        [FindsBy(How = How.XPath, Using = "//span[starts-with(text(),'You can rent items worth ')]//span[starts-with(text(),'$')]")]
        public IWebElement txtRentalCapValue;


        [FindsBy(How = How.XPath, Using = "//button[contains(@class,'d-md-block') and text()='START SHOPPING!']")]
        public IWebElement btnStartShopping;
        #endregion

        public MemberCreationFifthPage(IWebDriver usedBrowser): base(usedBrowser, false)
        {
            driver = usedBrowser;
            PageFactory.InitElements(driver, this);
            
        }
    }
}
