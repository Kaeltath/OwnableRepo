using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OwnableCI_TestLib.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnableCI.Pages
{
    class MemberCreationPersonalInfoPage : HomePage
    {
        #region IWebelements 
        [FindsBy(How = How.XPath, Using = "//button[text()='Finish later']")]
        public IWebElement btnFinishLater;


        [FindsBy(How = How.XPath, Using = "//input[@id='firstName']")]
        public IWebElement inputFirstName;


        [FindsBy(How = How.XPath, Using = "//input[@id='lastName']")]
        public IWebElement inputLastName;


        [FindsBy(How = How.XPath, Using = "//input[@id='addressLine1']")]
        public IWebElement inputHomeAddress;


        [FindsBy(How = How.XPath, Using = "//input[@id='addressLine2']")]
        public IWebElement inputAddress2Optional;


        [FindsBy(How = How.XPath, Using = "//input[@id='city']")]
        public IWebElement inputCity;


        [FindsBy(How = How.XPath, Using = "//ng-select[@role='listbox']")]
        public IWebElement lstState;


        [FindsBy(How = How.XPath, Using = "//input[@id='zipCode']")]
        public IWebElement inputZipCode;


        [FindsBy(How = How.XPath, Using = "//input[@id='phoneNumber']")]
        public IWebElement inputMobile;


        [FindsBy(How = How.XPath, Using = "//input[@placeholder='mm/dd/yyyy']")]
        public IWebElement inputBirthdate;


        [FindsBy(How = How.XPath, Using = "//input[@id='last4Ssn']")]
        public IWebElement inputLast4SSN;


        [FindsBy(How = How.XPath, Using = "//label[@for='agreeCheckbox']")]
        public IWebElement chkAgreement;


        [FindsBy(How = How.XPath, Using = "//a[text()='ownable Terms of Use']")]
        public IWebElement lnkOwnableTermsOfUse;
        

        [FindsBy(How = How.XPath, Using = "//a[text()='Privacy Policy']")]
        public IWebElement lnkPrivacyPolicy;


        [FindsBy(How = How.XPath, Using = "//button[text()='Next']")]
        public IWebElement btnNext;
        #endregion

        public MemberCreationPersonalInfoPage(IWebDriver usedBrowser): base(usedBrowser, false)
        {
            driver = usedBrowser;
            PageFactory.InitElements(driver, this);
            
        }
    }
}
