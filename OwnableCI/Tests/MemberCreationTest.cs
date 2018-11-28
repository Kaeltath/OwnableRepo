using NUnit.Framework;
using OpenQA.Selenium;
using OwnableCI.Pages;
using OwnableCI.TestDataObjs;
using OwnableCI_TestLib.Constants;
using OwnableCI_TestLib.Tests;
using System;


namespace OwnableCI.Tests
{
    [TestFixtureSource(typeof(TestProperties), "oneTimeUsers")]
    [TestFixture]
    class MemberCreationTest : BaseTest
    {
        private TestUser user;
        private IWebElement confirmElement;
        private bool expectedResults;

        public MemberCreationTest(TestUser user)
        {
            this.user = user;
        }

        [Test]
        [Category("MemberCreationTest")]
        [Order(1)]
        public void MemberCreationSuccessful()
        {
            TestAction(() =>
            {
                string currentTestName = "Member Creation from SignUp";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                Assume.That(user.ExpResult == "Accept", "User is not from this test. Test will not run.");
                SignInPage signIn = new SignInPage(driverForRun);
                SmallSleep();
                signIn.Login(user);
                log.Debug("Start get your rental cap");
                BigSleep();
                BigSleep();
                driverForRun.FindElement(By.XPath("//button/div[text()=' GET YOUR RENTAL CAP ']")).Click();

                MidSleep();
                MemberCreationFirstPage pagePersonalInfo = new MemberCreationFirstPage(driverForRun);
                pagePersonalInfo.SetPersonalInfo(user);

                MidSleep();
                MemberCreationSecondPage pageIncomeInfo = new MemberCreationSecondPage(driverForRun);
                pageIncomeInfo.SetIncomeInfo(user);

                MidSleep();
                MemberCreationThirdPage pageMembershipAgreement = new MemberCreationThirdPage(driverForRun);
                pageMembershipAgreement.SetMembershipAgreement(user);

                MidSleep();
                MemberCreationFourthPage pageApplicationDisclosure = new MemberCreationFourthPage(driverForRun);
                pageApplicationDisclosure.btnAgree.Click();

                MidSleep();
                MemberCreationFifthPage pageCongratulations = new MemberCreationFifthPage(driverForRun);
                string rentActualValue = pageCongratulations.txtRentalCapValue.Text;
                string rentExpectedValue = RentalCapExpected(user);
                Assert.AreEqual(rentExpectedValue, rentActualValue);
                pageCongratulations.btnStartShopping.Click();

                BigSleep();
                ValidateMember(user);
                Assert.AreEqual(rentExpectedValue, GetRentalCap());
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
                Assume.That(user.ExpResult == "Reject-Low Income", "User is not from this test. Test will not run.");
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

        private string GetRentalCap()
        {
            string rentalCap = driverForRun.FindElement(By.XPath("//li[@container='body']//span[contains(text(),'Rental cap:')]")).Text;
            rentalCap = rentalCap.Split(new char[] { ':' })[1];
            return rentalCap;
        }

    }
}
