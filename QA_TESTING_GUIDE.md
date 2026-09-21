# Partello - End-to-End QA Testing Guide

This document covers every feature in the Partello application. QA should test each item and mark it as **PASS** or **FAIL** with notes.

---

## 1. Authentication

### 1.1 Sign Up

- [ ] Navigate to `/sign-up`
- [ ] Create an account with valid email + password (min 8 chars)
- [ ] Verify redirect to `/settings` after successful sign-up
- [ ] **Onboarding wizard** appears as a floating card in bottom-right corner
- [ ] Try signing up with an already-registered email → error message
- [ ] Try signing up with invalid email format → validation error
- [ ] Try signing up with password < 8 chars → validation error
- [ ] Password visibility toggle masks/reveals the password input
- [ ] "Continue with Google" button visible and clickable
- [ ] Rate limiting: attempt 6+ rapid sign-ups → "Too many sign-up attempts"

### 1.2 Sign In

- [ ] Navigate to `/sign-in`
- [ ] Sign in with valid credentials → redirect to `/settings`
- [ ] Sign in with wrong password → error "Invalid email or password"
- [ ] Sign in with non-existent email → error "Invalid email or password"
- [ ] "Continue with Google" → Google OAuth flow → redirect to `/settings`
- [ ] Rate limiting: attempt 6+ rapid sign-ins → "Too many login attempts"

### 1.3 Sign Out

- [ ] Click avatar in top-right → "Sign out"
- [ ] Verify redirect to homepage
- [ ] Verify protected routes (`/events`, `/settings`) redirect to `/sign-in`

### 1.4 Session Persistence

- [ ] Sign in, close browser, reopen → still signed in
- [ ] Session cookie expires after 24h of inactivity

---

## 2. Onboarding Wizard

### 2.1 First Login Experience

- [ ] New user lands on `/settings` after sign-up
- [ ] Floating card appears in bottom-right corner with 3 steps
- [ ] Progress bar shows "Step 1 of 3"

### 2.2 Step 1 - Create Event

- [ ] Shows "Create your first event" with action button
- [ ] Click "Create event" → navigates to `/events/new`
- [ ] After creating event, return to `/settings` → Step 1 marked as done ✓

### 2.3 Step 2 - Add Guest

- [ ] Shows "Add a guest" with action button
- [ ] Only active after Step 1 is complete
- [ ] Click "Add guest" → navigates to event guest creation page
- [ ] After adding guest, step 2 marked as done ✓

### 2.4 Step 3 - Copy Invite Link

- [ ] Shows "Copy their invite link" with action button
- [ ] Click "View invite link" → navigates to event guest tab
- [ ] Step 3 only completes when user actually copies a link (clicks "Copy link" on a guest row)
- [ ] Custom event `onboarding-link-copied` dispatched on copy

### 2.5 Dismissal

- [ ] Click X button → wizard dismissed permanently
- [ ] Click minimize (chevron-down) → collapses to compact pill showing progress
- [ ] Click compact pill → expands back to full card
- [ ] After dismissal, re-login → wizard does NOT reappear

### 2.6 All Done State

- [ ] Complete all 3 steps → celebratory "You're all set!" message with "Let's go!" button
- [ ] Click "Let's go!" → wizard dismissed

---

## 3. Event Management

### 3.1 Create Event

- [ ] Navigate to `/events/new`
- [ ] **Progress bar** at top: `① Details → ② Guests → ③ Share` (step 1 highlighted)
- [ ] **Form fields**: Event name*, Type, Date & time*, Venue, Address, Description, RSVP deadline
- [ ] Event name is required - cannot submit without it
- [ ] Date & time is required - cannot submit without it
- [ ] All form fields accept input correctly
- [ ] Date picker opens calendar on click
- [ ] Time selector shows 30-min slots (00:00–23:30)
- [ ] RSVP deadline picker works
- [ ] "Create Event" submits and redirects to `/events/{id}`
- [ ] Free plan banner shows for users without credits
- [ ] Paid credit picker shows for users with purchased credits
- [ ] Credit selection (free vs paid) works correctly

> ⚠️ **Known defects (Open, 2026-09-21):** past-date validation not consistently enforced (BUG-001, BUG-005); RSVP deadline after event date accepted (BUG-002); RSVP deadline picker hardcoded year cap (BUG-003); date picker can't select future years (BUG-004). See [BUGS.md](BUGS.md).

### 3.2 Events List

- [ ] Navigate to `/events`
- [ ] Cards show: title, **color-coded type pill** (rose=wedding, indigo=birthday, amber=party, violet=anniversary, slate=other)
- [ ] Cards show: date and venue
- [ ] **RSVP progress bar**: three-segment split bar (emerald=attending, rose=declined, gray=pending)
- [ ] RSVP stats row: "14 attending · 3 declined · of 40"
- [ ] Events with no guests show "No guests yet"
- [ ] **Past event overlay**: events with date < today show gray overlay with "Event passed" badge
- [ ] Past events have reduced opacity and slight grayscale
- [ ] "New Event" button in top-right
- [ ] Multi-team users see team name in subtitle

### 3.3 Edit Event

- [ ] Navigate to event → "Edit" button
- [ ] All fields pre-populated with current values
- [ ] Edit fields and "Save Changes" → values persist
- [ ] "Delete Event" in danger zone → confirmation popover → event deleted
- [ ] Back to event link works

### 3.4 Delete Event

- [ ] From edit page: click "Delete Event" → confirmation popover
- [ ] Confirm → event deleted, redirects to `/events`
- [ ] All associated guests, RSVPs, and invitee data removed
- [ ] Event purchase credit unlinked (not restored)

---

## 4. Guest Management

### 4.1 Guest List View (Event Detail)

- [ ] Navigate to event → "Guests" tab
- [ ] **Stats row**: Total / Accepted / Declined / Pending counts
- [ ] **Guest cards**: colored left border (gray=pending, amber=opened, green=accepted, red=declined, indigo=partial)
- [ ] Each card shows: avatar initials, name, status badge, copy link button, delete button
- [ ] Contact info (email/phone) shown below name if present
- [ ] Meta row: guest count, attending count, "View RSVP" button (if opened)
- [ ] **Guest sub-rows**: individual guest details with name, attendance, dietary, allergies, notes
- [ ] Guest cards use `flex-wrap` for responsive layout

### 4.2 Add Guest - Named Mode

- [ ] From event page, click "Add Guest"
- [ ] Select "Named guests" mode
- [ ] Add one or more named guests (first name required, last name optional)
- [ ] "Add another person" button adds a slot row
- [ ] Remove button (X) removes a slot
- [ ] Group label appears when 2+ slots added
- [ ] Contact fields: email, phone
- [ ] Personal message textarea
- [ ] **Preview button** opens modal showing guest's view
- [ ] Submit → guest added, redirects to event guest tab

### 4.3 Add Guest - Group Mode (Reserve Seats)

- [ ] Select "Reserve seats" mode
- [ ] Enter group name and number of guests (1–20)
- [ ] Contact fields and personal message
- [ ] **Preview button** works
- [ ] Submit → guest added

### 4.4 Copy Invite Link

- [ ] Click "Copy link" on any guest row
- [ ] Link copied to clipboard: `{origin}/invite/{token}`
- [ ] Button changes to "Copied!" with green check for 2 seconds
- [ ] Dispatches `onboarding-link-copied` custom event

### 4.5 Delete Guest

- [ ] Click trash icon on guest row → confirmation popover
- [ ] Confirm → guest and all their RSVP data deleted
- [ ] Guest removed from list

### 4.6 View RSVP (Guest Details)

- [ ] Click "View RSVP" button on a guest row (only visible if invitation opened)
- [ ] Modal shows: group name, contact, seen date, individual guest responses
- [ ] Each guest shows: name, attendance badge, dietary, allergies, notes

### 4.7 Guest Limit Enforcement

- [ ] Create enough guests to reach plan limit → error "Guest limit reached"
- [ ] Edit guest to increase count beyond limit → error

---

## 5. Invitation / RSVP Flow (Guest Side)

### 5.1 Invitation Page

- [ ] Open invite link `{baseUrl}/invite/{token}` in an incognito/separate browser
- [ ] Page shows: event title, date, venue, deadline indicator
- [ ] **Message card**: "Dear {name}," + personal message / default greeting
- [ ] RSVP form renders correctly

### 5.2 RSVP Submission - Named Mode

- [ ] Guest names pre-filled and shown as headers
- [ ] Attending/Not attending radio buttons per guest
- [ ] If attending: dietary, allergies, notes fields appear
- [ ] Submit → "Thank you!" confirmation

### 5.3 RSVP Submission - Open Mode

- [ ] Guest fills in their own first name (required) and last name
- [ ] Attending/Not attending radio buttons
- [ ] Dietary/allergies/notes fields appear if attending
- [ ] Submit → "Thank you!" confirmation

### 5.4 Already Responded

- [ ] Re-open invite link after responding
- [ ] Shows response summary with attendance badges
- [ ] "Need to make a change?" link → edit mode
- [ ] Edit and re-submit → updated RSVP

### 5.5 Deadline Behavior

- [ ] If RSVP deadline passed: form locked, message "RSVP is now closed"
- [ ] If deadline within 3 days: amber warning "Last chance"
- [ ] If deadline today: "Last chance to respond - closes today!"
- [ ] If event date passed: "This event has already taken place"

### 5.6 Invitation Opened Tracking

- [ ] Opening invite link → `openedAt` timestamp set
- [ ] Status changes from "pending" to "opened"
- [ ] Guest list shows "Seen" badge instead of "Pending"

---

## 6. Event Dashboard & Analytics

### 6.1 Guests Tab

- [ ] Stats cards: Total, Accepted, Declined, Pending
- [ ] Guest list with all invitees
- [ ] Add guest button

### 6.2 Analytics Tab

- [ ] **Response rate**: progress bar with "% of guests responded"
- [ ] **Not opened count**: number of guests who haven't opened their link
- [ ] **Dietary breakdown**: donut chart with vegan/vegetarian/gluten-free/other/none segments
- [ ] Shows "No accepted guests yet" if no RSVPs
- [ ] **Attendance trend**: 7-day bar chart of responses per day

### 6.3 Activity Tab

- [ ] Chronological feed of all actions: opened, accepted, declined, partial
- [ ] Each entry shows: group name, action type, timestamp, attending count
- [ ] "No activity yet" if empty
- [ ] Capped at 20 entries

### 6.4 Event Tabs (Mobile)

- [ ] Three tabs: Guests, Analytics, Activity
- [ ] Equal width on all screen sizes
- [ ] Active tab: indigo underline + text
- [ ] Inactive tab: gray text, hover shows gray underline
- [ ] Minimum 44px touch target

---

## 7. Pricing & Payments

### 7.1 Pricing Page

- [ ] Navigate to `/pricing`
- [ ] Four tiers shown: Free, Intimate, Standard, Large
- [ ] "Most Popular" badge on Standard tier
- [ ] **Savings tag**: "Save €X vs the Large tier" on Standard card
- [ ] Feature checkmarks per tier
- [ ] Comparison table below cards (feature × tier matrix)
- [ ] **Inline FAQ** accordion with 4 questions
- [ ] FAQ questions: guest limit, upgrade, guest accounts, subscription model
- [ ] "Read our FAQ" nudge links to `/faq`
- [ ] Prices display in correct currency format

### 7.2 Free Tier

- [ ] Create event without payment → free tier used
- [ ] Guest limit of 10 enforced
- [ ] "Free plan · up to 10 guests per event" banner on create form

### 7.3 Event Upgrade

- [ ] From event page: "Upgrade" button → `/events/{id}/upgrade`
- [ ] Shows current plan + available upgrade tiers
- [ ] Upgrade through LemonSqueezy checkout (if configured)
- [ ] After upgrade: success banner on event page

### 7.4 Team Credits

- [ ] Settings → Team → Event Credits section
- [ ] Shows available credits with plan name and guest limits
- [ ] "Buy Credits" button

---

## 8. Blog

### 8.1 Blog Index

- [ ] Navigate to `/blog`
- [ ] All posts listed with title, excerpt, category, read time
- [ ] Click post → navigates to `/blog/{slug}`

### 8.2 Blog Post

- [ ] Title, category badge, excerpt, date, read time
- [ ] Content rendered with h2, h3, paragraphs, lists (ul/ol), tip callouts
- [ ] Tip blocks have indigo background with heading
- [ ] "Ready to simplify your RSVPs?" CTA at bottom
- [ ] "More articles" back link
- [ ] **BlogPosting JSON-LD schema** present

---

## 9. SEO Landing Pages

### 9.1 Wedding RSVP (`/wedding-rsvp`)

- [ ] Hero with rose gradient, badge, title "The Easiest Way to Manage Wedding RSVPs"
- [ ] 4 how-it-works steps (01–04)
- [ ] 4 feature highlights with icons
- [ ] CTA button → `/sign-up`
- [ ] Bottom gradient CTA section
- [ ] WebPage JSON-LD schema

### 9.2 Birthday Invitations (`/birthday-invitations`)

- [ ] Hero with amber gradient, cake icon badge
- [ ] 4 how-it-works steps
- [ ] 4 feature highlights
- [ ] CTA → `/sign-up`

### 9.3 Features Index (`/features`)

- [ ] Hero: "Everything you need to manage event invitations"
- [ ] 4 feature cards in 2×2 grid with color coding
- [ ] Each card: icon, title, subtitle, hover effects, "Learn more" link
- [ ] Bottom CTA with "Get started free" + "View pricing"

### 9.4 Feature Detail Pages

- [ ] `/features/personal-links` - indigo theme
- [ ] `/features/guest-management` - emerald theme
- [ ] `/features/dietary-tracking` - amber theme
- [ ] `/features/live-dashboard` - violet theme
- [ ] Each page: hero, 3 numbered body paragraphs, demo placeholder, CTA
- [ ] "← All features" back link
- [ ] Demo section with dashed placeholder for GIF

---

## 10. Features Dropdown (Navigation)

### 10.1 Desktop Nav

- [ ] "Features" appears in nav bar with sparkle icon and chevron (public visitors only)
- [ ] Hover/click opens dropdown with 4 feature links
- [ ] Click any link → navigates to feature detail page
- [ ] Active state: indigo underline when on any `/features/`* page

### 10.2 Mobile Nav

- [ ] Features dropdown works in mobile hamburger menu
- [ ] All 4 links present and navigable

---

## 11. Team & Account Settings

### 11.1 General Settings (`/settings/general`)

- [ ] Edit name and email
- [ ] "Save Changes" → values update
- [ ] Activity log records "UPDATE_ACCOUNT"

### 11.2 Security (`/settings/security`)

- [ ] Change password: current + new + confirm
- [ ] "Current password is incorrect" error
- [ ] "New password must be different" error
- [ ] "New password and confirmation do not match" error
- [ ] Successful password change
- [ ] Delete account: requires password confirmation
- [ ] Account soft-deleted (email suffixed with `-id-deleted`)
- [ ] Redirected to `/sign-in` after deletion

### 11.3 Team Settings (`/settings` → Team tab)

- [ ] View team name
- [ ] Edit team name (owner only)
- [ ] View team members with roles
- [ ] Invite team member: email + role → invitation sent
- [ ] Remove team member (owner only, cannot remove self, cannot remove last owner)
- [ ] Pending invitations list with "Accept" button
- [ ] "You must be a team owner" message for non-owners

### 11.4 Roles (`/settings/roles`)

- [ ] View built-in roles (Member, Owner)
- [ ] Create custom role: name + permissions
- [ ] Edit custom role
- [ ] Delete custom role (unlinks members first)
- [ ] Assign custom role to member
- [ ] Permission validation (only known permissions accepted)

### 11.5 Team Switching

- [ ] Multi-team users: team switcher dropdown in header
- [ ] Switch teams → active team updated, cookie set
- [ ] Events filtered by active team

### 11.6 Activity Log (`/settings/activity`)

- [ ] Shows recent 10 actions
- [ ] Each entry: action type, timestamp, IP address
- [ ] Relative time display (just now, X minutes ago, X hours ago)
- [ ] "No activity yet" empty state

---

## 12. Structured Data (SEO)

### 12.1 Homepage

- [ ] WebSite schema with SearchAction
- [ ] SoftwareApplication schema with free offer

### 12.2 Pricing Page

- [ ] SoftwareApplication schema with 4 offers (Free, Intimate, Standard, Large)
- [ ] Prices and descriptions included

### 12.3 Blog Posts

- [ ] BlogPosting schema with headline, description, datePublished, author, publisher

### 12.4 FAQ Page

- [ ] FAQPage schema with Question/Answer entities

### 12.5 Landing Pages

- [ ] WebPage schemas on wedding-rsvp and birthday-invitations

---

## 13. Internationalization (i18n)

### 13.1 Language Switching

- [ ] Locale switcher in header (EN / ES)
- [ ] Switch to Spanish → all UI text translates
- [ ] URLs include locale prefix (`/es/events`, etc.)
- [ ] Invitation links work in both languages

### 13.2 Missing Keys

- [ ] No "MISSING_MESSAGE" errors in console for either locale
- [ ] Dynamic interpolation works (e.g., `{count} guests`, `{days} day(s)`)

---

## 14. Responsive / Mobile

### 14.1 Layout

- [ ] Dashboard layout uses `max-w-5xl/6xl` containers
- [ ] Hamburger menu on mobile (< 768px)
- [ ] Mobile drawer: all nav links + sign in/up buttons

### 14.2 Guest List on Mobile

- [ ] Cards flow naturally, action buttons wrap below name
- [ ] Colored left border visible
- [ ] Touch targets ≥ 44px for action buttons (copy, delete)

### 14.3 Event Tabs on Mobile

- [ ] Three tabs evenly distributed full-width
- [ ] Min 44px height

### 14.4 Forms on Mobile

- [ ] Input fields full-width
- [ ] Date + time fields stack on narrow screens
- [ ] Submit buttons full-width

### 14.5 Onboarding Wizard on Mobile

- [ ] Floating card stays in bottom-right
- [ ] Doesn't overlap critical content
- [ ] Minimize/dismiss work with touch

---

## 15. Edge Cases & Error States

### 15.1 Empty States

- [ ] Events list empty → "No events yet" with create CTA
- [ ] Guest list empty → "No guests added yet" with icon
- [ ] Activity log empty → "No activity yet"
- [ ] No team members → appropriate empty message
- [ ] No event credits → "No event credits"

### 15.2 Error Handling

- [ ] Invalid event ID → 404 page
- [ ] Invalid invite token → 404 page
- [ ] Server errors → appropriate error messages
- [ ] Network offline → graceful degradation

### 15.3 Concurrent Actions

- [ ] Two users adding guests simultaneously → no conflicts
- [ ] Rapid RSVP submissions → handled correctly
- [ ] Double-click submit buttons → disabled state prevents duplicates

### 15.4 Cookie Consent

- [ ] CookieConsentWrapper appears for new visitors
- [ ] Accept/Reject options work
- [ ] Respects locale

---

## 16. Performance & Accessibility

### 16.1 Performance

- [ ] Pages load within 2 seconds
- [ ] Images have appropriate sizes
- [ ] No layout shift on load (CLS)

### 16.2 Accessibility

- [ ] All dialogs have DialogTitle for screen readers
- [ ] Form inputs have associated labels
- [ ] Keyboard navigation works for modals (Esc to close)
- [ ] Focus trapping in dialogs
- [ ] Color contrast meets WCAG AA

---

## Test Environment Setup

1. **Two browsers** (or incognito windows): one for the host, one for the guest
2. **Test accounts**: create at least 2 accounts (one new, one with existing events)
3. **Test events**: create events of different types (wedding, birthday, party, other)
4. **Test guests**: add named guests, group guests, mix of accepted/declined/pending
5. **Mobile device** or browser DevTools responsive mode for Section 14
6. **Google test account** for OAuth sign-in/sign-up scenarios (Section 1)

---

*Generated: June 2026 · Last reviewed 2026-09-21*