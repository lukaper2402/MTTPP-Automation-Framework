using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Text;

namespace MTTP.Framework.Tests.Core
{
    public abstract class BaseTest
    {
        protected IWebDriver Driver = null!;
        protected WebDriverWait Wait = null!;

        protected string BaseUrl => TestContext.Parameters.Get("baseUrl", "https://www.njuskalo.hr");
        protected string Browser => TestContext.Parameters.Get("browser", "chrome");
        protected bool Headless => bool.TryParse(TestContext.Parameters.Get("headless", "false"), out var h) && h;

        private string ArtifactsDir => Path.Combine(TestContext.CurrentContext.WorkDirectory, "Artifacts");

        [SetUp]
        public void SetUp()
        {
            Driver = DriverFactory.Create(Browser, Headless);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                    SaveArtifacts();
            }
            catch { }
            finally
            {
                try { Driver?.Quit(); } catch { }
            }
        }

        private void SaveArtifacts()
        {
            Directory.CreateDirectory(ArtifactsDir);

            var testName = SanitizeFileName(TestContext.CurrentContext.Test.Name);
            var ts = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var prefix = $"{testName}_{ts}";

            // Screenshot
            if (Driver is ITakesScreenshot ss)
            {
                ss.GetScreenshot().SaveAsFile(Path.Combine(ArtifactsDir, $"{prefix}.png"));
            }

            // Page source
            File.WriteAllText(Path.Combine(ArtifactsDir, $"{prefix}.html"), Driver.PageSource, Encoding.UTF8);

            // URL
            File.WriteAllText(Path.Combine(ArtifactsDir, $"{prefix}.txt"), Driver.Url, Encoding.UTF8);
        }

        private static string SanitizeFileName(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
        }
    }
}
