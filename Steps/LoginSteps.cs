using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Partello.Config;
using Partello.Pages;
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

        [When(@"I submit the form with my registered account credentials")]
        public void WhenISubmitTheFormWithMyRegisteredAccountCredentials()
        {
            _signInPage.Login(_settings.LoginEmail, _settings.LoginPassword);
        }

        [Then(@"I should be redirected to the dashboard page")]
        public void ThenIShouldBeRedirectedToTheDashboardPage()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("/settings") || d.Url.Contains("/settings"));
            Assert.That(_driver.Url, Does.Contain("settings"));
        }
    }
}
