Feature: Login

As a registered user of Partello
I want to log into my account using my credentials
So that I can access my dashboard and premium features

@Smoke @Authentication
Scenario: Successfully login with valid user credentials
	Given I have navigated to the Partello sign-in page
	When I submit the form with my registered account credentials
	Then I should be redirected to the dashboard page

@Regression @Authentication @Negative
Scenario Outline: Unsuccessful login with invalid credentials
	Given I have navigated to the Partello sign-in page
	When I enter "<email>" as login email
	And I enter "<password>" as login password
	And I click the Sign In submit button
	Then I should see the expected validation outcome for "<test_type>"

Examples:
	| email                       | password  | test_type |
	| ivelinavasileva12355@abv.bg | Fiorii12. | wrong_email     |
	| ivelinavasileva123@abv.bg   | Pass123!  | wrong_password  |

@Regression @Authentication @GoogleLoginFlow
Scenario: Succesfull login with Google account
Given I have navigated to the Partello sign-in page
When I click Continue with Google button
And I enter google email as login email
And I enter google password 
Then I should be redirected to the dashboard page

@Regression @Authentication @RateLimiting
Scenario: Rate limiting triggers cooldown timer after excessive sign-ins
    Given I have navigated to the Partello sign-in page
    When I attempt to sign in rapidly with invalid credentials 7 times
    Then I should see the rate limit cooldown message