using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MTTP.Framework.Tests.Pages
{
    public class NjuskaloListingPage : BasePage
    {
        private readonly By Title = By.CssSelector("h1");
        private readonly By Body = By.TagName("body");

        public NjuskaloListingPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public void WaitUntilLoaded()
        {
            Visible(Body);
            Visible(Title);
        }

        public string GetTitleText()
        {
            return Visible(Title).Text.Trim();
        }

        public bool HasSomePriceOrInfo()
        {
            // “best effort” – ne ovisimo o točnoj strukturi cijene
            var priceLike = Driver.FindElements(By.XPath("//*[contains(.,'€') or contains(.,'EUR') or contains(.,'Cijena')]"));
            return priceLike.Any(e => e.Displayed);
        }
    }
}
