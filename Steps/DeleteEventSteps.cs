using NUnit.Framework;
using OpenQA.Selenium;
using Partello.Pages;
using Reqnroll;

namespace Partello.Steps
{
    [Binding]
    public class DeleteEventSteps
    {
        private readonly EventsPage _eventsPage;
        public DeleteEventSteps(EventsPage eventsPage)
        {
            _eventsPage = eventsPage;

        }

        [When(@"I delete the event named ""(.*)""")]
        public void WhenIDeleteTheEventNamed(string eventName)
        {
            _eventsPage.EventCardByName(eventName).Click();
            _eventsPage.EditEventButton.Click();
            _eventsPage.DeleteEventButton.Click();
            _eventsPage.ConfirmDeleteButton.Click();
        }

        [Then(@"I should not see the event ""(.*)"" in my events list")]
        public void ThenIShouldNotSeeTheEventInMyEventList(string eventName)
        {
            bool isDeleted = false;
            try
            {
                var element = _eventsPage.EventCardByName(eventName);
                isDeleted = !element.Displayed;
            }
            catch (NoSuchElementException)
            {
                isDeleted = true;
            }
            Assert.That(isDeleted, Is.True, $"ERROR: The Event still exist!");
        }
    }
}
