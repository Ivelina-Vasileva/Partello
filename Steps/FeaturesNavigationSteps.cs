using Io.Cucumber.Messages.Types;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
    public class FeaturesNavigationSteps
    {
        private readonly IWebDriver _driver;
        private readonly HomePage _homePage;
        private readonly TestSettings _settings;

        public FeaturesNavigationSteps(IWebDriver driver, HomePage homePage, TestSettings settings)
        {
            _driver = driver;
            _homePage = homePage;
            _settings = settings;
        }
        [Given (@"I have navigated to the homepage")]
        public void GivenIHaveNavigatedToTheHomepage()
        {
            _driver.Navigate().GoToUrl(_settings.BaseUrl);
            _driver.WaitForVisible(_homePage.FeaturesDropDownMenu);
        }
        [When(@"I hover over the Features dropdown menu")]
        public void WhenIHoverOverTheFeaturesDropdownMenu()
        {
            var menuBtn = _driver.WaitForVisible(_homePage.FeaturesDropDownMenu);
            Actions actions = new Actions(_driver);
            actions.MoveToElement(menuBtn).Perform();
        }
        [When(@"I click on the ""(.*)"" option")]
        public void WhenIClickOnTheOption(string featureLink)
        {
            IWebElement linkToClick = featureLink switch
            {
                "Personal Links" => _homePage.DropdownLinkPersonalLinks,
                "Guest Management" => _homePage.DropdownLinkGuestManagement,
                "Dietary Tracking" => _homePage.DropdownLinkDietaryTracking,
                "Live Dashboard" => _homePage.DropdownLinkLiveDashboard,
                _ => throw new ArgumentException($"Unknown feature link: {featureLink}")
            };

            _driver.SafeClick(linkToClick, timeoutSeconds: 10, scrollTo: false);
        }
        [When(@"I should be redirected to ""(.*)""")]
        public void WhenIShouldBeRedirectedTo(string expectedUrl)
        {
            bool isRedirected = _driver.WaitForUrlToContain(expectedUrl);
            Assert.That(isRedirected, Is.True, $"User was not redirected to expected feature page: {expectedUrl}");
        }
        [Then(@"the Features menu button should have an active state indicator")]
        public void ThenTheFeaturesMenuButtonShouldHaveAnActiveStateIndicator()
        {
            var featuresButton = _homePage.FeaturesDropDownMenu;

            string classAttribute = featuresButton.GetAttribute("class");

            bool isActive = classAttribute.Contains("indigo") ||
                             classAttribute.Contains("border-b") ||
                             featuresButton.GetAttribute("data-state") == "active";

            Assert.That(isActive, Is.True, "Features header menu item does not display the active state / indigo underline.");
        }
    }
}

