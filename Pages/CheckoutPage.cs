using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Partello.Pages
{
    public class CheckoutPage
    {
        private readonly IWebDriver _driver;

        public CheckoutPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement PaymentIFrame => _driver.FindElement(By.XPath("//iframe[@title='Secure payment input frame' or contains(@name, '__privateStripeFrame')]"));
        public IWebElement CodeInput => _driver.FindElement(By.XPath("//input[@data-testid='code-controlling-input']"));
        public IWebElement CloseButton => _driver.FindElement(By.XPath("//button[@aria-label='Close' or contains (@class, 'CloseButton') or contains (@class, 'p-CloseButton')]"));
        public IWebElement CardholderNameField => _driver.FindElement(By.Id("name"));
        public IWebElement CountryDropdown => _driver.FindElement(By.Id("country"));
        public IWebElement PostalCodeField => _driver.FindElement(By.Id("postal_code"));
        public IWebElement PayButton => _driver.FindElement(By.XPath("//button[@dusk='checkout-form-submit']"));
        public IWebElement SuccessMessage => _driver.FindElement(By.XPath("//div[@dusk='checkout-success']"));
        public IWebElement CardNumberField => _driver.FindElement(By.Id("payment-numberInput"));
        public IWebElement ExpirationDateField => _driver.FindElement(By.Id("payment-expiryInput"));
        public IWebElement SecurityCodeField => _driver.FindElement(By.Id("payment-cvcInput"));

        private readonly By AuthIFrameLocator = By.XPath("//iframe[@title='Secure email input frame' or contains(@name, 'privateStripeFrame')]");
        private readonly By StripeLinkCodeInputLocator = By.CssSelector("input[data-testid='code-controlling-input']");
        private readonly By PaymentIFrameLocator = By.XPath("//iframe[@title='Secure payment input frame' or contains(@src, 'elements-inner-payment')]");

        public void FillPaymentDetails(string cardNumber, string expiry, string cvc, string name, string countryText, string postalCode)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));

            _driver.SwitchTo().DefaultContent();

            var authFrame = wait.Until(d => d.FindElement(AuthIFrameLocator));
            _driver.SwitchTo().Frame(authFrame);
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;

                var hiddenInput = wait.Until(d => d.FindElement(StripeLinkCodeInputLocator));

                js.ExecuteScript("arguments[0].value = '000000';" +
                                 "arguments[0].dispatchEvent(new Event('input', { bubbles: true }));", hiddenInput);

                wait.Until(d => (string)js.ExecuteScript("return arguments[0].value;", hiddenInput) == "000000");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[INFO] Stripe Link bypass step was skipped. Reason: {ex.Message}");
            }

            _driver.SwitchTo().DefaultContent();

            var nameField = wait.Until(d => d.FindElement(By.Id("name")));
            nameField.Clear();
            nameField.SendKeys(name);

            SelectElement countrySelect = new SelectElement(CountryDropdown);
            countrySelect.SelectByText(countryText);

            PostalCodeField.Clear();
            PostalCodeField.SendKeys(postalCode);

            var paymentFrame = wait.Until(d => d.FindElement(PaymentIFrameLocator));
            _driver.SwitchTo().Frame(paymentFrame);

            wait.Until(d => CardNumberField.Displayed);
            CardNumberField.Clear();
            CardNumberField.SendKeys(cardNumber);

            ExpirationDateField.SendKeys(expiry);
            SecurityCodeField.SendKeys(cvc);

            _driver.SwitchTo().DefaultContent();

            wait.Until(d => PayButton.Enabled);
            PayButton.Click();
        }
    }
}

