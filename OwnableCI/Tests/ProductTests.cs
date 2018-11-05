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
            Assert.That(prodctsInCart == (CountProductsInContainer(ProductContainer.Cart, product, home) + 1), "Product wasn't added properly");
            Assert.That(counter == (GetContainerCounter(ProductContainer.Cart) + 1), "Conter wasn't updated properly"); 
        }

        [Test]
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
            Assume.That(prodctsInCart < 1, "No such product in cart, test will not proceed");
            MidSleep();
            AddProductToContainer(ProductContainer.Cart, InterctionControlSet.Product_Details, home, product);
            MidSleep();
            Assert.That(prodctsInCart == (CountProductsInContainer(ProductContainer.Cart, product, home) + 1), "Product wasn't added properly");
            Assert.That(counter == (GetContainerCounter(ProductContainer.Cart) + 1), "Conter wasn't updated properly");
        }

        [Test]
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
            Assume.That(prodctsInCart < 1, "No such product in cart, test will not proceed");
            MidSleep();
            RemoveProductFromContainer(ProductContainer.Cart, InterctionControlSet.From_container, home, product);
            Assert.That(prodctsInCart == (CountProductsInContainer(ProductContainer.Cart, product, home) - 1), "Product wasn't removed properly");
            Assert.That(counter == (GetContainerCounter(ProductContainer.Cart) - 1), "Conter wasn't updated properly");
        }

        [Test]
        [Ignore("WishList not working")]
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
            Assume.That(prodctsInCart > 0, "Product already in cart, test will not proceed");
            MidSleep();
            AddProductToContainer(ProductContainer.WishList, InterctionControlSet.Product_Title, home, product);
            Assert.That(CountProductsInContainer(ProductContainer.WishList, product, home) == 1, "Product did not got to WishList");
            MidSleep();
            AddProductToContainer(ProductContainer.Cart, InterctionControlSet.Container_Switch, home, product);
            MidSleep();
            Assert.That(prodctsInCart == (CountProductsInContainer(ProductContainer.Cart, product, home) + 1), "Product wasn't added properly");
            Assert.That(counter == (GetContainerCounter(ProductContainer.Cart) + 1), "Conter wasn't updated properly");
        }

        [Test]
        [Ignore("WishList not working")]
        [Order(5)]
        public void MovingProductFromCartToWishListUser()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName + user.LastName + ";");
            HomePage home = new HomePage(driverForRun, false);
            SmallSleep();
            Product product = new Product(ProductCategories.Top_deals, 3, home, driverForRun);
            SmallSleep();
            int counterCart = GetContainerCounter(ProductContainer.Cart);
            int prodctsInCart = CountProductsInContainer(ProductContainer.Cart, product, home);
            int counterWishList = GetContainerCounter(ProductContainer.WishList);
            int prodctsInWishList = CountProductsInContainer(ProductContainer.WishList, product, home);
            Assume.That(prodctsInCart < 1, "Product not in cart, test will not proceed");
            Assume.That(prodctsInWishList > 0, "Product already in wishist, test will not proceed");
            MidSleep();
            AddProductToContainer(ProductContainer.WishList, InterctionControlSet.Container_Switch, home, product);
            MidSleep();
            Assert.That(CountProductsInContainer(ProductContainer.WishList, product, home) == 1, "Product did not got to WishList");
            Assert.That(CountProductsInContainer(ProductContainer.Cart, product, home) == 0, "Product wasn't removed from cart properly");
            Assert.That(counterCart == (GetContainerCounter(ProductContainer.Cart) - 1), "Cart Conter wasn't updated properly");
            Assert.That(counterWishList == (GetContainerCounter(ProductContainer.WishList) + 1), "WishList Conter wasn't updated properly");
        }

        [Test]
        [Ignore("Blocked by bag + rework")]
        public void AddingProductToWishListTest()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName + user.LastName + ";");
            HomePage home = new HomePage(driverForRun, false);
            SignInPage signin = new SignInPage(driverForRun);
            Assume.That(signin.Login(user), "Failed login, test will not run");
            Assume.That(ValidateUser(user));
            SmallSleep();
            driverForRun.FindElement(By.XPath("//button[@routerlink='wishlist']")).Click(); 
            SmallSleep();
            var WLItems = driverForRun.FindElements(By.XPath("//div[@class='cart-holder ng-star-inserted']//div[@class='cart-item ng-star-inserted']"));
            Assert.That(WLItems.Count < 1, "WishList is not empty");
            home.btnTopDeals.Click();
            MidSleep();
            driverForRun.FindElement(By.XPath("//div[@class='row product-list']/div[1]")).Click();
            MidSleep();
            string productName = driverForRun.FindElement(By.XPath("//h2[@class='product-name']")).Text;
            MidSleep();
            driverForRun.FindElement(By.XPath("//button[text()='add to cart']")).Click();
            SmallSleep();
            driverForRun.FindElement(By.XPath("//div[@class='modal-content']//button[text()=' view cart ']")).Click();
            MidSleep();
            int cartCountAfter = 0;
            Int32.TryParse(driverForRun.FindElement(By.XPath("//a[@class='btn btn-link text-dark cart-link']/span")).Text, out cartCountAfter);
            SmallSleep();
            Assert.That(cartCountAfter == 1, "Cart counter wasn't updated");
            var AddedItem = driverForRun.FindElement(By.XPath("//a[text() = '" + productName + "']//ancestor::div[@class='cart-item ng-star-inserted']"));
            WLItems = driverForRun.FindElements(By.XPath("//div[@class='cart-holder ng-star-inserted']//div[@class='cart-item ng-star-inserted']"));
            Assert.That(WLItems.Contains(AddedItem));
        }

        [Test]
        [Ignore("Blocked by bag + rework")]
        public void RemovingProductFromWishListTest()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName + user.LastName + ";");
            HomePage home = new HomePage(driverForRun, false);
            SignInPage signin = new SignInPage(driverForRun);
            Assume.That(signin.Login(user), "Failed login, test will not run");
        }

        private int CountProductsInContainer(ProductContainer container, HomePage page)
        {
            switch (container)
            {
                case ProductContainer.Cart:
                    SmallSleep();
                    page.lblCart.Click();
                    MidSleep();
                    confirmElements = driverForRun.FindElements(By.XPath("//div[@class='empty-cart ng-star-inserted']"));
                    if (confirmElements.Count == 0)
                        return 0;
                    MidSleep();
                    confirmElements = driverForRun.FindElements(By.XPath("//div[@class='cart-holder ng-star-inserted']//div[@class='cart-item ng-star-inserted']"));
                    return confirmElements.Count;
                case ProductContainer.WishList:
                    SmallSleep();
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
                    SmallSleep();
                    page.lblCart.Click();
                    MidSleep();
                    confirmElements = driverForRun.FindElements(By.XPath("//div[@class='empty-cart ng-star-inserted']"));
                    if (confirmElements.Count != 0)
                    { return 0; }                       
                    MidSleep();
                    confirmElements = driverForRun.FindElements(By.XPath("//a[text() = '" + product.ProductName + "']//ancestor::div[@class='cart-item ng-star-inserted']"));
                    SmallSleep();
                    count = (confirmElements.Count == 0) ? 0 : 1;
                    if (count == 1)
                    {
                        count = Int32.Parse(driverForRun.FindElement(By.XPath("//div[@class='cart-holder ng-star-inserted']//div[@class='cart-item ng-star-inserted']//span[@class='ng-value-label ng-star-inserted']")).Text);
                        return count;
                    }
                    return count;
                case ProductContainer.WishList:
                    MidSleep();
                    driverForRun.FindElement(By.XPath("//button[@routerlink='wishlist']")).Click();
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
                    SmallSleep();
                    confirmElements = driverForRun.FindElements(By.XPath("//div[@class='icons']//a/span"));
                    if (confirmElements.Count == 0)
                        return 0;
                    MidSleep();
                    count = Int32.Parse(driverForRun.FindElement(By.XPath("//div[@class='icons']//a/span")).Text);
                    return count;
                case ProductContainer.WishList:
                    SmallSleep();
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
                            SmallSleep();
                            page.lblCart.Click();
                            MidSleep();
                            int count = CountProductsInContainer(ProductContainer.Cart, product, page);
                            if (count > 0)
                            {
                                SmallSleep();
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
                            SmallSleep();
                            product.categoryControl.Click();
                            MidSleep();
                            driverForRun.FindElement(By.XPath("//div[@class='row product-list']/div[" + product.ProductName + "]")).Click();
                            SmallSleep();
                            driverForRun.FindElement(By.XPath("//button[text()='add to cart']")).Click();
                            SmallSleep();
                            driverForRun.FindElement(By.XPath("//div[@class='modal-content']//button[text()=' view cart ']")).Click();
                            break;
                        case InterctionControlSet.Container_Switch:
                            SmallSleep();
                            driverForRun.FindElement(By.XPath("//button[@routerlink='wishlist']")).Click();
                            MidSleep();
                            driverForRun.FindElement(By.XPath("//div[@class='product-card-container']//div[@class='add-to-cart ng-star-inserted']/button")).Click();
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
                            SmallSleep();
                            product.categoryControl.Click();
                            MidSleep();
                            driverForRun.FindElement(By.XPath("//div[@class='product-card-container']//button[text()='Add to Wishlist']")).Click();
                            break;
                        case InterctionControlSet.From_container:
                            throw new NotSupportedException("Adding product from wishlist to wishlist is not supported");
                        case InterctionControlSet.Product_Details:
                            SmallSleep();
                            product.categoryControl.Click();
                            MidSleep();
                            driverForRun.FindElement(By.XPath("//div[@class='row product-list']/div[" + product.ProductName + "]")).Click();
                            SmallSleep();
                            driverForRun.FindElement(By.XPath("//button/span[text()='Add to Wishlist']")).Click();
                            break;
                        case InterctionControlSet.Container_Switch:
                            SmallSleep();
                            page.lblCart.Click(); 
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
                            SmallSleep();
                            page.lblCart.Click();
                            MidSleep();
                            driverForRun.FindElement(By.XPath("//div[@class='cart-item-container row']//button[text()='Remove']")).Click();
                            break;
                        case InterctionControlSet.Container_Switch:
                            throw new NotImplementedException();
                        default: break;
                    }
                    break;
                case ProductContainer.WishList:
                    SmallSleep();
                    switch (controlSet)
                    {
                        case InterctionControlSet.Product_Title:
                            SmallSleep();
                            break;
                        case InterctionControlSet.Product_Details:
                            SmallSleep();
                            break;
                        case InterctionControlSet.Container_Switch:
                            SmallSleep();
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
