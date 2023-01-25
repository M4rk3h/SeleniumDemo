using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using Xunit;
using System.Collections.ObjectModel;

namespace CreditCards.UITests
{
    public class _1CreditCardWebAppShould
    {
        // Declare Constants
        private const string HomeUrl = "http://localhost:44108/";
        private const string AboutUrl = "http://localhost:44108/Home/About";
        private const string HomeTitle = "Home Page - Credit Cards";
        
        // add a test
        [Fact]
        [Trait("Category", "Smoke")]

        public void _1LoadApplicationPage()
        {
            // create an instance of a browser (ChromeDriver, FirefoxDriver & EdgeDriver)

            using (IWebDriver driver = new ChromeDriver())
            {
                // Open Maximized
                driver.Manage().Window.Maximize();
                // Go to URL
                driver.Navigate().GoToUrl(HomeUrl);
                // Pause the browser
                DemoHelper.Pause();
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
                DemoHelper.Pause();
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
                DemoHelper.Pause();
                driver.Navigate().GoToUrl(AboutUrl);
                DemoHelper.Pause();
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
                DemoHelper.Pause();

                driver.Navigate().GoToUrl(HomeUrl);
                IWebElement generationTokenElement = driver.FindElement(By.Id("GenerationToken"));
                string initialToken = generationTokenElement.Text;
                DemoHelper.Pause();


                driver.Navigate().Back();
                DemoHelper.Pause();

                driver.Navigate().Forward();
                DemoHelper.Pause();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

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
                DemoHelper.Pause();

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
    }
}
