using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Diagnostics;
using System.Configuration;
using System.Threading;
using OwnableCI.TestDataObjs;

namespace OwnableCI_TestLib.Tests
{
    [TestFixture]
    public class BaseTest
    {
        internal log4net.ILog log = log4net.LogManager.GetLogger(typeof(BaseTest));
        internal IWebDriver driverForRun;
        

        protected void TestAction(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                StackTrace trace = new StackTrace();
                //var screenshot = ((ITakesScreenshot)driverForRun).GetScreenshot();
                //var filePath = String.Format(@"C:\Screenshots\{0}",trace.GetFrame(1).GetMethod().Name);

                //screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);

                log.ErrorFormat("Test: {0} Thrown Exception: {1}", trace.GetFrame(1).GetMethod().Name, ex.Message);

                throw;
            }
        }

        [OneTimeSetUp]
        public void CreateBrowser()
        {
            var appsettings = ConfigurationManager.AppSettings;
            driverForRun = new ChromeDriver(appsettings["ChromeDriverPath"]);
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            driverForRun.Close();
            driverForRun.Quit();
        }

        public void SmallSleep()
        {
            Thread.Sleep(2000);
        }

        public void MidSleep()
        {
            Thread.Sleep(3500);
        }

        public void BigSleep()
        {
            Thread.Sleep(5000);
        }

        public virtual bool ValidateGuest()
        {
            SmallSleep();
            driverForRun.FindElement(By.XPath("//button[text()=' Sign In ']"));
            return true;
        }

        public virtual bool ValidateUser(TestUser user)
        {
            MidSleep();        
            driverForRun.FindElement(By.XPath("//div[@class='modal-content']//button/div[text()=' START BROWSING ']")).Click();
            SmallSleep();
            var confirmElement = driverForRun.FindElement(By.XPath("//a[@id='navbarDropdownMenuLink']"));
            if (confirmElement.Text == String.Format("Hello, " + user.Email.ToLower()))
            { return true; }
            else
            { return false; }
        }

        public virtual bool ValidateMember(TestUser user)
        {
            var confirmElement = driverForRun.FindElement(By.XPath("//a[@id='navbarDropdownMenuLink']"));
            if (confirmElement.Text == String.Format("Hello, " + user.FirstName))
            { return true; }
            else
            { return false; }
        }

    }
}