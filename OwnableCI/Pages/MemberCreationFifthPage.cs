using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using OwnableCI_TestLib.Pages;
using System;

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

        public string GetRentalCapValue()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//span[starts-with(text(),'You can rent items worth ')]//span[starts-with(text(),'$')]")));//ToDo: need to wait 'txtRentalCapValue' here
            return txtRentalCapValue.Text;
        }
    }
}
