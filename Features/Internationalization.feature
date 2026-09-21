Feature: Internationalization

As a user of Partello
I want to switch between languages
So that I can use the application in my preferred language and see translated UI and URLs
@Internationalization
Scenario: Locale switcher is visible in header
	Given I have navigated to the Partello Base page
	Then the locale switcher should be visible in the header

Scenario Outline: URLs include locale prefix when switching language
	Given I have navigated to the Partello Base page
	When I switch language to "<language>"
	Then the current URL should contain "<prefix>"

Examples:
	| language | prefix |
	| English  |        |
	| Spanish  | /es    |
