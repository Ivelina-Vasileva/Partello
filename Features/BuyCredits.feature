Feature: BuyCredits

As a registered user of Partello
I want to purchase credits using a credit card
So that I can use premium features on the platform

@Smoke @Payments
Scenario: Successful credit purchase
	Given I'm logged in Partello
	When I click Buy Credits link
	And I choose to purchase the "Intimate" credit plan
	And I enter payment details
	Then I shoud see that my payment was successful
