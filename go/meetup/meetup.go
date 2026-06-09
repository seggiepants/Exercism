// Meetup scheduling example.
package meetup

import "time"

// Define the WeekSchedule type here.
type WeekSchedule int

const (
	First WeekSchedule = iota
	Second
	Third
	Fourth
	Teenth
	Last
)

const ONE_DAY = time.Hour * 24
const ONE_WEEK = time.Hour * 24 * 7

// Give a date return the first of the next month.
// @param CurrentDate: The current Date and Time
// @returns: First date of the next month (same time of day).
func NextMonth(CurrentDate time.Time) time.Time {
	CurrentMonth := CurrentDate.Month()
	for CurrentDate.Month() == CurrentMonth {
		CurrentDate = CurrentDate.Add(ONE_DAY)
	}
	return CurrentDate
}

// Give a date return the next Sunday.
// @param CurrentDate: The current Date and Time
// @returns: Next sunday (after current date).
func NextWeek(CurrentDate time.Time) time.Time {
	NextDate := CurrentDate
	for NextDate != CurrentDate && NextDate.Weekday() != time.Sunday {
		NextDate = NextDate.Add(ONE_DAY)
	}
	return NextDate
}

// Return the Day of the Month for the nth Day of the Week with the start of the month as the given start date.
// @param StartDate: The first day of the month.
// @param nth: Count of weekday in the month
// @param Weekday: The desired weekday.
// @returns: Day of the month for the target date.
func NthWeekday(StartDate time.Time, nth int, Weekday time.Weekday) int {
	current := StartDate
	var counter int
	if current.Weekday() == Weekday {
		counter = 1
	} else {
		counter = 0
	}
	for current.Weekday() != Weekday || counter < nth {
		current = current.Add(ONE_DAY)
		if current.Weekday() == Weekday {
			counter++
		}
	}
	return current.Day()
}

// Find the next day relative to referenceDate that is the given day of the week.
// @param ReferenceDate: The date to start looking at
// @param DayOfWeek: The desired day of the week (Sunday - Saturday)
// @param step: The time to move forward (or backward) for each step.
// @returns: First date found of the given week day
func FindWeekday(ReferenceDate time.Time, DayOfWeek time.Weekday, Step time.Duration) int {
	current := ReferenceDate
	for current.Weekday() != DayOfWeek {
		current = current.Add(Step)
	}
	return current.Day()
}

// Find a date on the calendar that matches the description given by the parameters
// @param wSched: The week to schedule (First, Second, Third, Fourth, Last or Teenth of the month)
// @param wDay: Desired Weekday (Sunday - Staturday)
// @param month: Month of the year (January - December)
// @param year: Year of the month (example: 2027)
// @returns: Day of the month for the given Month and Year that matches the weekday and scheduled week
func Day(wSched WeekSchedule, wDay time.Weekday, month time.Month, year int) int {
	first := time.Date(year, month, 1, 0, 0, 0, 0, time.Local)   // First of the month
	teenth := time.Date(year, month, 13, 0, 0, 0, 0, time.Local) // Thirteen of the month
	last := NextMonth(first).Add(-1 * ONE_DAY)                   // One day prior to next month

	switch wSched {
	case First:
		return FindWeekday(first, wDay, ONE_DAY)
	case Last:
		return FindWeekday(last, wDay, -1*ONE_DAY)
	case Teenth:
		return FindWeekday(teenth, wDay, ONE_DAY)
	case Second:
		return NthWeekday(first, 2, wDay)
	case Third:
		return NthWeekday(first, 3, wDay)
	case Fourth:
		return NthWeekday(first, 4, wDay)
	}
	return -1
}
