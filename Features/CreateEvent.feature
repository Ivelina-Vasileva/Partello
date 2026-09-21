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

@Regression @Validation @Negative @Login_Before_Test
Scenario Outline: Validate event creation form with invalid data inputs
	Given I'm on the Create Event Page
	When  I select Free plan option
	And I set event name "<EventName>", date option to "<DateOption>" and RSVP option to "<RsvpOption>"
	And I click Save Event button
	Then Event should not be created
	And I should see validation message "<ExpectedError>"

Examples:
	| EventName            | DateOption | RsvpOption | ExpectedError                            |
	|                      | Tomorrow   | Today      | Please fill out this field               |
	| My Party             | None       | Today      | Date & time is required                  |
	| Party After Deadline | Today      | Tomorrow   | RSVP deadline cannot be after event date |
	| Past Event Test      | Yesterday  | Yesterday  | Date cannot be in the past               |

	@Regression @UI @Checklist @Login_Before_Test
  Scenario Outline: Verify form fields and pickers accept input correctly
    Given I'm on the Create Event Page
    When I fill field "<FieldName>" with value "<InputValue>"
    Then Field "<FieldName>" should contain value "<InputValue>"

    Examples:
      | FieldName        | InputValue              |
      | Event Title      | Auto Test Wedding       |
      | Venue            | Grand Hotel Sofia       |
      | Address          | Tsar Osvoboditel 10     |
      | Description      | Welcome to our party!   |
      | Type             | birthday                |
      | Time Slot        | 23:30                   |
      | Date Picker      | Tomorrow                |
      | RSVP Picker      | Today                   |

	  @Regression @Positive @Login_Before_Test
  Scenario: Successfully create an event scheduled for next year
  Given I'm on the Create Event Page
    When I select Free plan option
    And I create an event titled "Next Year Celebration" with date option "next year"
	And I click Save Event button
    Then I should see the event listed in my events
