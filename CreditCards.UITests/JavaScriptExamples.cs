using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CreditCards.UITests
{
    public class JavaScriptExamples
    {
        [Fact]
        public void ClickOverlayedLink() 
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:44108/jsoverlay.html");

                DemoHelper.Pause();

                driver.FindElement(By.Id("HiddenLink")).Click();

                Assert.Equal("https://pluralsight.com/", driver.Url);
            }
        }
    }
}
