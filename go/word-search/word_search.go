// Word Search Exercise
package wordsearch

import (
	"errors"
	"strings"
)

// Information about a diagonal on the puzzle
type DiagonalInfo struct {
	startX int
	startY int
	endX   int
	endY   int
	text   string
}

// Find the given words in the word search and return their locations.
// @param words: slice of string - the words to search for
// @param puzzle: slice of string - rows of the puzzle.
// @returns: map of word to x, y location of start and end of the word in the puzzle
func Solve(words []string, puzzle []string) (map[string][2][2]int, error) {
	result := make(map[string][2][2]int, 0)

	for _, word := range words {
		result[word] = [2][2]int{{-1, -1}, {-1, -1}} // default
	}

	for _, word := range words {
		wordReverse := ReverseString(word)
		found := false

		for rowIndex, row := range puzzle {
			// Look for word left to right or right to left.
			leftToRight := strings.Index(row, word)
			rightToLeft := strings.Index(row, wordReverse)
			if leftToRight >= 0 {
				result[word] = [2][2]int{{leftToRight, rowIndex}, {leftToRight + len(word) - 1, rowIndex}}
				found = true
				break
			} else if rightToLeft >= 0 {
				result[word] = [2][2]int{{rightToLeft + len(word) - 1, rowIndex}, {rightToLeft, rowIndex}}
				found = true
				break
			}
		}
		if !found && len(words) > 0 {
			// Now try columns
			for colIndex := 0; colIndex < len(puzzle[0]); colIndex++ {
				//Build the column
				column := ""
				for rowIndex := 0; rowIndex < len(puzzle); rowIndex++ {
					column += string(puzzle[rowIndex][colIndex])
				}

				// Look for word top to bottom or bottom to top.
				topToBottom := strings.Index(column, word)
				bottomToTop := strings.Index(column, wordReverse)
				if topToBottom >= 0 {
					result[word] = [2][2]int{{colIndex, topToBottom}, {colIndex, topToBottom + len(word) - 1}}
					found = true
					break
				} else if bottomToTop >= 0 {
					result[word] = [2][2]int{{colIndex, bottomToTop + len(word) - 1}, {colIndex, bottomToTop}}
					found = true
					break
				}
			}
		}

		if !found && len(words) > 0 {
			// Now try diagonals
			diagonals := Diagonals(puzzle)
			for _, diagonal := range diagonals {

				// Look for word top to bottom or bottom to top.
				forward := strings.Index(diagonal.text, word)
				reverse := strings.Index(diagonal.text, wordReverse)
				dx := 1
				if diagonal.startX > diagonal.endY {
					dx = -1
				}
				dy := 1
				if diagonal.startY > diagonal.endY {
					dy = -1
				}
				if forward >= 0 {
					start := diagonal.startX + (dx * forward)
					end := diagonal.startY + (dy * forward)
					result[word] = [2][2]int{{start, end}, {start + (dx * (len(word) - 1)), end + (dy * (len(word) - 1))}}
					found = true
					break
				} else if reverse >= 0 {
					start := diagonal.startX + (dx * (reverse + len(word) - 1))
					end := diagonal.startY + (dy * (reverse + len(word) - 1))
					result[word] = [2][2]int{{start, end}, {start - (dx * (len(word) - 1)), end - (dy * (len(word) - 1))}}
					found = true
					break
				}
			}
		}

		if !found {
			return result, errors.New("Word not found: " + word)
		}
	}
	// list 2,5-> 5,2 instead of 2,9 -> 5,12
	return result, nil
}

// Reverse a string, why is this not in the GO common libraries?
// @param line: the string to reverse
// @returns: reversed copy of the input string. May fail on multi-byte strings.
func ReverseString(line string) string {
	result := make([]rune, len(line))
	lastIndex := len(line) - 1
	for i, char := range line {
		result[lastIndex-i] = char
	}
	return string(result)
}

// Calculate the diagonals of a given grid with a given size.
// @param grid: The grid to calculate the diagonals from.
// @returns: A slice of DiagonalInfo one for each diagonal (has start x,y end x,y and the text.)
func Diagonals(grid []string) []DiagonalInfo {
	rows := len(grid)
	cols := 0
	if rows > 0 {
		cols = len(grid[0])
	}
	result := make([]DiagonalInfo, 0)

	// Diagonal left to right.
	for j := 0; j < rows; j++ {
		var diagonal string = ""
		x := 0
		y := j
		for x < cols && y < rows {
			diagonal += string(grid[y][x])
			x++
			y++
		}
		result = append(result, DiagonalInfo{
			startX: 0, startY: j, endX: x - 1, endY: y - 1, text: diagonal})
	}
	for i := 1; i < cols; i++ {
		var diagonal string = ""
		x := i
		y := 0
		for x < cols && y < rows {
			diagonal += string(grid[y][x])
			x++
			y++
		}
		result = append(result, DiagonalInfo{
			startX: i, startY: 0, endX: x - 1, endY: y - 1, text: diagonal})
	}

	// Diagonal Right to Left
	for j := 0; j < rows; j++ {
		var diagonal string = ""
		x := 0
		y := j
		for x < cols && y >= 0 {
			diagonal += string(grid[y][x])
			x++
			y--
		}
		result = append(result, DiagonalInfo{startX: 0, startY: j, endX: x - 1, endY: y + 1, text: diagonal})
	}

	for i := 1; i < cols; i++ {
		var diagonal string = ""
		x := i
		y := rows - 1
		for x < cols && y >= 0 {
			diagonal += string(grid[y][x])
			x++
			y--
		}
		result = append(result, DiagonalInfo{startX: i, startY: rows - 1, endX: x - 1, endY: y + 1, text: diagonal})
	}
	return result
}
