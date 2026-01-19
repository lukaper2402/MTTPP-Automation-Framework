using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Linq;

namespace MTTP.Framework.Tests.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        protected BasePage(IWebDriver driver, WebDriverWait wait)
        {
            Driver = driver;
            Wait = wait;
        }

        protected IWebElement Visible(By by) =>
            Wait.Until(ExpectedConditions.ElementIsVisible(by));

        protected IWebElement Clickable(By by) =>
            Wait.Until(ExpectedConditions.ElementToBeClickable(by));

        public void AcceptCookiesIfPresent()
        {
            var selectors = new[]
            {
        By.XPath("//button[contains(.,'Prihvati') or contains(.,'Prihvaćam') or contains(.,'Accept')]"),
        By.CssSelector("button[aria-label*='Prihvati']"),
    };

            foreach (var by in selectors)
            {
                try
                {
                    var btn = Wait.Until(d =>
                    {
                        var el = d.FindElements(by).FirstOrDefault();
                        return (el != null && el.Displayed && el.Enabled) ? el : null;
                    });

                    btn?.Click();
                    return;
                }
                catch { }
            }
        }

    }
}
