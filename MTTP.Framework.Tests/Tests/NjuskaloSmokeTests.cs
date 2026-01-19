using NUnit.Framework;
using MTTP.Framework.Tests.Core;
using MTTP.Framework.Tests.Pages;


namespace MTTP.Framework.Tests.Tests
{


    public class NjuskaloSmokeTests : BaseTest
    {

        [Test,Category("Smoke")]
        public void HomePage_ShouldLoad()
        {
            var home = new NjuskaloHomePage(Driver, Wait);
            home.Open(BaseUrl);

            Assert.That(home.IsSearchVisible(), Is.True);
        }

        [Test, Category("Smoke")]
        public void Search_ShouldWork()
        {
            var home = new NjuskaloHomePage(Driver, Wait);
            home.Open(BaseUrl);
            home.Search("audi a3");

            Assert.That(Driver.Url, Does.Not.EndWith("njuskalo.hr/"), "Nismo otišli na stranicu rezultata.");
        }
        [Test, Category("Smoke")]
        public void Search_ShouldShowSomeResults()
        {
            var home = new NjuskaloHomePage(Driver, Wait);
            home.Open(BaseUrl);
            home.Search("audi a3");

            var results = new NjuskaloSearchResultsPage(Driver, Wait);
            results.WaitUntilLoaded();

            Assert.That(results.HasAnyHeading(), Is.True, "Rezultati nemaju heading (stranica možda nije učitana).");
            Assert.That(results.GetResultLinkCount(), Is.GreaterThan(0), "Nema rezultata nakon pretrage.");
        }

        [Test, Category("Regression")]
        public void OpenFirstResult_ShouldOpenListingPage()
        {
            var home = new NjuskaloHomePage(Driver, Wait);
            home.Open(BaseUrl);
            home.Search("audi a3");

            var results = new NjuskaloSearchResultsPage(Driver, Wait);
            results.WaitUntilLoaded();
            results.OpenFirstResult();

            var listing = new NjuskaloListingPage(Driver, Wait);
            listing.WaitUntilLoaded();

            Assert.That(listing.GetTitleText().Length, Is.GreaterThan(3), "Naslov oglasa je prazan ili prekratak.");
        }

        [Test, Category("Regression")]
        public void BackFromListing_ShouldReturnToResults()
        {
            var home = new NjuskaloHomePage(Driver, Wait);
            home.Open(BaseUrl);
            home.Search("audi a3");

            var results = new NjuskaloSearchResultsPage(Driver, Wait);
            results.WaitUntilLoaded();
            results.OpenFirstResult();

            var listing = new NjuskaloListingPage(Driver, Wait);
            listing.WaitUntilLoaded();

            Driver.Navigate().Back();

            var resultsAgain = new NjuskaloSearchResultsPage(Driver, Wait);
            resultsAgain.WaitUntilLoaded();

            Assert.That(resultsAgain.GetResultLinkCount(), Is.GreaterThan(0), "Nakon povratka nema rezultata.");
        }
    }
}
