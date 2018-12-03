using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OwnableCI.Constants;
using OwnableCI.Enums;
using OwnableCI.Pages;
using OwnableCI.ServiceClasses;
using OwnableCI.TestDataObjs;
using OwnableCI_TestLib.Constants;
using OwnableCI_TestLib.Enums;
using OwnableCI_TestLib.Pages;
using OwnableCI_TestLib.Tests;
using System;
using System.Collections.Generic;

namespace OwnableCI.Tests
{
    [TestFixtureSource(typeof(TestProperties), "MemberCreationTestSource")]
    [TestFixture]
    class MemberCreationTests : BaseTest
    {
        private WebDriverWait wait;

        public MemberCreationTests(KeyValuePair<TestUser, BrowserType> source)
        {
            user = source.Key;
            currentBrowser = source.Value;
        }

        [Test]
        [Category("MemberCreationTests")]
        [Order(1)]
        public void MemberCreationAcceptFromLogIn()
        {
            TestAction(() =>
            {
                string currentTestName = "MemberCreationAcceptFromLogIn";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                if (user.ExpResult == "Accept-FromLogIn")
                {
                    MemberCreationAccept(user);
                }
                else
                {
                    Assume.That(false, "User is not from this test. Test will not run.");
                }
            });
        }

        [Test]
        [Category("MemberCreationTests")]
        [Order(2)]
        public void MemberCreationAcceptFromCart()
        {
            TestAction(() =>
            {
                string currentTestName = "MemberCreationAcceptFromCart";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                if (user.ExpResult == "Accept-FromCart")
                {
                    MemberCreationAccept(user);
                }
                else
                {
                    Assume.That(false, "User is not from this test. Test will not run.");
                }
            });
        }

        [Test]
        [Category("MemberCreationTests")]
        [Order(3)]
        public void MemberCreationAcceptFinishLater()
        {
            TestAction(() =>
            {
                string currentTestName = "MemberCreationAcceptFinishLater";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                if (user.ExpResult == "Accept-FinishLater")
                {
                    MemberCreationAccept(user);
                }
                else
                {
                    Assume.That(false, "User is not from this test. Test will not run.");
                }
            });
        }

        [Test]
        [Category("MemberCreationTests")]
        [Order(4)]
        public void MemberCreationRejectWrongZipCode()
        {
            TestAction(() =>
            {
                string currentTestName = "MemberCreationRejectWrongZipCode";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                if (user.ExpResult == "Reject-WrongZipCode")
                {
                    MemberCreationReject(user);
                }
                else
                {
                    Assume.That(false, "User is not from this test. Test will not run.");
                }
            });
        }

        [Test]
        [Category("MemberCreationTests")]
        [Order(5)]
        public void MemberCreationRejectUnsupportedState()
        {
            TestAction(() =>
            {
                string currentTestName = "MemberCreationRejectUnsupportedState";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                if (user.ExpResult == "Reject-UnsupportedState")
                {
                    MemberCreationReject(user);
                }
                else
                {
                    Assume.That(false, "User is not from this test. Test will not run.");
                }
            });
        }

        [Test]
        [Category("MemberCreationTests")]
        [Order(6)]
        public void MemberCreationRejectLowIncome()
        {
            TestAction(() =>
            {
                string currentTestName = "MemberCreationRejectLowIncome";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                if (user.ExpResult == "Reject-LowIncome")
                {
                    MemberCreationReject(user);
                }
                else
                {
                    Assume.That(false, "User is not from this test. Test will not run.");
                }
            });
        }

        private void MemberCreationAccept(TestUser user)
        {
            bool navigate = true;
            bool letsGetYourRentalCapMessageExpected = true;
            bool finishLater = false;
            switch (user.ExpResult)
            {
                case "Accept-FromLogIn":
                    log.Debug("How to Invoke: from LogIn");
                    break;
                case "Accept-FromCart":
                    log.Debug("How to Invoke: from Cart");
                    HomePage home = new HomePage(driverForRun);
                    SmallSleep();
                    ProductHandler handler = new ProductHandler(driverForRun, home);
                    Product product = new Product(ProductCategories.Top_deals, 1, driverForRun);
                    SmallSleep();
                    handler.AddProductToContainer(ProductContainer.Cart, InterctionControlSet.Product_Details, product);
                    wait = new WebDriverWait(driverForRun, TimeSpan.FromSeconds(10));
                    string btnBecomeMemberInCartXPath = "//button[text()='become a member']";
                    wait.Until(ExpectedConditions.ElementExists(By.XPath(btnBecomeMemberInCartXPath)));
                    var btnBecomeMemberInCart = driverForRun.FindElement(By.XPath(btnBecomeMemberInCartXPath));
                    TestHelper.JSexecutorClick(btnBecomeMemberInCart, driverForRun);
                    navigate = false;
                    letsGetYourRentalCapMessageExpected = false;
                    break;
                case "Accept-FinishLater":
                    log.Debug("How to Invoke: from Finish Later - Become A Member");
                    finishLater = true;
                    break;
                default:
                    Assume.That(false, "User is not from this test. Test will not run.");
                    break;
            }

            SignUpPage signUp = new SignUpPage(driverForRun, navigate);
            Assert.That(signUp.UserSignUp(user), "Sign Up failed");
            
            if (letsGetYourRentalCapMessageExpected)
            {
                GetYourRentalCapButtonClick();
            }

            MemberCreationFirstPage pagePersonalInfo = new MemberCreationFirstPage(driverForRun);
            if (finishLater)
            {
                MidSleep();
                pagePersonalInfo.btnFinishLater.Click();
                Assert.IsTrue(ValidateUser(user), "User validation is Failed");
                var btnBecomeMemberOnHome = driverForRun.FindElement(By.XPath("//button[text()='BECOME A MEMBER']"));
                btnBecomeMemberOnHome.Click();
            }
            pagePersonalInfo.SetPersonalInfo(user);

            MemberCreationSecondPage pageIncomeInfo = new MemberCreationSecondPage(driverForRun);
            pageIncomeInfo.SetIncomeInfo(user);

            MemberCreationThirdPage pageMembershipAgreement = new MemberCreationThirdPage(driverForRun);
            pageMembershipAgreement.SetMembershipAgreement(user);

            MemberCreationFourthPage pageApplicationDisclosure = new MemberCreationFourthPage(driverForRun);
            pageApplicationDisclosure.SetAgreement();

            MemberCreationFifthPage pageCongratulations = new MemberCreationFifthPage(driverForRun);
            string rentExpectedValue = RentalCapExpected(user);
            string rentActualValue = pageCongratulations.GetRentalCapValue();
            Assert.AreEqual(rentExpectedValue, rentActualValue);
            TestHelper.JSexecutorClick(pageCongratulations.btnStartShopping, driverForRun);

            Assert.IsTrue(rentExpectedValue == GetCurrentRentalCap(), "Rental Cap validation is Failed");
            Assert.IsTrue(ValidateMember(user), "Member validation is Failed");
        }

        private void MemberCreationReject(TestUser user)
        {
            bool validationError = false;
            string errorText = "";
            bool btnBecomeMemberExpected = false;
            bool popupErrorMessage = false;
            bool gotoSecondPage = false;
            switch (user.ExpResult)
            {
                case "Reject-WrongZipCode":
                    log.Debug("Fail reason: Wrong ZipCode");
                    validationError = true;
                    errorText = "Zip code and State do not match!";
                    break;
                case "Reject-UnsupportedState":
                    log.Debug("Fail reason: Unsupported State");
                    btnBecomeMemberExpected = true;
                    popupErrorMessage = true;
                    errorText = "We are not yet available in your state.";
                    break;
                case "Reject-LowIncome":
                    log.Debug("Fail reason: Low Income");
                    popupErrorMessage = true;
                    gotoSecondPage = true;
                    errorText = "We are sorry...";
                    break;
                default:
                    Assume.That(false, "User is not from this test. Test will not run.");
                    break;
            }

            SignUpPage signUp = new SignUpPage(driverForRun);
            Assert.That(signUp.UserSignUp(user), "Sign Up failed");

            GetYourRentalCapButtonClick();

            MemberCreationFirstPage pagePersonalInfo = new MemberCreationFirstPage(driverForRun);
            pagePersonalInfo.SetPersonalInfo(user);

            string errorTextXPath;
            IWebElement txtErrorText;
            if (validationError)
            {
                errorTextXPath = "//li[text()='" + " " + errorText + " " + "']";
                txtErrorText = driverForRun.FindElement(By.XPath(errorTextXPath));
                SmallSleep();
                Assert.That(txtErrorText.Displayed, "Error Message '" + errorText + "' is not displayed");
                pagePersonalInfo.btnFinishLater.Click();
                Assert.That(ValidateUser(user), "User validation is Failed");
                return;
            }

            if (gotoSecondPage)
            {
                MemberCreationSecondPage pageIncomeInfo = new MemberCreationSecondPage(driverForRun);
                pageIncomeInfo.SetIncomeInfo(user);
            }
            if (popupErrorMessage)
            {
                wait = new WebDriverWait(driverForRun, TimeSpan.FromSeconds(10));
                errorTextXPath = "//h3[text()='" + errorText + "']";
                wait.Until(ExpectedConditions.ElementExists(By.XPath(errorTextXPath)));
                txtErrorText = driverForRun.FindElement(By.XPath(errorTextXPath));
                SmallSleep();
                Assert.That(txtErrorText.Displayed, "Error Message '" + errorText + "' is not displayed");
                driverForRun.FindElement(By.XPath("//span[text()='Close']/parent::button")).Click();
                Assert.That(ValidateMember(user), "Member validation is Failed");
                bool btnBecomeMemberExists = true;
                try { driverForRun.FindElement(By.XPath("//button[text()='BECOME A MEMBER']")); }
                catch { btnBecomeMemberExists = false; }
                Assert.AreEqual(btnBecomeMemberExpected, btnBecomeMemberExists);
            }
        }

        private string GetCurrentRentalCap() //return Rental Cap value from Home page
        {
            wait = new WebDriverWait(driverForRun, TimeSpan.FromSeconds(10));
            string txtRentalCapXPath = "//li[@container='body']//span[contains(text(),'Rental cap:')]";
            wait.Until(ExpectedConditions.ElementExists(By.XPath(txtRentalCapXPath)));
            string rentalCap = driverForRun.FindElement(By.XPath(txtRentalCapXPath)).Text;
            rentalCap = rentalCap.Split(new char[] { ':' })[1];
            return rentalCap;
        }

        private void GetYourRentalCapButtonClick() //"Lets Get Your Rental Cap" popup message => click button "Get Rental Cap"
        {
            wait = new WebDriverWait(driverForRun, TimeSpan.FromSeconds(20));
            string btnGetRentalCapXPath = "//button/div[text()=' GET YOUR RENTAL CAP ']";
            wait.Until(ExpectedConditions.ElementExists(By.XPath(btnGetRentalCapXPath)));
            driverForRun.FindElement(By.XPath(btnGetRentalCapXPath)).Click();
        }
    }
}
