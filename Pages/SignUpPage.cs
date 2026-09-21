using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Partello.Utils;

namespace Partello.Pages
{
    public class SignUpPage : BasePage
    {
        public SignUpPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement SignUpEmailField => _driver.FindElement(By.Id("email"));
        public IWebElement SignUpPasswordField => _driver.FindElement(By.Id("password"));
        public IWebElement TogglePasswordVisibilityButton => _driver.FindElement(By.XPath("//input[@id='password']/following-sibling::button | //button[@aria-label='Show password' or @aria-label='Hide password']"));
        public IWebElement SubmitSignUpButton => _driver.FindElement(By.XPath("//form[.//input[@id='email']]//button[@type='submit']"));
        //public IWebElement SignUpContinueWithGoogleButton => _driver.FindElement(By.XPath("//button[@type='button'][.//path[@fill='#4285F4']]"));
        private readonly By _googleBtnLocator = By.CssSelector("button.rounded-full.border-gray-300");
        public IWebElement SignInToExistingAccountLink => _driver.FindElement(By.XPath("//a[contains(@href, '/sign-in')]"));
        private By ErrorMessageLocator => By.XPath("//div[@id='form-error']");

        public void EnterEmail(string email)
        {
            SignUpEmailField.Clear();
            SignUpEmailField.SendKeys(email);
        }
        public void EnterPassword(string password)
        {
            SignUpPasswordField.Clear();
            SignUpPasswordField.SendKeys(password);
        }
        public string GetErrorMessageText()
        {
            IWebElement ErrorMessageElement = _driver.FindElement(ErrorMessageLocator);
            return ErrorMessageElement.Text;
        }
        public bool IsEmailFieldInvalid()
        {
            IWebElement emailInput = _driver.FindElement(By.Id("email"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;

            return (bool)js.ExecuteScript("return !arguments[0].validity.valid;", emailInput);
        }
        public bool IsPasswordFieldInvalid()
        {
            IWebElement passwordInput = _driver.FindElement(By.Id("password"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;

            return (bool)js.ExecuteScript("return !arguments[0].validity.valid;", passwordInput);
        }
        public string GetEmailValidationMessage()
        {
            IWebElement emailInput = _driver.FindElement(By.Id("email"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;

            return (string)js.ExecuteScript("return arguments[0].validationMessage;", emailInput);
        }

        public bool IsSignUpContinueWithGoogleButtonReady()
        {
            var btn = _driver.WaitForElementClickable(_googleBtnLocator, timeoutSeconds: 5);
            return btn != null && btn.Displayed && btn.Enabled;
        }
       public void ClickToggleVisibilityButton()
        {
            var button = _driver.WaitForElementClickable(TogglePasswordVisibilityButton, timeoutSeconds: 5);
            new Actions(_driver)
        .MoveToElement(button)
        .Click()
        .Perform();
        }
        public string GetPasswordFieldType()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

            var passElement = _driver.WaitForVisible(SignUpPasswordField, timeoutSeconds: 5);
            return passElement.GetAttribute("type");
        }
    }
}


