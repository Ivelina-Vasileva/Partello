using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partello.Pages
{
    public  class TeamSettingsPage : BasePage
    {
        public TeamSettingsPage(IWebDriver driver) : base(driver)
        {
        }
        public IWebElement BuyCreditsButton =>
            _driver.FindElement(By.XPath("//div[@data-slot='card']//a[@href='/pricing']"));
        public IWebElement EditTeamNameButton =>
            _driver.FindElement(By.XPath("//button[.//path[contains(@d, 'M21.174')]]"));
        public IWebElement InviteMemberEmailInput =>
            _driver.FindElement(By.Id("email"));
        public IWebElement InviteMemberRoleSelect =>
            _driver.FindElement(By.XPath("//form//select"));
        public IWebElement InviteMemberSubmitButton =>
            _driver.FindElement(By.XPath("//form//button[@type='submit']"));
    }
}
