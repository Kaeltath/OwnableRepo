

using OpenQA.Selenium;
using OwnableCI_TestLib.Enums;
using OwnableCI_TestLib.Pages;
using System.Threading;

namespace OwnableCI.Constants
{
    public class Product
    {
        private string m_productname;
        private IWebDriver m_driverForRun;

        public string ProductName
        {
            get { return m_productname; }
            set { m_productname = value; }
        }

        public IWebElement categoryControl;

        public Product(ProductCategories category, int productNumber, HomePage page, IWebDriver driverForRun)
        {
            m_driverForRun = driverForRun;
            switch (category)
            {
                case ProductCategories.Top_deals:
                    GetProductFromCategory(ProductCategories.Top_deals, productNumber, page);
                    break;
                case ProductCategories.TVs:
                    GetProductFromCategory(ProductCategories.TVs, productNumber, page);
                    break;
                case ProductCategories.Electronics:
                    GetProductFromCategory(ProductCategories.Electronics, productNumber, page);
                    break;
                case ProductCategories.Computers:
                    GetProductFromCategory(ProductCategories.Computers, productNumber, page);
                    break;
                case ProductCategories.Appliances:
                    GetProductFromCategory(ProductCategories.Appliances, productNumber, page);
                    break;
            }
                    


        }

        private void GetProductFromCategory(ProductCategories category, int productNumber, HomePage page)
        {
            categoryControl = m_driverForRun.FindElement(By.XPath(category.GetDescription()));
            if (categoryControl == null)
            {
                Thread.Sleep(2000);
                categoryControl.Click();
                Thread.Sleep(3000);
                ProductName = m_driverForRun.FindElement(By.XPath("//div[@class='row product-list']/div["+productNumber.ToString()+ "]//div[@class='description']/div")).Text;
            }
        }
    }
}
