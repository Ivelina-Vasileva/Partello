using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Partello.Config;
using Partello.Pages;
using Partello.Utils;
using Reqnroll;
using System.Runtime.InteropServices;


namespace Partello.Steps
{
    [Binding]
    public class RegisterSteps
    {
        private readonly IWebDriver _driver;
        private readonly SignUpPage _signUpPage;
        private readonly SignInPage _signInPage;
        private readonly TestSettings _settings;
        private static string _generatedEmail = string.Empty;

        public RegisterSteps(IWebDriver driver, SignUpPage signUpPage, SignInPage signInPage, TestSettings settings)
        {
            _driver = driver;
            _signUpPage = signUpPage;
            _signInPage = signInPage;
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

        [When(@"I enter ""(.*)"" as registration email")]
        public void WhenIEnterAsRegistrationEmail(string email)
        {
            _signUpPage.EnterEmail(email);
        }

        [When(@"I click the Sign In link on the sign-up page")]
        public void WhenIClickTheSignInLinkOnTheSignUpPage()
        {
            _signUpPage.SignInToExistingAccountLink.Click();
        }

        [When(@"I toggle the password visibility button")]
        public void WhenIToggleThePasswordVisibilityButton()
        {
            _signUpPage.TogglePasswordVisibilityButton.Click();
        }

        [Then(@"I should be successfully logged into the system")]
        public void ThenIShouldBeSuccessfullyLoggedIntoTheSystem()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => !d.Url.Contains("/sign-up"));
            Assert.That(_driver.Url, Does.Not.Contain("/sign-up"));
        }

        [Then(@"I should see the expected validation outcome for ""(.*)""")]
        public void ThenIShouldSeeTheExpectedValidationOutcomeFor(string testType)
        {
            if (testType == "existing_email")
            {
                string actualError = _signUpPage.GetErrorMessageText();
                Assert.That(actualError, Is.EqualTo("An account with this email already exists. Please sign in instead."));
            }
            else if (testType == "invalid_email")
            {
                bool isInvalid = _signUpPage.IsEmailFieldInvalid();
                Assert.That(isInvalid, Is.True, "Email field should be invalid in HTML5 validation.");
            }
            else if (testType == "short_password")
            {
                bool isPasswordInvalid = _signUpPage.IsPasswordFieldInvalid();
                Assert.That(isPasswordInvalid, Is.True);
            }
            else if (testType == "wrong_email" || testType == "wrong_password")
            {
                string actualError = _signUpPage.GetErrorMessageText();

                Assert.That(actualError, Is.EqualTo("Invalid email or password. Please try again."));
            }
        }

        [Then(@"I should be redirected to the sign-in page")]
        public void ThenIShouldBeRedirectedToTheSignInPage()
        {
            _driver.WaitForUrlToContain("/sign-in");
            Assert.That(_driver.Url, Does.Contain("sign-in"));
        }

        [Then(@"the Continue with Google button should be visible and clickable")]
        public void ThenTheContinueWithGoogleButtonShouldBeVisibleAndClickable()
        {
            _signUpPage.IsSignUpContinueWithGoogleButtonReady();
        }

        [Then(@"the password field should mask the input")]
        public void ThenThePasswordFieldShouldMaskTheInput()
        {
            string currentType = _signUpPage.GetPasswordFieldType();
            currentType.Should().Be("password",
                "The password input should have type='password' to mask the characters.");
        }

        [Then(@"the password field should display the text in plain text")]
        public void ThenThePasswordFieldShouldDisplayTheTextInPlainText()
        {
            string currentType = _signUpPage.GetPasswordFieldType();
            currentType.Should().Be("text", "The password input should have type='text' to reveal the characters.");
        }
    }

}

