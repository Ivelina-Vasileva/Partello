using FluentAssertions;
using OpenQA.Selenium;
using Partello.Config;
using Partello.Pages;
using Partello.Utils;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partello.Steps
{
    [Binding]
    public class EditEventSteps
    {
        private readonly EventsPage _eventsPage;
        private IWebDriver _driver;
        private readonly TestSettings _settings;
        public EditEventSteps(EventsPage eventsPage, IWebDriver driver, TestSettings settings)
        {
            _eventsPage = eventsPage;
            _driver = driver;
            _settings = settings;
        }

        [Given(@"I open the created event")]
        public void GivenIOpenTheCreatedEvent()
        {
            _eventsPage.OpenFirstEvent();
        }

        [When(@"I click the ""Edit"" button")]
        public void WhenIClickTheEditButton()
        {
            _eventsPage.ClickEdit();
        }

        [Then(@"the edit form should display the pre-populated event details:")]
        public void ThenTheEditFormShouldDisplayThePre_PopulatedEventDetails(Table table)
        {
            foreach (var row in table.Rows)
            {
                string field = row["Field"];
                string expectedValue = row["ExpectedValue"];

                switch (field)
                {
                    case "Title":
                        string actualTitle = _eventsPage.GetTitleInputValue();
                        actualTitle.Should().StartWith(expectedValue, "The title should begin with the base generated name.");
                        break;

                    case "Description":
                        string actualDesc = _eventsPage.GetDescriptionInputValue();
                        if (string.IsNullOrWhiteSpace(expectedValue))
                        {
                            actualDesc.Should().BeNullOrEmpty("Description should be empty by default.");
                        }
                        else
                        {
                            actualDesc.Should().Be(expectedValue);
                        }
                        break;
                }

            }
        }

        [When(@"I update the event title:")]
        public void WhenIUpdateTheEventTitle(Table table)
        {
            var row = table.Rows.FirstOrDefault(r => r["Field"].Equals("Title", StringComparison.OrdinalIgnoreCase));

            if (row != null)
            {
                string newTitle = row["Value"];
                _eventsPage.UpdateTitle(newTitle);
            }
        }

        [When(@"I click ""Save Changes""")]
        public void WhenIClickSaveChanges()
        {
            _eventsPage.SaveChangesButtonClick();
            
        }

        [Then(@"the event page should display the updated title ""(.*)""")]
        public void ThenTheEventPageShouldDisplayTheUpdatedTitle(string expectedTitle)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            bool isUpdated = wait.Until(d =>
            {
                try
                {
                    string currentText = _eventsPage.GetCreatedEventTitle().Trim();
                    return currentText.Equals(expectedTitle, StringComparison.OrdinalIgnoreCase);
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });

            isUpdated.Should().BeTrue($"Expected title to update to '{expectedTitle}', but stayed '{_eventsPage.GetCreatedEventTitle()}'.");
        }
    }
}

