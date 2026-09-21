using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Partello.Utils
{
    public static class ClickSafe
    {
        public static void Click(IWebDriver driver, By locator, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));

            try
            {
                element.Click();
            }
            catch (ElementClickInterceptedException)
            {
                // Fallback: Use Actions API to perform the click
                ScrollIntoView(driver, element);
                new Actions(driver)
                    .MoveToElement(element)
                    .Click()
                    .Perform();
            }
            catch (StaleElementReferenceException)
            {
                // Retry once if element becomes stale
                Thread.Sleep(500);
                element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                new Actions(driver)
                    .MoveToElement(element)
                    .Click()
                    .Perform();
            }
        }

        public static void Click(IWebDriver driver, IWebElement element, int timeoutInSeconds = 10)
        {
            Click(driver, () => element, timeoutInSeconds);
        }

        public static void Click(IWebDriver driver, Func<IWebElement> elementProvider, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

            try
            {
                var element = wait.Until(d =>
                {
                    try
                    {
                        var el = elementProvider();
                        return el.Enabled && el.Displayed ? el : null;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return null;
                    }
                });

                ClickElement(driver, element);
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Element was not clickable within {timeoutInSeconds} seconds");
            }
        }

        private static void ClickElement(IWebDriver driver, IWebElement element)
        {
            try
            {
                element.Click();
            }
            catch (ElementClickInterceptedException)
            {
                ScrollIntoView(driver, element);
                Thread.Sleep(100); // Small delay for React to process scroll
                new Actions(driver)
                    .MoveToElement(element)
                    .Click()
                    .Perform();
            }
            catch (StaleElementReferenceException)
            {
                ScrollIntoView(driver, element);
                Thread.Sleep(500);
                new Actions(driver)
                    .MoveToElement(element)
                    .Click()
                    .Perform();
            }
        }

        private static void ScrollIntoView(IWebDriver driver, IWebElement element)
        {
            // Using Actions to scroll to element (no JavaScript)
            new Actions(driver)
                .MoveToElement(element)
                .Perform();
        }
    }
}
