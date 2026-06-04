// Sum of Multiples exercise.
package sumofmultiples

// Sum the multiples of divisors under a given limit
// return the sum of the set of multiples
// @param limit: The value to stop at (non-inclusive)
// @param divisors: Variadic slice of possible divisors
// @returns: sum of unique divisors and their multiples less than limit.
func SumMultiples(limit int, divisors ...int) int {
	var set = make(map[int]bool)

	for _, divisor := range divisors {
		step := divisor
		current := step
		for current < limit {

			set[current] = true
			current += step

			if step <= 0 {
				break
			}
		}
	}
	total := 0
	for key := range set {
		total += key
	}
	return total
}
