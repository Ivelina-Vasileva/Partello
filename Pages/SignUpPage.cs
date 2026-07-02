using OpenQA.Selenium;

namespace Partello.Pages
{
    public class SignUpPage : SignInPage
    {
        public SignUpPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement SignUpEmailField => _driver.FindElement(By.Id("email"));
        public IWebElement SignUpPasswordField => _driver.FindElement(By.Id("password"));
        public IWebElement SubmitSignUpButton => _driver.FindElement(By.XPath("//button[contains(., 'Sign up') or @type='submit']"));
        public IWebElement SignUpContinueWithGoogleButton => _driver.FindElement(By.XPath("//button[contains(., 'Continue with Google')]"));
        public IWebElement SignInToExistingAccountButton => _driver.FindElement(By.XPath("//a[@href='/sign-in' and contains(., 'Sign in to existing account')]"));
    }
}
