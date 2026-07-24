// Transpose exercise.
package transpose

import "strings"

// Transpose a slice of strings. (Basically rotate it 90 degrees counter clockwise)
// width x height becomes height x width
// @paran input: slice of strings to transpose
// @returns: transformed copy of the input slice.
func Transpose(input []string) []string {
	rows := len(input)
	cols := 0
	for _, row := range input {
		if len(row) > cols {
			cols = len(row)
		}
	}
	ret := make([]string, cols)
	for j := 0; j < cols; j++ {
		row := ""
		// I need to distinguish between spaces and
		// extra padding. So the extra padding is character 01.
		for i := 0; i < rows; i++ {
			if j >= len(input[i]) {
				row += string(rune(0x01))
			} else {
				row += input[i][j : j+1]
			}
		}
		// Trim any extra padding that wasn't a space in the input.
		row = strings.TrimRightFunc(row, func(r rune) bool { return r == 0x01 })
		// Replace non-trimmed padding with a space.
		row = strings.ReplaceAll(row, string(rune(0x01)), " ")
		ret[j] = row
	}
	return ret
}
