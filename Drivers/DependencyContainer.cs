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
                    LoginPassword = Environment.GetEnvironmentVariable("PARTELLO_LOGIN_PASSWORD") ?? ""
                };
            });

            services.AddScoped<IWebDriver>(sp =>
            {
                var driver = new ChromeDriver();
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
            services.AddScoped<PricingPage>();
            services.AddScoped<EventsPage>();
            services.AddScoped<CheckoutPage>();
        }
    }
}