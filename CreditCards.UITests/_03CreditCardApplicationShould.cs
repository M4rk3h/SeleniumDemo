using CreditCards.UITests.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CreditCards.UITests
{
    // Add Test
    [Trait("Category", "Applications")]
    public class _03CreditCardApplicationShould : IClassFixture<FirefoxDriverFixture>
    {
        private const string HomeUrl = "http://localhost:44108/";
        private const string ApplyUrl = "http://localhost:44108/Apply";
        private readonly FirefoxDriverFixture FirefoxDriverFixture;

        public _03CreditCardApplicationShould(FirefoxDriverFixture firefoxDriverFixture)
        {
            FirefoxDriverFixture = firefoxDriverFixture;
            FirefoxDriverFixture.Driver.Manage().Cookies.DeleteAllCookies();
            FirefoxDriverFixture.Driver.Navigate().GoToUrl("about:blank");

        }

        [Fact]
        public void _1BeInitiatedFromHomePage_NewLowRate()
        {
            var homePage = new HomePage(FirefoxDriverFixture.Driver);
            homePage.NavigateTo();
            ApplicationPage applicationPage = homePage.ClickApplyLowRateLink();
            applicationPage.EnsurePageLoaded();

        }

        [Fact]
        public void _2BeInitiatedFromHomePage_EasyApplication()
        {
            var homePage = new HomePage(FirefoxDriverFixture.Driver);
            homePage.NavigateTo();
            homePage.WaitForEasyApplicationCarouselPage();
            ApplicationPage applicationPage = homePage.ClickApplyEasyApplicationLink();
            applicationPage.EnsurePageLoaded();
        }

        [Fact]
        public void _3BeInitiatedFromHomePage_CustomerService()
        {
            var homePage = new HomePage(FirefoxDriverFixture.Driver);
            homePage.NavigateTo();
            
            WebDriverWait wait = new WebDriverWait(FirefoxDriverFixture.Driver, TimeSpan.FromSeconds(35));
            // Use built in function instead of bespoke (above)
            IWebElement applyLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("customer-service-apply-now")));
            
            // Click on the button
            applyLink.Click();
        }

        [Fact]
        public void _4BeInitiatedFromHomePage_RandomGreeting()
        {
            var homePage = new HomePage(FirefoxDriverFixture.Driver);
            homePage.NavigateTo();

            IWebElement randomPartial = FirefoxDriverFixture.Driver.FindElement(By.PartialLinkText("- Apply Now!"));
            randomPartial.Click();
            // Wait 1 second
            DemoHelper.Pause();

            var appPage = new ApplicationPage(FirefoxDriverFixture.Driver);
            appPage.EnsurePageLoaded();
        }

        [Fact]
        public void _5BeInitiatedFromHomePage_RandomGreeting_XPath()
        {
            var homePage = new HomePage(FirefoxDriverFixture.Driver);
            homePage.NavigateTo();

            IWebElement randomPartial = FirefoxDriverFixture.Driver.FindElement(By.XPath("//a[text()[contains(.,'- Apply Now!')]]"));
            randomPartial.Click();
            // Wait 1 second
            //DemoHelper.Pause();

            var appPage = new ApplicationPage(FirefoxDriverFixture.Driver);
            appPage.EnsurePageLoaded();
        }

        [Fact]
        public void _6BIFHP_EA_PC()
        {
            var homePage = new HomePage(FirefoxDriverFixture.Driver);
            homePage.NavigateTo();
            DemoHelper.Pause();
            WebDriverWait wait = new WebDriverWait(FirefoxDriverFixture.Driver, TimeSpan.FromSeconds(11));

            // The below method is depreciated - source code copied into ExpectedConditions.cs
            // from https://github.com/DotNetSeleniumTools/DotNetSeleniumExtras/blob/master/src/WaitHelpers/ExpectedConditions.cs
            IWebElement applyLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Easy: Apply Now!")));
            applyLink.Click();
            DemoHelper.Pause();

            var appPage = new ApplicationPage(FirefoxDriverFixture.Driver);
            appPage.EnsurePageLoaded();
        }

        [Fact]
        public void _7BeSubmittedWhenValid()
        {
            const string FirstName = "Mark";
            const string LastName = "Baber";
            const string Number = "01633";
            const string Age = "30";
            const string Income = "50000";

            var applicationPage = new ApplicationPage(FirefoxDriverFixture.Driver);
            applicationPage.NavigateTo();

            applicationPage.EnterFirstName(FirstName);
            applicationPage.EnterLastName(LastName);
            applicationPage.EnterFrequentFlyerNumber(Number);
            applicationPage.EnterAge(Age);
            applicationPage.EnterGrossAnnualIncome(Income);
            applicationPage.ChooseMaritalStatusMarried();
            applicationPage.ChooseBusinessSourceTV();
            applicationPage.AcceptTerms();
            ApplicationCompletePage applicationCompletePage = applicationPage.SubmitApplication();
            DemoHelper.Pause();

            applicationCompletePage.EnsurePageLoaded();

            Assert.Equal("ReferredToHuman", applicationCompletePage.Decision);
            Assert.NotEmpty(applicationCompletePage.ReferenceNumber);
            Assert.Equal($"{FirstName} {LastName}", applicationCompletePage.FullName);
            Assert.Equal(Age, applicationCompletePage.Age);
            Assert.Equal(Income, applicationCompletePage.Income);
            Assert.Equal("Married", applicationCompletePage.RelationshipStatus);
            Assert.Equal("TV", applicationCompletePage.BusinessSource);
        }

        [Fact]
        public void _8BeSubmittedWhenValidationErrorsCorrected()
        {
            // set some constants
            const string firstName = "Mark";
            const string invalidAge = "17";
            const string validAge = "30";
            
            // Get and Set ApplicationPage
            // Get Driver from ApplicationPage and go to correct URL.
            var applicationPage = new ApplicationPage(FirefoxDriverFixture.Driver);
            applicationPage.NavigateTo();
            // Fill out the form
            applicationPage.EnterFirstName(firstName);
            // Don't enter last name
            applicationPage.EnterFrequentFlyerNumber("01633");
            applicationPage.EnterAge(invalidAge);
            applicationPage.EnterGrossAnnualIncome("50000");
            applicationPage.ChooseMaritalStatusMarried();
            applicationPage.ChooseBusinessSourceTV();
            applicationPage.AcceptTerms();
            applicationPage.SubmitApplication();
            // Assert validation failed
            Assert.Equal(2, applicationPage.ValidationErrorMessages.Count);
            Assert.Contains("Please provide a last name", applicationPage.ValidationErrorMessages);
            Assert.Contains("You must be at least 18 years old", applicationPage.ValidationErrorMessages);
            // Fix validation errors
            applicationPage.EnterLastName("Baber");
            applicationPage.ClearAge();
            applicationPage.EnterAge(validAge);
            // Resubmit form
            ApplicationCompletePage applicationCompletePage = applicationPage.SubmitApplication();
            // Check form submitted
            applicationCompletePage.EnsurePageLoaded();
        }
    }
}
