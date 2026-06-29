// Sieve of Eratosthenes exercise.
package sieve

// Implements the Sieve of Eratosthenes
// @param limit: Find primes up to and including this number.
// @returns: slice of primes greater or equal to 2 and less than or equal to limit.
func Sieve(limit int) []int {
	candidates := make([]bool, limit+1)
	var primes []int = []int{}

	// Set candidates to potentially prime
	for i := 0; i <= limit; i++ {
		candidates[i] = i >= 2
	}

	// flag the non-primes
	for j := 2; j <= limit; j++ {
		if candidates[j] && j*2 <= limit {
			for i := j * 2; i <= limit; i += j {
				candidates[i] = false
			}
		}
	}

	// collect the primes
	for i := 2; i <= limit; i++ {
		if candidates[i] {
			primes = append(primes, i)
		}
	}
	return primes
}
