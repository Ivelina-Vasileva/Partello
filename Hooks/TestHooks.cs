using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Partello.Config;
using Partello.Pages;
using Partello.Utils;
using Reqnroll;
using System.Runtime;

namespace Partello.Hooks
{
    [Binding]
    public class TestHooks
    {
       private readonly IWebDriver _driver;
       private readonly TestSettings _settings;
        private readonly SignInPage _signInPage;
        private readonly EventsPage _eventsPage;
        private readonly ScenarioContext _context;
        public TestHooks(IWebDriver driver, TestSettings testSettings, SignInPage signInPage, EventsPage eventsPage, ScenarioContext context)
        {
            _driver = driver;
            _settings = testSettings;
            _signInPage = signInPage;
            _eventsPage = eventsPage;
            _context = context;
        }

        [BeforeScenario("Create_Event_First")]
        public void CreateEventFirst()
        {

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            if (!_driver.Url.Contains("/events") && !_driver.Url.Contains("/settings"))
            {
                _driver.Navigate().GoToUrl(_settings.BaseUrl + "/sign-in");
                _signInPage.Login(_settings.LoginEmail, _settings.LoginPassword);
                wait.Until(d => !d.Url.Contains("/sign-in"));
            }
            _driver.Navigate().GoToUrl(_settings.BaseUrl + "/events/new");
            wait.Until(d => d.Url.Contains("/events/new"));

            var random = new Random();
            string[] times = new string[] { "10:00", "12:30", "14:00", "16:30", "18:00" };
            string[] venues = new string[] { "Grand Hotel Therme", "Hotel Marinela", "Hilton Sofia", "Paradise Center" };
            string[] addresses = new string[] { "Pirin Street 1", "Vitosha Blvd 45", "Tsar Osvoboditel 10" };
            string randomTime = times[random.Next(times.Length)];
            string randomRsvpTime = times[random.Next(times.Length)];
            string randomVenue = venues[random.Next(venues.Length)];
            string randomAddress = addresses[random.Next(addresses.Length)];
            string title = $"Auto Event {DateTime.Now.Ticks.ToString().Substring(10)}";
            string description = "We have FREE Parking";

            _context["CreatedEventTitle"] = title;

            ClickSafe.Click(_driver, _eventsPage.FreePlanRadioButton);

            _eventsPage.EventTitleField.Clear();
            _eventsPage.EventTitleField.SendKeys(title + Keys.Tab);

            DateTime eventDate = DateTime.Now.AddDays(14);
            DateTime rsvpDeadline = DateTime.Now.AddDays(7);


            ClickSafe.Click(_driver, _eventsPage.DatePickerButton);
            ClickSafe.Click(_driver, () => _eventsPage.GetCalendarDayButton(eventDate));

            SelectElement timeSelect = new SelectElement(_eventsPage.EventTimeDropdown);
            timeSelect.SelectByText(randomTime);

            ClickSafe.Click(_driver, _eventsPage.RsvpDatePickerButton);
            ClickSafe.Click(_driver, () => _eventsPage.GetCalendarDayButton(rsvpDeadline));

            SelectElement rsvpTimeSelect = new SelectElement(_eventsPage.RsvpTimeDropdown);
            rsvpTimeSelect.SelectByText(randomRsvpTime);

            _eventsPage.EventVenueField.Clear();
            _eventsPage.EventVenueField.SendKeys(randomVenue + Keys.Tab);

            _eventsPage.EventAddressField.Clear();
            _eventsPage.EventAddressField.SendKeys(randomAddress + Keys.Tab);

            _eventsPage.EventDescriptionField.Clear();
            _eventsPage.EventDescriptionField.SendKeys(description + Keys.Tab);

            ClickSafe.Click(_driver, () => _eventsPage.CreateEventButton);

            wait.Until(d => d.Url.Contains("/events") && !d.Url.EndsWith("/new"));
            _driver.Navigate().GoToUrl(_settings.BaseUrl + "/events");
            wait.Until(d => d.Url.EndsWith("/events"));

        }

        [BeforeScenario("Login_Before_Test")]
        public void LoginBeforeTest()
        {
            if (_driver.Url.Contains("/events") || _driver.Url.Contains("/settings"))
            {
                return;
            }
            _driver.Navigate().GoToUrl(_settings.BaseUrl + "/sign-in");
            _signInPage.Login(_settings.LoginEmail, _settings.LoginPassword);

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => !d.Url.Contains("/sign-in") && (d.Url.Contains("/settings")));
        }

        [AfterScenario]
        public void TearDown()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }
    }
}