using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;

namespace CreditCards.UITests
{
    public sealed class FirefoxDriverFixture : IDisposable
    {
        public IWebDriver Driver { get; private set; }

        public FirefoxDriverFixture()
        {
            Driver = new FirefoxDriver();
        }

        public void Dispose()
        {
            Driver.Dispose();
        }
    }
}
