// Convert an arabic number to roman numerals.
package romannumerals

import "errors"

// Instead of complicated logic to handle the special cases like IX being nine I just
// added them to the list to find. The no more than three case should be handled without
// any special case code.
var ArabicToRoman map[int]string = map[int]string{
	1000: "M",
	900:  "CM",
	500:  "D",
	400:  "CD",
	100:  "C",
	90:   "XC",
	50:   "L",
	40:   "XL",
	10:   "X",
	9:    "IX",
	5:    "V",
	4:    "IV",
	1:    "I",
}

// List of keys and order to iterate over them.
var ArabicOrderedKeys []int = []int{1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1}

// Convert and integer into a Roman Numeral
// @param input: The number to convert
// @returns: string the value converted to roman numeral
// @raises: Error if input is 0, negative, or >= 4000
// (since I have nothing bigger than 1000 and I can only repeat it three times)
func ToRomanNumeral(input int) (string, error) {
	var ret string = ""

	if input < 1 || input >= 4000 {
		return "", errors.New("Number out of range.")
	}
	for input > 0 {
		for _, key := range ArabicOrderedKeys {
			if key <= input {
				ret = ret + ArabicToRoman[key]
				input -= key
				break
			}
		}
	}

	return ret, nil
}
