using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OwnableCI_TestLib.Pages;

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


        [FindsBy(How = How.XPath, Using = "//span[text()='AGREE']")]
        public IWebElement btnAgree;
        #endregion

        public MemberCreationThirdPage(IWebDriver usedBrowser): base(usedBrowser, false)
        {
            driver = usedBrowser;
            PageFactory.InitElements(driver, this);
            
        }
    }
}
