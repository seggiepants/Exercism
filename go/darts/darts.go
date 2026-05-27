package darts

// Compute the score for a dart thrown in a game of darts.
// Center is assumed to be at 0, 0
// @param x: x-coordinate of where the dart lands
// @param y: y-coordinate of where the dart lands
// @returns: Score based on distance from the center point (0, 0)
func Score(x, y float64) int {
	distance_squared := (x * x) + (y * y)
	if distance_squared > 100 { // Outer ring is 10
		return 0
	}
	if distance_squared > 25 { // Middle circle is 5
		return 1
	}
	if distance_squared > 1 { // Inner circle is 1
		return 5
	}
	return 10
}
