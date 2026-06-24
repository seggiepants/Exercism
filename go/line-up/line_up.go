// Greet and number the customers in a line up.
package lineup

import "fmt"

// Return a string with the customer's name and number
// @param name: The name of the customer
// @param number: The number of the customer
// @returns: The customer name with the nicely suffixed number greeting
func Format(name string, number int) string {
	return fmt.Sprintf("%s, you are the %s customer we serve today. Thank you!", name, FormatNum(number))
}

// Format a number with a suffix
// @param number: The number to format
// @returns: string with the proper suffix.
func FormatNum(number int) string {
	lastDigit := number % 10
	lastTwoDigits := number % 100

	switch {
	case lastDigit == 1 && lastTwoDigits != 11:
		return fmt.Sprintf("%dst", number)
	case lastDigit == 2 && lastTwoDigits != 12:
		return fmt.Sprintf("%dnd", number)
	case lastDigit == 3 && lastTwoDigits != 13:
		return fmt.Sprintf("%drd", number)
	default:
		return fmt.Sprintf("%dth", number)
	}
}
