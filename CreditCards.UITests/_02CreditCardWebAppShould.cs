using ApprovalTests;
using ApprovalTests.Reporters;
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
        private const string HomeUrl = "http://localhost:44108/";
        private const string AboutUrl = "http://localhost:44108/Home/About";
        private const string HomeTitle = "Home Page - Credit Cards";
        
        // add a test
        [Fact]
        [Trait("Category", "Smoke")]

        public void _1LoadHomePage()
        {
            // create an instance of a browser (ChromeDriver)

            using (IWebDriver driver = new ChromeDriver() )
            {
                // Go to URL
                driver.Navigate().GoToUrl(HomeUrl);
                // Open Maximized
                driver.Manage().Window.Maximize();
                //DemoHelper.Pause();
                // Min
                driver.Manage().Window.Minimize();
                //DemoHelper.Pause();
                // Draw
                driver.Manage().Window.Size = new System.Drawing.Size(300, 400);
                //DemoHelper.Pause();
                // Move to Top Left (1,1)
                driver.Manage().Window.Position = new System.Drawing.Point(1, 1);
                //DemoHelper.Pause();
                // Move 
                driver.Manage().Window.Position = new System.Drawing.Point(50, 50);
                //DemoHelper.Pause();
                // Move 
                driver.Manage().Window.Position = new System.Drawing.Point(100, 100);
                //DemoHelper.Pause();
                // Move to Top Left (1,1)
                driver.Manage().Window.FullScreen();
                DemoHelper.Pause(5000);

                // Check Title = String
                Assert.Equal(HomeTitle, driver.Title);
                // Check we're at the correct url
                Assert.Equal(HomeUrl, driver.Url);
            }
        }

        // add a new test
        [Fact]
        [Trait("Category", "Smoke")]
        public void _2ReloadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                // Open Maximized
                driver.Manage().Window.Maximize();
                // Go to URL
                driver.Navigate().GoToUrl(HomeUrl);
                // Pause the browser
                //DemoHelper.Pause();
                // Reload the Page
                driver.Navigate().Refresh();
                // Check Title = String
                Assert.Equal(HomeTitle, driver.Title);
                // Check we're at the correct url
                Assert.Equal(HomeUrl, driver.Url);
            }
        }

        // add a new test
        [Fact]
        [Trait("Category", "Smoke")]
        public void _3ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                // Open Maximized
                driver.Manage().Window.Maximize();
                // Go to URL
                driver.Navigate().GoToUrl(HomeUrl);

                IWebElement generationTokenElement = driver.FindElement(By.Id("GenerationToken"));
                string initialToken = generationTokenElement.Text;


                // Nav to AboutURL
                //DemoHelper.Pause();
                driver.Navigate().GoToUrl(AboutUrl);
                //DemoHelper.Pause();
                driver.Navigate().Back();
                
                // Check Title = String
                Assert.Equal(HomeTitle, driver.Title);
                // Check we're at the correct url
                Assert.Equal(HomeUrl, driver.Url);

                string reloadedToken = driver.FindElement(By.Id("GenerationToken")).Text;
                Assert.NotEqual(initialToken, reloadedToken);
            }
        }
        [Fact]
        [Trait("Category", "Smoke")]
        public void _4ReloadHomePageOnForward()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                // Open Maximized
                driver.Manage().Window.Maximize();

                driver.Navigate().GoToUrl(AboutUrl);
                //DemoHelper.Pause();

                driver.Navigate().GoToUrl(HomeUrl);
                IWebElement generationTokenElement = driver.FindElement(By.Id("GenerationToken"));
                string initialToken = generationTokenElement.Text;
                //DemoHelper.Pause();


                driver.Navigate().Back();
                //DemoHelper.Pause();

                driver.Navigate().Forward();
                //DemoHelper.Pause();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

                // TODO: Load state from previously suspended application
                // TODO: assert that page was reloaded.
                string reloadedToken = driver.FindElement(By.Id("GenerationToken")).Text;
                Assert.NotEqual(initialToken, reloadedToken);
            }
        }

        [Fact]
        public void _5DisplayProductsAndRates()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                // Open Maximized
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(HomeUrl);
                //DemoHelper.Pause();

                // As a ReadOnly Collection, grab all elements within the table (td)
                ReadOnlyCollection<IWebElement> tableCells = driver.FindElements(By.TagName("td"));

                // Assert each td
                Assert.Equal("Easy Credit Card", tableCells[0].Text);
                Assert.Equal("20% APR", tableCells[1].Text);
                Assert.Equal("Silver Credit Card", tableCells[2].Text);
                Assert.Equal("18% APR", tableCells[3].Text);
                Assert.Equal("Gold Credit Card", tableCells[4].Text);
                Assert.Equal("17% APR", tableCells[5].Text);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void _6ContactUsFooterTAB()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                // Open Maximized
                driver.Manage().Window.Maximize();
                //DemoHelper.Pause();

                driver.FindElement(By.Id("ContactFooter")).Click();
                //DemoHelper.Pause();

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
        public void _7AlertIfLiveChatClosed()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                // Open Maximized
                driver.Manage().Window.Maximize();
                //DemoHelper.Pause();

                driver.FindElement(By.Id("LiveChat")).Click();
                DemoHelper.Pause();
                // switch to alert
                IAlert alert = driver.SwitchTo().Alert();
                // check it is correct
                Assert.Equal("Live chat is currently closed.", alert.Text);
                //DemoHelper.Pause();
                // Accept alert
                alert.Accept();
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void _8NotNavigateToAboutUsWhenCancelClicked()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                driver.Manage().Window.Maximize();
                //DemoHelper.Pause();

                driver.FindElement(By.Id("LearnAboutUs")).Click();
                //DemoHelper.Pause();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IAlert alertBox = wait.Until(ExpectedConditions.AlertIsPresent());

                alertBox.Dismiss();

                Assert.Equal(HomeTitle, driver.Title);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void _9NotDisplayCookieUseMessage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                // Go to Index
                driver.Navigate().GoToUrl(HomeUrl);
                // Create a cookie
                driver.Manage().Cookies.AddCookie(new Cookie("acceptedCookies", "true"));
                // Refresh the page
                driver.Navigate().Refresh();
                // return an emplty read only collection
                ReadOnlyCollection<IWebElement> message = driver.FindElements(By.Id("CookiesBeingUsed"));
                // Is the Message Empty?
                Assert.Empty(message);
                // Get the cookie named x
                Cookie cookieValue = driver.Manage().Cookies.GetCookieNamed("acceptedCookies");
                // Does the cookie from above = true
                Assert.Equal("true", cookieValue.Value);

                // remove cookie
                driver.Manage().Cookies.DeleteCookieNamed("acceptedCookies");
                driver.Navigate().Refresh();
                // check cookies being used
                Assert.NotNull(driver.FindElement(By.Id("CookiesBeingUsed")));
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
