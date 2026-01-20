using MTTP.Framework.ApiTests.Core;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Net;

namespace MTTP.Framework.ApiTests.Tests
{
    [TestFixture]
    [Category("API")]
    public class PostmanEchoApiTests : BaseApiTest
    {
        [Test]
        public async Task Get_ShouldReturn200()
        {
            var res = await Client.GetAsync("https://postman-echo.com/get?foo=bar");
            Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Get_ResponseBody_ShouldContainArgs()
        {
            var res = await Client.GetAsync("https://postman-echo.com/get?search=njuskalo");
            var json = JObject.Parse(await res.Content.ReadAsStringAsync());

            Assert.That(json["args"]?["search"]?.ToString(), Is.EqualTo("njuskalo"));
        }

        [Test]
        public async Task Get_ResponseTime_ShouldBeUnder2Seconds()
        {
            Stopwatch.Restart();
            var res = await Client.GetAsync("https://postman-echo.com/get?ping=1");
            Stopwatch.Stop();

            Assert.That(res.IsSuccessStatusCode, Is.True);
            Assert.That(Stopwatch.ElapsedMilliseconds, Is.LessThan(2000));
        }

        [Test]
        public async Task Status_404_ShouldReturn404()
        {
            var res = await Client.GetAsync("https://postman-echo.com/status/404");
            Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Headers_ShouldContainUserAgent()
        {
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Add("User-Agent", "MTTPP-ApiTests");

            var res = await Client.GetAsync("https://postman-echo.com/headers");
            var json = JObject.Parse(await res.Content.ReadAsStringAsync());
            var ua = json["headers"]?["user-agent"]?.ToString();

            Assert.That(ua, Does.Contain("MTTPP-ApiTests"));
        }
    }
}
