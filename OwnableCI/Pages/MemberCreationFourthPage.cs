using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using OwnableCI_TestLib.Pages;
using OwnableCI.ServiceClasses;
using System;

namespace OwnableCI.Pages
{
    class MemberCreationFourthPage : HomePage   //"APPLICATION DISCLOSURE" page
    {
        #region IWebelements 
        [FindsBy(How = How.XPath, Using = "//a[@title='To Next Signature']")]
        public IWebElement btnToSign;


        [FindsBy(How = How.XPath, Using = "//button[text()='Decline']")]
        public IWebElement btnDecline;


        [FindsBy(How = How.XPath, Using = "//span[text()='AGREE']/parent::button")]
        public IWebElement btnAgree;
        #endregion

        public MemberCreationFourthPage(IWebDriver usedBrowser): base(usedBrowser, false)
        {
            driver = usedBrowser;
            PageFactory.InitElements(driver, this);
            
        }

        public void SetAgreement()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//span[text()='AGREE']")));//ToDo: need to wait 'btnAgree' here
            //btnAgree.Click();
            TestHelper.JSexecutorClick(btnAgree, driver);
        }
    }
}
