using OpenQA.Selenium;

namespace Partello.Pages
{
    public class BlogPage : BasePage
    {

        public BlogPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement WeddingReadArticleButton => _driver.FindElement(By.XPath("//a[@href='/blog/how-to-plan-perfect-wedding-rsvp' and contains(., 'Read')]"));
        public IWebElement TipsReadArticleButton => _driver.FindElement(By.XPath("//a[@href='/blog/5-tips-managing-large-group-invitations' and contains(., 'Read')]"));
        public IWebElement InsightsReadArticleButton => _driver.FindElement(By.XPath("//a[@href='/blog/why-digital-invitations-are-the-future' and contains(., 'Read')]"));
        public IWebElement HowToCollectReadArticleButton => _driver.FindElement(By.XPath("//a[@href='/blog/how-to-collect-dietary-requirements' and contains(., 'Read')]"));
        public IWebElement PartiesReadArticleButton => _driver.FindElement(By.XPath("//a[@href='/blog/planning-surprise-party-managing-rsvps' and contains(., 'Read')]"));
        public IWebElement GuidesReadArticleButton => _driver.FindElement(By.XPath("//a[@href='/blog/ultimate-guide-tracking-event-attendance' and contains(., 'Read')]"));
        public IWebElement BackToTheBlogLink => _driver.FindElement(By.XPath("//a[@href='/blog' and contains(., 'Back to Blog')]"));
    }
}
