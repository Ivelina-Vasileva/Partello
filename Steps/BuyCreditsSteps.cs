using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Partello.Config;
using Partello.Pages;
using Reqnroll;

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

        public BuyCreditsSteps(IWebDriver driver, SignInPage signUpPage, TestSettings settings, PricingPage pricingPage, CheckoutPage checkoutPage)
        {
            _driver = driver;
            _signInPage = signUpPage;
            _settings = settings;
            _pricingPage = pricingPage;
            _checkoutPage = checkoutPage;
        }

        [Given(@"I'm logged in Partello")]
        public void GivenImLoggedInPartello()
        {
            _driver.Navigate().GoToUrl(_settings.BaseUrl + "/sign-in");
            _signInPage.Login(_settings.LoginEmail, _settings.LoginPassword);

        }

        [When(@"I click Buy Credits link")]
        public void WhenIClickBuyCreditsLink()

        {
            _pricingPage.BuyCreditsLink.Click();
        }

        [When(@"I choose to purchase the ""(.*)"" credit plan")]
        public void WhenIChooseToPurchaseTheCreditPlan(string PlanName)
        {
            _pricingPage.ButtonIntimateGetStarted.Click();
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
