using FluentAssertions;
using Microsoft.VisualBasic;
using NUnit.Framework;
using OpenQA.Selenium;
using Partello.Config;
using Partello.Pages;
using Partello.Utils;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Partello.Steps
{
    [Binding]
    public class PricingSteps
    {
        private readonly IWebDriver _driver;
        private readonly PricingPage _pricingPage;
        private readonly TestSettings _settings;
        public PricingSteps(IWebDriver driver, PricingPage pricingPage, TestSettings settings)
        {
            _driver = driver;
            _pricingPage = pricingPage;
            _settings = settings;
        }
        [Given(@"I have navigated to the pricing page")]
        public void GivenIHaveNavigatedToThePricingPage()
        {
            _pricingPage.NavigateToThePricingPage();
        }

        [When(@"I click the Read our FAQ link")]
        public void WhenIClickTheReadOurFAQLink()
        {
            _pricingPage.ClickReadFaqLink();
        }

       [Then(@"the {string} pricing tier should be displayed")]
        public void ThenThePricingTierShouldBeDisplayed(string tier)
        {
            bool isDisplayed = _pricingPage.IsTierDisplayed(tier);
            isDisplayed.Should().BeTrue($"The '{tier}' pricing tier should be visible on the page.");
        }

        [Then(@"the Standard pricing tier should display the {string} badge")]
        public void ThenTheStandardPricingTierShouldDisplayTheBadge(string p0)
        {
            _pricingPage.IsMostPopularBadgeVisible();
        }

        [Then(@"the Standard pricing tier should display the savings badge")]
        public void ThenTheStandardPricingTierShouldDisplayTheSavingsBadge()
        {
            _pricingPage.IsStandardSavingsBadgeVisible();
        }

        [Then(@"the ""(.*)"" tier should display (.*) feature checkmarks")]
        public void ThenTheTierShouldDisplayFeatureCheckmarks(string tier, int expectedCount)
        {
            _pricingPage.GetFeatureCheckmarksCount(tier).Should().Be(expectedCount);
            _pricingPage.AreAllFeatureCheckmarksVisible(tier, expectedCount);

        }
        [Then(@"I should be redirected to the FAQ page")]
        public void ThenIShouldBeRedirectedToTheFAQPage()
        {
            _driver.WaitForUrlToContain("/faq");
            Assert.That(_driver.Url, Does.Contain("faq"));
        }
    }
}
