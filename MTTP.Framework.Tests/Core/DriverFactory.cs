using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace MTTP.Framework.Tests.Core
{
    public static class DriverFactory
    {
        public static IWebDriver Create(string browser, bool headless)
        {
            browser = (browser ?? "chrome").ToLowerInvariant();

            return browser switch
            {
                "firefox" => CreateFirefox(headless),
                _ => CreateChrome(headless),
            };
        }

        private static IWebDriver CreateChrome(bool headless)
        {
            var options = new ChromeOptions();
            options.AddArgument("--window-size=1920,1080");
            if (headless) options.AddArgument("--headless=new");
            return new ChromeDriver(options);
        }

        private static IWebDriver CreateFirefox(bool headless)
        {
            var options = new FirefoxOptions();
            if (headless) options.AddArgument("-headless");
            return new FirefoxDriver(options);
        }
    }
}
