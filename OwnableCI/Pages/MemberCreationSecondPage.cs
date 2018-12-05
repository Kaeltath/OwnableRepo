﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using OwnableCI_TestLib.Pages;
using OwnableCI.TestDataObjs;
using OwnableCI.ServiceClasses;
using System;

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

        [FindsBy(How = How.XPath, Using = "//button[text()='Become a member' or text()='Next']")] //adapted for Stage and Dev
        public IWebElement btnBecomeMember;
        #endregion

        public MemberCreationSecondPage(IWebDriver usedBrowser): base(usedBrowser, false)
        {
            driver = usedBrowser;
            PageFactory.InitElements(driver, this);
            
        }

        public void SetIncomeInfo(TestUser user)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//input[@placeholder='Monthly Income']")));//ToDo: need to wait 'inputMonthlyIncome' here
            inputMonthlyIncome.SendKeys(user.MonthlyIncome);
            inputCompany.SendKeys(user.Company);
            inputYearsEmployed.SendKeys(user.YearsEmployed);
            inputSSN.SendKeys(user.DigitsOFSocial); //first 5 SSN numbers are random
            TestHelper.JSexecutorClick(btnBecomeMember, driver);
        }
    }
}
