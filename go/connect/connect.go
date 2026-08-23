package connect

// Can move left, right, upper-left, upper-right, lower-left, or lower-right
// (this maps counter-intuitively when packed to a rectangle )
//
//	O - connect from top to bottom.
//	X - connect from left to right.
var moves [][2]int = [][2]int{
	{0, -1}, {1, -1},
	{-1, 0}, {1, 0},
	{-1, 1}, {0, 1},
}

// Clear out the visited data resetting all spots to false (not visited)
// @param visited: 2D slice of booleans so we only try each place on the board once.
func ClearVisited(visited [][]bool) {
	for j := 0; j < len(visited); j++ {
		for i := 0; i < len(visited[j]); i++ {
			visited[j][i] = false
		}
	}
}

// Return the possible moves for a position on the board for the given player.
// @param board: slice of string with the board information
// @param width: board width
// @param height: board height
// @param x: x-coordinate of position to search for next move from
// @param y: y-coordinate of position to search for next move from
// @param player: Which player we are looking for moves for.
func GetMoves(board []string, width, height, x, y int, player byte) [][2]int {
	result := make([][2]int, 0)
	for _, move := range moves {
		if IsValidPoint(width, height, x+move[0], y+move[1]) && board[y+move[1]][x+move[0]] == player {
			result = append(result, [2]int{x + move[0], y + move[1]})
		}
	}
	return result
}

// Check if a given point on the board is valid.
// @param width: width of the board
// @param height: height of the board
// @param x: x-coordinate of position to test
// @param y: y-coordinate of position to test
// @returns: True if the position is ok
func IsValidPoint(width, height, x, y int) bool {
	if x < 0 || x >= width || y < 0 || y >= height {
		return false
	}
	return true
}

// Figure out if X has made it from left to right or O from top to bottom on the board
// @param lines: The board represented as a slice of string.
// @returns: "X" = X Wins, "O" = O Wins, "" = No winner.
// @raises: None seems like I should have a test that requires this.
func ResultOf(lines []string) (string, error) {
	height := len(lines)
	width := 0
	for _, row := range lines {
		width = max(width, len(row))
	}

	// Search O - Top to bottom.
	// Get any O on the top row
	var candidates [][2]int = make([][2]int, 0)
	for i, char := range lines[0] {
		if char == 'O' {
			candidates = append(candidates, [2]int{i, 0})
		}
	}
	var visited = make([][]bool, height)
	for j := 0; j < height; j++ {
		visited[j] = make([]bool, width)
		for i := 0; i < width; i++ {
			visited[j][i] = false
		}
	}

	if len(candidates) > 0 {
		ClearVisited(visited)
		for _, candidate := range candidates {
			next := make([][2]int, 0)
			next = append(next, candidate)
			for len(next) > 0 {
				current := next[len(next)-1]
				next = next[:len(next)-1] // Pop
				visited[current[1]][current[0]] = true

				// Check if we won
				if current[1] >= height-1 {
					// White made it to bottom side.
					return "O", nil
				}

				for _, item := range GetMoves(lines, width, height, current[0], current[1], 'O') {
					if visited[item[1]][item[0]] == false {
						next = append(next, item)
					}
				}
			}
		}
	}

	// Search x, left to right.
	candidates = candidates[:0]
	for i, row := range lines {
		if row[0] == 'X' {
			candidates = append(candidates, [2]int{0, i})
		}
	}

	if len(candidates) > 0 {
		ClearVisited(visited)
		for _, candidate := range candidates {
			next := make([][2]int, 0)
			next = append(next, candidate)

			for len(next) > 0 {
				current := next[len(next)-1]
				next = next[:len(next)-1] // Pop
				visited[current[1]][current[0]] = true

				// Check if we won
				if current[0] >= width-1 {
					// X made it to right hand side.
					return "X", nil
				}

				for _, item := range GetMoves(lines, width, height, current[0], current[1], 'X') {
					if visited[item[1]][item[0]] == false {
						next = append(next, item)
					}
				}
			}
		}
	}
	return "", nil
}
