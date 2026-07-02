using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Partello.Pages
{
    public class EventsPage : BasePage
    {
        public EventsPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement EventsLink => _driver.FindElement(By.XPath("//span[contains(text(), 'Events')]"));
        public IWebElement CreateStartEventButton => _driver.FindElement(By.XPath("//a[@href='/events/new']"));
        public IWebElement FreePlanRadioButton => _driver.FindElement(By.CssSelector("input[type='radio'][value='free']"));
        public IWebElement EventTitleField => _driver.FindElement(By.Id("title"));
        public IWebElement EventTypeDropdown => _driver.FindElement(By.Id("type"));
        public IWebElement DatePickerButton => _driver.FindElement(By.Id("eventDate-date"));
        public IWebElement DaySixteenButton => _driver.FindElement(By.CssSelector("button[aria-label*='July 16th, 2026']"));
        public IWebElement EventTimeDropdown => _driver.FindElement(By.Id("eventDate-time"));
        public IWebElement RsvpDatePickerButton => _driver.FindElement(By.Id("rsvpDeadline-date"));
        public IWebElement DayFourteenButton => _driver.FindElement(By.XPath("//div[@data-radix-popper-content-wrapper]//button[text()='14']"));
        public IWebElement RsvpTimeDropdown => _driver.FindElement(By.Id("rsvpDeadline-time"));
        public IWebElement EventVenueField => _driver.FindElement(By.Id("venue"));
        public IWebElement EventAddressField => _driver.FindElement(By.Id("address"));
        public IWebElement EventDescriptionField => _driver.FindElement(By.Id("description"));
        public IWebElement CreatedEventTitle => _driver.FindElement(By.CssSelector("h1.text-2xl.font-bold"));
        public IWebElement UpgradeButton => _driver.FindElement(By.XPath("//a[contains(text(), 'Upgrade')]"));
        public IWebElement GetStartedElement => _driver.FindElement(By.XPath("//button[contains(text(), 'Get Started')]"));
        public IWebElement CreateEventButton => _driver.FindElement(By.XPath("//button[@type='submit' and contains(., 'Create Event')]"));
        public IWebElement EventCardByName(string eventName) => _driver.FindElement(By.XPath($"//a[.//h2[contains(text(), \"{eventName}\")]]"));
        public IWebElement EditEventButton => _driver.FindElement(By.XPath("//a[contains(@href, '/edit') and contains(., 'Edit')]"));
        public IWebElement DeleteEventButton => _driver.FindElement(By.XPath("//button[@type='button' and text()='Delete Event']"));
        public IWebElement ConfirmDeleteButton => _driver.FindElement(By.XPath("//button[@type='button' and text()='Yes, delete event']"));

        public void FillEventForm(string title, string description)
        {
            FreePlanRadioButton.Click();

            EventTitleField.Clear();
            EventTitleField.SendKeys(title);

            EventTypeDropdown.Click();
            SelectElement typeSelect = new SelectElement(EventTypeDropdown);
            typeSelect.SelectByText("Birthday");

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            wait.Until(d => DatePickerButton.Displayed);
            DatePickerButton.Click();
            wait.Until(d => DaySixteenButton.Displayed);
            DaySixteenButton.Click();

            SelectElement timeSelect = new SelectElement(EventTimeDropdown);
            timeSelect.SelectByText("16:30");

            wait.Until(d => RsvpDatePickerButton.Displayed);
            RsvpDatePickerButton.Click();
            wait.Until(d => DayFourteenButton.Displayed);
            DayFourteenButton.Click();

            SelectElement rsvpTimeSelect = new SelectElement(RsvpTimeDropdown);
            rsvpTimeSelect.SelectByText("12:30");

            EventVenueField.Clear();
            EventVenueField.SendKeys("Grand Hotel Therme");


            EventAddressField.Clear();
            EventAddressField.SendKeys("Pirin Street 1");

            EventDescriptionField.Clear();
            EventDescriptionField.SendKeys(description);

        }

        public string GetCreatedEventTitle()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => CreatedEventTitle.Displayed);
            return CreatedEventTitle.Text;
        }
    }
}
