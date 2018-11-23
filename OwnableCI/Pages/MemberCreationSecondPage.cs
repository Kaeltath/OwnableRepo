﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OwnableCI_TestLib.Pages;
using OwnableCI.TestDataObjs;

namespace OwnableCI.Pages
{
    class MemberCreationSecondPage : HomePage   //"INCOME INFO" page
    {
        #region IWebelements 
        [FindsBy(How = How.XPath, Using = "//button[text()='Finish later']")]
        public IWebElement btnFinishLater;


        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Monthly Income']")]
        public IWebElement inputMonthlyIncome;


        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Company, Social Security, etc.']")]
        public IWebElement inputCompany;


        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Years employed']")]
        public IWebElement inputYearsEmployed;


        [FindsBy(How = How.XPath, Using = "//input[@placeholder='SSN']")]
        public IWebElement inputSSN;

        [FindsBy(How = How.XPath, Using = "//button[text()='Become a member']")]
        public IWebElement btnBecomeMember;
        #endregion

        public MemberCreationSecondPage(IWebDriver usedBrowser): base(usedBrowser, false)
        {
            driver = usedBrowser;
            PageFactory.InitElements(driver, this);
            
        }

        public void SetIncomeInfo(TestUser user)
        {
            inputMonthlyIncome.SendKeys(user.MonthlyIncome);
            inputCompany.SendKeys(user.Company);
            inputYearsEmployed.SendKeys(user.YearsEmployed);
            inputSSN.SendKeys("12345" + user.LastDigitsOFSocial); //first 5 SSN numbers are random
            btnBecomeMember.Click();
        }
    }
}
