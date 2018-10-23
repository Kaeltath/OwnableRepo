using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using System.Reflection;
using OwnableCI.TestDataObjs;

namespace OwnableCI_TestLib.Pages
{
    abstract class BasePage
    {
        protected IWebDriver driver;
        protected string errorMessageToLog;
        private log4net.ILog logger;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            logger = log4net.LogManager.GetLogger(typeof(BasePage));
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
        /// Login specific user
        /// </summary>
        /// <returns>bool - login succeeded</returns>
        public abstract bool Login(TestUser user);

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
                         BindingFlags.Public |
                         BindingFlags.Instance);

            
            foreach (FieldInfo field in fieldInfo)
            {
                IWebElement elem;
                try { elem = (IWebElement)field.GetValue(Obj); }
                catch (Exception) { continue; }
                try
                {                    
                    if (!elem.Displayed)
                    {
                        errorMessageToLog = "Element " + field.Name + " is not loadede, or not visible, please check the problem"; 
                    }
                }
                catch (Exception e)
                {
                    logger.ErrorFormat("Field {0}, thrown an exception {1}", field.Name, e.Message);              
                    throw e;
                }

            }
        }
    }
}



