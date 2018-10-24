using OwnableCI_TestLib.Constants;
using NUnit.Framework;
using OwnableCI.TestDataObjs;
using OwnableCI.Pages;
using OwnableCI_TestLib.Tests;


namespace OwnableCI.Tests
{
    [TestFixtureSource(typeof(TestProperties), "users")]
    [TestFixture]
    class UserCreationTest : BaseTest
    {
        private TestUser user;
        
        public UserCreationTest(TestUser inpUser)
        {
            this.user = inpUser;
        }

        
        [Test]
        public void FirsOne()
        {
            string currentTestName = "User Creation";
            log.Debug("Starting " + currentTestName + " Test;");
            log.Debug("For user " + user.FirstName+ user.LastName + ";");
            string birth = user.BirthDate;
            int i = 1 + 1;
            SignUpPage page = new SignUpPage(driverForRun);
            page.inputEmail.SendKeys("testtest@test.test");
            page.inputPassword.SendKeys("123qweQWE!@#");
            page.chkIAgreeToTheTerms.Click();
            page.btnLogIn.Click();
            MemberCreationFirstPage memberInput = new MemberCreationFirstPage(driverForRun);            

        }

        
    }
}
