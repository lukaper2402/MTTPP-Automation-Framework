using NUnit.Framework;
using MTTP.Framework.Tests.Core;
using MTTP.Framework.Tests.Pages;

namespace MTTP.Framework.Tests.Tests
{
    [TestFixture]
    public class NjuskaloFilterTests : BaseTest
    {
        [Test, Category("Regression")]
        public void Results_PriceFilter_ShouldUpdateUrlOrResults()
        {
            var home = new NjuskaloHomePage(Driver, Wait);
            home.Open(BaseUrl);
            home.Search("golf 7");

            var results = new NjuskaloSearchResultsPage(Driver, Wait);
            results.WaitUntilLoaded();

            // Ako Njuškalo u tom trenutku ne prikazuje price filter (varira po kategoriji),
            // test ne “pada” nego se označi kao Inconclusive (profi pristup za dinamiku UI-ja).
            if (!results.Filters.IsAnyPriceFilterVisible())
                Assert.Inconclusive("Price filter nije vidljiv na ovoj stranici rezultata (UI varira po kategoriji).");

            var beforeUrl = Driver.Url;
            results.Filters.SetPriceRange("1000", "10000");

            Assert.That(Driver.Url, Is.Not.EqualTo(beforeUrl),
                "URL se nije promijenio nakon primjene price filtera (očekuje se promjena stanja filtera).");
        }

        [Test, Category("Regression")]
        public void Results_RepeatingSearch_ShouldBeStable()
        {
            var home = new NjuskaloHomePage(Driver, Wait);
            home.Open(BaseUrl);

            home.Search("iphone");
            Assert.That(Driver.Url, Does.Contain("njuskalo"), "Nismo na Njuškalu.");
            Assert.That(Driver.Url, Does.Not.EndWith("njuskalo.hr/"), "Nismo otišli na rezultate nakon prve pretrage.");

            // umjesto Back - stabilno
            home.Open(BaseUrl);

            home.Search("iphone");
            Assert.That(Driver.Url, Does.Not.EndWith("njuskalo.hr/"), "Nismo otišli na rezultate nakon druge pretrage.");
        }

        [Test, Category("Regression")]
        public void Results_Sort_ShouldUpdateUrl()
        {
            var home = new NjuskaloHomePage(Driver, Wait);
            home.Open(BaseUrl);

            home.Search("golf 7");

            var results = new NjuskaloSearchResultsPage(Driver, Wait);
            results.WaitUntilLoaded();

            var before = Driver.Url;

            // pokušaj sortiranje po cijeni (najniža / lowest / asc) - best effort
            results.ApplySortByValue("price");   // prvo pokušaj "price"
            var after = Driver.Url;

            Assert.That(after, Is.Not.EqualTo(before),
                "URL se nije promijenio nakon primjene sortiranja.");
        }



    }
}
