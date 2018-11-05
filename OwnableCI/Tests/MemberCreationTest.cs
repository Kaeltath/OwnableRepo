using NUnit.Framework;
using OwnableCI.TestDataObjs;
using OwnableCI_TestLib.Constants;
using OwnableCI_TestLib.Tests;

namespace OwnableCI.Tests
{
    [TestFixtureSource(typeof(TestProperties), "users")]
    [TestFixture]
    class MemberCreationTest : BaseTest
    {
        TestUser user;

        public MemberCreationTest(TestUser user)
        {
            this.user = user;
        }

        public void MemberViableUser()
        {

        }
    }
}
