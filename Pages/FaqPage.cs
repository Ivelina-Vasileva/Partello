using OpenQA.Selenium;

namespace Partello.Pages
{
    public class FaqPage : BasePage
    {
        public FaqPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement FaqWhatIsPartelloButton => _driver.FindElement(By.XPath("//button[contains(., 'What is Partello?')]"));
        public IWebElement FaqHowDoTheInvitationLinksWorkButton => _driver.FindElement(By.XPath("//button[contains(., 'How do the invitation links work?')]"));
        public IWebElement FaqCanIInviteCouplesOrWholeFamiliesButton => _driver.FindElement(By.XPath("//button[contains(., 'Can I invite couples or whole families?')]"));
        public IWebElement FaqHowDoITrackRSVPsButton => _driver.FindElement(By.XPath("//button[contains(., 'How do I track RSVPs?')]"));
        public IWebElement FaqIsThereALimitOnGuestsOrEventsButton => _driver.FindElement(By.XPath("//button[contains(., 'Is there a limit on guests or events?')]"));
        public IWebElement FaqWhatDoesItCostButton => _driver.FindElement(By.XPath("//button[contains(., 'What does it cost?')]"));
        public IWebElement FaqCanGuestsRespondInTheirOwnLanguageButton => _driver.FindElement(By.XPath("//button[contains(., 'Can guests respond in their own language?')]"));
        public IWebElement GetFaqWhatHappensAfterTheRSVPDeadlineButton()
        {
            return _driver.FindElement(By.XPath("//button[contains(., 'What happens after the RSVP deadline?')]"));
        }
        public IWebElement ContactUsButton => _driver.FindElement(By.XPath("//a[@href='/contact' and contains(., 'Contact us')]"));
    }
}
