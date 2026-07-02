Feature: Login

As a registered user of Partello
I want to log into my account using my credentials
So that I can access my dashboard and premium features

@Smoke @Authentication
Scenario: Successfully login with valid user credentials
	Given I have navigated to the Partello sign-in page
	When I submit the form with my registered account credentials
	Then I should be redirected to the dashboard page