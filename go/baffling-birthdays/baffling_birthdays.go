// Baffling Birthdays exercise.
package bafflingbirthdays

import (
	"math"
	"math/rand"
	"time"
)

const DAYS_IN_YEAR int = 365
const MONTHS_IN_YEAR int = 12

// Give a set of dates see if there are any matching birthdays.
// I was going to do something fancy with a map but decided this O(n^2)
// search was probably faster and less memory consuming. Shouldn't be as
// bad as N^2 since I can exit early and the inner loop starts from outer loop plus one
// @param dates: The dates to search for matching birthdays
// @returns: True if at least one month and day match was found.
func SharedBirthday(dates []time.Time) bool {
	for j := 0; j < len(dates); j++ {
		for i := j + 1; i < len(dates); i++ {
			if dates[j].Month() == dates[i].Month() && dates[j].Day() == dates[i].Day() {
				return true
			}
		}
	}
	return false
}

// Fill a slice of size 'size' with random birthdays. To get a random birthday add a duration
// of up to 365 (# days in a year) to a base date. But first scale the duration up to days
// @param size: The number of birthdays to generate
// @returns: A slize of size 'size' filled with random birthdays.
func RandomBirthdates(size int) []time.Time {
	ret := make([]time.Time, size)
	startDate := time.Date(2026, time.January, 1, 0, 0, 0, 0, time.Local)
	for i := 0; i < size; i++ {
		ret[i] = startDate.Add(time.Hour * 24 * time.Duration(rand.Intn(DAYS_IN_YEAR)))
	}
	return ret
}

// Factorial of an integer value (as float64)
// 1 * 2 * 3 * 4 * ...... n
// @param n: The factorial to generate.
// @returns: float64 version of the factorial of n.
func Factorial(n int) float64 {
	var ret float64 = 1.0
	for i := 1; i <= n; i++ {
		ret *= float64(i)
	}
	return ret
}

// Estimate the probability of any two people sharing a birthday among a group of size 'size'
// see: https://en.wikipedia.org/wiki/Birthday_problem
// @param size: The number of people in the group.
// @returns: probablity of a match as a float64
func EstimatedProbability(size int) float64 {
	var combinationDistinct float64 = Factorial(DAYS_IN_YEAR) / Factorial(DAYS_IN_YEAR-size)
	var combinationTotal float64 = math.Pow(float64(DAYS_IN_YEAR), float64(size))
	var probabilityUnique = combinationDistinct / combinationTotal
	return 1.0 - probabilityUnique
}
