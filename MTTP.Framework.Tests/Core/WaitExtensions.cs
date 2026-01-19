using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MTTP.Framework.Tests.Core
{
    public static class WaitExtensions
    {
        public static void WaitForPageToBeStable(IWebDriver driver, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(200)
            };
            wait.IgnoreExceptionTypes(typeof(WebDriverException), typeof(InvalidOperationException));

            // čekamo da se document.readyState vrati na 'complete'
            wait.Until(d =>
            {
                try
                {
                    var js = (IJavaScriptExecutor)d;
                    var ready = js.ExecuteScript("return document.readyState")?.ToString();
                    return ready == "complete";
                }
                catch
                {
                    return false;
                }
            });
        }

        public static void WaitUntil(Func<bool> condition, int timeoutSeconds = 10, int pollMs = 200)
        {
            var end = DateTime.UtcNow.AddSeconds(timeoutSeconds);

            while (DateTime.UtcNow < end)
            {
                try
                {
                    if (condition()) return;
                }
                catch
                {
                    // ignore and retry
                }

                Thread.Sleep(pollMs);
            }

            throw new WebDriverTimeoutException($"Condition not met within {timeoutSeconds}s.");
        }
    }
}
