using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Partello.Pages;
using Reqnroll.Microsoft.Extensions.DependencyInjection;
using Partello.Config;

namespace Partello.Drivers
{
    public class DependencyContainer
    {
        [ScenarioDependencies]
        public static IServiceCollection RegisterDependencies()
        {
            var services = new ServiceCollection();

            services.AddSingleton(sp =>
            {
                DotNetEnv.Env.Load();
                return new TestSettings
                {
                    BaseUrl = Environment.GetEnvironmentVariable("PARTELLO_BASE_URL") ?? "",
                    Browser = "Chrome",
                    LoginEmail = Environment.GetEnvironmentVariable("PARTELLO_LOGIN_EMAIL") ?? "",
                    LoginPassword = Environment.GetEnvironmentVariable("PARTELLO_LOGIN_PASSWORD") ?? "",
                    GoogleEmail = Environment.GetEnvironmentVariable("PARTELLO_GOOGLE_EMAIL") ?? "",
                    GooglePassword = Environment.GetEnvironmentVariable("PARTELLO_GOOGLE_PASSWORD") ?? ""
                };
            });

            services.AddScoped<IWebDriver>(sp =>
            {
                var options = new ChromeOptions();
                options.AddExcludedArgument("enable-automation");
                options.AddAdditionalOption("useAutomationExtension", false);
                options.AddArgument("--disable-blink-features=AutomationControlled");

                options.AddUserProfilePreference("credentials_enable_service", false);
                options.AddUserProfilePreference("profile.password_manager_enabled", false);

                options.AddArgument("--disable-notifications");
                options.AddArgument("--disable-popup-blocking");

                var driver = new ChromeDriver(options);
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                return driver;
            });

            RegisterPages(services);

            return services;
        }

        private static void RegisterPages(IServiceCollection services)
        {
            services.AddScoped<HomePage>();
            services.AddScoped<PricingPage>();
            services.AddScoped<BlogPage>();
            services.AddScoped<SignInPage>();
            services.AddScoped<SignUpPage>();
            services.AddScoped<EventsPage>();
            services.AddScoped<CheckoutPage>();
            services.AddScoped<TeamSettingsPage>();
        }
    }
}