using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Partello.Config;
using Partello.Pages;
using Partello.Utils;
using Reqnroll;
using SeleniumExtras.WaitHelpers;

namespace Partello.Steps
{
    [Binding]
    public class BuyCreditsSteps
    {
        private readonly IWebDriver _driver;
        private readonly SignInPage _signInPage;
        private readonly TestSettings _settings;
        private readonly PricingPage _pricingPage;
        private readonly CheckoutPage _checkoutPage;
        private readonly TeamSettingsPage _teamSettingsPage;


        public BuyCreditsSteps(IWebDriver driver, SignInPage signUpPage, TestSettings settings, PricingPage pricingPage, CheckoutPage checkoutPage, TeamSettingsPage teamSettingsPage)
        {
            _driver = driver;
            _signInPage = signUpPage;
            _settings = settings;
            _pricingPage = pricingPage;
            _checkoutPage = checkoutPage;
            _teamSettingsPage = teamSettingsPage;
        }

        [Given(@"I'm logged in Partello")]
        public void GivenImLoggedInPartello()
        {
            if (_driver.Url.Contains("/events") || _driver.Url.Contains("/settings"))
            {
                return;
            }
            _driver.Navigate().GoToUrl(_settings.BaseUrl + "/sign-in");
            _signInPage.Login(_settings.LoginEmail, _settings.LoginPassword);

            _driver.WaitForUrlToContain("/settings");
        }

        [When(@"I click Buy Credits Button")]
        public void WhenIClickBuyCreditsButton()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("/settings"));
            IWebElement buyCreditsBtn = wait.Until(d =>
            {
                try
                {
                    var elem = _teamSettingsPage.BuyCreditsButton;
                    return (elem != null && elem.Displayed && elem.Enabled) ? elem : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });
            Actions actions = new Actions(_driver);
            actions.MoveToElement(buyCreditsBtn).Perform();
            buyCreditsBtn.Click();
        }

        [When(@"I choose to purchase the ""(.*)"" credit plan")]
        public void WhenIChooseToPurchaseTheCreditPlan(string planName)
        {
            var planButton = _pricingPage.GetPlanButton(planName);
            planButton.Click();
        }

        [When(@"I enter payment details")]
        public void WhenIEnterPaymentDetails()
        {
            _checkoutPage.FillPaymentDetails(
                "4242424242424242",
                "1230",
                "123",
                "Ivelina Vasileva",
                "Bulgaria",
                "1000"
            );
        }

        [Then(@"I shoud see that my payment was successful")]
        public void ThenIShouldSeeThatMyPaymentWasSuccessful()
        {
            _driver.SwitchTo().DefaultContent();

            var waitSuccess = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            IWebElement successContainer = waitSuccess.Until(d =>
            {
                try
                {
                    var element = _checkoutPage.SuccessMessage;
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });

            Assert.That(successContainer, Is.Not.Null);
            Assert.That(successContainer.Text, Does.Contain("Thanks for your order!"));
        }
    }
}
