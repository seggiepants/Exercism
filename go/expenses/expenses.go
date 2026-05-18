package expenses

import (
	"errors"
	"fmt"
)

// Record represents an expense record.
type Record struct {
	Day      int
	Amount   float64
	Category string
}

// DaysPeriod represents a period of days for expenses.
type DaysPeriod struct {
	From int
	To   int
}

// Filter returns the records for which the predicate function returns true.
func Filter(in []Record, predicate func(Record) bool) []Record {
	out := []Record{}
	for _, record := range in {
		if predicate(record) {
			out = append(out, record)
		}
	}
	return out
}

// ByDaysPeriod returns predicate function that returns true when
// the day of the record is inside the period of day and false otherwise.
func ByDaysPeriod(p DaysPeriod) func(Record) bool {
	from := p.From
	to := p.To

	return func(record Record) bool {
		return record.Day >= from && record.Day <= to
	}
}

// ByCategory returns predicate function that returns true when
// the category of the record is the same as the provided category
// and false otherwise.
func ByCategory(c string) func(Record) bool {
	category := c

	return func(record Record) bool {
		return category == record.Category
	}
}

// TotalByPeriod returns total amount of expenses for records
// inside the period p.
func TotalByPeriod(in []Record, p DaysPeriod) float64 {
	var total float64
	total = 0.0
	for _, record := range Filter(in, ByDaysPeriod(p)) {
		total += record.Amount
	}
	return total
}

// CategoryExpenses returns total amount of expenses for records
// in category c that are also inside the period p.
// An error must be returned only if there are no records in the list that belong
// to the given category, regardless of period of time.
func CategoryExpenses(in []Record, p DaysPeriod, c string) (float64, error) {
	categoryRecords := Filter(in, ByCategory(c))
	if len(categoryRecords) == 0 {
		message := fmt.Sprintf("unknown category %s", c)
		return 0, errors.New(message)
	}
	return TotalByPeriod(categoryRecords, p), nil
}
