using ApprovalTests;
using ApprovalTests.Reporters;
using CreditCards.UITests.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Xunit;

namespace CreditCards.UITests
{
    public class _02CreditCardWebAppShould
    {
        // Declare Constants
        private const string AboutUrl = "http://localhost:44108/Home/About";
        
        // add a test
        [Fact]
        [Trait("Category", "Smoke")]

        public void _1LoadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver() )
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();
            }
        }

        // add a new test
        [Fact]
        [Trait("Category", "Smoke")]
        public void _2ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                string initialToken = homePage.GenerationToken;

                driver.Navigate().GoToUrl(AboutUrl);
                driver.Navigate().Back();
                
                homePage.EnsurePageLoaded(); 

                string reloadedToken = homePage.GenerationToken;
                Assert.NotEqual(initialToken, reloadedToken);
            }
        }

        [Fact]
        public void _3DisplayProductsAndRates()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                //DemoHelper.Pause();

                // Assert each td
                Assert.Equal("Easy Credit Card", homePage.Products[0].name);
                Assert.Equal("20% APR", homePage.Products[0].interestRate);
                Assert.Equal("Silver Credit Card", homePage.Products[1].name);
                Assert.Equal("18% APR", homePage.Products[1].interestRate);
                Assert.Equal("Gold Credit Card", homePage.Products[2].name);
                Assert.Equal("17% APR", homePage.Products[2].interestRate);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void _4ContactUsFooterTAB()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                homePage.ClickContactFooterLink();

                // Change Tabs
                ReadOnlyCollection<string> getTabs = driver.WindowHandles;
                string homePageTab = getTabs[0];
                string contactTab = getTabs[1];
                // switch to tab x
                driver.SwitchTo().Window(contactTab);
                //DemoHelper.Pause();

                Assert.EndsWith("/Home/Contact", driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void _5AlertIfLiveChatClosed()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();
                homePage.ClickLiveChatFooterLink();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

                Assert.Equal("Live chat is currently closed.", alert.Text);
                alert.Accept();
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void _6NavigateToAboutUsWhenCancelClicked()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();
                homePage.ClickAboutUsLink();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IAlert alertBox = wait.Until(ExpectedConditions.AlertIsPresent());
                alertBox.Accept();

                Assert.EndsWith("/Home/About", driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void _7NotNavigateToAboutUsWhenCancelClicked()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();
                homePage.ClickAboutUsLink();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IAlert alertBox = wait.Until(ExpectedConditions.AlertIsPresent());
                alertBox.Dismiss();
                DemoHelper.Pause();

                Assert.EndsWith("#", driver.Url);
            }
        }


            [Fact]
        [Trait("Category", "Smoke")]
        public void _8DisplayCookieUseMessage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                
                driver.Manage().Cookies.AddCookie(new Cookie("acceptedCookies", "true"));
                driver.Navigate().Refresh();

                Assert.False(homePage.IsCookieMessagePresent);

                driver.Manage().Cookies.DeleteCookieNamed("acceptedCookies");
                driver.Navigate().Refresh();

                Assert.True(homePage.IsCookieMessagePresent);
            }
        }

        /*
        [Fact]
        [UseReporter(typeof(BeyondCompareReporter))] //Switched fromBeyondCompare4Reporter to BeyondCompareReporter
        public void _XRenderAboutPage()
        {
            // Make sure the screenshot is saved as follows:
            // "(ClassName).(TestName).approved.bmp" i.e
            // "_02CreditCardWebAppShould._XRenderAboutPage.approved.bmp"
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(AboutUrl);

                ITakesScreenshot screenie = (ITakesScreenshot)driver;

                Screenshot screenshot = screenie.GetScreenshot();

                screenshot.SaveAsFile("aboutpage.bmp", ScreenshotImageFormat.Bmp);

                FileInfo file = new FileInfo("aboutpage.bmp");
                Approvals.Verify(file);
            }
        }
        */

        
        
    }
}
