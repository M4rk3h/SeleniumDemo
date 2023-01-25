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
                driver.Manage().Window.Maximize();
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
                // Click on the button
                applyLink.Click();
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking element");

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

        [Fact]
        public void _7BeSubmittedWhenValid()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                // Open Maximized
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(ApplyUrl);

                // Send FirstName
                driver.FindElement(By.Id("FirstName")).SendKeys("Mark");
                // Send LastName
                driver.FindElement(By.Id("LastName")).SendKeys("Baber");
                // Send FrequentFlyerNumber
                driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("01633");
                // Send Age
                driver.FindElement(By.Id("Age")).SendKeys("30");
                // Send Income
                driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("50000");
                // Send Relationship Status
                driver.FindElement(By.Id("Married")).Click();
                // Create iWebElement for Dropdown
                IWebElement howHeardField = driver.FindElement(By.Id("BusinessSource"));
                //SelectElement Elements in Dropdown
                SelectElement businessSource = new SelectElement(howHeardField);
                // Assert the default (ensure we have selected correct dropdown)
                Assert.Equal("I'd Rather Not Say", businessSource.SelectedOption.Text);
                // Lets write what we found (Text / Value)
                output.WriteLine("Get Attributes and Text from BusinessSource Dropdown:");
                // Get all available options
                foreach (IWebElement option in businessSource.Options)
                {
                    output.WriteLine($"*Value*: {option.GetAttribute("value")}  *Text*: {option.Text}");
                }

                //Assert the number of options
                Assert.Equal(5, businessSource.Options.Count);
                // Select Option via Value
                businessSource.SelectByValue("Email");
                // Select Option via Text
                businessSource.SelectByText("Internet Search");
                // Select by Index
                businessSource.SelectByIndex(3);

                driver.FindElement(By.Id("TermsAccepted")).Click();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);

                driver.FindElement(By.Id("SubmitApplication")).Click();

                Assert.StartsWith("Application Complete", driver.Title);
                Assert.Equal("ReferredToHuman", driver.FindElement(By.Id("Decision")).Text);
                Assert.NotEmpty(driver.FindElement(By.Id("ReferenceNumber")).Text);
                Assert.Equal("Mark Baber", driver.FindElement(By.Id("FullName")).Text);
                Assert.Equal("30", driver.FindElement(By.Id("Age")).Text);
                Assert.Equal("50000", driver.FindElement(By.Id("Income")).Text);
                Assert.Equal("Married", driver.FindElement(By.Id("RelationshipStatus")).Text);
                Assert.Equal("Word of Mouth", driver.FindElement(By.Id("BusinessSource")).Text);
            }
        }

        [Fact]
        public void _8BeSubmittedWhenValidationErrorsCorrected()
        {
            // set some constants
            const string firstName = "Mark";
            const string invalidAge = "17";
            const string validAge = "30";

            using (IWebDriver driver = new ChromeDriver())
            {
                // Open Maximized
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(ApplyUrl);
                driver.FindElement(By.Id("FirstName")).SendKeys(firstName);
                driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("01633");
                driver.FindElement(By.Id("Age")).SendKeys(invalidAge);
                driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("50000");
                driver.FindElement(By.Id("Married")).Click();
                IWebElement howHeardField = driver.FindElement(By.Id("BusinessSource"));
                SelectElement businessSource = new SelectElement(howHeardField);
                businessSource.SelectByIndex(3);
                driver.FindElement(By.Id("TermsAccepted")).Click();
                driver.FindElement(By.Id("SubmitApplication")).Click();
                DemoHelper.Pause();

                // Assert validation failed
                var validationErr = driver.FindElements(By.CssSelector(".validation-summary-errors > ul > li"));
                Assert.Equal(2, validationErr.Count);
                Assert.Equal("Please provide a last name", validationErr[0].Text);
                Assert.Equal("You must be at least 18 years old", validationErr[1].Text);

                // Fix validation errors
                driver.FindElement(By.Id("LastName")).SendKeys("Baber");
                driver.FindElement(By.Id("Age")).Clear();
                driver.FindElement(By.Id("Age")).SendKeys(validAge);

                // Resubmit form
                driver.FindElement(By.Id("SubmitApplication")).Click();

                Assert.StartsWith("Application Complete", driver.Title);
                Assert.Equal("ReferredToHuman", driver.FindElement(By.Id("Decision")).Text);
                Assert.NotEmpty(driver.FindElement(By.Id("ReferenceNumber")).Text);
                Assert.Equal("Mark Baber", driver.FindElement(By.Id("FullName")).Text);
                Assert.Equal("30", driver.FindElement(By.Id("Age")).Text);
                Assert.Equal("50000", driver.FindElement(By.Id("Income")).Text);
                Assert.Equal("Married", driver.FindElement(By.Id("RelationshipStatus")).Text);
                Assert.Equal("Word of Mouth", driver.FindElement(By.Id("BusinessSource")).Text);
            }
        }
    }
}
