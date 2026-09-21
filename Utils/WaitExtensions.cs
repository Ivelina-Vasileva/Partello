using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Partello.Utils
{
    public static class WaitExtensions
    {
        public static bool WaitForUrlToContain(this IWebDriver driver, string urlFraction, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(d => d.Url.Contains(urlFraction));
        }
        public static IWebElement WaitForElementClickable(this IWebDriver driver, By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(locator);
                    return (element.Displayed && element.Enabled) ? element : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });
        }
        public static bool SafeClick(this IWebDriver driver, By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(d =>
            {
                try
                {
                    var elem = d.FindElement(locator);
                    if (elem.Displayed && elem.Enabled)
                    {
                        elem.Click();
                        return true;
                    }
                }
                catch (StaleElementReferenceException) { }
                catch (NoSuchElementException) { }
                return false;
            });
        }
        public static bool SafeClick(this IWebDriver driver, IWebElement element, int timeoutSeconds = 10, bool scrollTo = true)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(d =>
            {
                try
                {
                    if (element.Displayed && element.Enabled)
                    {
                        var actions = new Actions(d);
                        if (scrollTo)
                        {
                            actions.ScrollToElement(element);
                        }

                        actions.MoveToElement(element)
                        .Click()
                        .Perform();

                        return true;
                    }
                }
                catch (StaleElementReferenceException) { }
                catch (NoSuchElementException) { }
                catch (ElementClickInterceptedException) { }

                return false;
            });
        }
        public static IWebElement WaitForVisible(this IWebDriver driver, IWebElement element, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(d =>
            {
                try
                {
                    return element.Displayed ? element : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });
        }
        public static IReadOnlyCollection<IWebElement> WaitForElementsPresent(this IWebDriver driver, By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(d =>
            {
                var elements = d.FindElements(locator);
                return elements.Count > 0 ? elements : null;
            });
        }
        public static IWebElement WaitForElementClickable(this IWebDriver driver, IWebElement element, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(d =>
            {
                try
                {
                    return (element.Displayed && element.Enabled) ? element : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });
        }
        public static bool WaitForElementToDisappear(this IWebDriver driver, By locator, int timeoutSeconds = 5)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(d =>
            {
                try
                {
                    var elem = d.FindElement(locator);
                    return !elem.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            });
        }
        public static bool WaitForInvisibility(this IWebDriver driver, By locator, int timeoutSeconds = 10)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            bool isInvisible = false;

            try
            {
                isInvisible = wait.Until(d =>
                {
                    try
                    {
                        var elements = d.FindElements(locator);
                        if (elements.Count == 0) return true;
                        return !elements[0].Displayed;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return true;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                isInvisible = false;
            }
            finally
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            }

            return isInvisible;
        }
        public static bool ClickByTestId(this IWebDriver driver, string testId, int timeoutSeconds = 10)
        {
            return driver.SafeClick(By.CssSelector($"[data-testid='{testId}']"), timeoutSeconds);
        }
        public static void WaitForReactToBeReady(this IWebDriver driver, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(d =>
            {
                var reactRoots = d.FindElements(By.CssSelector("#root, [data-reactroot], [data-reactid]"));
                return reactRoots.Count > 0 && reactRoots.All(r => r.Displayed);
            });
            Thread.Sleep(500);
        }
        public static void SafeClickWithActions(this IWebDriver driver, By locator, int timeoutSeconds = 10)
        {
            var element = driver.WaitForElementClickable(locator, timeoutSeconds);
            if (element != null)
            {
                var actions = new Actions(driver);
                actions.MoveToElement(element).Click().Perform();
            }
        }
        public static IWebElement WaitForVisible(this IWebDriver driver, By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(locator);
                    return el.Displayed ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });
        }
    }
}

