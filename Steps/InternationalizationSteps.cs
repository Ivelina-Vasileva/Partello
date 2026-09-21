using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Partello.Config;
using Partello.Pages;
using Partello.Utils;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partello.Steps
{
    [Binding]
    public class InternationalizationSteps
    {
        private readonly IWebDriver _driver;
        private readonly TestSettings _settings;
        private readonly HomePage _homePage;
        public InternationalizationSteps(IWebDriver driver, TestSettings settings, HomePage homePage)
        {
            _driver = driver;
            _settings = settings;
            _homePage = homePage;
        }

        [Given(@"I have navigated to the Partello Base page")]
        public void GivenIHaveNavigatedToThePartelloBasePage()
        {
            _driver.Navigate().GoToUrl(_settings.BaseUrl);
        }

        [When(@"I switch language to ""(.*)""")]
        public void WhenISwitchLanguageTo(string language)
        {
            _homePage.SelectLanguage(language);
        }
        [Then(@"the locale switcher should be visible in the header")]
        public void ThenTheLocaleSwitcherShouldBeVisibleInTheHeader()
        {
            var trigger = _driver.WaitForVisible(_homePage.LanguageDropDownMenu, timeoutSeconds: 5);
            trigger.Displayed.Should().BeTrue("The locale switcher trigger button must be visible in the header.");
        }

        [Then(@"the current URL should contain ""(.*)""")]
        public void ThenTheCurrentURLShouldContain(string prefix)
        {
            bool urlMatches = _driver.WaitForUrlToContain(prefix, timeoutSeconds: 5);
            urlMatches.Should().BeTrue($"The URL should contain the locale prefix '{prefix}', but was '{_driver.Url}'.");
        }
    }
}
