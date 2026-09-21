Feature: DeleteEvent

As a registered Partello user
I want to be able to delete my existing events
So that I can keep my events list clean and up to date

@Smoke @Events @Create_Event_First
Scenario: Successful event deletion
    When I delete the latest created event
    Then I should not see the deleted event in my events list
