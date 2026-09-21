# Partello - End-to-End Test Automation

Automated end-to-end tests for **[Partello](https://saas-test-two-eta.vercel.app)**, a SaaS event management platform that lets users create, manage, and track RSVPs for events. The suite covers the full user journey - registration, login (credentials + Google OAuth), sign out, session persistence, event create/edit/delete, credit purchasing, pricing, feature navigation, and internationalization.

For a complete manual QA checklist see [QA_TESTING_GUIDE.md](QA_TESTING_GUIDE.md); for a gap analysis of what is (and isn't) automated, see [MISSING_TEST_SCENARIOS.md](MISSING_TEST_SCENARIOS.md).

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

**Architecture:** Page Object Model (POM) with Dependency Injection. Each page (`SignInPage`, `EventsPage`, `CheckoutPage`, etc.) encapsulates its locators and actions. Step-definition classes receive only the pages they need via constructor injection. Resilient click/wait helpers live in `Utils/` (`WaitExtensions`, `ClickSafe`) to handle Selenium's common failure modes (stale elements, occluded clicks).

---

## ✅ Test Cases Covered

The suite has **11 feature files · 28 scenarios / 52 test cases** (scenario-outline examples expanded).

| Feature File | Covers |
|---|---|
| `Register.feature` | Valid sign-up; invalid/existing email + short password; password-visibility toggle; "Continue with Google"; sign-in link |
| `Login.feature` | Valid login; wrong email/password; Google OAuth; sign-in rate-limiting cooldown |
| `SignOut.feature` | Sign out via avatar menu + protected-route redirect |
| `SessionPersistence.feature` | Session survives browser restart; 24 h inactivity expiry |
| `CreateEvent.feature` | Event creation (happy path); validation errors; form-field inputs; next-year event |
| `EditEvent.feature` | Edit opens pre-populated form; save persists changes |
| `DeleteEvent.feature` | Delete a pre-created event (via the edit-page danger zone) |
| `BuyCredits.feature` | Purchase the "Intimate" credit plan via hosted checkout |
| `Pricing.feature` | Four tiers; "Most Popular"/savings badges; per-tier feature checkmarks; "Read our FAQ" → `/faq` |
| `FeaturesNavigation.feature` | Feature dropdown navigation to the four feature pages |
| `Internationalization.feature` | Locale switcher visibility + URL locale prefix (`/es`) |

### Tags

Scenarios are tagged for targeted runs: `@Smoke` (happy paths), `@Regression`, `@Authentication`, `@Negative`, `@Payments`, `@Pricing`, `@Navigation`, `@Session`, `@Security`, `@Internationalization`, and `@Events`. Two hook tags manage prerequisite state - `@Create_Event_First` pre-creates an event, and `@Login_Before_Test` pre-authenticates the session.

---

## 🐛 Bug Report

Known defects are tracked in [BUGS.md](BUGS.md). Open defects (reported 2026-09-21) affect the Create Event date/RSVP pickers:

- **BUG-001** Date can't be in the past - past event dates are not consistently rejected.
- **BUG-002** RSVP deadline after event date - a deadline later than the event date is accepted.
- **BUG-003** RSVP hardcoded year limit - the RSVP deadline picker is capped to a fixed year range.
- **BUG-004** Date picker doesn't allow future years - far-future event dates can't be selected.
- **BUG-005** Missing validation for past dates in date picker - past dates stay selectable in the calendar.

One additional observation worth noting:

- **Stripe Link popup**: During checkout, Stripe Link occasionally intercepts the flow if the browser session has a saved email. The test works around this by injecting a dummy verification code into the hidden Stripe Link iframe. In a production test suite this should be handled via a dedicated test-mode configuration or by clearing browser state before the payment scenario.

---

## 📋 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Google Chrome](https://www.google.com/chrome/) (the tests use ChromeDriver; it is bundled with the Selenium NuGet package)
- A **pre-registered Partello account** (see [Setup](#-setup) below)
- A **Google test account** for the OAuth sign-in/sign-up scenarios

---

## ⚙️ Setup

### 1. Clone the repository

```bash
git clone https://github.com/Ivelina-Vasileva/Partello.git
cd Partello
```

### 2. Create a Partello account

Before running the tests, go to [Partello](https://saas-test-two-eta.vercel.app/sign-up) and create an account. You will use these credentials for the authenticated tests (login, create event, edit/delete event, buy credits).

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

# Google OAuth credentials (for the Google login/sign-up scenarios)
PARTELLO_GOOGLE_EMAIL=your_google_email
PARTELLO_GOOGLE_PASSWORD=your_google_password
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

### Run by tag

```bash
# Smoke (happy paths)
dotnet test --filter "Category=Smoke"

# Regression / Authentication
dotnet test --filter "Category=Regression"
dotnet test --filter "Category=Authentication"
```

### Run a specific feature

```bash
dotnet test --filter "FullyQualifiedName~Login"
dotnet test --filter "FullyQualifiedName~Register"
dotnet test --filter "FullyQualifiedName~SignOut"
dotnet test --filter "FullyQualifiedName~SessionPersistence"
dotnet test --filter "FullyQualifiedName~CreateEvent"
dotnet test --filter "FullyQualifiedName~EditEvent"
dotnet test --filter "FullyQualifiedName~DeleteEvent"
dotnet test --filter "FullyQualifiedName~BuyCredits"
dotnet test --filter "FullyQualifiedName~Pricing"
dotnet test --filter "FullyQualifiedName~FeaturesNavigation"
dotnet test --filter "FullyQualifiedName~Internationalization"
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
├── appsettings.json          # Fallback config (BaseUrl, Browser, timeout)
├── reqnroll.json             # Reqnroll configuration (NUnit + DI plugin)
├── Partello.csproj           # .NET 8 project with NuGet dependencies
│
├── Config/
│   └── TestSettings.cs       # Strongly-typed settings model
│
├── Drivers/
│   └── DependencyContainer.cs # DI wiring: driver, pages, config
│
├── Features/                 # Gherkin feature files (11)
│   ├── Register.feature
│   ├── Login.feature
│   ├── SignOut.feature
│   ├── SessionPersistence.feature
│   ├── CreateEvent.feature
│   ├── EditEvent.feature
│   ├── DeleteEvent.feature
│   ├── BuyCredits.feature
│   ├── Pricing.feature
│   ├── FeaturesNavigation.feature
│   └── Internationalization.feature
│
├── Steps/                    # Step definitions (bind Gherkin to code)
│   ├── RegisterSteps.cs
│   ├── LoginSteps.cs
│   ├── SignOutSteps.cs
│   ├── SessionPersistenceSteps.cs
│   ├── CreateEventSteps.cs
│   ├── EditEventSteps.cs
│   ├── DeleteEventSteps.cs
│   ├── BuyCreditsSteps.cs
│   ├── PricingSteps.cs
│   ├── FeaturesNavigationSteps.cs
│   └── InternationalizationSteps.cs
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
│   ├── FaqPage.cs
│   └── TeamSettingsPage.cs   # Team settings + Buy Credits
│
├── Utils/                    # Resilient click/wait helpers
│   ├── WaitExtensions.cs
│   └── ClickSafe.cs
│
└── Hooks/
    └── TestHooks.cs          # Before/After scenario (prereqs, driver cleanup)
```

---

## 🔮 What I'd Test Next (With More Time)

Ordered roughly by priority (see [MISSING_TEST_SCENARIOS.md](MISSING_TEST_SCENARIOS.md)):

1. **Guest Management + RSVP flow** - add named/group guests, invite links, and the guest-side RSVP submission (the core product value and the largest un-automated gap).
2. **Event dashboard & analytics** - guests/analytics/activity tabs, RSVP stats, dietary breakdown, attendance trend.
3. **Onboarding wizard** - the first-login floating card and its three steps.
4. **Account & security settings** - change password, delete account, team roles/invites, activity log.
5. **Negative payment scenarios** - use test decline cards (`4000000000000002` for decline, `4000000000000259` for 3D Secure) to verify error handling.
6. **Blog + SEO landing pages** - blog index/posts and the wedding/birthday/features pages.
7. **Cross-browser matrix** - parameterise `Browser` to run the same suite against Firefox and Edge.
8. **Visual regression** - integrate Percy or Applitools to catch unintended UI changes.
9. **API-level test layer** - supplement UI tests with direct HTTP calls (via RestSharp, already in the project) for faster feedback on backend logic.
10. **CI/CD pipeline** - run the suite on every push via GitHub Actions, with Allure report published as an artifact.

---

## 📝 Assumptions

- The target environment (`https://saas-test-two-eta.vercel.app`) is a stable staging/QA deployment that tolerates automated test data.
- The pre-registered account has no credits; the buy-credits scenario purchases new credits.
- The register test generates a unique email at each run (`qa_test_{timestamp}@partello-test.com`), so it never collides with a previous registration.
- The hosted checkout is in test mode, so the hard-coded card `4242 4242 4242 4242` succeeds without real charges.
- The Google OAuth scenarios use the credentials from `.env` and expect the Google consent/account picker to be reachable.
- Tests run sequentially in a single thread (no parallelisation configured).
