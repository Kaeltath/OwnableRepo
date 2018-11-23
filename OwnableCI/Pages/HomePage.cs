using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Support.PageObjects;
using OwnableCI.Pages;
using OwnableCI.TestDataObjs;
using System.Threading;

namespace OwnableCI_TestLib.Pages
{
    public class HomePage : BasePage
    {
        #region IWebelements        

        #region Header:
        [FindsBy(How = How.XPath, Using = "//img[@class='logo_color']")]
        public IWebElement lblOwnableLogo;


        [FindsBy(How = How.XPath, Using = "//li[@class='nav-item']//a[text()='Home']")]
        public IWebElement lnkHome;


        [FindsBy(How = How.XPath, Using = "//li[@class='nav-item']//a[text()='How it works']")]
        public IWebElement lnkHowItWorks;


        [FindsBy(How = How.XPath, Using = "//li[@class='nav-item']//a[text()='Faqs']")]
        public IWebElement lnkFAQS;


        [FindsBy(How = How.XPath, Using = "//button[text()=' Sign In ']")]
        public IWebElement btnSignIn;

        
        [FindsBy(How = How.XPath, Using = "//button[text()=' Sign Up ']")]
        public IWebElement btnSignUp;

        
        [FindsBy(How = How.XPath, Using = "//*[@role='listbox' and @placeholder='All']")]
        public IWebElement lstAll;


        [FindsBy(How = How.XPath, Using = "//input[@container='body']")]
        public IWebElement inputSearch;


        [FindsBy(How = How.XPath, Using = "//button[text()='Search']")]
        public IWebElement btnSearch;


        [FindsBy(How = How.XPath, Using = "//div[@class='icons']//i[text()='shopping_cart']")]
        public IWebElement lblCart;

        //[FindsBy(How = How.XPath, Using = "//button[@routerlink='wishlist']")]
        //public IWebElement btnWishlist;

        [FindsBy(How = How.XPath, Using = "//div[@id='v-pills-tab']//button[text()=' Top Deals ']")]
        public IWebElement btnTopDeals;

        [FindsBy(How = How.XPath, Using = "//div[@id='v-pills-tab']/button[9]")]
        public IWebElement btnTVs;

        [FindsBy(How = How.XPath, Using = "//div[@id='v-pills-tab']//button[text()=' Electronics ']")]
        public IWebElement btnElectronics;

        [FindsBy(How = How.XPath, Using = "//div[@id='v-pills-tab']//button[text()=' Computers ']")]
        public IWebElement btnComputers;

        [FindsBy(How = How.XPath, Using = "//div[@id='v-pills-tab']//button[text()=' Appliances ']")]
        public IWebElement btnAppliances;

        [FindsBy(How = How.XPath, Using = "//div[@id='v-pills-tab']//button[text()=' Cameras ']")]
        public IWebElement btnCameras;

        [FindsBy(How = How.XPath, Using = "//div[@id='v-pills-tab']//button[text()=' Game Consoles ']")]
        public IWebElement btnGameConsoles;

        [FindsBy(How = How.XPath, Using = "//div[@id='v-pills-tab']//button[text()=' Furniture ']")]
        public IWebElement btnFurniture;

        [FindsBy(How = How.XPath, Using = "//div[@id='v-pills-tab']//button[text()=' Cell Phones ']")]
        public IWebElement btnCellPhones;

        #endregion

        #region Home(Landing):
        //Looks like was removed
        //[FindsBy(How = How.XPath, Using = "//span[text()='get started!']")]
        //public IWebElement btnGetStarted;


        //[FindsBy(How = How.XPath, Using = "//div[@class='image-box']//img[contains(@src,'top_deals')]")]
        //public IWebElement lblTopDeals;

        //[FindsBy(How = How.XPath, Using = "//ul[@class='product-category-list']//*[text()='Top deals']")]
        //public IWebElement lnkCategory_TopDeals;


        //[FindsBy(How = How.XPath, Using = "//div[@class='image-box']//img[contains(@src,'tvs')]")]
        //public IWebElement lblTVs;

        ////ask why it's needed, and rewrite
        //[FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/section[1]/ng-component[1]/div[1]/div[1]/div[2]/product-category-grid[1]/ul[1]/li[2]/product-category-card[1]/a[1]/h3[1]")]
        ////[FindsBy(How = How.XPath, Using = "//ul[@class='product-category-list']//*[text()="TV's"]")]
        //public IWebElement lnkCategory_TVs;


        //[FindsBy(How = How.XPath, Using = "//div[@class='image-box']//img[contains(@src,'electronics')]")]
        //public IWebElement lblElectronics;

        //ask why it's needed, and rewrite
        //[FindsBy(How = How.XPath, Using = "//ul[@class='product-category-list']//*[text()='Electronics']")]
        //public IWebElement lnkCategory_Electronics;


        //[FindsBy(How = How.XPath, Using = "//div[@class='image-box']//img[contains(@src,'computers')]")]
        //public IWebElement lblComputers;

        //ask why it's needed, and rewrite
        //[FindsBy(How = How.XPath, Using = "//ul[@class='product-category-list']//*[text()='Computers']")]
        //public IWebElement lnkCategory_Computers;


        //[FindsBy(How = How.XPath, Using = "//div[@class='image-box']//img[contains(@src,'appliances')]")]
        //public IWebElement lblAppliances;

        //[FindsBy(How = How.XPath, Using = "//ul[@class='product-category-list']//*[text()='Appliances']")]
        //public IWebElement lnkCategory_Appliances;


        //[FindsBy(How = How.XPath, Using = "//div[text()='EASY']//parent::div//a[text()='Learn more']")]
        //public IWebElement lnkEasy_LearnMore;


        [FindsBy(How = How.XPath, Using = "//div[text()='AFFORDABLE']//parent::div//a[text()='Learn more']")]
        public IWebElement lnkAffordable_LearnMore;


        [FindsBy(How = How.XPath, Using = "//div[text()='QUALITY']//parent::div//a[text()='Learn more']")]
        public IWebElement lnkQuality_LearnMore;
        #endregion

        #region Footer:
        [FindsBy(How = How.XPath, Using = "//button[text()='Sign Up']")]
        public IWebElement lnkAccount_SignUp;


        [FindsBy(How = How.XPath, Using = "//button[text()='Sign In']")]
        public IWebElement lnkAccount_SignIn;


        //[FindsBy(How = How.XPath, Using = "//a[text()='Top Deals']")]
        //public IWebElement lnkShop_TopDeals;


        //[FindsBy(How = How.XPath, Using = @"id(""wrapper"")/app-root[1]/ng-component[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/footer[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[3]/a[1]")]
        ////[FindsBy(How = How.XPath, Using = "//a[text()="TV's"]")]
        //public IWebElement lnkShop_TVs;


        //[FindsBy(How = How.XPath, Using = "//a[text()='Electronics']")]
        //public IWebElement lnkShop_Electronics;


        //[FindsBy(How = How.XPath, Using = "//a[text()='Computers']")]
        //public IWebElement lnkShop_Computers;


        //[FindsBy(How = How.XPath, Using = "//a[text()='Appliances']")]
        //public IWebElement lnkShop_Appliances;
        

        //[FindsBy(How = How.XPath, Using = "//a[text()='How it works']")]
        //public IWebElement lnkHelp_HowItWorks;


        //[FindsBy(How = How.XPath, Using = "//a[text()='FAQs']")]
        //public IWebElement lnkHelp_FAQs;


        //[FindsBy(How = How.XPath, Using = "//a[text()='Contact Us']")]
        //public IWebElement lnkHelp_ContactUs;


        //[FindsBy(How = How.XPath, Using = "//a[text()='Meet the Team']")]
        //public IWebElement lnkHelp_MeetTheTeam;


        //[FindsBy(How = How.XPath, Using = "//a[text()='Privacy Policy']")]
        //public IWebElement lnkLegal_PrivacyPolicy;


        //[FindsBy(How = How.XPath, Using = "//a[text()='Terms of Use']")]
        //public IWebElement lnkLegal_TermsOfUse;


        //[FindsBy(How = How.XPath, Using = "//a[text()='Return Info']")]
        //public IWebElement lnkLegal_ReturnInfo;


        //[FindsBy(How = How.XPath, Using = "//a[text()='Shipping Policy']")]
        //public IWebElement lnkLegal_ShippingPolicy;


        [FindsBy(How = How.XPath, Using = "//i[contains(@class,'twitter')]")]
        public IWebElement lblFollowUs_Twitter;


        [FindsBy(How = How.XPath, Using = "//i[contains(@class,'facebook')]")]
        public IWebElement lblFollowUs_Facebook;


        [FindsBy(How = How.XPath, Using = "//i[contains(@class,'instagram')]")]
        public IWebElement lblFollowUs_Instagram;


        [FindsBy(How = How.XPath, Using = "//i[contains(@class,'youtube')]")]
        public IWebElement lblFollowUs_YouTube;
        #endregion

        #region LoadedElements:
        protected Dictionary<string, IWebElement> logedUserControls;
        #endregion

        #endregion

        log4net.ILog logger;

        // <summary>
        /// Call the base class constructor
        /// </summary>
        /// <param name="browser"></param>
        public HomePage(IWebDriver browser) : base(browser)
        {
            logger = log4net.LogManager.GetLogger(typeof(HomePage));
            PageFactory.InitElements(driver, this);
            NavigateToPage();            
        }

        public HomePage(IWebDriver browser, bool navigate) : base(browser)
        {
            if (navigate)
            {
                PageFactory.InitElements(driver, this);
                NavigateToPage();
            } 
        }


        //TODO declare elements
        public void LoadLogedControls()
        {
            logedUserControls.Add("ELEMENT_NAME", driver.FindElement(By.XPath("")));

            foreach (KeyValuePair<string, IWebElement> pair in logedUserControls)
            {   
                try
                    {
                    if (!pair.Value.Displayed)
                    {
                        logger.Debug("Element " + pair.Key + " is not loadede, or not visible, please check the problem");
                    }
                }
                catch (Exception e)
                {
                    logger.ErrorFormat("Control {0} thrown an exception, message {1}", pair.Key, e.Message);
                    throw e;
                }
            }
        }

        public override bool Login(TestUser user)
        {
            SignInPage signIn = new SignInPage(driver);
            return signIn.Login(user);
        }

        public override void NavigateToPage(string parameter = "http://staging.ownable.us/app/home")
        {
            this.driver.Navigate().GoToUrl(parameter);
            InitPage(this);
        }

        public void OpenWishlist()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click()", driver.FindElement(By.XPath("//button[@routerlink='wishlist']")));
        }

        public void OpenCart()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click()", lblCart);
        }
    }
}
