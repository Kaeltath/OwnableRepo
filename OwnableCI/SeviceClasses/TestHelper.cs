using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OwnableCI.ServiceClasses
{
    public static class TestHelper
    {
        public static void JSexecutorClick(IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click()", element);
        }

        //public static void SmallSleep()
        //{
        //    Thread.Sleep(2000);
        //}

        //public static void MidSleep()
        //{
        //    Thread.Sleep(3500);
        //}

        //public static void BigSleep()
        //{
        //    Thread.Sleep(5000);
        //}
    }
}
