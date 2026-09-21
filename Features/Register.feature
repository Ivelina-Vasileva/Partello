Feature: Register

As a new visitor of Partello
I want to create a new account
So that I can access the platform features and buy credits

@Smoke @Authentication
Scenario: Successfully register a new user with valid credentials
	Given I have navigated to the Partello sign-up page
	When I enter a unique registration email
	And I enter "Pass123!" as registration password
	And I click the Sign Up submit button
	Then I should be successfully logged into the system

@Regression @Authentication @Negative
Scenario Outline: Unsuccessful registration with invalid or existing credentials
	Given I have navigated to the Partello sign-up page
	When I enter "<email>" as registration email
	And I enter "<password>" as registration password
	And I click the Sign Up submit button
	Then I should see the expected validation outcome for "<test_type>"

Examples:
	| email                     | password | test_type      |
	| ivelinavasileva123@abv.bg | Pass123! | existing_email |
	| ivelinavasileva123abv.bg  | Pass123! | invalid_email  |
	| new_user@partello.com     |    12345 | short_password |

	@Smoke @Authentication
  Scenario: Clicking the Sign In link redirects to the sign-in page
    Given I have navigated to the Partello sign-up page
    When I click the Sign In link on the sign-up page
    Then I should be redirected to the sign-in page

	@Regression @Authentication
  Scenario: Google authentication button is available on sign-up page
    Given I have navigated to the Partello sign-up page
    Then the Continue with Google button should be visible and clickable

	@Regression @Authentication
  Scenario: Toggle password visibility on sign-up page
    Given I have navigated to the Partello sign-up page
    When I enter "Pass123!" as registration password
    Then the password field should mask the input
    When I toggle the password visibility button
    Then the password field should display the text in plain text
    When I toggle the password visibility button
    Then the password field should mask the input