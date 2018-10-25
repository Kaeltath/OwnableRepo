using NUnit.Framework;
using OpenQA.Selenium;
using OwnableCI.Pages;
using OwnableCI.TestDataObjs;
using OwnableCI_TestLib.Constants;
using OwnableCI_TestLib.Pages;
using OwnableCI_TestLib.Tests;
using System;
using System.Threading;

namespace OwnableCI.Tests
{
    [TestFixtureSource(typeof(TestProperties), "users")]
    [TestFixture]
    class CartTests : BaseTest
    {
        TestUser user;

        public CartTests(TestUser user)
        {
            this.user = user;
        }

        [Test]
        public void AddingProductUserTest()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName + user.LastName + ";");
            HomePage home = new HomePage(driverForRun);
            SignInPage signin = new SignInPage(driverForRun);
            Assume.That(signin.Login(user), "Failed login, test will not run");
            Thread.Sleep(1000);
            driverForRun.FindElement(By.XPath("//div[@class='modal-content']//button/div[text()=' START BROWSING ']")).Click();
            Thread.Sleep(4000);            
            home.lblCart.Click();           
            Thread.Sleep(2000);
            var cartItems = driverForRun.FindElements(By.XPath("//div[@class='cart-holder ng-star-inserted']//div[@class='cart-item ng-star-inserted']"));
            Assert.That(cartItems.Count < 1, "Cart is not empty");
            home.lnkTopDeals.Click();
            Thread.Sleep(3000);
            driverForRun.FindElement(By.XPath("//div[@class='row product-list']/div[1]")).Click();
            Thread.Sleep(3000);
            //IJavaScriptExecutor executor = driverForRun as IJavaScriptExecutor;
            //executor.ExecuteScript("window.scrollBy(0,700);");
            string productName = driverForRun.FindElement(By.XPath("//h2[@class='product-name']")).Text;
            Thread.Sleep(3000);
            driverForRun.FindElement(By.XPath("//button[text()='add to cart']")).Click();
            Thread.Sleep(2000);
            driverForRun.FindElement(By.XPath("//div[@class='modal-content']//button[text()=' view cart ']")).Click();
            Thread.Sleep(3000);
            int cartCountAfter = 0;
            Int32.TryParse(driverForRun.FindElement(By.XPath("//a[@class='btn btn-link text-dark cart-link']/span")).Text, out cartCountAfter);
            Thread.Sleep(2000);
            Assert.That(cartCountAfter == 1, "Cart counter wasn't updated");
            var AddedItem = driverForRun.FindElement(By.XPath("//a[text() = '" + productName + "']//ancestor::div[@class='cart-item ng-star-inserted']"));
            cartItems = driverForRun.FindElements(By.XPath("//div[@class='cart-holder ng-star-inserted']//div[@class='cart-item ng-star-inserted']"));
            Assert.That(cartItems.Contains(AddedItem));

        }


        [Test]
        public void RemovingProductTest()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName + user.LastName + ";");
            HomePage home = new HomePage(driverForRun);
            SignInPage signin = new SignInPage(driverForRun);
            Assume.That(signin.Login(user), "Failed login, test will not run");
            Thread.Sleep(1000);
            driverForRun.FindElement(By.XPath("//div[@class='modal-content']//button/div[text()=' START BROWSING ']")).Click();
            Thread.Sleep(4000);
            home.lblCart.Click();
            Thread.Sleep(2000);
            var cartItems = driverForRun.FindElements(By.XPath("//div[@class='cart-holder ng-star-inserted']//div[@class='cart-item ng-star-inserted']"));
            Assert.That(cartItems.Count == 1, "Cart is empty");
            int cartCountAfter = 0;
            Int32.TryParse(driverForRun.FindElement(By.XPath("//a[@class='btn btn-link text-dark cart-link']/span")).Text, out cartCountAfter);
            Thread.Sleep(2000);
            Assert.That(cartCountAfter == 1, "Cart count is not equal to amount of items in cart");
            Thread.Sleep(2000);
            driverForRun.FindElement(By.XPath("//div[@class='cart-item ng-star-inserted']//button[text()='Remove']")).Click();
            Thread.Sleep(2000);
            var cartcounter = driverForRun.FindElements(By.XPath("//a[@class='btn btn-link text-dark cart-link']/span"));
            Assert.That(cartcounter.Count < 1);
            Thread.Sleep(2000);
            cartItems = driverForRun.FindElements(By.XPath("//div[@class='cart-holder ng-star-inserted']//div[@class='cart-item ng-star-inserted']"));
            Assert.That(cartItems.Count < 1);
        }
    }

}
