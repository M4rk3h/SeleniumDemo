using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace CreditCards.UITests
{
    public class _04JavaScriptExamples
    {
        [Fact]
        public void _1ClickOverlayedLink()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:44108/jsoverlay.html");
                //DemoHelper.Pause();

                // JS to find the HiddenLink via Element
                string script = "document.getElementById('HiddenLink').click();";
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript(script);

                //driver.FindElement(By.Id("HiddenLink")).Click();
                //DemoHelper.Pause();
                Assert.Equal("https://www.pluralsight.com/", driver.Url);
            }
        }

        [Fact]
        public void _2ClickOverlayedLinkText()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:44108/jsoverlay.html");
                //DemoHelper.Pause();

                // JS to find the HiddenLink via Element
                string script = "return document.getElementById('HiddenLink').innerHTML;";
                
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                string linkText = (string)js.ExecuteScript(script);

                //driver.FindElement(By.Id("HiddenLink")).Click();
                //DemoHelper.Pause();
                Assert.Equal("Go to Pluralsight", linkText);
            }
        }
    }
}
