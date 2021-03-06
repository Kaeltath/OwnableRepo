﻿using NUnit.Framework;
using OpenQA.Selenium;
using OwnableCI.Pages;
using OwnableCI.ServiceClasses;
using OwnableCI.TestDataObjs;
using OwnableCI_TestLib.Constants;
using OwnableCI_TestLib.Enums;
using OwnableCI_TestLib.Tests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace OwnableCI.Tests
{
    [TestFixtureSource(typeof(TestProperties), "NewUsersTestSource")]
    [TestFixture]
    public class NewUserTests : BaseTest
    {
        private bool expectedResults;

        public NewUserTests(KeyValuePair<TestUser, BrowserType> source)
        {
            user = source.Key;
            currentBrowser = source.Value;
        }

        [Test]
        [Category("UserIntearctionTest")]
        [Order(1)]
        public void UserCreation()
        {
            TestAction(() =>
            {
                string currentTestName = "User Creation";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                SignUpPage page = new SignUpPage(driverForRun);
                MidSleep();
                page.inputEmail.SendKeys(user.Email);
                page.inputPassword.SendKeys(user.Password);
                //page.chkIAgreeToTheTerms.Click(); //obsolete control
                TestHelper.JSexecutorClick(page.btnLogIn, driverForRun);
                bool SignUpSuccesfull = page.ValidateSignUp();
                SmallSleep();
                if (!SignUpSuccesfull)
                {
                    TestHelper.JSexecutorClick(page.btnClose, driverForRun);
                }
                Assume.That(SignUpSuccesfull, "User already exists");
            });
        }

        [Test]
        [Category("UserIntearctionTest")]
        [Ignore("Not usable due to romoval of email validation flow")]
        [Order(2)]
        public void EMailValidation()
        {
            TestAction(() =>
            {
                string currentTestName = "EMailValidation";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                SignInPage page = new SignInPage(driverForRun);
                Assert.That(page.Login(user), "Login did not passed, incorect credentials");
                var emailValidationPanel = driverForRun.FindElements(By.XPath("//div[@class='email-validation-holder']"));
                var emailValidationSkipButton = driverForRun.FindElements(By.XPath("//div[@class='email-validation-holder']//button[text()='Skip']"));
                expectedResults = (emailValidationPanel.Count > 0 && emailValidationSkipButton.Count > 0);
                Assume.That(expectedResults, "Not first Login, not running the test");
                Assert.That(ValidateEmail());
            });
        }

        [Test]
        [Category("UserIntearctionTest")]
        [Order(3)]
        public void UserLogin()
        {
            TestAction(() =>
            {
                string currentTestName = "UserLogin";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                bool newUserCreated = false;
                IWebElement SignIn;
                try { SignIn = driverForRun.FindElement(By.XPath("//button[text()=' Sign In ']")); }
                catch
                {
                    newUserCreated = true;
                }

                if (!newUserCreated)
                {
                    SignInPage page = new SignInPage(driverForRun);
                    Assert.That(page.Login(user), "Login failed");
                }

                BigSleep(); //moved from ValidateUser()
                var element = driverForRun.FindElement(By.XPath("//button[contains(@class,'d-md-block')]/div[text()=' START BROWSING ']"));
                TestHelper.JSexecutorClick(element, driverForRun);
                Assume.That(ValidateUser(user), "Login successfull, but not for user, but for member");
            });           
        }

        [Test]
        [Category("UserIntearctionTest")]
        [Order(4)]
        public void MemberLogin()
        {
            TestAction(() =>
            {
                string currentTestName = "MemberLogin";
                log.Debug("Starting " + currentTestName + " Test;");
                log.Debug("For user " + user.FirstName + user.LastName + ";");
                Assume.That(ValidateMember(user), "Login successfull, but not for member, but for user");
            });
        }

        public bool ValidateEmail()
        {
            try
            {
                SignInPage page = new SignInPage(driverForRun);
                driverForRun.Navigate().GoToUrl("http://gmail.com");
                driverForRun.FindElement(By.Id("identifierId")).SendKeys(user.Email);
                var element = driverForRun.FindElement(By.Id("identifierNext"));
                TestHelper.JSexecutorClick(element, driverForRun);
                Thread.Sleep(2000);
                driverForRun.FindElement(By.XPath("//input[@name='password']")).SendKeys(user.Password);
                element = driverForRun.FindElement(By.Id("passwordNext"));
                TestHelper.JSexecutorClick(element, driverForRun);
                Thread.Sleep(3000);
                element = driverForRun.FindElement(By.XPath("//div[@class='UI']//table/tbody/tr/td//div[@role='link']"));
                TestHelper.JSexecutorClick(element, driverForRun);
                Thread.Sleep(1000);
                element = driverForRun.FindElement(By.XPath("//a[contains(text(),'ownable.auth0.com')]"));
                TestHelper.JSexecutorClick(element, driverForRun);
                Thread.Sleep(4000);
                ReadOnlyCollection<string> windowHandles = driverForRun.WindowHandles;
                driverForRun.SwitchTo().Window(windowHandles[1]);
                driverForRun.Close();
                driverForRun.SwitchTo().Window(windowHandles[0]);
                page.NavigateToPage();
                Thread.Sleep(4000);
                page.Login(user);
                Thread.Sleep(4000);
                var Testelement = driverForRun.FindElements(By.XPath("//div[@class='email-validation-holder']//button[text()='Skip']"));
                if (Testelement.Count > 0)
                { Testelement[0].Click(); }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

    }

}
