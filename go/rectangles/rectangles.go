// Rectangles exercise - count the rectangles on a grid.
package rectangles

// Find the number of rectangles on the given diagram.
// @param diagram: slice of strings that rectangles are plotted on.
// @returns: Count of rectangles found.
func Count(diagram []string) int {
	var rows = len(diagram)
	var cols = 0
	data := make([][]rune, rows)
	for i, row := range diagram {
		data[i] = []rune(row)
		if len(row) > cols {
			cols = len(row)
		}
	}
	var countRectangles int = 0

	if rows == 0 || cols == 0 {
		return 0
	}

	for y, row := range data {
		for x, char := range row {
			if char == '+' {
				countRectangles += Probe(data, cols, rows, x, y)
			}
		}
	}

	return countRectangles
}

// Look for retangles with the top left corner at x, y
// @param data: The 2d slice of runes with the grid to find rectangles on
// @param cols: Number of columns in the data grid
// @param rows: Number of rows in the data grid
// @param x: x-coordinate of the top left corner
// @param y: y-coordinate of the top left corner
// @returns: count of rectangles found with a top left corner at x, y.
func Probe(data [][]rune, cols, rows, x, y int) int {
	// Probe left
	var x1 int = x + 1
	var y1 int = y
	var countRectangles int = 0

	for x1 < cols {
		var char rune = data[y1][x1]
		if char == ' ' {
			return countRectangles
		}
		if char == '+' {
			countRectangles += ProbeDown(data, cols, rows, x, y, x1)
		}
		if char != '-' && char != '+' {
			return countRectangles
		}
		x1++
	}
	return countRectangles
}

// Search for the bottom right corner of a rectangle.
// @param data: The 2d slice of runes with the grid to find rectangles on
// @param cols: Number of columns in the data grid
// @param rows: Number of rows in the data grid
// @param x: x-coordinate of the top left corner
// @param y: y-coordinate of the top left corner
// @param x1: x-coordinate of the top right corner
// @returns: count of rectangles found with (x,y)-(x1,?)
func ProbeDown(data [][]rune, cols, rows, x, y, x1 int) int {
	var y1 int = y + 1
	var countRectangles int = 0

	for y1 < rows {
		var char rune = data[y1][x1]
		if char == ' ' {
			return countRectangles
		}
		if char == '+' {
			countRectangles += ValidateRect(data, x, y, x1, y1)
		}
		if char != '|' && char != '+' {
			return countRectangles
		}
		y1++
	}
	return countRectangles
}

// Ensure that a pair of x, y coordinates contain a rectangle.
// @param data: The 2d slice of runes with the grid to find rectangles on
// @param x1: x-coordinate of the top left corner
// @param y1: y-coordinate of the top left corner
// @param x2: x-coordinate of the bottom right corner
// @param y2: y-coordinate of the bottom right corner
// @returns: 1 if rectangle found, and 0 if it was not.
func ValidateRect(data [][]rune, x1, y1, x2, y2 int) int {
	// Check the corners
	if data[y1][x1] != '+' || data[y1][x2] != '+' || data[y2][x1] != '+' || data[y2][x2] != '+' {
		return 0
	}

	// Check top/bottom
	for x := x1 + 1; x < x2; x++ {
		if data[y1][x] != '+' && data[y1][x] != '-' {
			return 0
		}
		if data[y2][x] != '+' && data[y2][x] != '-' {
			return 0
		}
	}

	// Check left/right
	for y := y1 + 1; y < y2; y++ {
		if data[y][x1] != '+' && data[y][x1] != '|' {
			return 0
		}
		if data[y][x2] != '+' && data[y][x2] != '|' {
			return 0
		}
	}
	return 1
}
