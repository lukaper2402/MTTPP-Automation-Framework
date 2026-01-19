using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;


namespace MTTP.Framework.Tests.Pages
{
    public class NjuskaloHomePage : BasePage
    {
        // Širi selektori jer se Njuškalo zna mijenjati
        private readonly By SearchInput = By.CssSelector(
            "input[type='search'], input[placeholder*='Pretra'], input[name*='search'], input[id*='search']"
        );
        private readonly By SearchButton = By.CssSelector("button[type='submit'], button[aria-label*='Pretra']");

        public NjuskaloHomePage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public void Open(string baseUrl)
        {
            Driver.Navigate().GoToUrl(baseUrl);
            AcceptCookiesIfPresent();
        }

        public bool IsSearchVisible()
        {
            try { return Visible(SearchInput).Displayed; }
            catch { return false; }
        }

        public void Search(string term)
        {
            var input = Visible(SearchInput);
            input.Clear();
            input.SendKeys(term);
            input.SendKeys(Keys.Enter);

            // čekaj da ode na rezultate (URL se promijeni)
            Wait.Until(d => d.Url.Contains("njuskalo") && !d.Url.EndsWith("/"));
        }

    }
}

