package differenceofsquares

// More efficient formulas than brute force found at:
// https://iq.opengenus.org/difference-between-square-of-sum-and-sum-of-squares/

func SquareOfSum(n int) int {
	// Sum of 1 to N = N * (N+1) / 2
	sum := (n * (n + 1)) / 2
	return sum * sum
}

func SumOfSquares(n int) int {
	//Sum of square of 1 to N = (2 * N + 1) * (N + 1) / 6
	return n * (n + 1) * (2*n + 1) / 6
}

func Difference(n int) int {
	return SquareOfSum(n) - SumOfSquares(n)
}
