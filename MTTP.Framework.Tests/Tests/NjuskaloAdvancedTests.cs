using NUnit.Framework;
using MTTP.Framework.Tests.Core;
using MTTP.Framework.Tests.Pages;
using OpenQA.Selenium;


namespace MTTP.Framework.Tests.Tests
{
    [TestFixture]
    [Category("Regression")]
    public class NjuskaloAdvancedTests : BaseTest
    {
        [TestCase("golf")]
        [TestCase("iphone")]
        [TestCase("bicikl")]
        [TestCase("ps5")]
        [TestCase("stan zagreb")]
        public void Search_DataDriven_ShouldShowResults(string term)
        {
            var home = new NjuskaloHomePage(Driver, Wait);
            home.Open(BaseUrl);
            home.Search(term);

            var results = new NjuskaloSearchResultsPage(Driver, Wait);
            results.WaitUntilLoaded();

            Assert.That(results.IsResultsPageDisplayed(), Is.True,
                $"Stranica s rezultatima nije prikazana za '{term}'.");
            Assert.That(results.HasAnyContent(), Is.True,
                $"Rezultati nemaju vidljiv sadržaj za '{term}'.");

        }

        [Test]
        public void Search_Nonsense_ShouldStillShowResultsPage()
        {
            var home = new NjuskaloHomePage(Driver, Wait);
            home.Open(BaseUrl);
            home.Search("asdfqwerty123456___nope");

            var results = new NjuskaloSearchResultsPage(Driver, Wait);
            results.WaitUntilLoaded();

            Assert.That(results.IsResultsPageDisplayed(), Is.True,
                "Aplikacija nije pravilno obradila besmislen pojam pretrage.");
        }


        [Test]
        public void Results_Scrolling_ShouldKeepPageResponsive()
        {
            var home = new NjuskaloHomePage(Driver, Wait);
            home.Open(BaseUrl);
            home.Search("golf 7");

            var results = new NjuskaloSearchResultsPage(Driver, Wait);
            results.WaitUntilLoaded();

            var before = results.GetResultLinkCount();

            // scroll na dno – “best effort”
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            MTTP.Framework.Tests.Core.WaitExtensions.WaitForPageToBeStable(Driver, 10);


            var after = results.GetResultLinkCount();

            // Ne mora uvijek biti više rezultata (ovisno o stranici),
            // ali stranica mora ostati “živa” i imati rezultate.
            Assert.That(after, Is.GreaterThan(0), "Nakon scrolla nema rezultata (stranica se možda nije učitala).");
            Assert.That(before, Is.GreaterThan(0), "Nije bilo rezultata ni prije scrolla.");
        }
    }
}
