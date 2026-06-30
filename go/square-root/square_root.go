// SquareRoot (for positive integers) example.
package squareroot

import "errors"

// Find the integer square root of a number.
// @param number: Value to find integer square root of.
// @returns Error if value is <= 0 or integer square root on success.
func SquareRoot(number int) (int, error) {
	if number <= 0 {
		return 0, errors.New("Index out of bounds.")
	}
	if number == 1 {
		return 1, nil
	}
	return SquareRoot_Helper(number, 0, number), nil
}

// Binary Search for an integer square root of number with the given
// upper and lower bounds.
// @param number: Value to find integer square root of
// @param lower: Lower bound of the number range.
// @param upper: Upper bound of the nuber range.
// @returns: Best match at integer square root of number parameter.
func SquareRoot_Helper(number int, lower int, upper int) int {
	var middle int = lower + ((upper - lower) / 2)
	var sqrMinusOne, sqrPlusOne int
	sqrMinusOne = (middle - 1) * (middle - 1)
	sqrPlusOne = (middle + 1) * (middle + 1)

	if sqrMinusOne < number && sqrPlusOne > number {
		return middle
	} else if sqrMinusOne == number {
		return middle - 1
	} else if sqrPlusOne == number {
		return middle + 1
	} else if sqrMinusOne > number {
		return SquareRoot_Helper(number, lower, middle-1)
	} else if number > sqrPlusOne {
		return SquareRoot_Helper(number, middle+1, upper)
	}
	return middle // shouldn't get here.
}
