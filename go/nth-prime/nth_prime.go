// Find the nth Prime Number.
package nthprime

import "errors"

var (
	primes []int
)

// Nth returns the nth prime number. An error must be returned if the nth prime number can't be calculated ('n' is equal or less than zero)
// @param n: The nth prime to find.
// @returns: The nth prime (int) or an error/nil if desired number is out of bounds
func Nth(n int) (int, error) {
	if n == 0 {
		return 0, errors.New("there is no zeroth prime")
	} else if n < 0 {
		return 0, errors.New("there are no negative primes")
	}

	if len(primes) < n {
		current := 1 // start counting at 1 (well 2 actually)
		for len(primes) < n {
			current++
			if isNextPrime(current, primes) {
				// if we found a prime add it to the list.
				primes = append(primes, current)
			}
		}
	}

	return primes[n-1], nil
}

// Check to see if a given number is the next prime number available.
// You must run this in order or you may have gaps.
// Parameters:
// @param n: The value to check to see if it is prime or not.
// @param primes: List of prime numbers found while iterating up to number.
// @returns: false if this is a multiple of any prime in the list, true otherwise.
func isNextPrime(number int, primes []int) bool {

	// sanity check
	if number <= 1 {
		return false
	}

	// if we aren't divisible by any of the primes found so far, then we
	// must be a prime number.
	for _, prime := range primes {
		if number%prime == 0 {
			return false
		}
	}

	// Not a multiple of any prime so it must be a prime.
	return true
}
