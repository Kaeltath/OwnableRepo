using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OwnableCI.Pages;
using OwnableCI.ServiceClasses;
using OwnableCI.TestDataObjs;
using OwnableCI_TestLib.Constants;
using OwnableCI_TestLib.Enums;
using OwnableCI_TestLib.Tests;
using System;
using System.Collections.Generic;

namespace OwnableCI.Tests
{
    [TestFixtureSource(typeof(TestProperties), "oneTimeTestSoure")]
    [TestFixture]
    class MemberCreationTest : BaseTest
    {
        private TestUser user;
        private WebDriverWait wait;

        public MemberCreationTest(KeyValuePair<TestUser, BrowserType> source)
        {
            user = source.Key;
            currentBrowser = source.Value;
        }

        [Test]
        [Category("MemberCreationTest")]
        [Order(1)]
        public void MemberCreationAcceptFromLogIn()
        {
            TestAction(() =>
            {
                string currentTestName = "Member Creation Accept from LogIn";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                Assume.That(user.ExpResult == "Accept-FromLogIn", "User is not from this test. Test will not run.");
                SignInPage signIn = new SignInPage(driverForRun);
                SmallSleep();
                signIn.Login(user);

                wait = new WebDriverWait(driverForRun, TimeSpan.FromSeconds(20));
                string btnGetRentalCapXPath = "//button/div[text()=' GET YOUR RENTAL CAP ']";
                wait.Until(ExpectedConditions.ElementExists(By.XPath(btnGetRentalCapXPath)));
                driverForRun.FindElement(By.XPath(btnGetRentalCapXPath)).Click();

                MemberCreationFirstPage pagePersonalInfo = new MemberCreationFirstPage(driverForRun);
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
                //pageCongratulations.btnStartShopping.Click();
                TestHelper.JSexecutorClick(pageCongratulations.btnStartShopping, driverForRun);

                Assert.AreEqual(rentExpectedValue, GetCurrentRentalCap());
                Assert.IsTrue(ValidateMember(user), "Member validation is Failed");
            });
        }

        [Test]
        [Category("MemberCreationTest")]
        [Order(2)]
        public void MemberCreationFailedLowIncome()
        {
            TestAction(() =>
            {
                string currentTestName = "Member Creation from SignUp";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                Assume.That(user.ExpResult == "Reject-LowIncome", "User is not from this test. Test will not run.");
                SignInPage signIn = new SignInPage(driverForRun);
                SmallSleep();
                signIn.Login(user);
                log.Debug("Start get your rental cap");
                BigSleep();
                driverForRun.FindElement(By.XPath("//button/div[text()=' GET YOUR RENTAL CAP ']")).Click();

                MidSleep();
                MemberCreationFirstPage pagePersonalInfo = new MemberCreationFirstPage(driverForRun);
                pagePersonalInfo.SetPersonalInfo(user);

                MidSleep();
                MemberCreationSecondPage pageIncomeInfo = new MemberCreationSecondPage(driverForRun);
                pageIncomeInfo.SetIncomeInfo(user);

                BigSleep();
                BigSleep();
                IWebElement elemWeSorry = driverForRun.FindElement(By.XPath("//h3[text() = 'We are sorry...']"));
                if (!elemWeSorry.Displayed)
                {
                    log.Error("Element " + elemWeSorry + " is not exists, please check the problem");
                }
                else
                {
                    driverForRun.FindElement(By.XPath("//span[text()='Close']")).Click();
                }

                MidSleep();
                ValidateMember(user);
                IWebElement elemRentalCap = null;
                try
                {
                    elemRentalCap = driverForRun.FindElement(By.XPath("//li[@container='body']//span[contains(text(),'Rental cap:')]"));
                }
                catch (Exception)
                {
                }
                Assert.That(elemRentalCap == null, "Element " + elemRentalCap + " exists on Home page, but not expected for this User");
            });
        }

        private string GetCurrentRentalCap() //on Home page
        {
            wait = new WebDriverWait(driverForRun, TimeSpan.FromSeconds(10));
            string txtRentalCapXPath = "//li[@container='body']//span[contains(text(),'Rental cap:')]";
            wait.Until(ExpectedConditions.ElementExists(By.XPath(txtRentalCapXPath)));
            string rentalCap = driverForRun.FindElement(By.XPath(txtRentalCapXPath)).Text;
            rentalCap = rentalCap.Split(new char[] { ':' })[1];
            return rentalCap;
        }
    }
}
