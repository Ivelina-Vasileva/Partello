using Reqnroll;
using OpenQA.Selenium;

namespace Partello.Hooks
{
    [Binding]
    public class TestHooks
    {
       private readonly IWebDriver _driver;
        public TestHooks(IWebDriver driver)
        {
            _driver = driver;
        }

        [BeforeScenario]
        public void Setup()
        {
        }

        [AfterScenario]
        public void TearDown()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }
    }
}