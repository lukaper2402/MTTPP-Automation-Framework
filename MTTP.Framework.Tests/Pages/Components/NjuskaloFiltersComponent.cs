using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MTTP.Framework.Tests.Pages.Components
{
    public class NjuskaloFiltersComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public NjuskaloFiltersComponent(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }

        // Best-effort selektori (Njuškalo zna mijenjati DOM)
        private By PriceMinInput => By.CssSelector("input[placeholder*='Od'], input[aria-label*='Od'], input[name*='priceFrom'], input[id*='priceFrom']");
        private By PriceMaxInput => By.CssSelector("input[placeholder*='Do'], input[aria-label*='Do'], input[name*='priceTo'], input[id*='priceTo']");
        private By ApplyButton => By.XPath("//button[contains(.,'Primijeni') or contains(.,'Apply') or contains(.,'Filtriraj')]");

        public bool IsAnyPriceFilterVisible()
        {
            return _driver.FindElements(PriceMinInput).Any(e => e.Displayed) ||
                   _driver.FindElements(PriceMaxInput).Any(e => e.Displayed);
        }

        public void SetPriceRange(string? min, string? max)
        {
            if (!string.IsNullOrWhiteSpace(min))
            {
                var minEl = FindVisible(PriceMinInput);
                minEl.Clear();
                minEl.SendKeys(min);
            }

            if (!string.IsNullOrWhiteSpace(max))
            {
                var maxEl = FindVisible(PriceMaxInput);
                maxEl.Clear();
                maxEl.SendKeys(max);
            }

            // Ako postoji gumb "Primijeni" klikni, inače probaj Enter
            var apply = _driver.FindElements(ApplyButton).FirstOrDefault(e => e.Displayed && e.Enabled);
            if (apply != null) apply.Click();
            else
            {
                // fallback: Enter na max inputu ako postoji
                var maxEl = _driver.FindElements(PriceMaxInput).FirstOrDefault(e => e.Displayed);
                maxEl?.SendKeys(Keys.Enter);
            }

            WaitForUrlChange();
        }

        private IWebElement FindVisible(By by)
        {
            return _wait.Until(d =>
            {
                var el = d.FindElements(by).FirstOrDefault(e => e.Displayed && e.Enabled);
                return el;
            })!;
        }

        private void WaitForUrlChange()
        {
            var old = _driver.Url;
            _wait.Until(d => d.Url != old);
        }
    }
}
