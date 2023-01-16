using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CreditCards.UITests
{
    // Add Test
    [Trait("Category", "Applications")]
    public class _2CreditCardApplicationShould
    {
        private const string HomeUrl = "http://localhost:44108/";
        private const string ApplyUrl = "http://localhost:44108/Apply";

        private readonly ITestOutputHelper output;

        public _2CreditCardApplicationShould(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void _1BeInitiatedFromHomePage_NewLowRate()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                // Open Maximized
                driver.Manage().Window.Maximize();

                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                // Grab ApplyLowRate button
                IWebElement applyLink = driver.FindElement(By.Name("ApplyLowRate"));
                // Click on the button
                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);

            }
        }

        [Fact]
        public void _2BeInitiatedFromHomePage_EasyApplication()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                // Open Maximized
                driver.Manage().Window.Maximize();

                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement rightCarousel = driver.FindElement(By.CssSelector("[data-slide='next']"));
                rightCarousel.Click();
                //Dynamic Pause for atleast 1 second
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                // Wait until the Element we want (Easy Apply Now, second img)
                IWebElement applyLink = wait.Until((d) => d.FindElement(By.LinkText("Easy: Apply Now!")));
                // Click on the button
                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void _3BeInitiatedFromHomePage_CustomerService()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigating to '{HomeUrl}'");
                driver.Navigate().GoToUrl(HomeUrl);
                
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element using explicity");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(35));

                //// <summary>
                //// Function to check if element is found,
                //// enabled and displayed = true
                //Func<IWebDriver, IWebElement> findEnabledAndVisible = delegate (IWebDriver d)
                //{
                //    var e = d.FindElement(By.ClassName("customer-service-apply-now"));

                //    if (e is null)
                //    {
                //        throw new NotFoundException();
                //    }

                //    if (e.Enabled && e.Displayed)
                //    {
                //        return e;
                //    }

                //    throw new NotFoundException();
                //};
                
                // Use built in function instead of bespoke (above)
                IWebElement applyLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("customer-service-apply-now")));
                
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element Displayed={applyLink.Displayed} Enabled={applyLink.Enabled}");
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking element");
                // Click on the button
                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void _4BeInitiatedFromHomePage_RandomGreeting()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                // Open Maximized
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement randomPartial = driver.FindElement(By.PartialLinkText("- Apply Now!"));
                randomPartial.Click();
                // Wait 1 second
                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void _5BeInitiatedFromHomePage_RandomGreeting_XPath()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                // Open Maximized
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement randomPartial = driver.FindElement(By.XPath("//a[text()[contains(.,'- Apply Now!')]]"));
                randomPartial.Click();
                // Wait 1 second
                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void _6BIFHP_EA_PC()
        {

            using (IWebDriver driver = new ChromeDriver())
            {
                // Open Maximized
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(11));

                // The below method is depreciated - source code copied into ExpectedConditions.cs
                // from https://github.com/DotNetSeleniumTools/DotNetSeleniumExtras/blob/master/src/WaitHelpers/ExpectedConditions.cs
                IWebElement applyLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Easy: Apply Now!")));

                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }


    }
}
