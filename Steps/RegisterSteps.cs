using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Partello.Config;
using Partello.Pages;
using Reqnroll;


namespace Partello.Steps
{
    [Binding]
    public class RegisterSteps
    {
        private readonly IWebDriver _driver;
        private readonly SignUpPage _signUpPage;
        private readonly TestSettings _settings;
        private static string _generatedEmail = string.Empty;

        public RegisterSteps(IWebDriver driver, SignUpPage signUpPage, TestSettings settings)
        {
            _driver = driver;
            _signUpPage = signUpPage;
            _settings = settings;
        }

        [Given(@"I have navigated to the Partello sign-up page")]
        public void GivenIHaveNavigatedToThePartelloSignUpPage()
        {
            _driver.Navigate().GoToUrl(_settings.BaseUrl + "/sign-up");
        }

        [When(@"I enter a unique registration email")]
        public void WhenIEnterAUniqueRegistrationEmail()
        {
            var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            _generatedEmail = $"qa_test_{timestamp}@partello-test.com";

            _signUpPage.SignUpEmailField.Clear();
            _signUpPage.SignUpEmailField.SendKeys(_generatedEmail);
        }

        [When(@"I enter ""(.*)"" as registration password")]
        public void WhenIEnterAsRegistrationPassword(string password)
        {
            _signUpPage.SignUpPasswordField.Clear();
            _signUpPage.SignUpPasswordField.SendKeys(password);
        }

        [When(@"I click the Sign Up submit button")]
        public void WhenIClickTheSignUpSubmitButton()
        {
            _signUpPage.SubmitSignUpButton.Click();
        }

        [Then(@"I should be successfully logged into the system")]
        public void ThenIShouldBeSuccessfullyLoggedIntoTheSystem()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => !d.Url.Contains("/sign-up"));
            Assert.That(_driver.Url, Does.Not.Contain("/sign-up"));
        }
    }
}
