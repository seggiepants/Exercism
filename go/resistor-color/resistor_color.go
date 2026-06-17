// Resistor Color exercise - expect more exercises with more bands in the future.
package resistorcolor

import (
	"maps"
	"slices"
)

var lookup map[string]int = map[string]int{
	"black":  0,
	"brown":  1,
	"red":    2,
	"orange": 3,
	"yellow": 4,
	"green":  5,
	"blue":   6,
	"violet": 7,
	"grey":   8,
	"white":  9,
}

// Colors returns the list of all colors.
// @returns: A slice of the available color keys
func Colors() []string {
	return slices.Collect(maps.Keys(lookup))
}

// ColorCode returns the resistance value of the given color.
// @param color: Which color to lookup
// @returns: 0 if not found, otherwise the corresponding color value
func ColorCode(color string) int {
	value, ok := lookup[color]
	if !ok {
		return 0
	}
	return value
}
