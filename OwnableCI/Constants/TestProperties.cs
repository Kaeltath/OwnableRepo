using OwnableCI.TestDataObjs;
using OwnableCI.XMLParsers;
using System.Collections.Generic;
using OwnableCI_TestLib.Enums;
using System;
using OwnableCI.Enums;
using System.Text.RegularExpressions;
using OwnableCI.SeviceClasses;

namespace OwnableCI_TestLib.Constants
{
    public static class TestProperties
    {
        public static List<CreditCard> cards = new XMLParseCreditCards().CardsForTests();
        public static List<CodeAndState> statesCodes = new XMLParseStatesAndCodes().CardsForTests();
        public static List<KeyValuePair<TestUser, BrowserType>> NewUsersTestSource = new TestSourceProvider().GetTestSource(TestGroup.NewUserTests);
        public static List<KeyValuePair<TestUser, BrowserType>> MemberCreationTestSource = new TestSourceProvider().GetTestSource(TestGroup.MemberCreationTest);
        public static List<KeyValuePair<TestUser, BrowserType>> WishListAndCartTestsSource = new TestSourceProvider().GetTestSource(TestGroup.WishListAndCartTests);


    }
}