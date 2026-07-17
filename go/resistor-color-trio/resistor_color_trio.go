// For a trio of resistor colors calculate the amount and prefix and return the value as a human readable string.
package resistorcolortrio

import (
	"fmt"
	"math"
)

// Resistor color to band value.
var colorLookup map[string]int = map[string]int{
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

// index = how many times we add an extra three zeros which gives the proper prefix.
var prefix map[int]string = map[int]string{
	0: "",
	1: "kilo",
	2: "mega",
	3: "giga",
}

// Label describes the resistance value given the colors of a resistor.
// The label is a string with a resistance value with an unit appended
// (e.g. "33 ohms", "470 kiloohms").
// @param colors: slice of color bands (only first three will be used)
// @returns: String representation of resistance with a number then ohm rating in (/kilo/mega/giga)ohms
func Label(colors []string) string {
	amount := 0
	for i := 0; i < min(2, len(colors)); i++ {
		amount = (amount * 10) + colorLookup[colors[i]]
	}
	if len(colors) >= 3 {
		amount *= int(math.Pow(10.0, float64(colorLookup[colors[2]])))
	}
	prefixIdx := 0
	for prefixIdx < len(prefix) && amount > 1000 {
		prefixIdx++
		amount /= 1000
	}

	return fmt.Sprintf("%d %sohms", amount, prefix[prefixIdx])
}
