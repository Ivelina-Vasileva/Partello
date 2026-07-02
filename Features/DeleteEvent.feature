Feature: DeleteEvent

As a registered Partello user
I want to be able to delete my existing events
So that I can keep my events list clean and up to date

@Smoke @Events
Scenario: Successful event deletion
    Given I'm logged in Partello
    When I click Events link in the header
    And I delete the event named "Ivelina's Birthday"
    Then I should not see the event "Ivelina's Birthday" in my events list
