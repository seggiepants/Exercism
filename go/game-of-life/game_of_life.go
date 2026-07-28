// Game of Life exercise
package gameoflife

// Calculate the next state of the game board in the game of life.
// @param matrix: The current state of the game as a nxm slice of integer slices where
// 1 = alive and 0 = dead.
// @returns: new nxm slice of integer slices filled with the calculated state of the next
// step of the simulation.
func Tick(matrix [][]int) [][]int {
	var rows = len(matrix)
	var ret = make([][]int, rows)
	for j := range rows {
		ret[j] = make([]int, len(matrix[j]))
		for i := 0; i < len(matrix[j]); i++ {
			ret[j][i] = Next(matrix, i, j)
		}
	}

	return ret
}

// Calcuate the next state of a cell in the game of life.
// @param matrix: The current game of life board
// @param x: x coordinate of the position to evaluate
// @param y: y coordinate of the position to evaluate
// @returns: Integer 1 if alive or 0 if dead on the next iteration.
func Next(matrix [][]int, x int, y int) int {
	var countAlive int = 0
	var isAlive = matrix[y][x]
	for j := y - 1; j <= y+1; j++ {
		if j < 0 || j >= len(matrix) {
			continue
		}
		for i := x - 1; i <= x+1; i++ {
			if i < 0 || i >= len(matrix[j]) {
				continue
			}
			if i == x && j == y {
				continue
			}
			countAlive += matrix[j][i]
		}
	}
	if isAlive == 1 && (countAlive == 2 || countAlive == 3) {
		return 1
	}
	if isAlive == 0 && countAlive == 3 {
		return 1
	}
	return 0
}
