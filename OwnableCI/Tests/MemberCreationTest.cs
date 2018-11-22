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
            TestAction(() =>
            {
                string currentTestName = "Member Creation from SignUp";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                SignUpPage pageSignUp = new SignUpPage(driverForRun);
                MidSleep();
                pageSignUp.inputEmail.SendKeys(user.Email);
                pageSignUp.inputPassword.SendKeys(user.Password);
                //pageSignUp.chkIAgreeToTheTerms.Click(); //removed from current version
                pageSignUp.btnLogIn.Click();
                log.Debug("Start get your rental cap");
                MidSleep();
                driverForRun.FindElement(By.XPath("//button/div[text()=' GET YOUR RENTAL CAP ']")).Click();

                //TODO: move set "Personal Info" to separate method
                MidSleep();
                MemberCreationFirstPage pagePersonalInfo = new MemberCreationFirstPage(driverForRun);
                pagePersonalInfo.inputFirstName.SendKeys(user.FirstName);
                pagePersonalInfo.inputLastName.SendKeys(user.LastName);
                pagePersonalInfo.inputHomeAddress.SendKeys(user.Adress);
                pagePersonalInfo.inputCity.SendKeys(user.City);
                pagePersonalInfo.lstState.Click();
                driverForRun.FindElement(By.XPath("//span[text()='Texas']")).Click();
                pagePersonalInfo.inputZipCode.SendKeys(user.ZipCode);
                pagePersonalInfo.inputMobile.SendKeys(user.Mobile);
                pagePersonalInfo.inputBirthdate.SendKeys(user.BirthDate);
                pagePersonalInfo.chkAgreement.Click();
                pagePersonalInfo.btnNext.Click();

                //TODO: move set "Income Info" to separate method
                MidSleep();
                MemberCreationSecondPage pageIncomeInfo = new MemberCreationSecondPage(driverForRun);
                pageIncomeInfo.inputMonthlyIncome.SendKeys(user.MonthlyIncome);
                pageIncomeInfo.inputCompany.SendKeys(user.Company);
                pageIncomeInfo.inputYearsEmployed.SendKeys(user.YearsEmployed);
                pageIncomeInfo.inputSSN.SendKeys("12345"+user.LastDigitsOFSocial);
                pageIncomeInfo.btnBecomeMember.Click();

                //MEMBERSHIP AGREEMENT
                MidSleep();
                MemberCreationThirdPage pageMembershipAgreement = new MemberCreationThirdPage(driverForRun);
                pageMembershipAgreement.btnAgree.Click(); //do this for going to txtMemberSignature field
                SmallSleep();
                pageMembershipAgreement.txtMemberSignature.Click(); //set digital signature
                SmallSleep();
                pageMembershipAgreement.btnAgree.Click();

                //APPLICATION DISCLOSURE
                MidSleep();
                MemberCreationFourthPage pageApplicationDisclosure = new MemberCreationFourthPage(driverForRun);
                pageApplicationDisclosure.btnAgree.Click();

                //CONGRATULATIONS!
                MidSleep();
                MemberCreationFifthPage pageCongratulations = new MemberCreationFifthPage(driverForRun);
                string sRentValue = pageCongratulations.txtRentalCapValue.Text;
                Assert.AreEqual("$500.00", sRentValue);
                pageCongratulations.btnStartShopping.Click();

                //Home Page
                MidSleep();
                HomePage pageHome = new HomePage(driverForRun);
                ValidateMember(user);
                Assert.AreEqual("$500.00",pageHome.GetRentalCap());
            });
        }

        [Test]
        [Category("MemberCreationTest")]
        [Order(2)]
        public void MemberCreationFromCart()
        {

        }

    }
}
