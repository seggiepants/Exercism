package phonenumber

import (
	"errors"
	"fmt"
	"regexp"
)

// Parse the give phone number using a regular expression. Return the find matches result.
// @param phoneNumber: The phone number to parse
// @returns: error if regex did not find a number else an array of strings with the first entry
// being the entire group matched followed by named matches in order.
func ParsePhoneNumber(phoneNumber string) ([]string, error) {
	pattern := `^\+?1?[ \.-]*\(?(?<area_code>[2-9]\d{2})\)?[ \.-]*(?<exchange>[2-9]\d{2})[ \.-]*(?<subscriber>\d{4})[ \.-]*$`
	re := regexp.MustCompile(pattern)
	matches := re.FindStringSubmatch(phoneNumber)
	if matches == nil {
		return make([]string, 0), errors.New("Phone number not found.")
	}
	return matches, nil
}

// Return the phone number (area code, exchange, and subscriber as a string of all digits)
// from the given phoneNumber parameter
// @param phoneNumber: The phone number to parse
// @returns error if phone number not parsed otherwise area code, exchange and subscriber as one long string of digits
func Number(phoneNumber string) (string, error) {
	parts, err := ParsePhoneNumber(phoneNumber)
	if err != nil {
		return "", err
	}
	return parts[1] + parts[2] + parts[3], nil
}

// Return the area code of a phone number (just the digits) from the given phoneNumber parameter
// @param phoneNumber: The phone number to parse
// @returns error if phone number not parsed otherwise the digits of the area code as a string
func AreaCode(phoneNumber string) (string, error) {
	parts, err := ParsePhoneNumber(phoneNumber)
	if err != nil {
		return "", err
	}
	return parts[1], nil
}

// Parse and return a phone number in the desired format (NNN) NNN-NNNN (N is a digit)
// @param phoneNumber: The phone number to parse
// @returns error is phone number not parsed otherwise a formatted phone number string.
func Format(phoneNumber string) (string, error) {
	number, err := Number(phoneNumber)
	if err != nil {
		return "", err
	}
	return fmt.Sprintf("(%s) %s-%s", number[:3], number[3:6], number[6:]), nil
}
