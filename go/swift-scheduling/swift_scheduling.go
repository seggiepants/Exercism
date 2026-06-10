// Swift Scheduling Exercise
package swiftscheduling

import (
	"slices"
	"time"
)

var QUARTERS [][]time.Month = [][]time.Month{{time.January, time.February, time.March},
	{time.April, time.May, time.June},
	{time.July, time.August, time.September},
	{time.October, time.November, time.December},
}

var MONTHS []time.Month = []time.Month{
	time.January, time.February, time.March,
	time.April, time.May, time.June,
	time.July, time.August, time.September,
	time.October, time.November, time.December,
}

// For a given month, return the quarter (1-4)
// @param Month: The month to check for
// @returns: The quarter, and a boolean indicating an error occured
func GetQuarter(Month time.Month) (int, bool) {
	for i := 0; i < len(QUARTERS); i++ {
		if slices.Contains(QUARTERS[i], Month) {
			return i + 1, true
		}
	}
	return 0, false
}

// Calculate the actual date from a current date and delivery modifier value
// NOW - In two hours
// ASAP - Today at 5pm or Tomorrow at 1pm if current time is after 1pm
// EOW - End of Week for Monday, Tuesday, Wednesday Friday at 5pm, for the rest of the work week Sunday at 8pm
// [digit]M - 8:00 on first workday of the month of this year if before the month and next year if this month or later - digit is 1-12
// Q[digit] - 8:00 on the last workday of the last month of the given quarter (next year if quarter passed already this year) digit is 1-4
// @param start: The date the delivery time period was stated
// @param delivery: The delivery string NOW, ASAP, EOW, 2M, Q3, etc.
// @returns: Empty string on error otherwise the desired date formatted in 2026-01-02T15:04:05 format.
func DeliveryDate(start, delivery string) string {
	const FORMAT_STRING = "2006-01-02T15:04:05"

	StartDate, err := time.Parse(FORMAT_STRING, start)
	var ret time.Time
	var ok bool = true

	if err != nil {
		return ""
	}
	switch {
	case delivery == "NOW":
		ret = StartDate.Add(time.Hour * 2)
	case delivery == "ASAP":
		if StartDate.Hour() < 13 {
			ret = time.Date(StartDate.Year(), StartDate.Month(), StartDate.Day(), 17, 0, 0, 0, time.Local)
		} else {
			ret = time.Date(StartDate.Year(), StartDate.Month(), StartDate.Day(), 13, 0, 0, 0, time.Local)
			ret = ret.AddDate(0, 0, 1)
		}
	case delivery == "EOW":
		if StartDate.Weekday() == time.Monday || StartDate.Weekday() == time.Tuesday || StartDate.Weekday() == time.Wednesday {
			ret = time.Date(StartDate.Year(), StartDate.Month(), StartDate.Day(), 17, 0, 0, 0, time.Local)
			for ret.Weekday() != time.Friday {
				ret = ret.AddDate(0, 0, 1)
			}
		} else if StartDate.Weekday() == time.Thursday || StartDate.Weekday() == time.Friday {
			ret = time.Date(StartDate.Year(), StartDate.Month(), StartDate.Day(), 20, 0, 0, 0, time.Local)
			for ret.Weekday() != time.Sunday {
				ret = ret.AddDate(0, 0, 1)
			}
		} else {
			ok = false
		}
	case delivery[0] == 'Q':
		if delivery[1] == '1' || delivery[1] == '2' || delivery[1] == '3' || delivery[1] == '4' {
			var Quarter int = int(delivery[1]) - int('0')
			CurrentQuarter, ok := GetQuarter(StartDate.Month())
			if !ok {
				return ""
			}
			lastMonth := QUARTERS[Quarter-1][2]
			if CurrentQuarter <= Quarter {
				ret = time.Date(StartDate.Year(), lastMonth, 1, 8, 0, 0, 0, time.Local)
			} else {
				ret = time.Date(StartDate.Year()+1, lastMonth, 1, 8, 0, 0, 0, time.Local)
			}
			ret = ret.AddDate(0, 1, 0)  // Month forward
			ret = ret.AddDate(0, 0, -1) // Subtract a day for last day of month
			for ret.Weekday() == time.Saturday || ret.Weekday() == time.Sunday {
				ret = ret.AddDate(0, 0, -1) // Move backwards from weekend
			}
		} else {
			ok = false
		}
	case delivery[0] >= '0' && delivery[0] <= '9':
		MonthNum := 0
		var i int
		for i = 0; delivery[i] >= '0' && delivery[i] <= '9'; i++ {
			MonthNum *= 10
			MonthNum += int(delivery[i]) - int('0')
		}
		if delivery[i] == 'M' {
			Month := MONTHS[MonthNum-1]
			if StartDate.Month() < Month {
				ret = time.Date(StartDate.Year(), Month, 1, 8, 0, 0, 0, time.Local)
			} else {
				ret = time.Date(StartDate.Year()+1, Month, 1, 8, 0, 0, 0, time.Local)
			}
			for ret.Weekday() == time.Saturday || ret.Weekday() == time.Sunday {
				ret = ret.AddDate(0, 0, 1) // Move forward from weekend
			}
		} else {
			ok = false
		}
	}
	if !ok {
		return ""
	}
	return ret.Format(FORMAT_STRING)
}
