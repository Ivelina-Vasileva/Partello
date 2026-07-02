using NUnit.Framework;
using Partello.Pages;
using Reqnroll;

namespace Partello.Steps
{

    [Binding]
    public class CreateEventSteps
    {
        private readonly EventsPage _eventsPage;
        public CreateEventSteps(EventsPage eventsPage)
        {
            _eventsPage = eventsPage;

        }

        [When(@"I click Events link in the header")]
        public void WhenIClickEventsLinkInTheHeader()
        {
            _eventsPage.EventsLink.Click();
        }

        [When(@"I click Create Event button")]
        public void WhenIClickCreateEventButton()
        {
            _eventsPage.CreateStartEventButton.Click();
        }

        [When(@"I fill event details")]
        public void WhenIFillEventDetails()
        {
            _eventsPage.FillEventForm("Ivelina's Birthday", "We have FREE Parking");
        }

        [When(@"I click Save Event button")]
        public void WhenIClickSaveEventButton()
        {
            _eventsPage.CreateEventButton.Click();
        }

        [Then(@"I should see the event listed in my events")]
        public void ThenIShouldSeeTheEventListedInMyEvents()
        {
            string actualTitle = _eventsPage.GetCreatedEventTitle();
            Assert.That(actualTitle, Is.Not.Null.Or.Empty);
        }
    }
}
