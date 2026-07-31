// Spiral Matrix exercise
package spiralmatrix

// Generate a spiral clockwise matrix of size x size.
// @param size: The width and height of the matrix.
// @returns: size x size matrix with a spiral going clockwise starting at 0, 0
func SpiralMatrix(size int) [][]int {
	ret := make([][]int, size)
	for i := 0; i < size; i++ {
		ret[i] = make([]int, size)
		for j := 0; j < size; j++ {
			ret[i][j] = 0
		}
	}
	dx := []int{1, 0, -1, 0}
	dy := []int{0, 1, 0, -1}
	dir := 0
	x := 0
	y := 0
	counter := 0
	for counter < size*size {
		counter++
		ret[y][x] = counter
		y += dy[dir]
		x += dx[dir]
		if x < 0 || x >= size || y < 0 || y >= size || ret[y][x] != 0 {
			y -= dy[dir] // Step back
			x -= dx[dir]
			dir = (dir + 1) % len(dx) // Turn
			y += dy[dir]              // Move in new direction
			x += dx[dir]
		}
	}

	return ret
}
