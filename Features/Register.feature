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
