using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using Xunit;

namespace CreditCards.UITests
{
    // Add Test
    [Trait("Category", "Applications")]
    public class _2CreditCardApplicationShould
    {
        private const string HomeUrl = "http://localhost:44108/";
        private const string ApplyUrl = "http://localhost:44108/Apply";

        [Fact]
        public void _6BeInitiatedFromHomePage_NewLowRate()
        {
            using (IWebDriver driver = new FirefoxDriver())
            {
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
        public void _7BeInitiatedFromHomePage_EasyApplication()
        {
            using (IWebDriver driver = new FirefoxDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement rightCarousel = driver.FindElement(By.CssSelector("[data-slide='next']"));
                rightCarousel.Click();
                // Wait 1 second
                DemoHelper.Pause(1000);

                // Grab ApplyLowRate button
                IWebElement applyLink = driver.FindElement(By.LinkText("Easy: Apply Now!"));
                // Click on the button
                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void _8BeInitiatedFromHomePage_CustomerService()
        {
            using (IWebDriver driver = new FirefoxDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement rightCarousel = driver.FindElement(By.CssSelector("[data-slide='next']"));
                rightCarousel.Click();
                // Wait 1 second
                DemoHelper.Pause(1000);

                rightCarousel.Click();
                // Wait 1 second
                DemoHelper.Pause(1000);

                // Grab ApplyLowRate button
                IWebElement applyLink = driver.FindElement(By.ClassName("customer-service-apply-now"));
                // Click on the button
                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }
    }
}
