using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using OwnableCI_TestLib.Constants;
using System.Threading;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;



namespace OwnableCI_TestLib.Pages
{
    class HomePage : BasePage
    {

        //
        // to declare all needed elements
        //

        #region IWebelements        

        [FindsBy(How = How.XPath, Using = @"id(""header"")/div[2]/div[1]/div[1]/div[1]/a[1]/logo-color[1]/img[1]")]
        protected IWebElement lblOwnableLogo;


        [FindsBy(How = How.XPath, Using = @"id(""header"")/div[2]/div[1]/div[1]/div[2]/ul[1]/li[1]/a[1]")]
        protected IWebElement lnkHome;


        [FindsBy(How = How.XPath, Using = @"id(""header"")/div[2]/div[1]/div[1]/div[2]/ul[1]/li[2]/a[1]")]
        protected IWebElement lnkHowItWorks;


        [FindsBy(How = How.XPath, Using = @"id(""header"")/div[2]/div[1]/div[1]/div[2]/ul[1]/li[3]/a[1]")]
        protected IWebElement lnkFAQS;


        [FindsBy(How = How.XPath, Using = "//button[text() = ' Sign In ']")]
        protected IWebElement btnSignIn;

        
        [FindsBy(How = How.XPath, Using = "//button[text() = ' Sign Up ']")]
        protected IWebElement btnSignUp;

        
        [FindsBy(How = How.XPath, Using = @"id(""header"")/div[2]/div[1]/div[2]/div[1]/div[1]/ng-select[1]/div[1]/div[1]/div[2]/input[1]")]
        protected IWebElement lstAll;


        [FindsBy(How = How.XPath, Using = @"id(""search-container"")/input[1]")]
        protected IWebElement inputSearch;


        [FindsBy(How = How.XPath, Using = @"id(""search-container"")/div[2]/button[1]")]
        protected IWebElement btnSearch;


        [FindsBy(How = How.XPath, Using = @"id(""header"")/div[2]/div[1]/div[2]/div[1]/div[3]/div[1]/a[1]/i[1]")]
        protected IWebElement lblCart;


        [FindsBy(How = How.XPath, Using = @"id(""v-pills-tab"")/a[1]/span[1]")]
        protected IWebElement lnkTopDeals;


        [FindsBy(How = How.XPath, Using = @"id(""v-pills-tab"")/a[2]")]
        protected IWebElement lnkElectronics;


        [FindsBy(How = How.XPath, Using = @"id(""v-pills-tab"")/a[3]")]
        protected IWebElement lnkAppliances;


        [FindsBy(How = How.XPath, Using = @"id(""v-pills-tab"")/a[4]")]
        protected IWebElement lnkComputers;


        [FindsBy(How = How.XPath, Using = @"id(""v-pills-tab"")/a[5]")]
        protected IWebElement lnkTVs;


        [FindsBy(How = How.XPath, Using = @"id(""container"")/div[1]/div[1]/div[1]/div[1]/div[5]/div[1]/div[1]/div[1]/button[1]")]
        protected IWebElement btnGetStarted;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/section[1]/ng-component[1]/div[1]/div[1]/div[2]/product-category-grid[1]/ul[1]/li[1]/product-category-card[1]/a[1]/div[1]/img[1]")]
        protected IWebElement lblTopDeals;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/section[1]/ng-component[1]/div[1]/div[1]/div[2]/product-category-grid[1]/ul[1]/li[2]/product-category-card[1]/a[1]/div[1]/img[1]")]
        protected IWebElement lblElectronics;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/section[1]/ng-component[1]/div[1]/div[1]/div[2]/product-category-grid[1]/ul[1]/li[3]/product-category-card[1]/a[1]/div[1]/img[1]")]
        protected IWebElement lblAppliances;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/section[1]/ng-component[1]/div[1]/div[1]/div[2]/product-category-grid[1]/ul[1]/li[4]/product-category-card[1]/a[1]/div[1]/img[1]")]
        protected IWebElement lblComputers;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/section[1]/ng-component[1]/div[1]/div[1]/div[2]/product-category-grid[1]/ul[1]/li[5]/product-category-card[1]/a[1]/div[1]/img[1]")]
        protected IWebElement lblTVs;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/section[1]/ng-component[1]/div[1]/div[1]/div[2]/product-category-grid[1]/ul[1]/li[1]/product-category-card[1]/a[1]/h3[1]")]
        protected IWebElement lnkCategory_TopDeals;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/section[1]/ng-component[1]/div[1]/div[1]/div[2]/product-category-grid[1]/ul[1]/li[2]/product-category-card[1]/a[1]/h3[1]")]
        protected IWebElement lnkCategory_Electronics;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/section[1]/ng-component[1]/div[1]/div[1]/div[2]/product-category-grid[1]/ul[1]/li[3]/product-category-card[1]/a[1]/h3[1]")]
        protected IWebElement lnkCategory_Appliances;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/section[1]/ng-component[1]/div[1]/div[1]/div[2]/product-category-grid[1]/ul[1]/li[4]/product-category-card[1]/a[1]/h3[1]")]
        protected IWebElement lnkCategory_Computers;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/section[1]/ng-component[1]/div[1]/div[1]/div[2]/product-category-grid[1]/ul[1]/li[5]/product-category-card[1]/a[1]/h3[1]")]
        protected IWebElement lnkCategory_TVs;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/section[1]/ng-component[1]/div[1]/div[1]/info-plate[1]/div[1]/div[2]/div[1]/div[4]/a[1]")]
        protected IWebElement lnkEasy_LearnMore;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/section[1]/ng-component[1]/div[1]/div[1]/info-plate[1]/div[1]/div[2]/div[2]/div[4]/a[1]")]
        protected IWebElement lnkAffordable_LearnMore;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/section[1]/ng-component[1]/div[1]/div[1]/info-plate[1]/div[1]/div[2]/div[3]/div[4]/a[1]")]
        protected IWebElement lnkQuality_LearnMore;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/button[1]")]
        protected IWebElement lnkAccount_SignUp;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[3]/button[1]")]
        protected IWebElement lnkAccount_SignIn;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[2]/a[1]")]
        protected IWebElement lnkShop_TopDeals;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[3]/a[1]")]
        protected IWebElement lnkShop_Electronics;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[4]/a[1]")]
        protected IWebElement lnkShop_Appliances;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[5]/a[1]")]
        protected IWebElement lnkShop_Computers;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[6]/a[1]")]
        protected IWebElement lnkShop_TVs;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[3]/div[2]/a[1]")]
        protected IWebElement lnkHelp_HowItWorks;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[3]/div[3]/a[1]")]
        protected IWebElement lnkHelp_FAQs;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[3]/div[4]/a[1]")]
        protected IWebElement lnkHelp_ContactUs;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[3]/div[5]/a[1]")]
        protected IWebElement lnkHelp_MeetTheTeam;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[4]/div[2]/a[1]")]
        protected IWebElement lnkLegal_PrivacyPolicy;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[4]/div[3]/a[1]")]
        protected IWebElement lnkLegal_TermsOfUse;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[4]/div[4]/a[1]")]
        protected IWebElement lnkLegal_ReturnInfo;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[4]/div[5]/a[1]")]
        protected IWebElement lnkLegal_ShippingPolicy;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[5]/div[1]/div[1]/div[1]/div[2]/a[1]/span[1]/i[2]")]
        protected IWebElement lblFollowUs_Twitter;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[5]/div[1]/div[1]/div[1]/div[3]/a[1]/span[1]/i[2]")]
        protected IWebElement lblFollowUs_Facebook;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[5]/div[1]/div[1]/div[1]/div[4]/a[1]/span[1]/i[2]")]
        protected IWebElement lblFollowUs_Instagram;


        [FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[5]/div[1]/div[1]/div[1]/div[5]/a[1]/span[1]/i[2]")]
        protected IWebElement lblFollowUs_YouTube;
                


        //To be moved to LoginPage.cs
        //[FindsBy(How = How.XPath, Using = "//input[@type = 'email']")]
        //private IWebElement EmailField;

        //[FindsBy(How = How.XPath, Using = "//input[@type = 'password']")]
        //private IWebElement PassField;

        #endregion



        // <summary>
        /// Call the base class constructor
        /// </summary>
        /// <param name="browser"></param>
        public HomePage(IWebDriver browser) : base(browser) {

            PageFactory.InitElements(driver, this);
            NavigateToPage();            
        }

        
        public void Login( string username = "123", string pass = "321")
        {
            //ChromeOptions options = new ChromeOptions();
            btnSignUp.Click();            
            //EmailField.SendKeys(username);
            //PassField.SendKeys(pass);
            Thread.Sleep(50000);

        }

        public override void NavigateToPage(string parameter = "http://dev.ownable.us/app/home")
        {
            this.driver.Navigate().GoToUrl(parameter);
            InitPage(this);
        }
    }
}
