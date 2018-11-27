using OwnableCI.TestDataObjs;
using OwnableCI.XMLParsers;
using System.Collections.Generic;

namespace OwnableCI_TestLib.Constants
{
    public static class TestProperties
    {
        public static List<TestUser> oneTimeUsers = new XMLParseTestUsers().UsersForTests(false);
        public static List<TestUser> reusableUsers = new XMLParseTestUsers().UsersForTests(true);
        public static List<CreditCard> cards = new XMLParseCreditCards().CardsForTests();
        public static List<CodeAndState> statesCodes = new XMLParseStatesAndCodes().CardsForTests();
    }
}