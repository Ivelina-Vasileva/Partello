using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Partello.Config;
using Partello.Pages;
using Reqnroll;

namespace Partello.Steps
{

    [Binding]
    public class CreateEventSteps
    {
        private readonly EventsPage _eventsPage;
        private readonly IWebDriver _driver;
        private readonly TestSettings _settings;
        public CreateEventSteps(EventsPage eventsPage, IWebDriver driver, TestSettings settings)
        {
            _eventsPage = eventsPage;
            _driver = driver;
            _settings = settings;

        }
        [Given(@"I'm on the Create Event Page")]
        public void GivenImOnTheCreateEventPage()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _driver.Navigate().GoToUrl(_settings.BaseUrl + "/events/new");
            wait.Until(d => d.Url.Contains("/events/new"));
        }

        [When(@"I click Events link in the header")]
        public void WhenIClickEventsLinkInTheHeader()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("/settings") || d.Url.Contains("/events"));
            var eventsLink = wait.Until(d =>
            {
                var element = d.FindElement(By.XPath("//a[contains(@href, '/events')]"));
                return (element.Displayed && element.Enabled) ? element : null;
            });

            eventsLink.Click();
        }

        [When(@"I click Create Event button")]
        public void WhenIClickCreateEventButton()
        {
            _eventsPage.CreateStartEventButton.Click();
        }

        [When(@"I select Free plan option")]
        public void WhenISelectFreePlanOption()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

            IWebElement freeRadio = wait.Until(d => _eventsPage.FreePlanRadioButton);

            Actions actions = new Actions(_driver);
            actions.MoveToElement(freeRadio).Perform();
            freeRadio.Click();
        }

        [When(@"I create an event titled ""(.*)"" with date option ""(.*)""")]
        public void WhenICreateAnEventTitledWithDateOption(string title, string dateOption)
        {
            _eventsPage.FillEventName(title);
            _eventsPage.SetDateOptionTo(dateOption);
        }

        [When(@"I fill event details")]
        public void WhenIFillEventDetails()
        {
            _eventsPage.FillEventForm("Ivelina's Birthday", "We have FREE Parking");
        }

        [When(@"I click Save Event button")]
        public void WhenIClickSaveEventButton()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            IWebElement btn = _eventsPage.CreateEventButton;
            Actions actions = new Actions(_driver);
            actions.MoveToElement(btn).Perform();

            if (btn.Enabled && btn.GetAttribute("disabled") == null)
            {
                btn.Click();
            }
            else
            {
              
            }
        }

        //[When(@"I fill event name ""(.*)""")]
        //public void WhenIFillEventName(string eventName)
        //{
        //    _eventsPage.EventTitleField.Clear();

        //    if (!string.IsNullOrEmpty(eventName))
        //    {
        //        _eventsPage.EventTitleField.SendKeys(eventName + Keys.Tab);
        //    }
        //    else
        //    {
        //        _eventsPage.EventTitleField.SendKeys(Keys.Tab);
        //    }
        //}

        //[When(@"I set date option to ""(.*)""")]
        //public void WhenISetDateOptionTo(string dateOption)
        //{
        //    if (dateOption.Equals("None", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return;
        //    }

        //    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

        //    DateTime targetDate = dateOption.ToLower() switch
        //    {
        //        "today" => DateTime.Now,
        //        "tomorrow" => DateTime.Now.AddDays(1),
        //        "yesterday" => DateTime.Now.AddDays(-1),
        //        "next year" => DateTime.Now.AddYears(1),
        //        _ => DateTime.Now.AddDays(7)
        //    };

        //    wait.Until(d => _eventsPage.DatePickerButton.Displayed);
        //    _eventsPage.DatePickerButton.Click();

        //    var dayBtn = wait.Until(d => _eventsPage.GetCalendarDayButton(targetDate));
        //    dayBtn.Click();
        //}

        //[When(@"I set RSVP option to ""(.*)""")]
        //public void WhenISetRsvpOptionTo(string rsvpOption)
        //{
        //    if (rsvpOption.Equals("None", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return;
        //    }

        //    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

        //    DateTime targetDate = rsvpOption.ToLower() switch
        //    {
        //        "today" => DateTime.Now,
        //        "tomorrow" => DateTime.Now.AddDays(1),
        //        "yesterday" => DateTime.Now.AddDays(-1),
        //        "next year" => DateTime.Now.AddYears(1),
        //        _ => DateTime.Now
        //    };

        //    wait.Until(d => _eventsPage.RsvpDatePickerButton.Displayed);
        //    _eventsPage.RsvpDatePickerButton.Click();

        //    var dayBtn = wait.Until(d => _eventsPage.GetCalendarDayButton(targetDate));
        //    dayBtn.Click();
        //}
        [When(@"I set event name ""(.*)"", date option to ""(.*)"" and RSVP option to ""(.*)""")]
        public void WhenISetEventFormInputs(string eventName, string dateOption, string rsvpOption)
        {
            _eventsPage.SetEventDetails(eventName, dateOption, rsvpOption);
        }

        [When(@"I fill field ""(.*)"" with value ""(.*)""")]
        public void WhenIFillFieldWithValue(string fieldName, string inputValue)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            switch (fieldName.ToLower())
            {
                case "event title":
                    _eventsPage.EventTitleField.Clear();
                    _eventsPage.EventTitleField.SendKeys(inputValue);
                    break;

                case "venue":
                    _eventsPage.EventVenueField.Clear();
                    _eventsPage.EventVenueField.SendKeys(inputValue);
                    break;

                case "address":
                    _eventsPage.EventAddressField.Clear();
                    _eventsPage.EventAddressField.SendKeys(inputValue);
                    break;

                case "description":
                    _eventsPage.EventDescriptionField.Clear();
                    _eventsPage.EventDescriptionField.SendKeys(inputValue);
                    break;

                case "type":
                    SelectElement typeSelect = new SelectElement(_eventsPage.EventTypeDropdown);
                    typeSelect.SelectByValue(inputValue);
                    break;

                case "time slot":
                    SelectElement timeSelect = new SelectElement(_eventsPage.EventTimeDropdown);
                    timeSelect.SelectByText(inputValue);
                    break;

                case "date picker":
                    _eventsPage.DatePickerButton.Click();
                    var dateBtn = wait.Until(d => _eventsPage.GetCalendarDayButton(DateTime.Now.AddDays(1)));
                    dateBtn.Click();
                    break;

                case "rsvp picker":
                    _eventsPage.RsvpDatePickerButton.Click();
                    var rsvpBtn = wait.Until(d => _eventsPage.GetCalendarDayButton(DateTime.Now));
                    rsvpBtn.Click();
                    break;

                default:
                    throw new ArgumentException($"Unknown field: {fieldName}");
            }
        }
        [When(@"I set event date to next year")]
        public void WhenISetEventDateToNextYear()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            _eventsPage.DatePickerButton.Click();

            for (int i = 0; i < 12; i++)
            {
                IWebElement nextBtn = wait.Until(d => d.FindElement(By.XPath("//button[contains(@aria-label, 'Next Month') or contains(@class, 'lucide-chevron-right') or contains(@class, 'rdp-button_next')]")));
                nextBtn.Click();
            }

            IWebElement dayButton = wait.Until(d => d.FindElement(By.XPath("//button[not(@disabled) and text()='15']")));
            dayButton.Click();
        }

        [When(@"I set RSVP deadline to next year")]
        public void WhenISetRsvpDeadlineToNextYear()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            _eventsPage.RsvpDatePickerButton.Click();

            IWebElement nextMonthBtn = wait.Until(d => d.FindElement(By.XPath("//button[contains(@aria-label, 'Next Month') or contains(@class, 'lucide-chevron-right') or contains(@class, 'rdp-button_next')]")));

            for (int i = 0; i < 11; i++)
            {
                nextMonthBtn.Click();
            }

            IWebElement dayButton = wait.Until(d => d.FindElement(By.XPath("//button[not(@disabled) and text()='10']")));
            dayButton.Click();
        }

        [Then(@"I should see the event listed in my events")]
        public void ThenIShouldSeeTheEventListedInMyEvents()
        {
            string actualTitle = _eventsPage.GetCreatedEventTitle();
            Assert.That(actualTitle, Is.Not.Null.Or.Empty);
        }

        [Then(@"Event should not be created")]
        public void ThenEventShouldNotBeCreated()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

            Assert.That(_driver.Url, Does.Contain("/events/new"),
                "Error: The form is send!");
        }

        [Then(@"I should see validation message ""(.*)""")]
        public void ThenIShouldSeeValidationMessage(string expectedError)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

            IWebElement titleInput = _eventsPage.EventTitleField;

            bool isTitleRequiredAndEmpty = titleInput.GetAttribute("required") != null &&
                                   string.IsNullOrEmpty(titleInput.GetAttribute("value"));

            if (expectedError.Contains("Please fill out") || expectedError.Contains("fill in"))
            {
                Assert.That(isTitleRequiredAndEmpty, Is.True, "ERROR: The title field should be invalid, but it's not!");
                return;
            }
            By errorLocator = By.XPath($"//*[contains(text(), '{expectedError}')]");
            IWebElement errorMessage = wait.Until(d => d.FindElement(errorLocator));

            Assert.That(errorMessage.Displayed, Is.True, $"Validation message '{expectedError}' was not displayed on screen.");
        }

        [Then(@"Field ""(.*)"" should contain value ""(.*)""")]
        public void ThenFieldShouldContainValue(string fieldName, string expectedValue)
        {
            switch (fieldName.ToLower())
            {
                case "event title":
                    Assert.That(_eventsPage.EventTitleField.GetAttribute("value"), Is.EqualTo(expectedValue));
                    break;

                case "venue":
                    Assert.That(_eventsPage.EventVenueField.GetAttribute("value"), Is.EqualTo(expectedValue));
                    break;

                case "address":
                    Assert.That(_eventsPage.EventAddressField.GetAttribute("value"), Is.EqualTo(expectedValue));
                    break;

                case "description":
                    Assert.That(_eventsPage.EventDescriptionField.GetAttribute("value"), Is.EqualTo(expectedValue));
                    break;

                case "type":
                    SelectElement typeSelect = new SelectElement(_eventsPage.EventTypeDropdown);
                    Assert.That(typeSelect.SelectedOption.GetAttribute("value"), Is.EqualTo(expectedValue));
                    break;

                case "time slot":
                    SelectElement timeSelect = new SelectElement(_eventsPage.EventTimeDropdown);
                    Assert.That(timeSelect.SelectedOption.Text, Is.EqualTo(expectedValue));
                    break;

                case "date picker":
                case "rsvp picker":
                   
                    IWebElement btn = (fieldName.ToLower() == "date picker") ? _eventsPage.DatePickerButton : _eventsPage.RsvpDatePickerButton;
                    Assert.That(btn.Text, Does.Not.Contain("Select date"));
                    break;
            }
        }
    }
}
   