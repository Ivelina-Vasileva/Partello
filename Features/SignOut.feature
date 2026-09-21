Feature: SignOut

As a logged-in user of Partello
I want to be able to sign out of my account
So that I can secure my session on shared devices

@Smoke @Authentication
Scenario Outline: Successful sign out via user avatar menu and verify protected routes redirect to sign-in after Sign Out
	Given I'm logged in Partello
	When I click on the user avatar in the top-right corner
	And I click the Sign Out option
	Then I should be redirected to the homepage
	And protected routes like "<route>" should redirect me to "<redirect_page>"

Examples:
	| route     | redirect_page |
	| /events   | /sign-in      |
	| /settings | /sign-in      |