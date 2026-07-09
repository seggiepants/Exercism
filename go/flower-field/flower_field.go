// (Bawlderized) Minesweeper exercise.
package flowerfield

import (
	"strconv"
	"strings"
)

// Returns an annotated board of counts for positions on a minesweeper board.
// @param board: Slice of string where bombs are marked as "*"
// @returns: A new Slice of string that has "*" for bombs, " " for cells with no
// neighboring bombs, and a count of neighboring bombs otherwise.
func Annotate(board []string) []string {
	var ret []string = make([]string, len(board))
	for j := 0; j < len(board); j++ {
		row := strings.Builder{}
		for i := 0; i < len(board[j]); i++ {
			if board[j][i:i+1] == "*" {
				row.WriteString("*")
			} else {
				count := CountNeighbors(board, i, j)
				if count > 0 {
					row.WriteString(strconv.Itoa(count))
				} else {
					row.WriteString(" ")
				}
			}
			ret[j] = row.String()
		}
	}
	return ret
}

// Count the number of neighboring cells from a point where the neighbor = "*"
// @param board: The board to look for neighbors on
// @param x: x-coordinate of the location to look at
// @param y: y-coordinate of the location to look at
// @returns: The number of neighbors marked as "*"
func CountNeighbors(board []string, x, y int) int {
	var count int = 0
	for j := y - 1; j < y+2; j++ {
		for i := x - 1; i < x+2; i++ {
			if i == x && j == y {
				continue
			}
			if i >= 0 && j >= 0 && j < len(board) && i < len(board[j]) {
				if board[j][i:i+1] == "*" {
					count++
				}
			}
		}
	}
	return count
}
