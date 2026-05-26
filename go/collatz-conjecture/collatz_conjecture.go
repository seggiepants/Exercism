package collatzconjecture

import "errors"

/*
Collatz Conjecture - How many iterations does it take to reach 1 following the
conjecture for the given number (n).
@param n int - The number to start with
@returns int - Number of steps to reach 0
@errors - Error if n <= 0.
*/
func CollatzConjecture(n int) (int, error) {
	if n <= 0 {
		return 0, errors.New("Value must be a positive integer.")
	}
	steps := 0
	value := n
	for value != 1 {
		steps++
		if value%2 == 0 {
			value = value / 2
		} else {
			value = (value * 3) + 1
		}
	}
	return steps, nil
}
