// Decode the resistance of two resistor color bands
package resistorcolorduo

var colorBands [10]string = [10]string{"black", "brown", "red", "orange", "yellow", "green", "blue", "violet", "grey", "white"}

// Return the resistance value of a resistor encoded by up to two color bands
// @param colors: Slice of string with color values to use.
// @returns: The calculated resistance value of up to the first two bands
func Value(colors []string) int {
	var result int = 0
	for index := 0; index < min(2, len(colors)); index++ {
		result = (result * 10) + ColorToValue(colors[index])
	}
	return result
}

// Find a color in the color bands array and return its index.
// @param color: the color to search for.
// @returns: -1 if not found otherwise the index of the desired color.
func ColorToValue(color string) int {
	for index := 0; index < len(colorBands); index++ {
		if colorBands[index] == color {
			return index
		}
	}
	return -1
}
