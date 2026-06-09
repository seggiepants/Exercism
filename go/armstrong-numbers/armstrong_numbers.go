// Check if a number is an armstrong number.
package armstrongnumbers

import "math"

// Check to see if the given number is an armstrong number
// which is when the sum of the digits raised to the power of the length of the number is equal to the number
// @param n: The number to check.
// @returns: True if the number is an armstrong number.
func IsNumber(n int) bool {
	var digits = []int{}
	total := n
	if total < 0 {
		total *= -1
	}
	for total > 0 {
		digit := total % 10
		total = (total - digit) / 10
		digits = append(digits, digit)
	}

	var armstrong float64
	var power float64 = float64(len(digits))
	for _, digit := range digits {
		armstrong += math.Pow(float64(digit), power)
	}
	return int(armstrong) == n
}
