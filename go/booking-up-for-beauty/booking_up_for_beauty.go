package booking

import (
	"fmt"
	"time"
)

func ParseDateTime(date string) time.Time {
	layouts := []string{
		"1/2/2006 15:04:05",
		"January 2, 2006 15:04:05",
		"Monday, January 2, 2006 15:04:05",
	}
	var dateTime time.Time
	var err error
	for _, layout := range layouts {
		dateTime, err = time.Parse(layout, date) // time.Time
		if err == nil {
			return dateTime
		}
	}
	if err != nil {
		panic(err)
	}
	return dateTime
}

// Schedule returns a time.Time from a string containing a date.
func Schedule(date string) time.Time {
	return ParseDateTime(date)
}

// HasPassed returns whether a date has passed.
func HasPassed(date string) bool {
	dateTime := ParseDateTime(date)
	return dateTime.Before(time.Now())
}

// IsAfternoonAppointment returns whether a time is in the afternoon.
func IsAfternoonAppointment(date string) bool {
	dateTime := ParseDateTime(date)
	return dateTime.Hour() >= 12 && dateTime.Hour() <= 18
}

// Description returns a formatted string of the appointment time.
func Description(date string) string {
	dateTime := ParseDateTime(date)
	dateValue := dateTime.Format("Monday, January 2, 2006")
	timeValue := dateTime.Format("15:04")
	return fmt.Sprintf("You have an appointment on %s, at %s.", dateValue, timeValue)
}

// AnniversaryDate returns a Time with this year's anniversary.
func AnniversaryDate() time.Time {
	currentDateTime := time.Now()
	return time.Date(currentDateTime.Year(), time.September, int(15), int(0), int(0), int(0), int(0), time.UTC)
}
