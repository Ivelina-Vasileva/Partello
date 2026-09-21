using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Partello.Config;
using Partello.Pages;
using Partello.Utils;
using Reqnroll;
using System.Runtime;

namespace Partello.Steps
{
    [Binding]
    public class DeleteEventSteps
    {
        private readonly EventsPage _eventsPage;
        private string _deletedEventTitle;
        private IWebDriver _driver;
        private readonly TestSettings _settings;
        private readonly ScenarioContext _context;
        public DeleteEventSteps(EventsPage eventsPage, IWebDriver driver, TestSettings settings, ScenarioContext context)
        {
            _eventsPage = eventsPage;
            _driver = driver;
            _settings = settings;
            _context = context;
        }

        [When(@"I delete the latest created event")]
        public void WhenIDeleteTheLatestCreatedEvent()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            _deletedEventTitle = _context.ContainsKey("CreatedEventTitle")
                ? _context.Get<string>("CreatedEventTitle")
                : _eventsPage.FirstEventName;

            if (!_driver.Url.EndsWith("/events"))
            {
                _driver.Navigate().GoToUrl(_settings.BaseUrl + "/events");
            }
            wait.Until(d => d.Url.EndsWith("/events"));

            ClickSafe.Click(_driver, () => _eventsPage.FirstEventCard);
            wait.Until(d => d.Url.Contains("/events/"));

            ClickSafe.Click(_driver, () => _eventsPage.EditEventButton);
            wait.Until(d => d.Url.EndsWith("/edit"));

            ClickSafe.Click(_driver, () => _eventsPage.DeleteEventButton);

            ClickSafe.Click(_driver, () => _eventsPage.ConfirmDeleteButton);

            wait.Until(d => d.Url.EndsWith("/events"));
        }

        [Then(@"I should not see the deleted event in my events list")]
        public void ThenIShouldNotSeeTheEventInMyEventList()
        {
            var deletedEventLocator = _eventsPage.GetEventCardLocatorByName(_deletedEventTitle);
            bool isDeleted = _driver.WaitForInvisibility(deletedEventLocator, timeoutSeconds: 10);
            Assert.That(isDeleted, Is.True, $"ERROR: The event '{_deletedEventTitle}' still exists on the page!");
        }
    }
}
