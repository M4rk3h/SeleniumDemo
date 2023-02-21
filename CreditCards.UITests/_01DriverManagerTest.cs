using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;


namespace CreditCards.UITests
{

    public class _01DriverManagerTest
    {
        [Fact]
        [Trait("Category", "Smoke")]
        public void _1ChromeSession()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());

            var driver = new ChromeDriver();

            driver.Quit();
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void _2EdgeSession()
        {
            new DriverManager().SetUpDriver(new EdgeConfig());

            var driver = new EdgeDriver();

            driver.Quit();
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void _3FirefoxSession()
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());

            var driver = new ChromeDriver();

            driver.Quit();
        }
        
    }
}