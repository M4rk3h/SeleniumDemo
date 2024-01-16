using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;

namespace CreditCards.UITests
{
    public sealed class EdgeDriverFixture : IDisposable
    {
        public IWebDriver Driver { get; private set; }

        public EdgeDriverFixture()
        {
            Driver = new EdgeDriver();
        }

        public void Dispose()
        {
            Driver.Dispose();
        }
    }
}
