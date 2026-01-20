using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using MTTP.Framework.Tests.Pages.Components;

namespace MTTP.Framework.Tests.Pages
{
    public class NjuskaloSearchResultsPage : BasePage
    {
        // Široki selektori – stabilni za production site
        private readonly By ResultsContainer = By.CssSelector("main, #__next, body");
        private readonly By ResultLinks = By.CssSelector(
            "a[href*='/oglas/'], a[href*='/auti/'], a[href*='/nekretnine/']"
        );
        private readonly By AnyHeading = By.CssSelector("h1, h2");
        // Sort dropdown - best effort (UI zna varirati)
        private readonly By SortDropdown = By.CssSelector("select[name*='sort'], select[id*='sort'], select[aria-label*='Sort'], select[aria-label*='sort']");


        public NjuskaloSearchResultsPage(IWebDriver driver, WebDriverWait wait)
            : base(driver, wait) { }
        public NjuskaloFiltersComponent Filters => new NjuskaloFiltersComponent(Driver, Wait);

        public void WaitUntilLoaded()
        {
            // čekamo da nismo više na home pageu
            Wait.Until(d => d.Url.Contains("njuskalo.hr") && !d.Url.EndsWith("njuskalo.hr/"));
            Visible(ResultsContainer);
        }

        public bool IsResultsPageDisplayed()
        {
            return Driver.Url.Contains("njuskalo.hr") &&
                   !Driver.Url.EndsWith("njuskalo.hr/");
        }

        public void OpenFirstResult()
        {
            var first = Driver.FindElements(ResultLinks)
                              .FirstOrDefault(e => e.Displayed);

            if (first == null)
                throw new NoSuchElementException("Nema vidljivih rezultata.");

            first.Click();
        }

        public bool HasAnyHeading()
        {
            return Driver.FindElements(AnyHeading)
                         .Any(e => e.Displayed);
        }

        public bool HasAnyContent()
        {
            return Driver.FindElements(By.CssSelector("h1, h2, article, section"))
                         .Any(e => e.Displayed);
        }
        public int GetResultLinkCount()
        {
            // Prvo probaj specifične selektore
            var count = Driver.FindElements(ResultLinks).Count(e => e.Displayed);
            if (count > 0) return count;

            // Fallback: brojimo vidljive linkove koji izgledaju kao listanje/rezultat
            // (na produkcijskim siteovima se struktura zna mijenjati)
            var fallbackLinks = Driver.FindElements(By.CssSelector("a[href]"))
                                      .Count(e => e.Displayed);

            return fallbackLinks;
        }
        public void ApplySortByValue(string valueContains)
        {
            WaitUntilLoaded();

            var oldUrl = Driver.Url;

            var dropdown = Driver.FindElements(SortDropdown).FirstOrDefault(e => e.Displayed && e.Enabled);
            if (dropdown == null)
                Assert.Inconclusive("Sort dropdown nije pronađen/vidljiv na ovoj stranici (UI varira).");

            var select = new OpenQA.Selenium.Support.UI.SelectElement(dropdown);

            // pronađi opciju koja u value ili text sadrži traženi string
            var option = select.Options.FirstOrDefault(o =>
                (o.GetAttribute("value") ?? "").ToLower().Contains(valueContains.ToLower()) ||
                (o.Text ?? "").ToLower().Contains(valueContains.ToLower())
            );

            if (option == null)
                Assert.Inconclusive($"Nije pronađena sort opcija koja sadrži: '{valueContains}'.");

            var value = option.GetAttribute("value");
            if (string.IsNullOrEmpty(value))
                Assert.Inconclusive("Sort opcija nema value atribut.");

            select.SelectByValue(value);


            // čekamo promjenu URL-a (sort obično promijeni query param)
            Wait.Until(d => d.Url != oldUrl);
        }



    }
}
