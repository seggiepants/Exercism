// Return the Prime factors of a given number
package primefactors

// Return the Prime factors of a given number
// @param n: The number to compute factors for (is a factor if number % factor == 0 and factor > 1 and factor <= number)
// @returns: Slice of int64 containing the factors found. Empty if none found.
func Factors(n int64) []int64 {
	ret := []int64{}
	remaining := n
	for i := int64(2); i <= n && remaining > 1; i++ {
		for remaining%i == 0 && remaining > 1 {
			ret = append(ret, i)
			remaining /= i
		}
	}
	return ret
}
