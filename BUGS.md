# Partello - Known Bugs / Defect Log

Defects found during QA of the Partello application. Reported 2026-09-21. Status: **Open**.

| ID | Area | Bug | Description |
|---|---|---|---|
| BUG-001 | Create Event | Date can't be in the past | The event "Date & time" field accepts a past date; the "Date cannot be in the past" rule is not consistently enforced. |
| BUG-002 | Create Event | RSVP deadline after event date | An RSVP deadline later than the event date is accepted; the "RSVP deadline cannot be after event date" rule is not enforced. |
| BUG-003 | Create Event | RSVP hardcoded year limit | The RSVP deadline date picker is capped to a hardcoded year range, blocking selection of later years. |
| BUG-004 | Create Event | Date picker doesn't allow future years | The event date picker cannot navigate to / select dates in future years, so far-future events cannot be scheduled. |
| BUG-005 | Create Event | Missing validation for past dates in date picker | The calendar UI does not disable or flag past dates, so invalid past dates remain selectable. |

> Exact repro steps, expected vs. actual behaviour, and severity are pending confirmation.

## Automation notes

- `CreateEvent.feature` already asserts the "Date cannot be in the past" and "RSVP deadline cannot be after event date" messages for single-day cases (yesterday, and today-vs-tomorrow). BUG-001 and BUG-002 indicate those validations are incomplete or bypassable in other cases.
- Regression scenarios to pin these down are listed in [MISSING_TEST_SCENARIOS.md](MISSING_TEST_SCENARIOS.md) §3.1.
