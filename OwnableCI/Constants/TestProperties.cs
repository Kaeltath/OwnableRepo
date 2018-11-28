using OwnableCI.TestDataObjs;
using OwnableCI.XMLParsers;
using System.Collections.Generic;
using OwnableCI_TestLib.Enums;
using System;

namespace OwnableCI_TestLib.Constants
{
    public static class TestProperties
    {
        public static List<TestUser> oneTimeUsers = new XMLParseTestUsers().UsersForTests(false);
        public static List<TestUser> reusableUsers = new XMLParseTestUsers().UsersForTests(true);
        public static List<CreditCard> cards = new XMLParseCreditCards().CardsForTests();
        public static List<CodeAndState> statesCodes = new XMLParseStatesAndCodes().CardsForTests();
        public static List<KeyValuePair<TestUser, BrowserType>> oneTimeTestSoure = GetTestSource(oneTimeUsers);
        public static List<KeyValuePair<TestUser, BrowserType>> reusableTestSoure = GetTestSource(reusableUsers);

        private static List<KeyValuePair<TestUser, BrowserType>> GetTestSource(List<TestUser> users)
        {
            List<KeyValuePair<TestUser, BrowserType>> testSource = new List<KeyValuePair<TestUser, BrowserType>>();
            foreach (var user in users)
            {
                foreach (BrowserType browser in Enum.GetValues(typeof(BrowserType)))
                {
                    testSource.Add(new KeyValuePair<TestUser, BrowserType>(user, browser));
                }
            }
            return testSource;
        }

    }
}