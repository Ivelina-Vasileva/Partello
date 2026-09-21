using OpenQA.Selenium;

namespace Partello.Pages
{
    public class FaqPage : BasePage
    {
        public FaqPage(IWebDriver driver) : base(driver)
        {
        }
        public IWebElement FaqWhatIsPartelloItem => _driver.FindElement(By.XPath("(//details//summary)[1]"));
        public IWebElement FaqHowDoTheInvitationLinksWorkItem => _driver.FindElement(By.XPath("(//details//summary)[2]"));
        public IWebElement FaqCanIInviteCouplesOrWholeFamiliesItem => _driver.FindElement(By.XPath("(//details//summary)[3]"));
        public IWebElement FaqHowDoITrackRSVPsItem => _driver.FindElement(By.XPath("(//details//summary)[4]"));
        public IWebElement FaqIsThereALimitOnGuestsOrEventsItem => _driver.FindElement(By.XPath("(//details//summary)[5]"));
        public IWebElement FaqWhatDoesItCostItem => _driver.FindElement(By.XPath("(//details//summary)[6]"));
        public IWebElement FaqCanGuestsRespondInTheirOwnLanguageItem => _driver.FindElement(By.XPath("(//details//summary)[7]"));
        public IWebElement GetFaqWhatHappensAfterTheRSVPDeadlineItem =>_driver.FindElement(By.XPath("(//details//summary)[8]"));
        public IWebElement ContactUsButton => _driver.FindElement(By.XPath("//a[contains(@href, '/contact')]"));
    }
}
