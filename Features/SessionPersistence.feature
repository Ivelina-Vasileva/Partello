Feature: SessionPersistence

As a registered Partello user
I want my sign-in session to persist after closing the browser
So that I don't have to log in every time I open the app

@Regression @Authentication @Session
Scenario: Session remains active after closing and reopening the browser
	Given I'm logged in Partello
	When I close and reopen the browser
	And I navigate to the settings page
	Then I should still be authenticated without being redirected to sign-in

@Regression @Security
Scenario: Session expires after 24 hours of inactivity
	Given I'm logged in Partello
	When 24 hours of inactivity pass
	And I refresh the page
	Then My session should expire after inactivity
