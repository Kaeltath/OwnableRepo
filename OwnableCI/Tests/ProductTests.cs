using NUnit.Framework;
using OpenQA.Selenium;
using OwnableCI.Constants;
using OwnableCI.Enums;
using OwnableCI.Pages;
using OwnableCI.TestDataObjs;
using OwnableCI_TestLib.Constants;
using OwnableCI_TestLib.Enums;
using OwnableCI_TestLib.Pages;
using OwnableCI_TestLib.Tests;
using System;
using System.Collections.ObjectModel;

namespace OwnableCI.Tests
{
    [TestFixtureSource(typeof(TestProperties), "users")]
    [TestFixture]
    class ProductTests : BaseTest
    {
        TestUser user;
        IWebElement confirmElement;
        ReadOnlyCollection<IWebElement> confirmElements;

        public ProductTests(TestUser user)
        {
            this.user = user;
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(1)]
        public void AddingProductToCartUser()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName + user.LastName + ";");
            HomePage home = new HomePage(driverForRun, false);
            SignInPage signin = new SignInPage(driverForRun);
            Assume.That(signin.Login(user), "Failed login, test will not run");
            Assume.That(ValidateUser(user), "Logged-in account is not a user");
            SmallSleep();
            Product product = new Product(ProductCategories.Top_deals, 1, home, driverForRun);
            SmallSleep();
            int counter = GetContainerCounter(ProductContainer.Cart);
            int prodctsInCart = CountProductsInContainer(ProductContainer.Cart, product, home);
            Assert.That(prodctsInCart < 1, "Selected product already in cart");
            MidSleep();
            AddProductToContainer(ProductContainer.Cart, InterctionControlSet.Product_Details, home, product);
            MidSleep();
            Assert.That((prodctsInCart + 1) == CountProductsInContainer(ProductContainer.Cart, product, home), "Product wasn't added properly");
            Assert.That((counter + 1) == GetContainerCounter(ProductContainer.Cart), "Conter wasn't updated properly"); 
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(2)]
        public void AddingExistingProductToCartUser()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName + user.LastName + ";");
            HomePage home = new HomePage(driverForRun, false);
            SmallSleep();
            Product product = new Product(ProductCategories.Top_deals, 1, home, driverForRun);
            SmallSleep();
            int counter = GetContainerCounter(ProductContainer.Cart);
            int prodctsInCart = CountProductsInContainer(ProductContainer.Cart, product, home);
            Assume.That(prodctsInCart >= 1, "No such product in cart, test will not proceed");
            MidSleep();
            AddProductToContainer(ProductContainer.Cart, InterctionControlSet.Product_Details, home, product);
            MidSleep();
            Assert.That((prodctsInCart + 1) == CountProductsInContainer(ProductContainer.Cart, product, home), "Product wasn't added properly");
            Assert.That((counter + 1) == GetContainerCounter(ProductContainer.Cart), "Conter wasn't updated properly");
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(3)]
        public void RemovingProductFromCartUser()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName + user.LastName + ";");
            HomePage home = new HomePage(driverForRun, false);
            SmallSleep();
            Product product = new Product(ProductCategories.Top_deals, 1, home, driverForRun);
            SmallSleep();
            int counter = GetContainerCounter(ProductContainer.Cart);
            int prodctsInCart = CountProductsInContainer(ProductContainer.Cart, product, home);
            Assume.That(prodctsInCart >= 1, "No such product in cart, test will not proceed");
            MidSleep();
            RemoveProductFromContainer(ProductContainer.Cart, InterctionControlSet.From_container, home, product);
            Assert.That(CountProductsInContainer(ProductContainer.Cart, product, home) == 0 , "Product wasn't removed properly");
            Assert.That((counter - prodctsInCart) == GetContainerCounter(ProductContainer.Cart), "Conter wasn't updated properly");
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(4)]
        public void AddingProductToCartFromWishListUser()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName + user.LastName + ";");
            HomePage home = new HomePage(driverForRun, false);
            SmallSleep();
            Product product = new Product(ProductCategories.Top_deals, 2, home, driverForRun);
            SmallSleep();
            int counter = GetContainerCounter(ProductContainer.Cart);
            int prodctsInCart = CountProductsInContainer(ProductContainer.Cart, product, home);
            Assume.That(prodctsInCart <= 1, "Product already in cart, test will not proceed");
            MidSleep();
            AddProductToContainer(ProductContainer.WishList, InterctionControlSet.Product_Title, home, product);
            Assert.That(CountProductsInContainer(ProductContainer.WishList, product, home) == 1, "Product did not got to WishList");
            MidSleep();
            AddProductToContainer(ProductContainer.Cart, InterctionControlSet.Container_Switch, home, product);
            MidSleep();
            RemoveProductFromContainer(ProductContainer.WishList, InterctionControlSet.From_container, home, product);
            Assert.That((prodctsInCart + 1) == CountProductsInContainer(ProductContainer.Cart, product, home), "Product wasn't added properly");
            Assert.That((counter + 1) == GetContainerCounter(ProductContainer.Cart), "Conter wasn't updated properly");
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(5)]
        public void MovingProductFromCartToWishListUser()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName + user.LastName + ";");
            HomePage home = new HomePage(driverForRun, false);
            SmallSleep();
            Product product = new Product(ProductCategories.Top_deals, 2, home, driverForRun);
            SmallSleep();
            int counterCart = GetContainerCounter(ProductContainer.Cart);
            int prodctsInCart = CountProductsInContainer(ProductContainer.Cart, product, home);
            int counterWishList = GetContainerCounter(ProductContainer.WishList);
            int prodctsInWishList = CountProductsInContainer(ProductContainer.WishList, product, home);
            Assume.That(prodctsInCart >= 1, "Product not in cart, test will not proceed");
            Assume.That(prodctsInWishList < 1, "Product already in wishist, test will not proceed");
            MidSleep();
            AddProductToContainer(ProductContainer.WishList, InterctionControlSet.Container_Switch, home, product);
            MidSleep();
            Assert.That(CountProductsInContainer(ProductContainer.WishList, product, home) == 1, "Product did not got to WishList");
            Assert.That(CountProductsInContainer(ProductContainer.Cart, product, home) == 0, "Product wasn't removed from cart properly");
            Assert.That((counterCart - 1) == GetContainerCounter(ProductContainer.Cart), "Cart Conter wasn't updated properly");
            Assert.That((counterWishList + 1) == GetContainerCounter(ProductContainer.WishList), "WishList Conter wasn't updated properly");
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(6)]
        public void AddingProductToWishListFromProductTitleTest()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName + user.LastName + ";");
            HomePage home = new HomePage(driverForRun, false);
            SmallSleep();
            Product product = new Product(ProductCategories.Top_deals, 3, home, driverForRun);
            SmallSleep();
            Assert.That(CountProductsInContainer(ProductContainer.WishList, product, home) == 0, "Product alreadty in wishlist");
            int counter = GetContainerCounter(ProductContainer.WishList);
            MidSleep();
            AddProductToContainer(ProductContainer.WishList, InterctionControlSet.Product_Title, home, product);
            Assert.That(CountProductsInContainer(ProductContainer.WishList, product, home) == 1, "Product wasn't added properly to wishlist");
            Assert.That((counter + 1) == GetContainerCounter(ProductContainer.WishList), "Counter wasn't updated properly");
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(7)]
        public void RemovingProductFromWishListFromProductTitleTest()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName + user.LastName + ";");
            HomePage home = new HomePage(driverForRun, false);
            SmallSleep();
            Product product = new Product(ProductCategories.Top_deals, 3, home, driverForRun);
            SmallSleep();
            Assert.That(CountProductsInContainer(ProductContainer.WishList, product, home) == 1, "Product not in wishlist");
            int counter = GetContainerCounter(ProductContainer.WishList);
            MidSleep();
            RemoveProductFromContainer(ProductContainer.WishList, InterctionControlSet.Product_Title, home, product);
            Assert.That(CountProductsInContainer(ProductContainer.WishList, product, home) == 0, "Product wasn't removed properly to wishlist");
            Assert.That((counter - 1) == GetContainerCounter(ProductContainer.WishList), "Counter wasn't updated properly");
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(8)]
        public void AddingProductToWishListFromProductDetailsTest()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName + user.LastName + ";");
            HomePage home = new HomePage(driverForRun, false);
            SmallSleep();
            Product product = new Product(ProductCategories.Top_deals, 4, home, driverForRun);
            SmallSleep();
            Assert.That(CountProductsInContainer(ProductContainer.WishList, product, home) == 0, "Product alreadty in wishlist");
            int counter = GetContainerCounter(ProductContainer.WishList);
            MidSleep();
            AddProductToContainer(ProductContainer.WishList, InterctionControlSet.Product_Details, home, product);
            Assert.That(CountProductsInContainer(ProductContainer.WishList, product, home) == 1, "Product wasn't added properly to wishlist");
            Assert.That((counter + 1) == GetContainerCounter(ProductContainer.WishList), "Counter wasn't updated properly");
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(9)]
        public void RemovingProductFromWishListFromProductDetailsTest()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName + user.LastName + ";");
            HomePage home = new HomePage(driverForRun, false);
            SmallSleep();
            Product product = new Product(ProductCategories.Top_deals, 4, home, driverForRun);
            SmallSleep();
            Assert.That(CountProductsInContainer(ProductContainer.WishList, product, home) == 1, "Product not in wishlist");
            int counter = GetContainerCounter(ProductContainer.WishList);
            MidSleep();
            RemoveProductFromContainer(ProductContainer.WishList, InterctionControlSet.Product_Details, home, product);
            Assert.That(CountProductsInContainer(ProductContainer.WishList, product, home) == 0, "Product wasn't removed properly to wishlist");
            Assert.That((counter - 1) == GetContainerCounter(ProductContainer.WishList), "Counter wasn't updated properly");
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(10)]
        public void AddingProductToWishListFromCartTest()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName + user.LastName + ";");
            HomePage home = new HomePage(driverForRun, false);
            SmallSleep();
            Product product = new Product(ProductCategories.Top_deals, 5, home, driverForRun);
            SmallSleep();
            AddProductToContainer(ProductContainer.Cart, InterctionControlSet.Product_Details, home, product);
            Assert.That(CountProductsInContainer(ProductContainer.WishList, product, home) == 0, "Product alreadty in wishlist");
            int counter = GetContainerCounter(ProductContainer.WishList);
            MidSleep();
            AddProductToContainer(ProductContainer.WishList, InterctionControlSet.Container_Switch, home, product);
            Assert.That(CountProductsInContainer(ProductContainer.WishList, product, home) == 1, "Product wasn't added properly to wishlist");
            Assert.That((counter + 1) == GetContainerCounter(ProductContainer.WishList), "Counter wasn't updated properly");
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(11)]
        public void RemovingProductFromWishListContainerTest()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName + user.LastName + ";");
            HomePage home = new HomePage(driverForRun, false);
            SmallSleep();
            Product product = new Product(ProductCategories.Top_deals, 5, home, driverForRun);
            SmallSleep();
            Assert.That(CountProductsInContainer(ProductContainer.WishList, product, home) == 1, "Product not in wishlist");
            int counter = GetContainerCounter(ProductContainer.WishList);
            MidSleep();
            RemoveProductFromContainer(ProductContainer.WishList, InterctionControlSet.From_container, home, product);
            Assert.That(CountProductsInContainer(ProductContainer.WishList, product, home) == 0, "Product wasn't removed properly to wishlist");
            Assert.That((counter - 1) == GetContainerCounter(ProductContainer.WishList), "Counter wasn't updated properly");
        }

        private int CountProductsInContainer(ProductContainer container, HomePage page)
        {
            switch (container)
            {
                case ProductContainer.Cart:
                    page.OpenCart();
                    MidSleep();
                    confirmElements = driverForRun.FindElements(By.XPath("//div[@class='empty-cart ng-star-inserted']"));
                    if (confirmElements.Count == 0)
                        return 0;
                    confirmElements = driverForRun.FindElements(By.XPath("//div[@class='cart-holder ng-star-inserted']//div[@class='cart-item ng-star-inserted']"));
                    return confirmElements.Count;
                case ProductContainer.WishList:
                    driverForRun.FindElement(By.XPath("//button[@routerlink='wishlist']")).Click();
                    MidSleep();
                    confirmElements = driverForRun.FindElements(By.XPath("//div[@class='no-wishlist-box ng-star-inserted']"));
                    if (confirmElements.Count == 0)
                        return 0;
                    confirmElements = driverForRun.FindElements(By.XPath("//div[@class='ng-star-inserted']//div[@class='row product-list']/div"));
                    return confirmElements.Count;
                default: return 0;
            }
        }

        private int CountProductsInContainer(ProductContainer container, Product product, HomePage page)
        {
            int count;
            switch (container)
            {
                case ProductContainer.Cart:
                    page.OpenCart();
                    MidSleep();
                    confirmElements = driverForRun.FindElements(By.XPath("//div[@class='empty-cart ng-star-inserted']"));
                    if (confirmElements.Count != 0)
                    { return 0; }                       
                    confirmElements = driverForRun.FindElements(By.XPath("//a[text() = '" + product.ProductName + "']//parent::div"));
                    count = (confirmElements.Count == 0) ? 0 : 1;
                    if (count == 1)
                    {
                        count = Int32.Parse(driverForRun.FindElement(By.XPath("//div[@class='cart-holder ng-star-inserted']//div[@class='cart-item ng-star-inserted']//span[@class='ng-value-label ng-star-inserted']")).Text);
                        return count;
                    }
                    return count;
                case ProductContainer.WishList:
                    page.OpenWishlist();
                    MidSleep();
                    confirmElements = driverForRun.FindElements(By.XPath("//div[@class='no-wishlist-box ng-star-inserted']"));
                    if (confirmElements.Count != 0)
                        return 0;
                    confirmElements = driverForRun.FindElements(By.XPath("//div[@class='row product-list']/div//div[@class='description']/div[text()='"+ product.ProductName +"']"));
                    SmallSleep();
                    count = (confirmElements.Count == 0) ? 0 : 1;
                    return count;
                default: return 0;
            }
        }

        private int GetContainerCounter(ProductContainer container)
        {
            int count;
            switch (container)
            {
                case ProductContainer.Cart:
                    confirmElements = driverForRun.FindElements(By.XPath("//div[@class='icons']//a/span"));
                    if (confirmElements.Count == 0)
                        return 0;
                    MidSleep();
                    count = Int32.Parse(driverForRun.FindElement(By.XPath("//div[@class='icons']//a/span")).Text);
                    return count;
                case ProductContainer.WishList:
                    confirmElements = driverForRun.FindElements(By.XPath("//div[@class='icons']//button/span"));
                    if (confirmElements.Count == 0)
                        return 0;
                    MidSleep();
                    count = Int32.Parse(driverForRun.FindElement(By.XPath("//div[@class='icons']//button/span")).Text);
                    return count;
                default: return 0;
            }
        }

        private void AddProductToContainer(ProductContainer container, InterctionControlSet controlSet, 
            HomePage page, Product product, int amount = 1)
        {
            switch (container)
            {
                case ProductContainer.Cart:
                    switch (controlSet)
                    {
                        case InterctionControlSet.Product_Title:                           
                            throw new NotSupportedException();
                        case InterctionControlSet.From_container:
                            page.OpenCart();
                            MidSleep();
                            int count = CountProductsInContainer(ProductContainer.Cart, product, page);
                            if (count > 0)
                            {
                                driverForRun.FindElement(By.XPath("//div[@class='cart-item ng-star-inserted']//span[@class='ng-arrow-wrapper'"));
                                SmallSleep();
                                driverForRun.FindElement(By.XPath("//*[@placeholder='Quantity']/ng-dropdown-panel//div[@role='option']/span[text()='"+(count++)+"']")).Click();
                                MidSleep();
                                break;
                            }
                            else
                            {
                                throw new NotSupportedException("Adding new product from cart is not supported");
                            }
                        case InterctionControlSet.Product_Details:
                            product.categoryControl.Click();
                            MidSleep();
                            driverForRun.FindElement(By.XPath("//div[@class='product-card-container']//div[@class='description']//div[text()='" + product.ProductName + "']//parent::div")).Click();
                            SmallSleep();
                            var element = driverForRun.FindElement(By.XPath("//button[text()='add to cart']"));
                            IJavaScriptExecutor js = (IJavaScriptExecutor)driverForRun;
                            js.ExecuteScript("arguments[0].click()", element);
                            SmallSleep();
                            driverForRun.FindElement(By.XPath("//div[@class='modal-content']//button[text()=' view cart ']")).Click();
                            break;
                        case InterctionControlSet.Container_Switch:
                            page.OpenWishlist();
                            MidSleep();
                            driverForRun.FindElement(By.XPath("//div[@class='description']//div[text()='" + product.ProductName + "']/ancestor::div[@class='product-card-container']//button/span[text()='Add to cart']")).Click();
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
                            product.categoryControl.Click();
                            MidSleep();
                            driverForRun.FindElement(By.XPath("//div[@class='description']//div[text()='" + product.ProductName + "']/ancestor::div[@class='product-card-container']//button[text()='Add to Wishlist']")).Click();
                            break;
                        case InterctionControlSet.From_container:
                            throw new NotSupportedException("Adding product from wishlist to wishlist is not supported");
                        case InterctionControlSet.Product_Details:
                            product.categoryControl.Click();
                            MidSleep();
                            driverForRun.FindElement(By.XPath("//div[@class='row product-list']//div[@class='product-card-container']//div[@class='description']/div[text()='" + product.ProductName + "']")).Click();
                            SmallSleep();
                            driverForRun.FindElement(By.XPath("//div[@class='wishlist-placeholder ng-star-inserted']//button/span[text()='Add to wishlist']")).Click();
                            break;
                        case InterctionControlSet.Container_Switch:
                            page.OpenCart();
                            MidSleep();
                            driverForRun.FindElement(By.XPath("//div[@class='cart-item ng-star-inserted']//button[text()='Move to wishlist']")).Click();
                            SmallSleep();
                            break;
                        default: break;
                    }
                    break;
                default: break;
            }
        }

        private void RemoveProductFromContainer(ProductContainer container, InterctionControlSet controlSet, 
            HomePage page, Product product, int amount = 1)
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
                            page.OpenCart();
                            MidSleep();
                            driverForRun.FindElement(By.XPath("//div[@class='cart-item-container row']//button[text()='Remove']")).Click();
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
                            product.categoryControl.Click();
                            MidSleep();
                            driverForRun.FindElement(By.XPath("//div[@class='description']//div[text()='" + product.ProductName + "']/ancestor::div[@class='product-card-container']//button[text()='Remove from Wishlist']")).Click();
                            break;
                        case InterctionControlSet.Product_Details:
                            product.categoryControl.Click();
                            MidSleep();
                            driverForRun.FindElement(By.XPath("//div[@class='row product-list']//div[@class='product-card-container']//div[@class='description']/div[text()='" + product.ProductName + "']")).Click();
                            SmallSleep();
                            driverForRun.FindElement(By.XPath("//div[@class='wishlist-placeholder ng-star-inserted']//button/span[text()='Remove from wishlist']")).Click();
                            break;                            
                        case InterctionControlSet.Container_Switch:
                            throw new NotSupportedException("Product need to be removed, not moved to other ontainer");
                        case InterctionControlSet.From_container:
                            page.OpenWishlist();
                            MidSleep();
                            IJavaScriptExecutor js = (IJavaScriptExecutor)driverForRun;
                            js.ExecuteScript("arguments[0].click()", driverForRun.FindElement(By.XPath("//div[@class='product-card-container']//div[@class='description']//div[text()='" + product.ProductName + "']/ancestor::div[@class='product-card-container']//button[text()='Remove from Wishlist']")));
                            break;
                        default: break;
                    }
                    break;
                default: break;
            }
        }

        private int GetProductQuantity(InterctionControlSet contorlSet)
        {
            return 0;
        }
    }
}
