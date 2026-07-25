// All your Base - Number base conversion exercise.
package allyourbase

import (
	"errors"
	"slices"
)

// Convert a slice of digits from one number base to another.
// @param inputBase: Number base for input 2 = Binary, 10 = Decimal, etc.
// @param outputBase: Number base for output 2 = Binary, 10 = Decimal, etc.
// @returns: slice of integers that has the same number in the desired base. (F in hexadecimal will be a 15).
// @raises: Error if input or output base is less than two, or if a digit doesn't conform it its base.
func ConvertToBase(inputBase int, inputDigits []int, outputBase int) ([]int, error) {
	if inputBase < 2 {
		return nil, errors.New("input base must be >= 2")
	}
	if outputBase < 2 {
		return nil, errors.New("output base must be >= 2")
	}

	digits := make([]int, 0)
	// convert to base 10
	input := 0
	for _, digit := range inputDigits {
		if digit < 0 || digit >= inputBase {
			return nil, errors.New("all digits must satisfy 0 <= d < input base")
		}
		input = (input * inputBase) + digit
	}

	// convert to base outputBase
	for input != 0 {
		digit := input % outputBase
		input = (input - digit) / outputBase
		digits = append(digits, digit)
	}
	slices.Reverse(digits)
	if len(digits) == 0 {
		digits = append(digits, 0)
	}
	return digits, nil
}
