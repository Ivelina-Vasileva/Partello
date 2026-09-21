using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V119.Preload;
using OpenQA.Selenium.Support.UI;
using Partello.Config;
using Partello.Pages;
using Partello.Utils;
using Reqnroll;

namespace Partello.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly IWebDriver _driver;
        private readonly SignInPage _signInPage;
        private readonly TestSettings _settings;
        public LoginSteps(IWebDriver driver, SignInPage signUpPage, TestSettings settings)
        {
            _driver = driver;
            _signInPage = signUpPage;
            _settings = settings;
        }

        [Given(@"I have navigated to the Partello sign-in page")]
        public void GivenIHaveNavigatedToThePartelloSignInPage()
        {
            _driver.Navigate().GoToUrl(_settings.BaseUrl + "/sign-in");
        }

        [When(@"I enter ""(.*)"" as login email")]
        public void WhenIEnterAsLoginEmail(string email)
        {
            _signInPage.EnterLoginEmail(email);
        }

        [When(@"I enter ""(.*)"" as login password")]
        public void WhenIEnterAsLoginPassword(string password)
        {
            _signInPage.EnterLoginPassword(password);
        }

        [When(@"I submit the form with my registered account credentials")]
        public void WhenISubmitTheFormWithMyRegisteredAccountCredentials()
        {
            _signInPage.Login(_settings.LoginEmail, _settings.LoginPassword);
        }

        [When(@"I click the Sign In submit button")]
        public void WhenIClickTheSignInSubmitButton()
        {
            _signInPage.SubmitSignInButton.Click();
        }

        [When(@"I click Continue with Google button")]
        public void WhenIClickContinueWithGoogleButton()
        {
            _signInPage.ContinueWithGoogleButton.Click();
        }

        [When(@"I enter google email as login email")]
        public void WhenIEnterGoogleEmailAsLoginEmail()
        {
            _signInPage.EnterGoogleEmail(_settings.GoogleEmail);
            _signInPage.ClickNextButton();
        }

        [When(@"I enter google password")]
        public void WhenIEnterGooglePassword()
        {
            _signInPage.EnterGooglePassword(_settings.GooglePassword);
            _signInPage.ClickPasswordNextButton();
        }

        [When(@"I attempt to sign in rapidly with invalid credentials (.*) times")]
        public void WhenIAttemptToSignInRapidlyWithInvalidCredentialsTimes(int attemptsCount)
        {
            var wait = new WebDriverWait (_driver, TimeSpan.FromSeconds(5));
            _signInPage.EnterLoginEmail("ivelinavasileva543@abv.bg");

            for (int i = 0; i < attemptsCount; i++)
            {
                _signInPage.EnterLoginPassword("WrongPassword123!");
                ClickSafe.Click(_driver, () => _signInPage.SubmitSignInButton);

                wait.Until(d =>
                {
                    try
                    {
                        var btn = _signInPage.SubmitSignInButton;
                        return btn.Enabled && btn.GetAttribute("disabled") == null;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return false;
                    }
                });
            }
        }

        [Then(@"I should be redirected to the dashboard page")]
        public void ThenIShouldBeRedirectedToTheDashboardPage()
        {
            _driver.WaitForUrlToContain("/settings");
            Assert.That(_driver.Url, Does.Contain("settings"));
        }

        [Then(@"I should see the rate limit cooldown message")]
        public void ThenIShouldSeeTheRateLimitCooldownMessage()
        {
            var icon = _driver.WaitForVisible(_signInPage.RateLimitTimerIconLocator, timeoutSeconds: 10);
            Assert.That(icon.Displayed, Is.True);
        }
    }
}
