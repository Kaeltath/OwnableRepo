using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using System.Reflection;

namespace OwnableCI_TestLib.Pages
{
    abstract class BasePage
    {
        public IWebDriver driver;
        public string errorMessageToLog;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            //Initializes all IWebElements for the page
            PageFactory.InitElements(driver, this);
            this.driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 30); ;
        }

        /// <summary>
        /// Navigates to the page
        /// </summary>
        /// <param name="parameter">Any parameter you want to tag on to the end of request</param>
        public abstract void NavigateToPage(string parameter = "");

        /// <summary>
        /// Waits for an element to be displayed for 1 minute
        /// </summary>
        /// <param name="element">Element to wait for</param>
        public void WaitForElementById(IWebElement element)
        {
            WebDriverWait _wait = new WebDriverWait(driver, new TimeSpan(0, 1, 0));
            _wait.Until(d => d.FindElement(By.Id(element.GetAttribute("id"))));
        }

        //
        public void InitPage(object Obj)
        {
            
            FieldInfo[] fieldInfo = Obj.GetType().GetFields(
                         BindingFlags.NonPublic |
                         BindingFlags.Instance);

            
            foreach (FieldInfo field in fieldInfo)
            {
                IWebElement elem = (IWebElement)field.GetValue(Obj);
                try
                {                    
                    if (!elem.Displayed)
                    {
                        errorMessageToLog = "Element " + field.Name + " is not loadede, or not visible, please check the problem"; 
                    }
                }
                catch (Exception e)
                {                    
                    throw e;
                }

            }
        }
    }
}



