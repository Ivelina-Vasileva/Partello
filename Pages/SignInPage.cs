using OpenQA.Selenium;

namespace Partello.Pages
{
    public class SignInPage : BasePage
    {
        public SignInPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement SignInEmailField => _driver.FindElement(By.Id("email"));
        public IWebElement SignInPasswordField => _driver.FindElement(By.Id("password"));
        public IWebElement SubmitSignInButton => _driver.FindElement(By.XPath("//button[contains(., 'Sign In') or @type='submit']"));
        public IWebElement ContinueWithGoogleButton => _driver.FindElement(By.XPath("//button[contains(., 'Continue with Google')]"));
        public IWebElement CreateAnAccountButton => _driver.FindElement(By.XPath("//a[@href='/sign-up' and contains(., 'Create an account')]"));

        public void Login(string email, string password)
        {
            SignInEmailField.Clear();
            SignInEmailField.SendKeys(email);

            SignInPasswordField.Clear();
            SignInPasswordField.SendKeys(password);

            SubmitSignInButton.Click();
        }
    }
}

