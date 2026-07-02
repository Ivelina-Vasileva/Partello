using OpenQA.Selenium;

namespace Partello.Pages
{
    public class PricingPage : BasePage
    {
        public PricingPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement ButtonFreeGetStarted => _driver.FindElement(By.XPath("//p[contains(text(), 'Free') or contains(text(), 'FREE')]/following-sibling::form//button[@type='submit']"));
        public IWebElement ButtonIntimateGetStarted => _driver.FindElement(By.XPath("//p[text()='Intimate']/following-sibling::form//button[@type='submit']"));
        public IWebElement ButtonStandardGetStarted => _driver.FindElement(By.XPath("//p[text()='Standard']/following-sibling::form//button[@type='submit']"));
        public IWebElement ButtonLargeGetStarted => _driver.FindElement(By.XPath("//p[text()='Large']/following-sibling::form//button[@type='submit']"));
        public IWebElement ReadOurFaqNudgeLink => _driver.FindElement(By.XPath("//a[@href='/faq' and contains(text(), 'Read our FAQ')]"));
        public IWebElement BuyCreditsLink => _driver.FindElement(By.XPath("//a[contains(text(), 'Buy Credits')]"));
    }
}
