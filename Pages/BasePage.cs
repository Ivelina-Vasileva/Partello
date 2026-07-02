using OpenQA.Selenium;

namespace Partello.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver _driver;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        //Header Elements
        public IWebElement FeaturesDropDownMenu => _driver.FindElement(By.XPath("//button[contains(., 'Features')]"));
        //Options inside the feature's DropDown Menu
        public IWebElement DropdownLinkPersonalLinks => _driver.FindElement(By.XPath("//a[@href='/features/personal-links']"));
        public IWebElement DropdownLinkGuestManagement => _driver.FindElement(By.XPath("//a[@href='/features/guest-management']"));
        public IWebElement DropdownLinkDietaryTracking => _driver.FindElement(By.XPath("//a[@href='/features/dietary-tracking']"));
        public IWebElement DropdownLinkLiveDashboard => _driver.FindElement(By.XPath("//a[@href='/features/live-dashboard']"));
        // Main page links
        public IWebElement PricingLink => _driver.FindElement(By.XPath("//span[contains(text(), 'Pricing')]"));
        public IWebElement BlogLink => _driver.FindElement(By.XPath("//span[contains(text(), 'Blog')]"));
        public IWebElement FaqLink => _driver.FindElement(By.XPath("//span[contains(text(), 'FAQ' )]"));
        public IWebElement LanguageDropDownMenu => _driver.FindElement(By.XPath("//button[contains(text(), 'English')]"));
        //Language DropDown Options
        public IWebElement LanguageOptionEnglish => _driver.FindElement(By.XPath("//div[@role='menuitem' or @data-slot='dropdown-menu-item']//span[text()='English']"));
        public IWebElement LanguageOptionSpanish => _driver.FindElement(By.XPath("//div[@role='menuitem' or @data-slot='dropdown-menu-item']//span[text()='Spanish']"));
        // Header buttons
        public IWebElement HeaderSignInButton => _driver.FindElement(By.XPath("//header//a[@href='/sign-in']"));
        public IWebElement HeaderSignUpButton => _driver.FindElement(By.XPath("//header//a[@href='/sign-up']"));
        // Body Elements 
        public IWebElement PricingCardFree => _driver.FindElement(By.XPath("//div[contains(@class, 'card') or data-slot='card']//h3[contains(text(), 'Free')]"));
        public IWebElement PricingCardIntimate => _driver.FindElement(By.XPath("//div[contains(@class, 'card') or data-slot='card']//h3[contains(text(), 'Intimate')]"));
        public IWebElement PricingCardStandard => _driver.FindElement(By.XPath("//div[contains(@class, 'card') or data-slot='card']//h3[contains(text(), 'Standard')]"));
        public IWebElement PricingCardLarge => _driver.FindElement(By.XPath("//div[contains(@class, 'card' or data-slot='card']//h3[contains(text(), 'Large' )]"));
        // Buttons inside the pricing cards
        public IWebElement PricingFreeGetStartedButton => _driver.FindElement(By.XPath("//h3[contains(text(), 'Free')]/following-sibling::..//a[@href='/sign-up']"));
        public IWebElement PricingIntimateGetStartedButton => _driver.FindElement(By.XPath("//h3[contains(text(), 'Intimate')]/following-sibling::..//a[@href='/sign-up']"));
        public IWebElement PricingStandardGetStartedButton => _driver.FindElement(By.XPath("//h3[contains(text(), 'Standard')]/following-sibling::..//a[@href='/sign-up']"));
        public IWebElement PricingLargeGetStartedButton => _driver.FindElement(By.XPath("//h3[contains(text(), 'Large')]/following-sibling::..//a[@href='/sign-up']"));
        //Footer Elements
        public IWebElement FooterPricingLink => _driver.FindElement(By.XPath("//footer//a[contains(text(), 'Pricing')]"));
        public IWebElement FooterBlogLink => _driver.FindElement(By.XPath("//footer//a[contains(text(), 'Blog')]"));
        public IWebElement FooterContactLink => _driver.FindElement(By.XPath("//footer//a[contains(text(), 'Contact')]"));
        public IWebElement FooterFAQLink => _driver.FindElement(By.XPath("//footer//a[contains(text(), 'FAQ')]"));
        public IWebElement FooterPrivacyPolicyLink => _driver.FindElement(By.XPath("//footer//a[contains(text(), 'Privacy Policy')]"));
        public IWebElement FooterTermsOfServiceLink => _driver.FindElement(By.XPath("//footer//a[contains(text(), 'Terms of Service')]"));
        public IWebElement FooterCookiePolicyLink => _driver.FindElement(By.XPath("//footer//a[contains(text(), 'Cookie Policy')]"));
        public IWebElement FooterDisclaimerLink => _driver.FindElement(By.XPath("//footer//a[contains(text(), 'Disclaimer')]"));

    }
}