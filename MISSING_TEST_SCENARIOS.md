# Partello - Missing Automated Test Scenarios

Gap analysis comparing existing Reqnroll automation against the full
[QA_TESTING_GUIDE.md](QA_TESTING_GUIDE.md).  Last updated 2026-09-21.

---

## Currently Automated (11 feature files · 28 scenarios / 52 test cases with outline examples)

| Feature File | Scenarios |
|---|---|
| `Register.feature` | valid sign-up; invalid/existing email + short password (outline, 3 examples); password-visibility toggle; "Continue with Google"; sign-in link |
| `Login.feature` | valid login; wrong email/password (outline, 2 examples); Google OAuth; rate-limiting cooldown (7 rapid sign-ins) |
| `SignOut.feature` | sign out + protected-route redirect (outline, 2 routes) |
| `CreateEvent.feature` | success; validation errors (outline, 4 examples); form-field inputs (outline, 8 examples); next-year event |
| `DeleteEvent.feature` | delete pre-created event (via edit page danger zone) |
| `BuyCredits.feature` | Stripe checkout |
| `SessionPersistence.feature` | reopen browser; 24 h expiry |
| `FeaturesNavigation.feature` | 4 feature links via dropdown |
| `Pricing.feature` | 4 tiers (outline); "Most Popular" badge; savings badge; feature checkmarks (outline, 4 examples); "Read our FAQ" → /faq |
| `EditEvent.feature` | edit opens pre-populated form; save persists changes |
| `Internationalization.feature` | locale switcher visible; URL locale prefix (outline, 2 examples) |

### Locators already in page objects but not yet exercised by any scenario

These are the cheapest starting points for new tests - the element locators exist, only the
feature/step wiring is missing:

- `BasePage` - footer legal links, header sign-in/up buttons, pricing-card + pricing-button locators (the language dropdown is now exercised by `Internationalization.feature`).
- `PricingPage` - four `Button*GetStarted` locators (the per-tier "Get Started" CTAs) are defined but not yet clicked by any scenario.
- `BlogPage` - six `*ReadArticleButton` locators + `BackToTheBlogLink` (Section 8).
- `FaqPage` - eight FAQ `<summary>` items + `ContactUsButton` (FAQ page / 7.1 inline FAQ).
- `TeamSettingsPage` - `EditTeamNameButton`, `InviteMemberEmailInput`, `InviteMemberRoleSelect`, `InviteMemberSubmitButton` (Section 11.3; `BuyCreditsButton` is already used by `BuyCredits.feature`).

---

## Section 1 - Authentication (partially covered)

> ✅ Automated in `Login.feature`: sign-in rate limiting (cooldown timer after 7 rapid sign-ins).

| # | Missing Scenario | Priority |
|---|---|---|
| 1.1 | Onboarding wizard floating card appears after successful sign-up | High |
| 1.1 | Rate limiting: 6+ rapid sign-ups → "Too many sign-up attempts" | Medium |

---

## Section 2 - Onboarding Wizard (**entirely missing**)

| # | Missing Scenario | Priority |
|---|---|---|
| 2.1 | New user lands on `/settings` after sign-up with floating card (3 steps) | High |
| 2.1 | Progress bar shows "Step 1 of 3" | High |
| 2.2 | Step 1 - "Create your first event" with action button → navigates to `/events/new` | High |
| 2.2 | After creating event, Step 1 marked as done ✓ | High |
| 2.3 | Step 2 - "Add a guest" (only active after Step 1 complete) | High |
| 2.3 | Click "Add guest" → navigates to event guest creation page | High |
| 2.4 | Step 3 - "Copy their invite link" → navigates to event guest tab | High |
| 2.4 | Step 3 completes when user clicks "Copy link" on a guest row | High |
| 2.4 | Custom event `onboarding-link-copied` dispatched on copy | Low |
| 2.5 | Dismiss wizard via X button → permanently gone | Medium |
| 2.5 | Minimize (chevron-down) → collapses to compact pill showing progress | Medium |
| 2.5 | Click compact pill → expands back to full card | Medium |
| 2.5 | After dismissal, re-login → wizard does NOT reappear | Medium |
| 2.6 | All 3 steps done → "You're all set!" with "Let's go!" button | High |
| 2.6 | Click "Let's go!" → wizard dismissed | High |

---

## Section 3 - Event Management (partially covered)

> ✅ Automated in `EditEvent.feature`: edit opens pre-populated form; save persists changes. `DeleteEvent.feature` also covers the edit-page danger-zone delete (confirmation popover → redirect).
>
> ⚠️ **Known defects (Open, 2026-09-21):** date/RSVP picker issues - see [BUGS.md](BUGS.md) (BUG-001…BUG-005). The regression scenarios below target them.

| # | Missing Scenario | Priority |
|---|---|---|
| 3.1 | Progress bar at top: `① Details → ② Guests → ③ Share` visible on create form | Medium |
| 3.1 | Date picker opens calendar on click | Medium |
| 3.1 | Time selector shows 30-min slots (00:00–23:30) | Low |
| 3.1 | Free plan banner shows for users without credits | Medium |
| 3.1 | Paid credit picker shows for users with purchased credits | Medium |
| 3.1 | Credit selection (free vs paid) changes the submit behavior | Medium |
| 3.1 | Regression: past event dates rejected consistently (BUG-001, BUG-005) | High |
| 3.1 | Regression: RSVP deadline after event date rejected (BUG-002) | High |
| 3.1 | Regression: RSVP deadline picker has no hardcoded year cap (BUG-003) | Medium |
| 3.1 | Regression: date picker allows future-year navigation (BUG-004) | Medium |
| 3.2 | Event cards show title, **color-coded type pill** (rose=wedding, indigo=birthday, etc.) | High |
| 3.2 | Event cards show date and venue | Medium |
| 3.2 | **RSVP progress bar**: three-segment split (emerald=attending, rose=declined, gray=pending) | High |
| 3.2 | RSVP stats row: "14 attending · 3 declined · of 40" | High |
| 3.2 | Events with no guests show "No guests yet" | Medium |
| 3.2 | **Past event overlay**: gray overlay with "Event passed" badge + reduced opacity | Medium |
| 3.2 | "New Event" button in top-right | Low |
| 3.2 | Multi-team users see team name in subtitle | Low |
| 3.3 | Back to event link works | Low |
| 3.4 | After delete: all associated guests, RSVPs, and invitee data removed | Medium |
| 3.4 | Event purchase credit unlinked (not restored) | Low |

---

## Section 4 - Guest Management (**entirely missing**)

| # | Missing Scenario | Priority |
|---|---|---|
| 4.1 | Guest List tab: **Stats row** (Total / Accepted / Declined / Pending counts) | **Critical** |
| 4.1 | Guest cards: colored left border (gray=pending, amber=opened, green=accepted, red=declined, indigo=partial) | High |
| 4.1 | Each card: avatar initials, name, status badge, copy link button, delete button | **Critical** |
| 4.1 | Contact info (email/phone) shown below name | Medium |
| 4.1 | Meta row: guest count, attending count, "View RSVP" button | High |
| 4.1 | Guest sub-rows with attendance, dietary, allergies, notes | Medium |
| 4.2 | Add Guest - Named Mode: add one or more named guests | **Critical** |
| 4.2 | "Add another person" button adds a slot row | High |
| 4.2 | Remove button (X) removes a slot | Medium |
| 4.2 | Group label appears with 2+ slots | Low |
| 4.2 | Contact fields: email, phone | High |
| 4.2 | Personal message textarea | Medium |
| 4.2 | **Preview button** opens modal showing guest's view | Medium |
| 4.2 | Submit → guest added, redirects to event guest tab | **Critical** |
| 4.3 | Add Guest - Group Mode (Reserve Seats): group name + guest count (1–20) | **Critical** |
| 4.3 | Contact fields + personal message in group mode | High |
| 4.3 | Preview button works in group mode | Medium |
| 4.3 | Submit → group guest added | **Critical** |
| 4.4 | Copy Invite Link → clipboard gets `{origin}/invite/{token}` | High |
| 4.4 | Button changes to "Copied!" with green check for 2 seconds | Medium |
| 4.4 | Dispatches `onboarding-link-copied` custom event | Low |
| 4.5 | Delete Guest: trash icon → confirmation popover → guest + RSVP deleted | High |
| 4.6 | View RSVP modal: group name, contact, seen date, individual guest responses | Medium |
| 4.7 | Guest limit enforcement: reaching plan limit → error "Guest limit reached" | Medium |

---

## Section 5 - Invitation / RSVP Flow (**entirely missing**)

| # | Missing Scenario | Priority |
|---|---|---|
| 5.1 | Open invite link in incognito → page shows event title, date, venue, deadline | **Critical** |
| 5.1 | Message card: "Dear {name}," + personal message / default greeting | High |
| 5.2 | RSVP Named Mode: guest names pre-filled, Attending/Not attending radios per guest | **Critical** |
| 5.2 | If attending: dietary, allergies, notes fields appear conditionally | High |
| 5.2 | Submit → "Thank you!" confirmation | **Critical** |
| 5.3 | RSVP Open Mode: guest fills own name, Attending/Not attending radios | High |
| 5.3 | Dietary/allergies/notes appear if attending | Medium |
| 5.3 | Submit → "Thank you!" confirmation | High |
| 5.4 | Already Responded: re-open link → response summary with attendance badges | Medium |
| 5.4 | "Need to make a change?" link → edit mode | Medium |
| 5.4 | Edit and re-submit → updated RSVP | Medium |
| 5.5 | Deadline passed: form locked, "RSVP is now closed" | High |
| 5.5 | Deadline within 3 days: amber warning "Last chance" | Medium |
| 5.5 | Deadline today: "Last chance to respond - closes today!" | Medium |
| 5.5 | Event date passed: "This event has already taken place" | Medium |
| 5.6 | Opening invite → `openedAt` timestamp set, status "pending" → "opened" | Low |
| 5.6 | Guest list shows "Seen" badge instead of "Pending" | Medium |

---

## Section 6 - Event Dashboard & Analytics (**entirely missing**)

| # | Missing Scenario | Priority |
|---|---|---|
| 6.1 | Guests Tab: stats cards (Total, Accepted, Declined, Pending) | **Critical** |
| 6.1 | Guest list with all invitees | High |
| 6.1 | Add guest button visible | Low |
| 6.2 | Analytics Tab: **Response rate** progress bar with "% of guests responded" | High |
| 6.2 | "Not opened count": number of guests who haven't opened their link | Medium |
| 6.2 | **Dietary breakdown**: donut chart with segments | Medium |
| 6.2 | "No accepted guests yet" empty state | Medium |
| 6.2 | **Attendance trend**: 7-day bar chart of responses per day | Low |
| 6.3 | Activity Tab: chronological feed (opened, accepted, declined, partial) | High |
| 6.3 | Each entry: group name, action type, timestamp, attending count | Medium |
| 6.3 | "No activity yet" empty state | Medium |
| 6.3 | Capped at 20 entries | Low |
| 6.4 | Three tabs: Guests, Analytics, Activity with correct active/inactive styling | Medium |
| 6.4 | Minimum 44px touch target on mobile | Low |

---

## Section 7 - Pricing & Payments (partially covered)

> ✅ Automated in `Pricing.feature`: four tiers rendered, "Most Popular" badge on Standard, savings badge vs Large, per-tier feature-checkmark counts, and the "Read our FAQ" nudge → `/faq`.

| # | Missing Scenario | Priority |
|---|---|---|
| 7.1 | Comparison table below cards (feature × tier matrix) | Medium |
| 7.1 | Inline FAQ accordion with 4 correct questions | High |
| 7.1 | Prices in correct currency format | Medium |
| 7.2 | Create event without payment → free tier used | Medium |
| 7.2 | Guest limit of 10 enforced on free tier | High |
| 7.3 | "Upgrade" button on event page → `/events/{id}/upgrade` | Medium |
| 7.3 | Shows current plan + available upgrade tiers | Medium |
| 7.3 | Upgrade through LemonSqueezy checkout (if configured) | Low |
| 7.3 | Success banner after upgrade | Medium |
| 7.4 | Settings → Team → Event Credits section shows available credits | Medium |

---

## Section 8 - Blog (**entirely missing**)

| # | Missing Scenario | Priority |
|---|---|---|
| 8.1 | Blog index: all posts with title, excerpt, category, read time | High |
| 8.1 | Click post → navigates to `/blog/{slug}` | High |
| 8.2 | Blog post: title, category badge, excerpt, date, read time | Medium |
| 8.2 | Content rendered: h2, h3, paragraphs, lists (ul/ol), tip callouts | Medium |
| 8.2 | Tip blocks have indigo background with heading | Low |
| 8.2 | "Ready to simplify your RSVPs?" CTA at bottom | Medium |
| 8.2 | "More articles" back link | Low |
| 8.2 | BlogPosting JSON-LD schema present | Low |

---

## Section 9 - SEO Landing Pages (**entirely missing**)

| # | Missing Scenario | Priority |
|---|---|---|
| 9.1 | Wedding RSVP (`/wedding-rsvp`): hero, badge, title, 4 steps, 4 features, CTA | Medium |
| 9.2 | Birthday Invitations (`/birthday-invitations`): hero, badge, title, 4 steps, 4 features | Medium |
| 9.3 | Features Index (`/features`): hero, 4 cards in 2×2 grid, color coding, hover effects | Medium |
| 9.4 | Feature Detail (`/features/personal-links`): hero, 3 paragraphs, demo placeholder, CTA | Medium |
| 9.4 | Feature Detail (`/features/guest-management`): same structure | Medium |
| 9.4 | Feature Detail (`/features/dietary-tracking`): same structure | Medium |
| 9.4 | Feature Detail (`/features/live-dashboard`): same structure | Medium |
| 9.4 | "← All features" back link on each detail page | Low |

---

## Section 10 - Features Dropdown Navigation (partially covered)

| # | Missing Scenario | Priority |
|---|---|---|
| 10.1 | "Features" in nav has sparkle icon and chevron (public visitors only) | Low |
| 10.1 | Features link hidden for authenticated users | Medium |
| 10.2 | Mobile hamburger menu: Features dropdown with 4 links | Medium |
| 10.2 | All 4 links present and navigable in mobile | Medium |

---

## Section 11 - Team & Account Settings (**almost entirely missing**)

| # | Missing Scenario | Priority |
|---|---|---|
| 11.1 | General Settings (`/settings/general`): edit name and email | **Critical** |
| 11.1 | "Save Changes" → values update | **Critical** |
| 11.1 | Activity log records "UPDATE_ACCOUNT" | Low |
| 11.2 | Security: change password (current + new + confirm) | **Critical** |
| 11.2 | "Current password is incorrect" error | High |
| 11.2 | "New password must be different" error | High |
| 11.2 | "New password and confirmation do not match" error | High |
| 11.2 | Successful password change → can login with new password | **Critical** |
| 11.2 | Delete account: requires password confirmation | Medium |
| 11.2 | Account soft-deleted (email suffixed with `-id-deleted`) | Medium |
| 11.2 | Redirect to `/sign-in` after deletion | Medium |
| 11.3 | Team Settings: view team name | High |
| 11.3 | Edit team name (owner only) | High |
| 11.3 | View team members with roles | High |
| 11.3 | Invite team member: email + role → invitation sent | High |
| 11.3 | Remove team member (owner only, cannot remove self, cannot remove last owner) | Medium |
| 11.3 | Pending invitations list with "Accept" button | Medium |
| 11.3 | "You must be a team owner" message for non-owners | Medium |
| 11.4 | Roles: view built-in roles (Member, Owner) | Medium |
| 11.4 | Create custom role: name + permissions | Medium |
| 11.4 | Edit custom role | Low |
| 11.4 | Delete custom role (unlinks members first) | Low |
| 11.4 | Assign custom role to member | Medium |
| 11.4 | Permission validation (only known permissions accepted) | Low |
| 11.5 | Multi-team: team switcher dropdown in header | Medium |
| 11.5 | Switch teams → active team updated, cookie set | Medium |
| 11.5 | Events filtered by active team | Medium |
| 11.6 | Activity Log (`/settings/activity`): shows recent 10 actions | Medium |
| 11.6 | Each entry: action type, timestamp, IP address, relative time | Low |
| 11.6 | "No activity yet" empty state | Low |

---

## Section 12 - Structured Data / SEO (not automated)

All 5 sub-sections (12.1–12.5) are missing. These are metadata assertions - can be tested by
inspecting page source or `<script type="application/ld+json">` blocks.

**Priority: Low** (unless SEO is a core concern).

---

## Section 13 - Internationalization / i18n (partially covered)

> ✅ Automated in `Internationalization.feature`: locale switcher visible; URL locale prefix (English → no prefix, Spanish → `/es`).

| # | Missing Scenario | Priority |
|---|---|---|
| 13.1 | Switch to Spanish → all UI text translates | High |
| 13.1 | Invitation links work in both languages | Medium |
| 13.2 | No "MISSING_MESSAGE" errors in console for either locale | Low |
| 13.2 | Dynamic interpolation works (`{count} guests`, `{days} day(s)`) | Low |

---

## Section 14 - Responsive / Mobile (**entirely missing**)

All 5 sub-sections (14.1–14.5) are missing. These require setting browser viewport
dimensions and verifying mobile-specific behavior.

**Priority: Medium** - can be covered by a handful of mobile-viewport scenarios.

---

## Section 15 - Edge Cases & Error States (**entirely missing**)

| # | Missing Scenario | Priority |
|---|---|---|
| 15.1 | Empty states: events list, guest list, activity log, team members, event credits | High |
| 15.2 | Invalid event ID → 404 page | Medium |
| 15.2 | Invalid invite token → 404 page | Medium |
| 15.2 | Server error → appropriate error message | Low |
| 15.2 | Network offline → graceful degradation | Low |
| 15.3 | Double-click submit buttons → disabled state prevents duplicates | Medium |
| 15.4 | CookieConsentWrapper appears for new visitors | Medium |
| 15.4 | Accept/Reject options work | Medium |
| 15.4 | Respects locale | Low |

---

## Section 16 - Performance & Accessibility (not automated)

Better suited for Lighthouse / axe-core audits than Selenium.
**Priority: Low** for automation.

---

## Summary by Priority

| Priority | Sections | Est. Count |
|---|---|---|
| **Critical** | Guest Management, Invitation/RSVP, Dashboard, Account Settings, Security | ~18 |
| **High** | Onboarding Wizard, Event Cards UI, Guest Details, Blog Index, i18n, Empty States, Team Settings | ~35 |
| **Medium** | SEO Landing Pages, Mobile/Responsive, Rate Limiting, Cookie Consent, Team Roles, Activity Log, Deadlines | ~45 |
| **Low** | Structured Data, Performance, Accessibility, Console checks, JSON-LD schemas | ~25 |

---

## Recommended Implementation Order

1. **Guest Management + RSVP Flow** (Sections 4 & 5) - core product value
2. **Event Dashboard** (Section 6) - critical user flow (Edit Event is now automated; only the "back to event" link remains)
3. **Onboarding Wizard** (Section 2) - first-time user experience
4. **Account & Security Settings** (Section 11) - user management
5. **Pricing Page** (Section 7) - revenue-related
6. **Blog + SEO Pages** (Sections 8 & 9) - marketing surface
7. **i18n, Mobile, Edge Cases** (Sections 13–15) - quality / completeness
8. **SEO schema + Perf / A11y** (Sections 12, 16) - best done with specialized tools
