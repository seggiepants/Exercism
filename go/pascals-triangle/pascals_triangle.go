// Paascal's Triangle
package pascalstriangle

// Return Pascal's Triangle up to row n.
// @param n: The row to stop on
// @returns: Slice of Slices of Integer where each sub-slice is a row
// of Pascal's Triangle.
func Triangle(n int) [][]int {
	var ret [][]int = make([][]int, n)
	for i := 1; i <= n; i++ {
		count := 1
		switch i {
		case 1:
			ret[i-1] = []int{1}
		case 2:
			ret[i-1] = []int{1, 1}
		default:
			previous := ret[i-2]
			count = len(previous) + 1
			row := make([]int, count)
			row[0] = previous[0]
			row[count-1] = previous[len(previous)-1]
			for i := 0; i < len(previous)-1; i++ {
				row[i+1] = previous[i] + previous[i+1]
			}
			ret[i-1] = row
		}
	}
	return ret
}
