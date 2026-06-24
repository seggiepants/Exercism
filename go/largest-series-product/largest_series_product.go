// Exercise: Largest Series Product
package largestseriesproduct

import (
	"errors"
	"fmt"
)

// Return largest product of digits within the given string of length span
// @param digits: The digits to iterate over
// @param span: Size of the window to look at
// @returns: Error for invalid span size or no input otherwise larges product found.
func LargestSeriesProduct(digits string, span int) (int64, error) {
	if span > len(digits) || span <= 0 || len(digits) == 0 {
		return int64(0), errors.New("Invalid span length or no input")
	}
	var first bool = true
	var lsp int64 = 0
	var runes []rune = []rune(digits)
	for i := 0; i <= len(runes)-span; i++ {
		var current int64 = 1
		for j := i; j < i+span; j++ {
			var digit rune = runes[j]
			if digit < '0' || digit > '9' {
				return int64(0), fmt.Errorf("Invalid character, not a digit: '%s'", string(digit))
			}
			current *= int64(digit - '0')
		}
		if first || current > lsp {
			lsp = current
			first = false
		}
	}
	return lsp, nil
}
