// Killer Sudoku Helper exercise - Find combinations that satisfy requirements
package killersudokuhelper

import "slices"

// Calculate the combinations of values 1-9 that satisfy the requirements.
// @param sum: The accepted combinations should add up to this
// @param size: The number of items needed in the accepted combinations
// @param exclude: Slice of values that may not be in the accepted combinations.
// @returns: A slice of slices of integers for the accepted unique combinations
// where each combination is sorted smallest to largest.
func Combinations(sum, size int, exclude []int) [][]int {
	empty := make([]int, 0)
	return Helper(sum, size, empty, exclude)
}

// Does the actual combinations work recursively but with a working set.
// @param sum: The accepted combinations should add up to this
// @param size: The number of items needed in the accepted combinations
// @param working: Slice of int of what we have built so far.
// @param exclude: Slice of values that may not be in the accepted combinations.
// @returns: A slice of slices of integers for the accepted unique combinations
// where each combination is sorted smallest to largest.
func Helper(sum int, size int, working []int, exclude []int) [][]int {
	var result [][]int = make([][]int, 0)
	var currentSum int = 0
	for _, value := range working {
		currentSum += value
	}
	// Base case we are there
	if len(working) == size && currentSum == sum {
		slices.Sort(working)
		result = append(result, working)
		return result
	}
	// Another base case can't add any more.
	if len(working)+1 > size || currentSum > sum {
		return result
	}

	for i := 1; i <= 9; i++ {
		if currentSum+i > sum || slices.Contains(working, i) || slices.Contains(exclude, i) {
			// Too Big, Already used in partial solution, or was excluded.
			continue
		}
		var next []int = make([]int, len(working))
		copy(next, working)
		next = append(next, i)
		output := Helper(sum, size, next, exclude)
		if len(output) > 0 {
			for _, row := range output {
				slices.Sort(row)
				if !slices.ContainsFunc(result, func(slice []int) bool {
					if len(row) == len(slice) {
						for index, value := range slice {
							if row[index] != value {
								return false
							}
						}
						return true
					}
					return false
				}) {
					result = append(result, row)
				}
			}
		}
	}
	return result
}
