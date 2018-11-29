using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using System.Diagnostics;
using System.Configuration;
using System.Threading;
using OwnableCI.TestDataObjs;
using System.IO;
using System.Reflection;
using OwnableCI_TestLib.Enums;

namespace OwnableCI_TestLib.Tests
{
    [TestFixture]
    public class BaseTest
    {
        internal log4net.ILog log = log4net.LogManager.GetLogger(typeof(BaseTest));
        internal IWebDriver driverForRun;
        internal BrowserType currentBrowser;

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
            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            if (!config.HasFile)
            { throw new FileNotFoundException("Missing configuration file for test dll"); }
            var DriverPathSetting = config.AppSettings.Settings["DriversPath"].Value;
            switch (currentBrowser)
            {
                case BrowserType.Chrome:
                    driverForRun = new ChromeDriver(DriverPathSetting);
                    break;
                case BrowserType.FireFox:
                    driverForRun = new FirefoxDriver(DriverPathSetting);
                    break;
                default:
                    driverForRun = new ChromeDriver(DriverPathSetting);
                    break;
            }
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            driverForRun.Manage().Cookies.DeleteAllCookies();
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

        public virtual bool ValidateUser(TestUser user)
        {
            SmallSleep();        
            driverForRun.FindElement(By.XPath("//div[@class='modal-content']//div[@class='modal-footer ng-star-inserted']//button/div[text()=' START BROWSING ']")).Click();
            SmallSleep();
            var confirmElement = driverForRun.FindElement(By.XPath("//a[@id='navbarDropdownMenuLink']"));
            if (confirmElement.Text.Trim() == String.Format("HELLO, " + user.Email.ToUpper()))
            { return true; }
            else
            { return false; }
        }

        public virtual bool ValidateMember(TestUser user)
        {
            var confirmElement = driverForRun.FindElement(By.XPath("//a[@id='navbarDropdownMenuLink']"));
            if (confirmElement.Text.Trim() == String.Format("HELLO, " + user.FirstName.ToUpper()))
            { return true; }
            else
            { return false; }
        }

        public string RentalCapExpected(TestUser user)
        {
            int incoming = int.Parse(user.MonthlyIncome);
            string rentExpected;
            if (1200 <= incoming)
            {
                if (incoming < 2000)
                {
                    rentExpected = "$500.00";
                }
                else
                {
                    if (incoming <= 4000)
                    {
                        rentExpected = "$750.00";
                    }
                    else
                    {
                        rentExpected = "$1000.00";
                    }
                }
            }
            else
            {
                rentExpected = "Low Income";
            }
            return rentExpected;
        }

    }
}