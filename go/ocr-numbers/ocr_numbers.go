// OCR Numbers exercise. Really just string matching.
package ocrnumbers

import (
	"errors"
	"strconv"
	"strings"
)

// Rearranged to put zero first so index matches digit.
var digits []string = []string{
	" _     _  _     _  _  _  _  _ ", //
	"| |  | _| _||_||_ |_   ||_||_|", // Decimal numbers.
	"|_|  ||_  _|  | _||_|  ||_| _|", //
	"                              ", // The fourth line is always blank,
}

// Recognize a string of digits from the given \n separated string.
// @param s: Input text (string)
// @returns: slice of strings where ocr digits are the expected number (? if not recognized). One entry for matching block.
// @raises: Error if the array is not the correct size/shape.
func Recognize(s string) ([]string, error) {
	result := make([]string, 0)
	rows := strings.Split(s, "\n")
	if len(rows) > 0 && len(rows[0]) == 0 {
		rows = rows[1:]
	}
	if len(rows) < 4 || len(rows)%4 != 0 {
		return result, errors.New("number of input lines is not a multiple of four")
	}
	for i := 1; i < len(rows); i++ {
		if len(rows[i]) < 3 || len(rows[i])%3 != 0 {
			return result, errors.New("number of input columns is not a multiple of three")
		}
	}

	for j := 0; j < len(rows); j += 4 {
		var number string = ""
		for i := 0; i < len(rows[j]); i += 3 {
			digit, err := recognizeDigit(rows, i, j)
			if err != nil {
				number += "?"
			} else {
				number += strconv.Itoa(digit)
			}
		}
		result = append(result, number)
	}
	return result, nil
}

// Recognize a single digit from the slice of string at position x, y
// @param rows: slice of string one string per line in the input
// @param x: x-coordinate of the position to match at.
// @param y: y-coordinate of the position to match at.
// @returns: The digit that was matched, or an error.
// @raises: Raises and error if no digit match can be found.
func recognizeDigit(rows []string, x int, y int) (int, error) {
	for i := 0; i < len(digits[0]); i += 3 {
		if rows[y+0][x:x+3] == digits[0][i:i+3] &&
			rows[y+1][x:x+3] == digits[1][i:i+3] &&
			rows[y+2][x:x+3] == digits[2][i:i+3] &&
			rows[y+3][x:x+3] == digits[3][i:i+3] {
			return i / 3, nil
		}
	}
	return 0, errors.New("No match found.")
}
