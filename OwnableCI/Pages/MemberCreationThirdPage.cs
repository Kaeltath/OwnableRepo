using OpenQA.Selenium;
using OwnableCI.TestDataObjs;
using OwnableCI.ServiceClasses;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using OwnableCI_TestLib.Pages;
using System;

namespace OwnableCI.Pages
{
    class MemberCreationThirdPage : HomePage   //"OWNABLE MEMBERSHIP AGREEMENT" page
    {
        #region IWebelements 
        [FindsBy(How = How.XPath, Using = "//a[@title='To Next Signature']")]
        public IWebElement btnToSign;


        [FindsBy(How = How.XPath, Using = "//p[text()=' Ownable Signature: ']//preceding::div[1]")]
        public IWebElement txtOwnableSignature;


        [FindsBy(How = How.XPath, Using = "//div[@class='col col-left']//p[text()='Name']//preceding::div[1]")]
        public IWebElement txtOwnableName;


        [FindsBy(How = How.XPath, Using = "//p[text()='Title']//preceding::div[1]")]
        public IWebElement txtOwnableTitle;


        [FindsBy(How = How.XPath, Using = "//div[@class='col col-left']//p[text()='Date']//preceding::div[1]")]
        public IWebElement txtOwnableDate;


        [FindsBy(How = How.XPath, Using = "//span[text()='Add signature']")]
        public IWebElement btnAddSignature;


        [FindsBy(How = How.XPath, Using = "//p[text()=' Member Signature: ']//preceding::div[1]")]
        public IWebElement txtMemberSignature;


        [FindsBy(How = How.XPath, Using = "//div[@class='col col-right']//p[text()='Name']//preceding::div[1]")]
        public IWebElement txtMemberName;


        [FindsBy(How = How.XPath, Using = "//div[@class='col col-right']//p[text()='Date']//preceding::div[1]")]
        public IWebElement txtMemberDate;


        [FindsBy(How = How.XPath, Using = "//button[text()='Decline']")]
        public IWebElement btnDecline;


        [FindsBy(How = How.XPath, Using = "//span[text()='AGREE']/parent::button")]
        public IWebElement btnAgree;
        #endregion

        public MemberCreationThirdPage(IWebDriver usedBrowser): base(usedBrowser, false)
        {
            driver = usedBrowser;
            PageFactory.InitElements(driver, this);
        }

        public void SetMembershipAgreement(TestUser user)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//span[text()='AGREE']")));//ToDo: need to wait 'btnAgree' here
            TestHelper.JSexecutorClick(btnAgree, driver);
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[text()=' Member Signature: ']//preceding::div[1]")));//ToDo: need to wait 'txtMemberSignature' here
            TestHelper.JSexecutorClick(txtMemberSignature, driver);
            TestHelper.JSexecutorClick(btnAgree, driver);
        }
    }
}
