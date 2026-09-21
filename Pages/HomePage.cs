using OpenQA.Selenium;

namespace Partello.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement HeroStartForFreeButton => _driver.FindElement(By.XPath("//main//a[contains(@href,'/sign-up')]"));
        public IWebElement HeroSignInButton => _driver.FindElement(By.XPath("//main//a[contains(@href,'/sign-in')]"));
        public IWebElement ViewFullPlanComparisonLink => _driver.FindElement(By.XPath("//a[contains(@href, '/pricing#pricing']"));
        public IWebElement GetStartedFreeButton => _driver.FindElement(By.XPath("//a[contains(@href,'/sign-up')]"));
    }
}
