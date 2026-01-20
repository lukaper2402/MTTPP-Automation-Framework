using NUnit.Framework;
using System.Diagnostics;
using System.Net.Http;

namespace MTTP.Framework.ApiTests.Core
{
    public abstract class BaseApiTest
    {
        protected HttpClient Client = null!;
        protected Stopwatch Stopwatch = null!;

        [SetUp]
        public void SetUp()
        {
            Client = new HttpClient();
            Stopwatch = new Stopwatch();
        }

        [TearDown]
        public void TearDown()
        {
            Client.Dispose();
        }
    }
}
