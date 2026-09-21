using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Partello.Utils;

namespace Partello.Pages
{
    public class EventsPage : BasePage
    {
        public EventsPage(IWebDriver driver) : base(driver)
        {
        }

        private readonly By _eventsLinkLocator = By.XPath("//a[contains(@href, '/events')] | //span[contains(text(), 'Events')]");
        public IWebElement EventsLink => _driver.WaitForElementClickable(_eventsLinkLocator);
        public IWebElement CreateStartEventButton => _driver.FindElement(By.XPath("//a[@href='/events/new']"));
        public IWebElement FreePlanRadioButton => _driver.FindElement(By.CssSelector("input[type='radio'][value='free']"));
        public IWebElement EventTitleField => _driver.FindElement(By.Id("title"));
        public IWebElement EventTypeDropdown => _driver.FindElement(By.Id("type"));
        public IWebElement DatePickerButton => _driver.FindElement(By.Id("eventDate-date"));
        public IWebElement EventTimeDropdown => _driver.FindElement(By.Id("eventDate-time"));
        public IWebElement RsvpDatePickerButton => _driver.FindElement(By.Id("rsvpDeadline-date"));
        public IWebElement RsvpTimeDropdown => _driver.FindElement(By.Id("rsvpDeadline-time"));
        public IWebElement EventVenueField => _driver.FindElement(By.Id("venue"));
        public IWebElement EventAddressField => _driver.FindElement(By.Id("address"));
        public IWebElement EventDescriptionField => _driver.FindElement(By.Id("description"));
        public IWebElement CreatedEventTitle => _driver.FindElement(By.CssSelector("h1.text-2xl.font-bold"));
        public IWebElement UpgradeButton => _driver.FindElement(By.XPath("//a[contains(@href, '/pricing')]"));
        public IWebElement GetStartedElement => _driver.FindElement(By.CssSelector("a[href='/events/new']"));
        public IWebElement CreateEventButton => _driver.FindElement(By.XPath("//button[@type='submit']"));
        public By GetEventCardLocatorByName(string eventName) =>
    By.XPath($"//a[.//h2[contains(text(), \"{eventName}\")]]");

        public IWebElement EventCardByName(string eventName) =>
            _driver.FindElement(GetEventCardLocatorByName(eventName));
        public IWebElement EditEventButton =>
    _driver.FindElement(By.XPath("//a[contains(@href, '/edit')]"));
        public IWebElement SaveChangesButton => _driver.FindElement(By.CssSelector("form button[type='submit']"));
        public IWebElement DeleteEventButton =>
    _driver.FindElement(By.XPath("//button[@type='button' and @aria-haspopup='dialog' and contains(@class, 'bg-destructive')]"));
        public IWebElement ConfirmDeleteButton =>
    _driver.FindElement(By.XPath("//div[@role='dialog']//button[contains(@class, 'bg-destructive') or @type='submit']"));
        public IReadOnlyCollection<IWebElement> AllEventCards =>
    _driver.FindElements(By.CssSelector("div.grid.gap-4 > a[href*='/events/']"));
        public IWebElement FirstEventCard =>
    _driver.FindElement(By.CssSelector("div.grid.gap-4 > a[href*='/events/']:first-of-type"));
        public string FirstEventName =>
    _driver.FindElement(By.CssSelector("div.grid.gap-4 > a[href*='/events/']:first-of-type h2")).Text;
        public IWebElement NextMonthArrow => _driver.FindElement(By.XPath("//button[contains(@aria-label, 'Next Month') or contains(@class, 'lucide-chevron-right') or contains(@class, 'rdp-button_next')]"));
        public string GetTitleInputValue() => EventTitleField.GetAttribute("value");
        public string GetDescriptionInputValue() => EventDescriptionField.GetAttribute("value");
        private By GetCalendarDayLocator(DateTime targetDate)
        {
            string dayWithSuffix = GetDayWithSuffix(targetDate.Day);
            string formattedAriaLabel = $"{targetDate:MMMM} {dayWithSuffix}, {targetDate:yyyy}";
            return By.XPath($"//button[contains(@aria-label, '{formattedAriaLabel}') or (text()='{targetDate.Day}' and not(contains(@class, 'day-outside')))]");
        }
        public IWebElement GetCalendarDayButton(DateTime targetDate)
        {
            string dayWithSuffix = GetDayWithSuffix(targetDate.Day);
            string formattedAriaLabel = $"{targetDate:MMMM} {dayWithSuffix}, {targetDate:yyyy}";

            By locator = By.XPath($"//button[contains(@aria-label, '{formattedAriaLabel}') or (text()='{targetDate.Day}' and not(contains(@class, 'day-outside')))]");

            return _driver.FindElement(locator);
        }
        private string GetDayWithSuffix(int day)
        {
            if (day >= 11 && day <= 13) return $"{day}th";
            return (day % 10) switch
            {
                1 => $"{day}st",
                2 => $"{day}nd",
                3 => $"{day}rd",
                _ => $"{day}th"
            };
        }
        public void FillEventForm(string title, string description)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            FreePlanRadioButton.Click();

            EventTitleField.Clear();
            EventTitleField.SendKeys(title);

            DateTime eventDate = DateTime.Now.AddDays(14);
            DateTime rsvpDeadline = DateTime.Now.AddDays(7);

            _driver.SafeClick(DatePickerButton);
            _driver.SafeClick(GetCalendarDayButton(eventDate));

            SelectElement timeSelect = new SelectElement(EventTimeDropdown);
            timeSelect.SelectByText("16:30");

            _driver.SafeClick(RsvpDatePickerButton);
            _driver.SafeClick(GetCalendarDayButton(rsvpDeadline));

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
            return _driver.WaitForVisible(CreatedEventTitle).Text;
        }
        public void FillEventName(string eventName)
        {
            EventTitleField.Clear();

            if (!string.IsNullOrEmpty(eventName))
            {
                EventTitleField.SendKeys(eventName + Keys.Tab);
            }
            else
            {
                EventTitleField.SendKeys(Keys.Tab);
            }

        }
        public void SetDateOptionTo(string dateOption)
        {
            if (dateOption.Equals("None", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            DateTime targetDate = dateOption.ToLower() switch
            {
                "today" => DateTime.Now,
                "tomorrow" => DateTime.Now.AddDays(1),
                "yesterday" => DateTime.Now.AddDays(-1),
                "next year" => DateTime.Now.AddYears(1),
                _ => DateTime.Now.AddDays(7)
            };

            _driver.SafeClick(DatePickerButton);

            if (dateOption.Equals("next year", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 0; i < 12; i++)
                {
                    _driver.SafeClick(NextMonthArrow);
                    System.Threading.Thread.Sleep(100);
                }
            }

            _driver.SafeClick(GetCalendarDayButton(targetDate));
        }
        public void SetRsvpOptionTo(string rsvpOption)
        {
            if (rsvpOption.Equals("None", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            DateTime targetDate = rsvpOption.ToLower() switch
            {
                "today" => DateTime.Now,
                "tomorrow" => DateTime.Now.AddDays(1),
                "yesterday" => DateTime.Now.AddDays(-1),
                "next year" => DateTime.Now.AddYears(1),
                _ => DateTime.Now
            };

            _driver.SafeClick(RsvpDatePickerButton);
            if (rsvpOption.Equals("next year", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 0; i < 12; i++)
                {
                    _driver.SafeClick(NextMonthArrow);
                    System.Threading.Thread.Sleep(100);
                }
            }

            _driver.SafeClick(GetCalendarDayButton(targetDate));
        }
        public void SetEventDetails(string eventName, string dateOption, string rsvpOption)
        {
            FillEventName(eventName);
            SetDateOptionTo(dateOption);
            SetRsvpOptionTo(rsvpOption);
        }
        public void ClickEdit()
        {
            _driver.WaitForElementClickable(EditEventButton, 5).Click();
        }
        public void UpdateTitle(string newTitle)
        {
            var titleInput = _driver.WaitForVisible(EventTitleField);
            titleInput.Click();
            titleInput.Clear();
            titleInput.SendKeys(newTitle);
        }
        public void UpdateDescription(string newDescription)
        {
            var description= _driver.WaitForVisible(EventDescriptionField);
            description.Click();
            description.Clear();
            description.SendKeys(newDescription);
        }
        public void SaveChangesButtonClick()
        {
            _driver.WaitForElementClickable(SaveChangesButton, timeoutSeconds : 5).Click();
        }
        public void OpenFirstEvent()
        {
            var card = _driver.WaitForElementClickable(FirstEventCard, 5);
            _driver.SafeClick(card);
        }
        public string GetCreatedEventName()
        {
            return _driver.WaitForVisible(CreatedEventTitle).Text;
        }
    }
}

