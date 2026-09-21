Feature: Features Navigation Dropdown

  As a public visitor of Partello
  I want to navigate through the Features dropdown menu
  So that I can learn more about specific application capabilities

  @Smoke @Navigation
  Scenario Outline: Navigate to feature pages via header dropdown
    Given I have navigated to the homepage
    When I hover over the Features dropdown menu
    And I click on the "<feature_link>" option
    And I should be redirected to "<expected_url>"
    Then the Features menu button should have an active state indicator

    Examples:
      | feature_link       | expected_url                  |
      | Personal Links     | /features/personal-links      |
      | Guest Management   | /features/guest-management    |
      | Dietary Tracking   | /features/dietary-tracking    |
      | Live Dashboard     | /features/live-dashboard      |