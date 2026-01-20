using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MTTP.Framework.Tests.Pages
{
    public class NjuskaloLoginPage : BasePage
    {
        // “best effort” selektori - Njuškalo može mijenjati DOM
        private readonly By EmailInput = By.CssSelector("input[type='email'], input[name*='email'], input[id*='email']");
        private readonly By PasswordInput = By.CssSelector("input[type='password'], input[name*='pass'], input[id*='pass']");
        private readonly By SubmitButton = By.CssSelector("button[type='submit'], button[name*='login'], button[id*='login']");
        private readonly By AnyErrorText = By.CssSelector("[class*='error'], [class*='invalid'], [role='alert'], .alert, .error");

        public NjuskaloLoginPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public void Open()
        {
            Driver.Navigate().GoToUrl("https://www.njuskalo.hr/prijava");
            AcceptCookiesIfPresent();
        }

        public bool IsLoginFormVisible()
        {
            return Driver.FindElements(EmailInput).Any(e => e.Displayed) &&
                   Driver.FindElements(PasswordInput).Any(e => e.Displayed);
        }

        public void SubmitEmpty()
        {
            var btn = Driver.FindElements(SubmitButton).FirstOrDefault(e => e.Displayed && e.Enabled);
            btn?.Click();
        }

        public void Login(string email, string password)
        {
            var emailEl = Visible(EmailInput);
            emailEl.Clear();
            emailEl.SendKeys(email);

            var passEl = Visible(PasswordInput);
            passEl.Clear();
            passEl.SendKeys(password);

            // submit
            var btn = Driver.FindElements(SubmitButton).FirstOrDefault(e => e.Displayed && e.Enabled);
            if (btn != null) btn.Click();
            else passEl.SendKeys(Keys.Enter);
        }

        public bool HasAnyError()
        {
            return Driver.FindElements(AnyErrorText).Any(e => e.Displayed);
        }

        public string GetFirstErrorText()
        {
            var el = Driver.FindElements(AnyErrorText).FirstOrDefault(e => e.Displayed);
            return el?.Text?.Trim() ?? "";
        }
    }
}
