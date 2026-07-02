# Partello - End-to-End Test Automation

Automated end-to-end tests for **[Partello](https://saas-test-two-eta.vercel.app)**, a SaaS event management platform that lets users create, manage, and track RSVPs for events. The test suite covers the core user journey: registration, login, event creation, credit purchasing, and event deletion.

---

## 🔧 Tech Stack & Decisions

| Area | Choice | Why |
|---|---|---|
| **Language** | C# (.NET 8) | Strongly typed, excellent tooling, and first-class DI support - ideal for maintainable test suites |
| **Test framework** | [NUnit](https://nunit.org/) | Mature, widely adopted, and integrates seamlessly with Reqnroll |
| **BDD layer** | [Reqnroll](https://reqnroll.github.io/) | Community-maintained fork of SpecFlow; bridges Gherkin feature files with .NET step definitions and supports dependency injection out of the box |
| **Browser automation** | [Selenium WebDriver](https://www.selenium.dev/) | Industry standard for web UI automation; supports all major browsers |
| **DI container** | `Microsoft.Extensions.DependencyInjection` | Built into .NET; cleanly wires up pages, driver, and config |
| **Configuration** | `.env` file via [DotNetEnv](https://github.com/tonerdo/dotnet-env) | Keeps secrets out of source control and makes setup trivial across environments |
| **Reporting** | [Allure](https://allure.qatools.ru/) (via `Allure.Reqnroll`) | Rich HTML reports with timeline, severity, and per-step screenshots |
| **Assertions** | [FluentAssertions](https://fluentassertions.com/) | Readable, expressive assertions that produce clear failure messages |

**Architecture:** Page Object Model (POM) with Dependency Injection. Each page (`SignInPage`, `EventsPage`, `CheckoutPage`, etc.) encapsulates its locators and actions. Step-definition classes receive only the pages they need via constructor injection.

---

## ✅ Test Cases Covered

| # | Scenario | Feature File | Type |
|---|---|---|---|
| 1 | Successful login with valid credentials | `Login.feature` | Happy path |
| 2 | Successful registration with a unique email | `Register.feature` | Happy path |
| 3 | Create a new event (Free plan) | `CreateEvent.feature` | Happy path |
| 4 | Delete an existing event | `DeleteEvent.feature` | Happy path |
| 5 | Purchase "Intimate" credit plan via LemonSqueezy | `BuyCredits.feature` | Happy path (payment) |

All scenarios are tagged `@Smoke` and can be run together as a smoke suite.

---

## 🐛 Bug Report

No blocking bugs were discovered during automation. However, one observation worth noting:

- **Stripe Link popup**: During checkout, Stripe Link occasionally intercepts the flow if the browser session has a saved email. The test works around this by injecting a dummy verification code into the hidden Stripe Link iframe. In a production test suite this should be handled via a dedicated LemonSqueezy test-mode configuration or by clearing browser state before the payment scenario.

---

## 📋 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Google Chrome](https://www.google.com/chrome/) (the tests use ChromeDriver; it is bundled with the Selenium NuGet package)
- A **pre-registered Partello account** (see [Setup](#-setup) below)

---

## ⚙️ Setup

### 1. Clone the repository

```bash
git clone https://github.com/Ivelina-Vasileva/Partello.git
cd Partello
```

### 2. Create a Partello account

Before running the tests, go to [Partello](https://saas-test-two-eta.vercel.app/sign-up) and create an account. You will use these credentials for the authenticated tests (login, create event, buy credits, delete event).

### 3. Configure environment variables

Copy the example environment file and fill in your account details:

```bash
cp .env.example .env
```

Then edit `.env`:

```env
# Partello Web Application Base URL
PARTELLO_BASE_URL=https://saas-test-two-eta.vercel.app/

# User Authentication Details for Automation Tests
PARTELLO_LOGIN_EMAIL=your_email_here@example.com
PARTELLO_LOGIN_PASSWORD=your_password_here
```

> ⚠️ **Important:** The `.env` file is git-ignored. Never commit real credentials. The `.env.example` file serves as the template.

### 4. Restore dependencies

```bash
dotnet restore
```

---

## ▶️ Running the Tests

### Run all tests

```bash
dotnet test
```

### Run only smoke tests

```bash
dotnet test --filter "Category=Smoke"
```

### Run a specific feature

```bash
dotnet test --filter "FullyQualifiedName~Login"
dotnet test --filter "FullyQualifiedName~Register"
dotnet test --filter "FullyQualifiedName~CreateEvent"
dotnet test --filter "FullyQualifiedName~DeleteEvent"
dotnet test --filter "FullyQualifiedName~BuyCredits"
```

### Allure Report

After a test run, Allure result files are written to `bin/Debug/net8.0/allure-results/`. To generate and view the HTML report:

```bash
# Install Allure CLI (one time)
# Download from: https://github.com/allure-framework/allure2/releases

# Generate and open the report
allure serve bin/Debug/net8.0/allure-results/
```

---

## 📁 Project Structure

```
Partello/
├── .env.example              # Template for environment variables
├── appsettings.json          # Fallback config (BaseUrl, Browser, etc.)
├── reqnroll.json             # Reqnroll configuration (NUnit + DI plugin)
├── Partello.csproj           # .NET 8 project with NuGet dependencies
│
├── Config/
│   └── TestSettings.cs       # Strongly-typed settings model
│
├── Drivers/
│   └── DependencyContainer.cs # DI wiring: driver, pages, config
│
├── Features/                 # Gherkin feature files
│   ├── Login.feature
│   ├── Register.feature
│   ├── CreateEvent.feature
│   ├── DeleteEvent.feature
│   └── BuyCredits.feature
│
├── Steps/                    # Step definitions (bind Gherkin to code)
│   ├── LoginSteps.cs
│   ├── RegisterSteps.cs
│   ├── CreateEventSteps.cs
│   ├── DeleteEventSteps.cs
│   └── BuyCreditsSteps.cs
│
├── Pages/                    # Page Object Model
│   ├── BasePage.cs           # Shared header/footer/navigation elements
│   ├── HomePage.cs
│   ├── SignInPage.cs
│   ├── SignUpPage.cs
│   ├── PricingPage.cs
│   ├── CheckoutPage.cs       # Stripe iframe handling
│   ├── EventsPage.cs         # Event CRUD actions
│   ├── BlogPage.cs
│   └── FaqPage.cs
│
└── Hooks/
    └── TestHooks.cs          # Before/After scenario (driver cleanup)
```

---

## 🔮 What I'd Test Next (With More Time)

1. **Negative payment scenarios** - use LemonSqueezy's test decline cards (`4000000000000002` for decline, `4000000000000259` for 3D Secure) to verify error handling.
2. **Edit event flow** - update an existing event's title, date, or venue and assert the changes are persisted.
3. **RSVP management** - simulate guest RSVP submissions and verify the live dashboard updates.
4. **Cross-browser matrix** - parameterise `Browser` to run the same suite against Firefox and Edge.
5. **Visual regression** - integrate Percy or Applitools to catch unintended UI changes.
6. **API-level test layer** - supplement UI tests with direct HTTP calls (via RestSharp, already in the project) for faster feedback on backend logic.
7. **CI/CD pipeline** - run the suite on every push via GitHub Actions, with Allure report published as an artifact.

---

## 📝 Assumptions

- The target environment (`https://saas-test-two-eta.vercel.app`) is a stable staging/QA deployment that tolerates automated test data.
- The pre-registered account has no credits; the buy-credits scenario purchases new credits.
- The register test generates a unique email at each run (`qa_test_{timestamp}@partello-test.com`), so it never collides with a previous registration.
- LemonSqueezy is in test mode, so the hard-coded card `4242 4242 4242 4242` succeeds without real charges.
- Tests run sequentially in a single thread (no parallelisation configured).
