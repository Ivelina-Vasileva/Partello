using OpenQA.Selenium;

namespace Partello.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement HeroStartForFreeButton => _driver.FindElement(By.XPath("//main//a[@href='/sign-up' and contains(., 'Start for free')]"));
        public IWebElement HeroSignInButton => _driver.FindElement(By.XPath("//main//a[@href='/sign-in' and contains(., 'Sign in')]"));
        public IWebElement ViewFullPlanComparisonLink => _driver.FindElement(By.XPath("//a[@href='/pricing#pricing']"));
        public IWebElement GetStartedFreeButton => _driver.FindElement(By.XPath("//a[@href='/sign-up' and contains(., 'Get started free')]"));
    }
}
