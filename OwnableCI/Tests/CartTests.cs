using NUnit.Framework;
using OwnableCI.TestDataObjs;
using OwnableCI_TestLib.Constants;
using OwnableCI_TestLib.Tests;

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
        public void AddingProductTest()
        {

        }

        [Test]
        public void RemovingProductTest()
        {

        }
    }

}
