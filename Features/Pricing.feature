Feature: Pricing Page

  As a public visitor of Partello
  I want to see the subscription tiers and their details on the pricing page
  So that I can choose the plan that best fits my needs

  @Smoke @Pricing
  Scenario Outline: All subscription tiers are displayed on the pricing page
    Given I have navigated to the pricing page
    Then the "<tier>" pricing tier should be displayed

    Examples:
      | tier     |
      | Free     |
      | Intimate |
      | Standard |
      | Large    |

  @Regression @Pricing
  Scenario: The Standard tier is highlighted as the most popular plan
    Given I have navigated to the pricing page
    Then the Standard pricing tier should display the "Most Popular" badge

    @Regression @Pricing
    Scenario: The Standard tier displays the savings badge compared to the Large tier
    Given I have navigated to the pricing page
    Then the Standard pricing tier should display the savings badge

    @Regression @Pricing
  Scenario Outline: Verify the number of included feature checkmarks per tier
    Given I have navigated to the pricing page
    Then the "<tier>" tier should display <expectedFeaturesCount> feature checkmarks

    Examples:
      | tier     | expectedFeaturesCount |
      | Free     | 3                     |
      | Intimate | 3                     |
      | Standard | 4                     |
      | Large    | 5                     |

      @Regression @Pricing
  Scenario: Clicking the Read our FAQ link navigates to the FAQ page
    Given I have navigated to the pricing page
    When I click the Read our FAQ link
    Then I should be redirected to the FAQ page