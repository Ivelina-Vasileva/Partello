using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Partello.Utils;

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
        public IWebElement FeaturesDropDownMenu => _driver.FindElement(By.XPath("//button[//*[name()='svg' and contains(@class, 'lucide-sparkles')]]"));
        private readonly By _userAvatarMenuLocator = By.XPath("//button[.//span[@data-slot='avatar' or @data-slot='avatar-fallback']]");
        public IWebElement UserAvatarMenu => _driver.WaitForElementClickable(_userAvatarMenuLocator);
        private readonly By _signOutButtonLocator = By.XPath("//div[@role='menu']//*[text()='Sign out']");
        public IWebElement SignOutButton => _driver.WaitForElementClickable(_signOutButtonLocator);

        //Options inside the feature's DropDown Menu
        public IWebElement DropdownLinkPersonalLinks => _driver.FindElement(By.XPath("//a[@href='/features/personal-links']"));
        public IWebElement DropdownLinkGuestManagement => _driver.FindElement(By.XPath("//a[@href='/features/guest-management']"));
        public IWebElement DropdownLinkDietaryTracking => _driver.FindElement(By.XPath("//a[@href='/features/dietary-tracking']"));
        public IWebElement DropdownLinkLiveDashboard => _driver.FindElement(By.XPath("//a[@href='/features/live-dashboard']"));
        // Main page links
        public IWebElement PricingLink => _driver.FindElement(By.XPath("a[href='/pricing']"));
        public IWebElement BlogLink => _driver.FindElement(By.XPath("a[href='/blog']"));
        public IWebElement FaqLink => _driver.FindElement(By.XPath("a[href='/faq']"));
        //Language DropDown Options
        public IWebElement LanguageDropDownMenu => _driver.FindElement(By.CssSelector("button[data-slot='dropdown-menu-trigger']"));
        public IWebElement LanguageOptionEnglish => _driver.FindElement(By.XPath("//div[@role='menuitem' and normalize-space()='English']"));
        public IWebElement LanguageOptionSpanish => _driver.FindElement(By.XPath("//div[@role='menuitem' and normalize-space()='Spanish']"));
        // Header buttons
        public IWebElement HeaderSignInButton => _driver.FindElement(By.XPath("//header//a[@href='/sign-in']"));
        public IWebElement HeaderSignUpButton => _driver.FindElement(By.XPath("//header//a[@href='/sign-up']"));
        // Body Elements 
        public IWebElement PricingCardFree => _driver.FindElement(By.CssSelector("div.relative:has(a[href$='/events/new'])"));
        public IWebElement PricingCardIntimate => _driver.FindElement(By.CssSelector("div.relative:has(input[value='1627188'])"));
        public IWebElement PricingCardStandard => _driver.FindElement(By.CssSelector("div.relative:has(input[value='1627191'])"));
        public IWebElement PricingCardLarge => _driver.FindElement(By.CssSelector("div.relative:has(input[value='1627192'])"));
        // Buttons inside the pricing cards
        public IWebElement PricingFreeGetStartedButton => _driver.FindElement(By.CssSelector("a[href$='/events/new']"));
        public IWebElement PricingIntimateGetStartedButton => _driver.FindElement(By.CssSelector("form:has(input[value='1627188']) button"));
        public IWebElement PricingStandardGetStartedButton => _driver.FindElement(By.CssSelector("form:has(input[value='1627191']) button"));
        public IWebElement PricingLargeGetStartedButton => _driver.FindElement(By.CssSelector("form:has(input[value='1627192']) button"));
        //Footer Elements
        public IWebElement FooterLinkFeatures => _driver.FindElement(By.XPath("//footer//a[contains(@href, '/features')]"));
        public IWebElement FooterPricingLink => _driver.FindElement(By.XPath("//footer//a[contains(@href, '/pricing')]"));
        public IWebElement FooterBlogLink => _driver.FindElement(By.XPath("//footer//a[contains(@href, '/blog')]"));
        public IWebElement FooterContactLink => _driver.FindElement(By.XPath("//footer//a[contains(href@, '/contact')]"));
        public IWebElement FooterFAQLink => _driver.FindElement(By.XPath("//footer//a[contains(href@, '/faq')]"));
        public IWebElement FooterPrivacyPolicyLink => _driver.FindElement(By.XPath("//footer//a[contains(@href, '/legal/privacy')]"));
        public IWebElement FooterTermsOfServiceLink => _driver.FindElement(By.XPath("//footer//a[contains(@href, '/legal/terms')]"));
        public IWebElement FooterCookiePolicyLink => _driver.FindElement(By.XPath("//footer//a[contains(@href, '/legal/cookies')]"));
        public IWebElement FooterDisclaimerLink => _driver.FindElement(By.XPath("//footer//a[contains(@href, '/legal/disclaimer')]"));

    
    public void SelectLanguage(string language)
        {
            var dropdown = _driver.WaitForElementClickable(LanguageDropDownMenu, timeoutSeconds: 5);
            new Actions(_driver).MoveToElement(dropdown).Click().Perform();

            IWebElement targetOption = language.ToLower() switch
            {
                "english" or "en" => LanguageOptionEnglish,
                "spanish" or "es" => LanguageOptionSpanish,
                _ => throw new ArgumentException($"Unsupported language: {language}")
            };

            var clickableOption = _driver.WaitForElementClickable(targetOption, timeoutSeconds: 5);
            new Actions(_driver).MoveToElement(clickableOption).Click().Perform();
        }
    }
}