// Exercise: Counting Territories on a Go Board
package gocounting

import "errors"

// Holds points held by each player or neither
type AllTerritories struct {
	Black [][2]int
	White [][2]int
	None  [][2]int
}

// Game specific variables
type Game struct {
	board []string
}

// Create a new game object and return it
// @param board: Slice of strings where each string is a row of the board. The strings are assumed to be of length >= 1 and the same length on each row.
// @returns: Pointer to new Game struct.
func NewGame(board []string) *Game {
	return &Game{board: board}
}

// Territory - Compute the locations within the territory containing the point at x, y
// @param x: x-coordinate of point in territory
// @param y: y-coordinate of point in territory
// @returns: string is the owner of the territory ("BLACK", "WHITE", or "NONE"). The slice of int arrays of 2 elements are the points contained in the territory.
// @raises: Error is populated when the desired position is not on the board.
func (g *Game) Territory(x, y int) (string, [][2]int, error) {
	if x < 0 || y < 0 || y >= len(g.board) || x >= len(g.board[y]) {
		return "", make([][2]int, 0), errors.New("invalid coordinate")
	}
	temp := make([][]rune, len(g.board))
	for i := 0; i < len(g.board); i++ {
		temp[i] = []rune(g.board[i])
	}
	markers := map[rune]bool{'B': false, 'W': false, ' ': false, 'X': false}

	points := g.FloodFill(&temp, x, y, &markers)
	if markers['B'] && !markers['W'] {
		return "BLACK", points, nil
	} else if markers['W'] && !markers['B'] {
		return "WHITE", points, nil
	}
	return "NONE", points, nil
}

// Fill a region of the board and return the positions updated.
// @param board: Pointer to 2d rune array (mimics the board slice)
// @param x: x-coordinate of point to start at
// @param y: y-coordinate of point to start at
// @param markers: This holds the characters we bump into while populating the region. If you get no or both B and W it is none.
// @returns: slice of two element int arrays where each int array is a position populated by the flood fill.
func (g *Game) FloodFill(board *[][]rune, x, y int, markers *map[rune]bool) [][2]int {
	results := make([][2]int, 0)
	char := (*board)[y][x]
	if char == 'B' || char == 'W' || char == 'X' {
		return results
	}

	results = append(results, [2]int{x, y})
	(*board)[y][x] = 'X'
	// up
	up := [][2]int{}
	down := [][2]int{}
	left := [][2]int{}
	right := [][2]int{}

	if x-1 >= 0 {
		(*markers)[(*board)[y][x-1]] = true
		left = g.FloodFill(board, x-1, y, markers)
	}
	if x+1 < len((*board)[y]) {
		(*markers)[(*board)[y][x+1]] = true
		right = g.FloodFill(board, x+1, y, markers)
	}

	if y-1 >= 0 {
		(*markers)[(*board)[y-1][x]] = true

		up = g.FloodFill(board, x, y-1, markers)
	}
	if y+1 < len((*board)) {
		(*markers)[(*board)[y+1][x]] = true
		down = g.FloodFill(board, x, y+1, markers)
	}
	results = append(results, up...)
	results = append(results, down...)
	results = append(results, left...)
	results = append(results, right...)

	return results
}

// Similar to the Territory call but calculates all positions for all territories.
// @returns: AllTerritories struct where Black, White, and None are populated by points found by repeated
// flood fill calls on unpopulated areas over the whole board.
func (g *Game) Territories() AllTerritories {
	temp := make([][]rune, len(g.board))
	for i := 0; i < len(g.board); i++ {
		temp[i] = []rune(g.board[i])
	}

	var results AllTerritories = AllTerritories{}

	markers := map[rune]bool{'B': false, 'W': false, ' ': false, 'X': false}
	for y := 0; y < len(g.board); y++ {
		for x := 0; x < len(g.board[y]); x++ {
			if temp[y][x] == ' ' {
				markers['B'] = false
				markers['W'] = false
				points := g.FloodFill(&temp, x, y, &markers)
				if markers['B'] && !markers['W'] {
					results.Black = append(results.Black, points...)
				} else if markers['W'] && !markers['B'] {
					results.White = append(results.White, points...)
				} else {
					results.None = append(results.None, points...)
				}
			}
		}
	}
	return results
}
