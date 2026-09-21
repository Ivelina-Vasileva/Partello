using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using Partello.Config;
using Partello.Utils;
using System.Runtime;

namespace Partello.Pages
{
    public class PricingPage : BasePage
    {
        private readonly TestSettings _settings;
        public PricingPage(IWebDriver driver, TestSettings settings) : base(driver)
        {
            _settings = settings;
        }

        public IWebElement ButtonFreeGetStarted => _driver.FindElement(By.XPath("//a[contains(@href, '/events/new')]"));
        public IWebElement ButtonIntimateGetStarted => _driver.FindElement(By.XPath("//input[@name='variantId' and @value='1627188']/following-sibling::button"));
        public IWebElement ButtonStandardGetStarted => _driver.FindElement(By.XPath("//input[@name='variantId' and @value='1627191']/following-sibling::button"));
        public IWebElement ButtonLargeGetStarted => _driver.FindElement(By.XPath("//input[@name='variantId' and @value='1627192']/following-sibling::button"));
        public IWebElement ReadOurFaqNudgeLink => _driver.FindElement(By.XPath("//a[contains(@href, '/faq')]"));
        public IWebElement MostPopularBadge => _driver.FindElement(By.CssSelector("span.bg-indigo-600.rounded-full"));
        private By GetTierHeaderLocator(string tierName) => By.XPath($"//p[contains(@class, 'uppercase') and normalize-space()='{tierName}']");
        public IWebElement StandardSavingsBadge => _driver.FindElement(By.CssSelector(".ring-2 span.bg-emerald-50.text-emerald-700"));
        private By GetFeatureItemsLocator(string tierName) =>
    By.XPath($"//p[contains(@class, 'uppercase') and normalize-space()='{tierName}']/ancestor::div[contains(@class, 'rounded-2xl')]//ul/li");
        private readonly By _checkmarkSvgLocator = By.CssSelector("svg.lucide-check");
        public IWebElement GetPlanButton(string planName)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            By planButtonLocator = By.XPath($"//h3[contains(text(), '{planName}')]/ancestor::div[contains(@class, 'card') or @data-slot='card']//a | //p[contains(text(), '{planName}')]/following-sibling::form//button");

            return wait.Until(d => d.FindElement(planButtonLocator));
        }

        public void NavigateToThePricingPage()
        {
            _driver.Navigate().GoToUrl(_settings.BaseUrl + "/pricing");
        }
        public bool IsMostPopularBadgeVisible()
        {
            try
            {
                return _driver.WaitForVisible(MostPopularBadge, timeoutSeconds: 5) != null;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }

        }
        public bool IsTierDisplayed(string tierName)
        {
            try
            {
                var element = _driver.FindElement(GetTierHeaderLocator(tierName));
                var visibleElement = _driver.WaitForVisible(element, timeoutSeconds: 5);
                return visibleElement != null && visibleElement.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public bool IsStandardSavingsBadgeVisible()
        {
            try
            {
                return _driver.WaitForVisible(StandardSavingsBadge, timeoutSeconds: 5) != null;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
        public int GetFeatureCheckmarksCount(string tierName)
        {
            var items = _driver.WaitForElementsPresent(GetFeatureItemsLocator(tierName), timeoutSeconds: 5);
            return items.Count;
        }
        public bool AreAllFeatureCheckmarksVisible(string tierName, int expectedCount)
        {
            try
            {
                var listItems = _driver.WaitForElementsPresent(GetFeatureItemsLocator(tierName), timeoutSeconds: 5);

                if (listItems == null || listItems.Count != expectedCount)
                {
                    return false;
                }
                foreach (var item in listItems)
                {
                    var svgCheckmark = item.FindElement(_checkmarkSvgLocator);
                    if (!svgCheckmark.Displayed)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }

        }
        public void ClickReadFaqLink()
        {
            ReadOurFaqNudgeLink.Click();
        }
    }
}
