using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using NUnit.Framework;
using System.Diagnostics;

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
                //StackTrace trace = new StackTrace();
                //var screenshot = ((ITakesScreenshot)driverForRun).GetScreenshot();
                //var filePath = String.Format(@"C:\Screenshots\{0}",trace.GetFrame(1).GetMethod().Name);

                //screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);

                log.ErrorFormat("Exception occured in Test: {0}", ex.Message);

                throw;
            }
        }

        [OneTimeSetUp]
        public void CreateBrowser()
        {
            driverForRun = new ChromeDriver("D:\\Downloads\\chromedriver_win32");
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            driverForRun.Close();
            driverForRun.Quit();
        }

    }
}