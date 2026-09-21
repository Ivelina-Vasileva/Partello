using OpenQA.Selenium;

namespace Partello.Pages
{
    public class BlogPage : BasePage
    {

        public BlogPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement WeddingReadArticleButton => _driver.FindElement(By.XPath("//a[contains(@href, '/blog/how-to-plan-perfect-wedding-rsvp')]"));
        public IWebElement TipsReadArticleButton => _driver.FindElement(By.XPath("//a[contains(@href, '/blog/5-tips-managing-large-group-invitations')]"));
        public IWebElement InsightsReadArticleButton => _driver.FindElement(By.XPath("//a[contains(@href, '/blog/why-digital-invitations-are-the-future')]"));
        public IWebElement HowToCollectReadArticleButton => _driver.FindElement(By.XPath("//a[contains(@href, '/blog/how-to-collect-dietary-requirements')]"));
        public IWebElement PartiesReadArticleButton => _driver.FindElement(By.XPath("//a[contains(@href, '/blog/planning-surprise-party-managing-rsvps')]"));
        public IWebElement GuidesReadArticleButton => _driver.FindElement(By.XPath("//a[contains(@href, '/blog/ultimate-guide-tracking-event-attendance')]"));
        public IWebElement BackToTheBlogLink => _driver.FindElement(By.XPath("//a[contains(@href, '/blog')]"));
    }
}
