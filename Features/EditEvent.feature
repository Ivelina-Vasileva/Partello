Feature: EditEvent

A short summary of the feature

@Create_Event_First
Scenario: Edit button opens form with pre-populated fields
	Given I open the created event
	When I click the "Edit" button
	Then the edit form should display the pre-populated event details:
		| Field       | ExpectedValue        |
		| Title       | Auto Event           |
		| Description | We have FREE Parking |

@Create_Event_First
Scenario: Editing event fields and saving persists changes
	Given I open the created event
	When I click the "Edit" button
	And I update the event title:
		| Field       | Value              |
		| Title       | Updated Event Name |
	And I click "Save Changes"
	Then the event page should display the updated title "Updated Event Name"
	