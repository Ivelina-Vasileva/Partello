Feature: CreateEvent

As a registered Partello user
I want to be able to create new events

@Smoke @Regression
Scenario: Successful Event Creation with Available Credits
Given I'm logged in Partello
    When I click Events link in the header
    And I click Create Event button
    And I fill event details 
    And I click Save Event button
    Then I should see the event listed in my events
