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
    [TestFixtureSource(typeof(TestProperties), "reusableUsers")]
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
            //TestAction(() =>
            //{
                string currentTestName = "AddingProductToCartUser";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                HomePage home = new HomePage(driverForRun, false);
                SignInPage signin = new SignInPage(driverForRun);
                //bool isUserLogedIn = ValidateUser(user);
                Assume.That(signin.Login(user), "Failed login, test will not run");
                Assume.That(ValidateUser(user), "Logged-in account is not a user");
                SmallSleep();
                ProductHandler handler = new ProductHandler(driverForRun, home);
                Product product = new Product(ProductCategories.Top_deals, 1, driverForRun);
                SmallSleep();
                int counter = handler.GetContainerCounter(ProductContainer.Cart);
                int prodctsInCart = handler.CountProductsInContainer(ProductContainer.Cart, product);
                Assert.That(prodctsInCart < 1, "Selected product already in cart");
                MidSleep();
                handler.AddProductToContainer(ProductContainer.Cart, InterctionControlSet.Product_Details, product);
                MidSleep();
                Assert.That((prodctsInCart + 1) == handler.CountProductsInContainer(ProductContainer.Cart, product), "Product wasn't added properly");
                Assert.That((counter + 1) == handler.GetContainerCounter(ProductContainer.Cart), "Conter wasn't updated properly");
            //});
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(2)]
        public void AddingExistingProductToCartUser()
        {
            TestAction(() =>
            {
                string currentTestName = "AddingExistingProductToCartUser";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                HomePage home = new HomePage(driverForRun, false);
                SmallSleep();
                ProductHandler handler = new ProductHandler(driverForRun, home);
                Product product = new Product(ProductCategories.Top_deals, 1, driverForRun);
                SmallSleep();
                int counter = handler.GetContainerCounter(ProductContainer.Cart);
                int prodctsInCart = handler.CountProductsInContainer(ProductContainer.Cart, product);
                Assume.That(prodctsInCart >= 1, "No such product in cart, test will not proceed");
                MidSleep();
                handler.AddProductToContainer(ProductContainer.Cart, InterctionControlSet.Product_Details, product);
                MidSleep();
                Assert.That((prodctsInCart + 1) == handler.CountProductsInContainer(ProductContainer.Cart, product), "Product wasn't added properly");
                Assert.That((counter + 1) == handler.GetContainerCounter(ProductContainer.Cart), "Conter wasn't updated properly");
            });
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(3)]
        public void RemovingProductFromCartUser()
        {
            TestAction(() =>
            {
                string currentTestName = "RemovingProductFromCartUser";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                HomePage home = new HomePage(driverForRun, false);
                SmallSleep();
                ProductHandler handler = new ProductHandler(driverForRun, home);
                Product product = new Product(ProductCategories.Top_deals, 1, driverForRun);
                SmallSleep();
                int counter = handler.GetContainerCounter(ProductContainer.Cart);
                int prodctsInCart = handler.CountProductsInContainer(ProductContainer.Cart, product);
                Assume.That(prodctsInCart >= 1, "No such product in cart, test will not proceed");
                MidSleep();
                handler.RemoveProductFromContainer(ProductContainer.Cart, InterctionControlSet.From_container, product);
                Assert.That(handler.CountProductsInContainer(ProductContainer.Cart, product) == 0, "Product wasn't removed properly");
                Assert.That((counter - prodctsInCart) == handler.GetContainerCounter(ProductContainer.Cart), "Conter wasn't updated properly");
            });
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(4)]
        public void AddingProductToCartFromWishListUser()
        {
            TestAction(() =>
            {
                string currentTestName = "AddingProductToCartFromWishListUser";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                HomePage home = new HomePage(driverForRun, false);
                SmallSleep();
                ProductHandler handler = new ProductHandler(driverForRun, home);
                Product product = new Product(ProductCategories.Top_deals, 2, driverForRun);
                SmallSleep();
                int counter = handler.GetContainerCounter(ProductContainer.Cart);
                int prodctsInCart = handler.CountProductsInContainer(ProductContainer.Cart, product);
                Assume.That(prodctsInCart <= 1, "Product already in cart, test will not proceed");
                MidSleep();
                handler.AddProductToContainer(ProductContainer.WishList, InterctionControlSet.Product_Title, product);
                Assert.That(handler.CountProductsInContainer(ProductContainer.WishList, product) == 1, "Product did not got to WishList");
                MidSleep();
                handler.AddProductToContainer(ProductContainer.Cart, InterctionControlSet.Container_Switch, product);
                MidSleep();
                handler.RemoveProductFromContainer(ProductContainer.WishList, InterctionControlSet.From_container, product);
                Assert.That((prodctsInCart + 1) == handler.CountProductsInContainer(ProductContainer.Cart, product), "Product wasn't added properly");
                Assert.That((counter + 1) == handler.GetContainerCounter(ProductContainer.Cart), "Conter wasn't updated properly");
            });
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(5)]
        public void MovingProductFromCartToWishListUser()
        {
            TestAction(() =>
            {
                string currentTestName = "MovingProductFromCartToWishListUser";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                HomePage home = new HomePage(driverForRun, false);
                SmallSleep();
                ProductHandler handler = new ProductHandler(driverForRun, home);
                Product product = new Product(ProductCategories.Top_deals, 2, driverForRun);
                SmallSleep();

                try
                {
                    int counterCart = handler.GetContainerCounter(ProductContainer.Cart);
                    int prodctsInCart = handler.CountProductsInContainer(ProductContainer.Cart, product);
                    int counterWishList = handler.GetContainerCounter(ProductContainer.WishList);
                    int prodctsInWishList = handler.CountProductsInContainer(ProductContainer.WishList, product);
                    Assume.That(prodctsInCart >= 1, "Product not in cart, test will not proceed");
                    Assume.That(prodctsInWishList < 1, "Product already in wishist, test will not proceed");
                    MidSleep();
                    handler.AddProductToContainer(ProductContainer.WishList, InterctionControlSet.Container_Switch, product);
                    MidSleep();
                    Assert.That(handler.CountProductsInContainer(ProductContainer.WishList, product) == 1, "Product did not got to WishList");
                    Assert.That(handler.CountProductsInContainer(ProductContainer.Cart, product) == 0, "Product wasn't removed from cart properly");
                    Assert.That((counterCart - 1) == handler.GetContainerCounter(ProductContainer.Cart), "Cart Conter wasn't updated properly");
                    Assert.That((counterWishList + 1) == handler.GetContainerCounter(ProductContainer.WishList), "WishList Conter wasn't updated properly");
                }
                //clean-up
                finally { handler.RemoveProductFromContainer(ProductContainer.WishList, InterctionControlSet.From_container, product); }
            });
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(6)]
        public void AddingProductToWishListFromProductTitleTest()
        {
            TestAction(() =>
            {
                string currentTestName = "AddingProductToWishListFromProductTitleTest";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                HomePage home = new HomePage(driverForRun, false);
                SmallSleep();
                ProductHandler handler = new ProductHandler(driverForRun, home);
                Product product = new Product(ProductCategories.Top_deals, 3, driverForRun);
                SmallSleep();
                Assert.That(handler.CountProductsInContainer(ProductContainer.WishList, product) == 0, "Product alreadty in wishlist");
                int counter = handler.GetContainerCounter(ProductContainer.WishList);
                MidSleep();
                handler.AddProductToContainer(ProductContainer.WishList, InterctionControlSet.Product_Title, product);
                Assert.That(handler.CountProductsInContainer(ProductContainer.WishList, product) == 1, "Product wasn't added properly to wishlist");
                Assert.That((counter + 1) == handler.GetContainerCounter(ProductContainer.WishList), "Counter wasn't updated properly");
            });
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(7)]
        public void RemovingProductFromWishListFromProductTitleTest()
        {
            TestAction(() =>
            {
                string currentTestName = "ProductIntearctionTest";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                HomePage home = new HomePage(driverForRun, false);
                SmallSleep();
                ProductHandler handler = new ProductHandler(driverForRun, home);
                Product product = new Product(ProductCategories.Top_deals, 3, driverForRun);
                SmallSleep();
                Assert.That(handler.CountProductsInContainer(ProductContainer.WishList, product) == 1, "Product not in wishlist");
                int counter = handler.GetContainerCounter(ProductContainer.WishList);
                MidSleep();
                handler.RemoveProductFromContainer(ProductContainer.WishList, InterctionControlSet.Product_Title, product);
                Assert.That(handler.CountProductsInContainer(ProductContainer.WishList, product) == 0, "Product wasn't removed properly to wishlist");
                Assert.That((counter - 1) == handler.GetContainerCounter(ProductContainer.WishList), "Counter wasn't updated properly");
            });
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(8)]
        public void AddingProductToWishListFromProductDetailsTest()
        {
            TestAction(() =>
            {
                string currentTestName = "AddingProductToWishListFromProductDetailsTest";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                HomePage home = new HomePage(driverForRun, false);
                //SignInPage signin = new SignInPage(driverForRun);
                //Assume.That(signin.Login(user), "Failed login, test will not run");
                //Assume.That(ValidateUser(user), "Logged-in account is not a user");
                SmallSleep();
                ProductHandler handler = new ProductHandler(driverForRun, home);
                Product product = new Product(ProductCategories.Top_deals, 4, driverForRun);
                SmallSleep();
                Assert.That(handler.CountProductsInContainer(ProductContainer.WishList, product) == 0, "Product alreadty in wishlist");
                int counter = handler.GetContainerCounter(ProductContainer.WishList);
                MidSleep();
                handler.AddProductToContainer(ProductContainer.WishList, InterctionControlSet.Product_Details, product);
                Assert.That(handler.CountProductsInContainer(ProductContainer.WishList, product) == 1, "Product wasn't added properly to wishlist");
                Assert.That((counter + 1) == handler.GetContainerCounter(ProductContainer.WishList), "Counter wasn't updated properly");
            });
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(9)]
        public void RemovingProductFromWishListFromProductDetailsTest()
        {
            TestAction(() =>
            {
                string currentTestName = "RemovingProductFromWishListFromProductDetailsTest";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                HomePage home = new HomePage(driverForRun, false);
                //SignInPage signin = new SignInPage(driverForRun);
                //Assume.That(signin.Login(user), "Failed login, test will not run");
                //Assume.That(ValidateUser(user), "Logged-in account is not a user");
                SmallSleep();
                ProductHandler handler = new ProductHandler(driverForRun, home);
                Product product = new Product(ProductCategories.Top_deals, 4, driverForRun);
                SmallSleep();
                Assert.That(handler.CountProductsInContainer(ProductContainer.WishList, product) == 1, "Product not in wishlist");
                int counter = handler.GetContainerCounter(ProductContainer.WishList);
                MidSleep();
                handler.RemoveProductFromContainer(ProductContainer.WishList, InterctionControlSet.Product_Details, product);
                Assert.That(handler.CountProductsInContainer(ProductContainer.WishList, product) == 0, "Product wasn't removed properly to wishlist");
                Assert.That((counter - 1) == handler.GetContainerCounter(ProductContainer.WishList), "Counter wasn't updated properly");
            });
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(10)]
        public void AddingProductToWishListFromCartTest()
        {
            TestAction(() =>
            {
                string currentTestName = "AddingProductToWishListFromCartTest";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                HomePage home = new HomePage(driverForRun, false);
                SmallSleep();
                ProductHandler handler = new ProductHandler(driverForRun, home);
                Product product = new Product(ProductCategories.Top_deals, 5, driverForRun);
                SmallSleep();
                handler.AddProductToContainer(ProductContainer.Cart, InterctionControlSet.Product_Details, product);
                Assert.That(handler.CountProductsInContainer(ProductContainer.WishList, product) == 0, "Product alreadty in wishlist");
                int counter = handler.GetContainerCounter(ProductContainer.WishList);
                MidSleep();
                handler.AddProductToContainer(ProductContainer.WishList, InterctionControlSet.Container_Switch, product);
                Assert.That(handler.CountProductsInContainer(ProductContainer.WishList, product) == 1, "Product wasn't added properly to wishlist");
                Assert.That((counter + 1) == handler.GetContainerCounter(ProductContainer.WishList), "Counter wasn't updated properly");
            });
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(11)]
        public void RemovingProductFromWishListContainerTest()
        {
            TestAction(() =>
            {
                string currentTestName = "RemovingProductFromWishListContainerTest";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                HomePage home = new HomePage(driverForRun, false);
                SmallSleep();
                ProductHandler handler = new ProductHandler(driverForRun, home);
                Product product = new Product(ProductCategories.Top_deals, 5, driverForRun);
                SmallSleep();
                Assert.That(handler.CountProductsInContainer(ProductContainer.WishList, product) == 1, "Product not in wishlist");
                int counter = handler.GetContainerCounter(ProductContainer.WishList);
                MidSleep();
                handler.RemoveProductFromContainer(ProductContainer.WishList, InterctionControlSet.From_container, product);
                Assert.That(handler.CountProductsInContainer(ProductContainer.WishList, product) == 0, "Product wasn't removed properly to wishlist");
                Assert.That((counter - 1) == handler.GetContainerCounter(ProductContainer.WishList), "Counter wasn't updated properly");
            });
        }

        [Test]
        [Category("ProductIntearctionTest")]
        [Order(12)]
        public void WishlistSortingTest()
        {
            TestAction(() =>
            {
                string currentTestName = "WishlistSortingTest";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                HomePage home = new HomePage(driverForRun, false);
                //SignInPage signin = new SignInPage(driverForRun);
                //Assume.That(signin.Login(user), "Failed login, test will not run");
                //Assume.That(ValidateUser(user), "Logged-in account is not a user");
                SmallSleep();
                ProductHandler handler = new ProductHandler(driverForRun, home);
                int[] selectedProductIndexes = new int[] { 1, 2, 3, 4, 5, 6, 7 };
                handler.BuildProductCollection(selectedProductIndexes, ProductCategories.Top_deals);
                handler.AddProdutRangeToContainer(selectedProductIndexes, ProductContainer.WishList, ProductCategories.Top_deals);
                handler.SortProducts(ProductContainer.WishList, SortingMethods.Brand);
                handler.ValidateProductSorting(SortingMethods.Brand);
                handler.SortProducts(ProductContainer.WishList, SortingMethods.Price, true);
                handler.ValidateProductSorting(SortingMethods.Price);
                handler.SortProducts(ProductContainer.WishList, SortingMethods.Rating);
                handler.ValidateProductSorting(SortingMethods.Rating);
            });
        }

        //[Test]
        //[Category("ProductIntearctionTest")]
        //[Explicit]
        //public void Tester()
        //{
        //    string currentTestName = "WishlistSortingTest";
        //    log.Debug("Starting " + currentTestName + " Test;");
        //    log.Debug("For user " + user.FirstName + user.LastName + ";");
        //    SignInPage signin = new SignInPage(driverForRun);
        //    Assume.That(signin.Login(user), "Failed login, test will not run");
        //    Assume.That(ValidateUser(user), "Logged-in account is not a user");
        //    HomePage home = new HomePage(driverForRun, false);
        //    SmallSleep();
        //    ProductHandler handler = new ProductHandler(driverForRun, home);
        //    //int[] selectedProductIndexes = new int[] { 1, 2, 3, 4, 5, 6, 7 };
        //    //handler.BuildProductCollection(selectedProductIndexes, ProductCategories.Top_deals);
        //    //handler.AddProdutRangeToContainer(selectedProductIndexes, ProductContainer.WishList, ProductCategories.Top_deals);
        //    //handler.InitialCollection = handler.GetCurrentProductsList(ProductContainer.WishList);
        //    handler.SortProducts(ProductContainer.WishList, SortingMethods.Brand);
        //    handler.ValidateProductSorting(SortingMethods.Brand);
        //    handler.SortProducts(ProductContainer.WishList, SortingMethods.Price);
        //    handler.ValidateProductSorting(SortingMethods.Price);
        //    handler.SortProducts(ProductContainer.WishList, SortingMethods.Rating);
        //    handler.ValidateProductSorting(SortingMethods.Rating);
        //}

    }
}
