using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Partello.Pages
{
    public class SignInPage : BasePage
    {
        public SignInPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement SignInEmailField => _driver.FindElement(By.Id("email"));
        public IWebElement SignInPasswordField => _driver.FindElement(By.Id("password"));
        public IWebElement SubmitSignInButton => _driver.FindElement(By.XPath("//input[@id='email']/ancestor::form//button[@type='submit']"));
        public IWebElement ContinueWithGoogleButton => _driver.FindElement(By.XPath("//div[contains(@class, 'mt-4')]/button[@type='button']"));
        public IWebElement CreateAnAccountLink => _driver.FindElement(By.XPath("//a[contains(@href, '/sign-in')]"));
        public IWebElement GoogleEmailField => _driver.FindElement(By.Id("identifierId"));
        public IWebElement NextButton => _driver.FindElement(By.XPath("//*[@id='identifierNext']//button"));
        public IWebElement GooglePasswordField => _driver.FindElement(By.XPath("//input[@type='password']"));
        public IWebElement PasswordNextButton => _driver.FindElement(By.XPath("//*[@id='passwordNext']//button"));
        public By RateLimitTimerIconLocator => By.CssSelector("svg.lucide-clock");

        public void Login(string email, string password)
        {
            SignInEmailField.Clear();
            SignInEmailField.SendKeys(email);

            SignInPasswordField.Clear();
            SignInPasswordField.SendKeys(password);

            SubmitSignInButton.Click();
        }
        public void EnterLoginEmail(string email)
        {
            SignInEmailField.Clear();
            SignInEmailField.SendKeys(email);
        }

        public void EnterLoginPassword(string password)
        {
            SignInPasswordField.Clear();
            SignInPasswordField.SendKeys(password);
        }
        public void EnterGoogleEmail(string email)
        {
            GoogleEmailField.Clear();
            GoogleEmailField.SendKeys(email);
        }
        public void EnterGooglePassword(string password)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => GooglePasswordField.Displayed);
            GooglePasswordField.Clear();
            GooglePasswordField.SendKeys(password);
        }
        public void ClickNextButton()
        {
            NextButton.Click();
        }
        public void ClickPasswordNextButton()
        {
            PasswordNextButton.Click();
        }
        public void ClickCreateAccountLink()
        {
            CreateAnAccountLink.Click();
        }

    }
}

