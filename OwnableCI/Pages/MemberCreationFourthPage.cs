using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OwnableCI_TestLib.Pages;

namespace OwnableCI.Pages
{
    class MemberCreationFourthPage : HomePage   //"APPLICATION DISCLOSURE" page
    {
        #region IWebelements 
        [FindsBy(How = How.XPath, Using = "//a[@title='To Next Signature']")]
        public IWebElement btnToSign;


        [FindsBy(How = How.XPath, Using = "//button[text()='Decline']")]
        public IWebElement btnDecline;


        [FindsBy(How = How.XPath, Using = "//span[text()='AGREE']")]
        public IWebElement btnAgree;
        #endregion

        public MemberCreationFourthPage(IWebDriver usedBrowser): base(usedBrowser, false)
        {
            driver = usedBrowser;
            PageFactory.InitElements(driver, this);
            
        }
    }
}
