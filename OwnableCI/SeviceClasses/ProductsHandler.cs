using NUnit.Framework;
using OpenQA.Selenium;
using OwnableCI.Enums;
using OwnableCI.ServiceClasses;
using OwnableCI_TestLib.Enums;
using OwnableCI_TestLib.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace OwnableCI.Constants
{
    #region ProductDeclaration

    public class Product
    {

        static log4net.ILog log = log4net.LogManager.GetLogger(typeof(Product));
        private IWebDriver m_driverForRun;

        public string ProductName { get; set; }
        public float ProductRate { get; set; }
        public float ProductPrice { get; set; }
        public int VotesCount { get; set; }
        public string Brand { get; set; }

        public IWebElement categoryControl;

        public Product(ProductCategories category, int productNumber, IWebDriver driverForRun)
        {
            m_driverForRun = driverForRun;
            GetProductFromCategory(category, productNumber);
        }

        public Product(ProductContainer container, int productNumber, HomePage home, IWebDriver driverForRun)
        {
            m_driverForRun = driverForRun;
            GetProductFromContainer(container, productNumber, home);
        }

        private void GetProductFromContainer(ProductContainer container, int productNumber, HomePage home)
        {
            switch (container)
            {
                case ProductContainer.Cart:
                    throw new NotImplementedException("Currently not required to get proucts from cart");
                case ProductContainer.WishList:
                    home.OpenWishlist();
                    Thread.Sleep(2000);
                    CaptureProductProperties(productNumber);
                    break;
            }
        }

        private void GetProductFromCategory(ProductCategories category, int productNumber)
        {
            if (category == ProductCategories.Top_deals)
            {
                var blackFridayControl = m_driverForRun.FindElement(By.XPath("//div[@id='v-pills-tab']//button[text()=' Black Friday ']"));
                blackFridayControl.Click();
                Thread.Sleep(2000);
                categoryControl = m_driverForRun.FindElement(By.XPath("//div[@class='sub-menu opened']//div[@class='column ng-star-inserted']//li[1]/a"));
                categoryControl.Click();
                Thread.Sleep(3000);
            }
            else {
                categoryControl = m_driverForRun.FindElement(By.XPath(category.GetDescription()));
                categoryControl.Click();
                Thread.Sleep(3000);

            }
            //if (categoryControl != null)
            //{
                //Thread.Sleep(2000);
                //categoryControl.Click();
                //Thread.Sleep(3000);
                CaptureProductProperties(productNumber);
            //}
        }

        private void CaptureProductProperties(int productNumber)
        {
            ProductName = m_driverForRun.FindElement(By.XPath("//div[@class='row product-list']/div[" + productNumber.ToString() + "]//div[@class='description']/div")).Text;
            Thread.Sleep(2000);
            TestHelper.JSexecutorClick(m_driverForRun.FindElement(By.XPath("//div[@class='product-card-container']//div[@class='description']//div[text()='" + ProductName + "']//parent::div")), m_driverForRun);
            Thread.Sleep(2500);
            string rate;
            try
            {
                rate = m_driverForRun.FindElement(By.XPath("//div[@class='product-top-bar']//span[@class='count ng-star-inserted']")).Text;
            }
            catch
            {
                rate = string.Empty;
            }
            if (!String.IsNullOrEmpty(rate))
            {
                string getRate = Regex.Match(rate, @"^[0-9]*\.?[0-9]+").Groups[0].Value;
                ProductRate = float.Parse(getRate);
                VotesCount = Int32.Parse(Regex.Match(rate, @"(?<=\().*(?=\))").Groups[0].Value);
            }
            else
            {
                ProductRate = 0;
                VotesCount = 0;
            }
            string getPrice = Regex.Match(m_driverForRun.FindElement(By.XPath("//div[@class='info-box']//span")).Text,
                @"[-+]?[0-9]*\.?[0-9]+").Groups[0].Value;
            ProductPrice = float.Parse(getPrice);
            Brand = Regex.Match(ProductName, @"^(.*?)\s").Groups[0].Value.TrimEnd();
            log.InfoFormat("We have product name {0}", ProductName);
        }
    }

    #endregion

    #region Product Comparer

    public class ProductComparer : IEqualityComparer<Product>
    {

        public bool Equals(Product x, Product y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
            return x != null && y != null && x.ProductName.Equals(y.ProductName);
        }

        public int GetHashCode(Product obj)
        {
            int hashProductName = obj.ProductName == null ? 0 : obj.ProductName.GetHashCode();
            int hashProductPrice = obj.ProductPrice.GetHashCode();
            int hashProductRate = obj.ProductRate.GetHashCode();
            return hashProductName ^ hashProductPrice ^ hashProductRate;
        }
    }

    #endregion

    public class ProductHandler
    {

        #region Handler properties

        public List<Product> InitialCollection;

        private HomePage m_home;
        private IWebDriver m_driver;
        private IWebElement confirmElement;
        private ReadOnlyCollection<IWebElement> confirmElements;

        #endregion

        public ProductHandler(IWebDriver DriverForRun, HomePage Page)
        {
            m_driver = DriverForRun;
            m_home = Page;
        }

        /// <summary>
        /// Load a bulk of Products to handler public collection
        /// </summary>
        /// <param name="ProdutIndexes"></param>
        /// <param name="Category"></param>
        public void BuildProductCollection(int[] ProdutIndexes, ProductCategories Category)
        {
            InitialCollection = new List<Product>();
            for (int i = 0; i < ProdutIndexes.Length; i++)
            {
                Product prod = new Product(Category, ProdutIndexes[i], m_driver);
                InitialCollection.Add(prod);
            }
        }

        public List<Product> GetCurrentProductsList(ProductContainer conteiner, SortingMethods sortingMethod = SortingMethods.Rating, bool ascending = false)
        {
            List<Product> currentProductsList = new List<Product>();
            switch (conteiner)
            {

                case ProductContainer.Cart:
                    throw new NotImplementedException("Currently not required");
                case ProductContainer.WishList:
                    //to do: capture names to another list, or smth like this
                    int count = CountProductsInContainer(ProductContainer.WishList);
                    for (int i = 1; i <= count; i++)
                    {
                        m_home.OpenWishlist();
                        SmallSleep();
                        Sort(sortingMethod, ascending);
                        SmallSleep();
                        currentProductsList.Add(new Product(ProductContainer.WishList, i, m_home, m_driver));
                    }
                    break;
            }

            return currentProductsList;
        }

        public void ValidateProductSorting(SortingMethods sortingMethod, bool ascending = false)
        {
            switch (sortingMethod)
            {
                case SortingMethods.Brand:
                    var extractedList = GetCurrentProductsList(ProductContainer.WishList, sortingMethod, ascending);
                    var expectedList = ascending ? extractedList.OrderBy(o => o.Brand, StringComparer.Ordinal) : extractedList.OrderByDescending(o => o.Brand, StringComparer.Ordinal);
                    Assert.That(extractedList.SequenceEqual(expectedList));
                    break;
                case SortingMethods.Category:
                    throw new NotImplementedException("This sorting type validation is not implemented yet");
                case SortingMethods.Newest:
                    throw new NotImplementedException("This sorting type validation is not implemented yet");
                case SortingMethods.Price:
                    extractedList = GetCurrentProductsList(ProductContainer.WishList, sortingMethod, ascending);
                    expectedList = ascending ? extractedList.OrderBy(o => o.ProductPrice) : extractedList.OrderByDescending(o => o.ProductPrice);
                    Assert.That(extractedList.SequenceEqual(expectedList));
                    break;
                case SortingMethods.Rating:
                    extractedList = GetCurrentProductsList(ProductContainer.WishList, sortingMethod, ascending);
                    expectedList = ascending ? extractedList.OrderBy(o => o.VotesCount) : extractedList.OrderByDescending(o => o.VotesCount);
                    Assert.That(extractedList.SequenceEqual(expectedList));
                    break;
                default: break;
            }

        }

        /// <summary>
        /// Sorting selected location, using selected method.
        /// </summary>
        /// <param name="sortLocation">Can take ProductContainer or ProductCategory</param>
        /// <param name="sortingMethod"></param>
        /// <param name="ascending">Optional parameter, default value = false - sorting is descending</param>
        public void SortProducts(Enum sortLocation, SortingMethods sortingMethod, bool ascending = false)
        {
            if (sortLocation is ProductContainer)
            {
                switch ((ProductContainer)sortLocation)
                {
                    case ProductContainer.Cart:
                        throw new NotSupportedException("Currently sorting is not supported in Cart");
                    case ProductContainer.WishList:
                        m_home.OpenWishlist();
                        SmallSleep();
                        Sort(sortingMethod, ascending);
                        break;
                    default: return;
                }
            }
            else if (sortLocation is ProductCategories)
            {
                throw new NotSupportedException("Currently sorting in categories is not supported");
            }
            else return;

        }

        private void Sort(SortingMethods sortingMethod, bool ascending = false)
        {
            var element = m_driver.FindElement(By.XPath("//div[@class='row justify-content-end products-sort']//span[@class='ng-arrow-wrapper']"));
            element.Click();
            SmallSleep();
            var options = m_driver.FindElements(By.XPath("//div[@class='row']//div[@class='row justify-content-end products-sort']//div[@role='option']"));
            options[(Int32)sortingMethod].Click();
            MidSleep();
            var ascDscButton = m_driver.FindElement(By.XPath("//div[@class='row justify-content-end products-sort']//button[@class='btn btn-outline-secondary']/i"));
            string indicator = ascDscButton.GetAttribute("class");
            if (indicator.Contains("-desc"))
            {
                if (ascending)
                    ascDscButton.Click();

            }
            else
            {
                if (!ascending)
                    ascDscButton.Click();
            }
        }

        public int CountProductsInContainer(ProductContainer container)
        {
            switch (container)
            {
                case ProductContainer.Cart:
                    m_home.OpenCart();
                    MidSleep();
                    confirmElements = m_driver.FindElements(By.XPath("//div[@class='empty-cart ng-star-inserted']"));
                    if (confirmElements.Count != 0)
                        return 0;
                    confirmElements = m_driver.FindElements(By.XPath("//div[@class='cart-holder ng-star-inserted']//div[@class='cart-item ng-star-inserted']"));
                    return confirmElements.Count;
                case ProductContainer.WishList:
                    m_home.OpenWishlist();
                    MidSleep();
                    confirmElements = m_driver.FindElements(By.XPath("//div[@class='no-wishlist-box ng-star-inserted']"));
                    if (confirmElements.Count != 0)
                        return 0;
                    confirmElements = m_driver.FindElements(By.XPath("//div[@class='ng-star-inserted']//div[@class='row product-list']/div"));
                    return confirmElements.Count;
                default: return 0;
            }
        }

        public int CountProductsInContainer(ProductContainer container, Product product)
        {
            int count;
            switch (container)
            {
                case ProductContainer.Cart:
                    m_home.OpenCart();
                    MidSleep();
                    confirmElements = m_driver.FindElements(By.XPath("//div[@class='empty-cart ng-star-inserted']"));
                    if (confirmElements.Count != 0)
                    { return 0; }
                    confirmElements = m_driver.FindElements(By.XPath("//a[text() = '" + product.ProductName + "']//parent::div"));
                    count = (confirmElements.Count == 0) ? 0 : 1;
                    if (count == 1)
                    {
                        count = Int32.Parse(m_driver.FindElement(By.XPath("//div[@class='cart-holder ng-star-inserted']//div[@class='cart-item ng-star-inserted']//span[@class='ng-value-label ng-star-inserted']")).Text);
                        return count;
                    }
                    return count;
                case ProductContainer.WishList:
                    m_home.OpenWishlist();
                    MidSleep();
                    confirmElements = m_driver.FindElements(By.XPath("//div[@class='no-wishlist-box ng-star-inserted']"));
                    if (confirmElements.Count != 0)
                        return 0;
                    confirmElements = m_driver.FindElements(By.XPath("//div[@class='row product-list']/div//div[@class='description']/div[text()='" + product.ProductName + "']"));
                    SmallSleep();
                    count = (confirmElements.Count == 0) ? 0 : 1;
                    return count;
                default: return 0;
            }
        }

        public int GetContainerCounter(ProductContainer container)
        {
            int count;
            switch (container)
            {
                case ProductContainer.Cart:
                    confirmElements = m_driver.FindElements(By.XPath("//div[@class='icons']//a/span"));
                    if (confirmElements.Count == 0)
                        return 0;
                    MidSleep();
                    count = Int32.Parse(m_driver.FindElement(By.XPath("//div[@class='icons']//a/span")).Text);
                    return count;
                case ProductContainer.WishList:
                    confirmElements = m_driver.FindElements(By.XPath("//div[@class='icons']//button/span"));
                    if (confirmElements.Count == 0)
                        return 0;
                    MidSleep();
                    count = Int32.Parse(m_driver.FindElement(By.XPath("//div[@class='icons']//button/span")).Text);
                    return count;
                default: return 0;
            }
        }

        public void AddProdutRangeToContainer(int[] productIds, ProductContainer container, ProductCategories category)
        {
            switch (container)
            {
                case ProductContainer.Cart:
                    for (int i = 0; i < productIds.Length; i++)
                    {
                        AddProductToContainer(ProductContainer.Cart, InterctionControlSet.Product_Details, new Product(category, productIds[i], m_driver));
                    }
                    break;
                case ProductContainer.WishList:
                    for (int i = 0; i < productIds.Length; i++)
                    {
                        AddProductToContainer(ProductContainer.WishList, InterctionControlSet.Product_Details, new Product(category, productIds[i], m_driver));
                    }
                    break;
            }

        }

        public void AddProductToContainer(ProductContainer container, InterctionControlSet controlSet, Product product)
        {

            switch (container)
            {
                case ProductContainer.Cart:
                    switch (controlSet)
                    {
                        case InterctionControlSet.Product_Title:
                            throw new NotSupportedException();
                        case InterctionControlSet.From_container:
                            m_home.OpenCart();
                            MidSleep();
                            int count = CountProductsInContainer(ProductContainer.Cart, product);
                            if (count > 0)
                            {
                                m_driver.FindElement(By.XPath("//div[@class='cart-item ng-star-inserted']//span[@class='ng-arrow-wrapper'"));
                                SmallSleep();
                                TestHelper.JSexecutorClick(m_driver.FindElement(By.XPath("//*[@placeholder='Quantity']/ng-dropdown-panel//div[@role='option']/span[text()='" + (count++) + "']")), m_driver);
                                MidSleep();
                                break;
                            }
                            else
                            {
                                throw new NotSupportedException("Adding new product from cart is not supported");
                            }
                        case InterctionControlSet.Product_Details:
                            var blackFridayControl = m_driver.FindElement(By.XPath("//div[@id='v-pills-tab']//button[text()=' Black Friday ']"));
                            blackFridayControl.Click();
                            Thread.Sleep(2000);
                            var categoryControl = m_driver.FindElement(By.XPath("//div[@class='sub-menu opened']//div[@class='column ng-star-inserted']//li[1]/a"));
                            categoryControl.Click();
                            //product.categoryControl.Click();
                            MidSleep();
                            m_driver.FindElement(By.XPath("//div[@class='product-card-container']//div[@class='description']//div[text()='" + product.ProductName + "']//parent::div")).Click();
                            SmallSleep();
                            var element = m_driver.FindElement(By.XPath("//button[text()='add to cart']"));
                            TestHelper.JSexecutorClick(element, m_driver);
                            SmallSleep();
                            m_driver.FindElement(By.XPath("//div[@class='modal-content']//button[text()=' view cart ']")).Click();
                            break;
                        case InterctionControlSet.Container_Switch:
                            m_home.OpenWishlist();
                            MidSleep();
                            m_driver.FindElement(By.XPath("//div[@class='description']//div[text()='" + product.ProductName + "']/ancestor::div[@class='product-card-container']//button/span[text()='Add to cart']")).Click();
                            MidSleep();
                            break;
                        default: break;
                    }
                    break;
                case ProductContainer.WishList:
                    SmallSleep();
                    switch (controlSet)
                    {
                        case InterctionControlSet.Product_Title:
                            //product.categoryControl.Click();
                            var blackFridayControl = m_driver.FindElement(By.XPath("//div[@id='v-pills-tab']//button[text()=' Black Friday ']"));
                            blackFridayControl.Click();
                            Thread.Sleep(2000);
                            var categoryControl = m_driver.FindElement(By.XPath("//div[@class='sub-menu opened']//div[@class='column ng-star-inserted']//li[1]/a"));
                            categoryControl.Click();
                            MidSleep();
                            TestHelper.JSexecutorClick(m_driver.FindElement(By.XPath("//div[@class='description']//div[text()='" + product.ProductName + "']/ancestor::div[@class='product-card-container']//button[text()='Add to Wishlist']")), m_driver);
                            break;
                        case InterctionControlSet.From_container:
                            throw new NotSupportedException("Adding product from wishlist to wishlist is not supported");
                        case InterctionControlSet.Product_Details:
                            //product.categoryControl.Click();
                            blackFridayControl = m_driver.FindElement(By.XPath("//div[@id='v-pills-tab']//button[text()=' Black Friday ']"));
                            blackFridayControl.Click();
                            Thread.Sleep(2000);
                            categoryControl = m_driver.FindElement(By.XPath("//div[@class='sub-menu opened']//div[@class='column ng-star-inserted']//li[1]/a"));
                            categoryControl.Click();
                            MidSleep();
                            var element = m_driver.FindElement(By.XPath("//div[@class='row product-list']//div[@class='product-card-container']//div[@class='description']/div[text()='" + product.ProductName + "']"));
                            TestHelper.JSexecutorClick(element, m_driver);
                            SmallSleep();
                            m_driver.FindElement(By.XPath("//div[@class='wishlist-placeholder ng-star-inserted']//button/span[text()='Add to wishlist']")).Click();
                            break;
                        case InterctionControlSet.Container_Switch:
                            m_home.OpenCart();
                            MidSleep();
                            m_driver.FindElement(By.XPath("//div[@class='cart-item ng-star-inserted']//button[text()='Move to wishlist']")).Click();
                            SmallSleep();
                            break;
                        default: break;
                    }
                    break;
                default: break;
            }
        }

        public void RemoveProductFromContainer(ProductContainer container, InterctionControlSet controlSet, Product product)
        {
            switch (container)
            {
                case ProductContainer.Cart:
                    switch (controlSet)
                    {
                        case InterctionControlSet.Product_Title:
                            throw new NotSupportedException();
                        case InterctionControlSet.Product_Details:
                            throw new NotSupportedException();
                        case InterctionControlSet.From_container:
                            m_home.OpenCart();
                            MidSleep();
                            m_driver.FindElement(By.XPath("//div[@class='cart-item-container row']//button[text()='Remove']")).Click();
                            break;
                        case InterctionControlSet.Container_Switch:
                            throw new NotSupportedException("Product need to be removed, not moved to other ontainer");
                        default: break;
                    }
                    break;
                case ProductContainer.WishList:
                    switch (controlSet)
                    {
                        case InterctionControlSet.Product_Title:
                            //product.categoryControl.Click();
                            var blackFridayControl = m_driver.FindElement(By.XPath("//div[@id='v-pills-tab']//button[text()=' Black Friday ']"));
                            blackFridayControl.Click();
                            Thread.Sleep(2000);
                            var categoryControl = m_driver.FindElement(By.XPath("//div[@class='sub-menu opened']//div[@class='column ng-star-inserted']//li[1]/a"));
                            categoryControl.Click();
                            MidSleep();
                            m_driver.FindElement(By.XPath("//div[@class='description']//div[text()='" + product.ProductName + "']/ancestor::div[@class='product-card-container']//button[text()='Remove from Wishlist']")).Click();
                            break;
                        case InterctionControlSet.Product_Details:
                            //product.categoryControl.Click();
                            blackFridayControl = m_driver.FindElement(By.XPath("//div[@id='v-pills-tab']//button[text()=' Black Friday ']"));
                            blackFridayControl.Click();
                            Thread.Sleep(2000);
                            categoryControl = m_driver.FindElement(By.XPath("//div[@class='sub-menu opened']//div[@class='column ng-star-inserted']//li[1]/a"));
                            categoryControl.Click();
                            MidSleep();
                            m_driver.FindElement(By.XPath("//div[@class='row product-list']//div[@class='product-card-container']//div[@class='description']/div[text()='" + product.ProductName + "']")).Click();
                            SmallSleep();
                            m_driver.FindElement(By.XPath("//div[@class='wishlist-placeholder ng-star-inserted']//button/span[text()='Remove from wishlist']")).Click();
                            break;
                        case InterctionControlSet.Container_Switch:
                            throw new NotSupportedException("Product need to be removed, not moved to other ontainer");
                        case InterctionControlSet.From_container:
                            m_home.OpenWishlist();
                            MidSleep();
                            TestHelper.JSexecutorClick(m_driver.FindElement(By.XPath("//div[@class='product-card-container']//div[@class='description']//div[text()='" + product.ProductName + "']/ancestor::div[@class='product-card-container']//button[text()='Remove from Wishlist']")), m_driver);
                            break;
                        default: break;
                    }
                    break;
                default: break;
            }
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
    }
}
