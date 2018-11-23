using NUnit.Framework;
using OpenQA.Selenium;
using OwnableCI.Pages;
using OwnableCI.TestDataObjs;
using OwnableCI_TestLib.Constants;
using OwnableCI_TestLib.Tests;
using OwnableCI_TestLib.Pages;


namespace OwnableCI.Tests
{
    [TestFixtureSource(typeof(TestProperties), "users")]
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
        public void MemberCreationSignUp()
        {
            if (user.ExpResult == "Accept")
            {
                TestAction(() =>
                {
                    string currentTestName = "Member Creation from SignUp";
                    log.Debug("Starting " + currentTestName + " Test;");
                    log.Debug("For user " + user.FirstName + user.LastName + ";");
                    SignUpPage pageSignUp = new SignUpPage(driverForRun);
                    MidSleep();
                    pageSignUp.UserSignUp(user);
                    log.Debug("Start get your rental cap");
                    BigSleep();
                    BigSleep();
                    driverForRun.FindElement(By.XPath("//button/div[text()=' GET YOUR RENTAL CAP ']")).Click();

                    MidSleep();
                    MemberCreationFirstPage pagePersonalInfo = new MemberCreationFirstPage(driverForRun);
                    pagePersonalInfo.lstState.Click();
                    driverForRun.FindElement(By.XPath("//span[text()='Texas']")).Click(); //QQ: need to implement selection state inside SetPersonalInfo() method
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
                    string sRentActualValue = pageCongratulations.txtRentalCapValue.Text;
                    string sRentExpectedValue = RentalCapExpected(user);
                    Assert.AreEqual(sRentExpectedValue, sRentActualValue); //QQ: how to make non-stop on this verification?
                    pageCongratulations.btnStartShopping.Click();

                    //Home Page
                    MidSleep();
                    //HomePage pageHome = new HomePage(driverForRun); //QQ: how to skip validation declared controls here
                    ValidateMember(user);
                    //Assert.AreEqual(sRentExpectedValue,pageHome.GetRentalCap());
                });
            }
        }

        [Test]
        [Category("MemberCreationTest")]
        [Order(2)]
        public void MemberCreationFromCart()
        {

        }

    }
}
