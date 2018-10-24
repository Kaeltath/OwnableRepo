using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Diagnostics;
using System.Configuration;

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

    }
}