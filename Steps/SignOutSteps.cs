using NUnit.Framework;
using OpenQA.Selenium;
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
    public class SignOutSteps
    {
        private readonly IWebDriver _driver;
        private readonly SignInPage _signInPage;
        private readonly TestSettings _settings;

        public SignOutSteps(IWebDriver driver, SignInPage signInPage, TestSettings settings)
        {
            _driver = driver;
            _signInPage = signInPage;
            _settings = settings;
        }

        [When(@"I click on the user avatar in the top-right corner")]
        public void WhenIClickOnTheUserAvatarInTheTopRightCorner()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => !d.Url.Contains("/sign-in"));
            var avatar = wait.Until(d =>
            {
                var elem = _signInPage.UserAvatarMenu;
                return (elem.Displayed && elem.Enabled) ? elem : null;
            });

            avatar.Click();
        }
        [When(@"I click the Sign Out option")]
        public void WhenIClickTheSignOutOption()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var signOutBtn = wait.Until(d => _signInPage.SignOutButton);
            signOutBtn.Click();
        }

        [Then(@"I should be redirected to the homepage")]
        public void ThenIShouldBeRedirectedToTheHomepage()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            wait.Until(d => d.Url.TrimEnd('/') == _settings.BaseUrl.TrimEnd('/') || d.Url == _settings.BaseUrl);

            Assert.That(_driver.Url.TrimEnd('/'), Is.EqualTo(_settings.BaseUrl.TrimEnd('/')), "User was not redirected to homepage after Sign Out.");
        }

        [Then(@"protected routes like ""(.*)"" should redirect me to ""(.*)""")]
        public void ThenProtectedRoutesShouldRedirectMeTo(string protectedRoute, string expectedRedirectPage)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            string fullProtectedUrl = _settings.BaseUrl.TrimEnd('/') + protectedRoute;
            _driver.Navigate().GoToUrl(fullProtectedUrl);

            wait.Until(d => d.Url.Contains(expectedRedirectPage));

            Assert.That(_driver.Url, Does.Contain(expectedRedirectPage),
                $"Security vulnerability: Unauthenticated user accessed {protectedRoute} without being redirected to {expectedRedirectPage}!");
        }
    }
}
