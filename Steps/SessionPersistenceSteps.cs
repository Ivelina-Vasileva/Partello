using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Partello.Config;
using Partello.Pages;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partello.Steps
{
    [Binding]
    public class SessionPersistenceSteps
    {
        private IWebDriver _driver;
        private readonly SignInPage _signInPage;
        private readonly TestSettings _settings;
        private IReadOnlyCollection<Cookie> _savedCookies;

        public SessionPersistenceSteps(IWebDriver driver, SignInPage signInPage, TestSettings settings)
        {
            _driver = driver;
            _signInPage = signInPage;
            _settings = settings;
        }

        [When(@"I close and reopen the browser")]
        public void WhenICloseAndReopenTheBrowser()
        {
            _savedCookies = _driver.Manage().Cookies.AllCookies;

            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Quit();

            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(_settings.BaseUrl);

            foreach (var cookie in _savedCookies)
            {
                _driver.Manage().Cookies.AddCookie(cookie);
            }
        }

        [When(@"I navigate to the settings page")]
        public void WhenINavigateToTheSettingsPage()
        {
            _driver.Navigate().GoToUrl(_settings.BaseUrl.TrimEnd('/') + "/settings");
        }
        [When(@"24 hours of inactivity pass")]
        public void When24HoursOfInactivityPass()
        {
            var cookies = _driver.Manage().Cookies.AllCookies;

            foreach (var cookie in cookies)
            {

                _driver.Manage().Cookies.DeleteCookie(cookie);

                var expiredCookie = new OpenQA.Selenium.Cookie(
                    cookie.Name,
                    cookie.Value,
                    cookie.Domain,
                    cookie.Path,
                    DateTime.Now.AddHours(-25)
                );

                _driver.Manage().Cookies.AddCookie(expiredCookie);
            }
        }

        [When(@"I refresh the page")]
        public void WhenIRefreshThePage()
        {
            _driver.Navigate().Refresh();
        }

        [Then(@"I should still be authenticated without being redirected to sign-in")]
        public void ThenIShouldStillBeAuthenticatedWithoutBeingRedirectedToSignIn()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            wait.Until(d => d.Url.Contains("/settings"));

            Assert.That(_driver.Url, Does.Not.Contain("/sign-in"),
                "Session Persistence Fail: User session was lost after reopening the browser!");
            _driver.Quit();
        }
        [Then(@"My session should expire after inactivity")]
        public void ThenMySessionShouldExpireAfterInactivity()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            bool isRedirected = wait.Until(d => d.Url.Contains("/sign-in"));
            Assert.That(isRedirected, Is.True, "Expected to be redirected to /sign-in after session expired, but was not.");
        }
    }
}
