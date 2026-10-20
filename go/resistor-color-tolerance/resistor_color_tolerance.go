// Exercise: Resistor Color Tolerance
package resistorcolortolerance

import (
	"fmt"
	"math"
	"strconv"
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

var tolerance map[string]string = map[string]string{
	"grey":   "±0.05%",
	"violet": "±0.1%",
	"blue":   "±0.25%",
	"green":  "±0.5%",
	"brown":  "±1%",
	"red":    "±2%",
	"gold":   "±5%",
	"silver": "±10%",
}

// Label describes the resistance value given the colors of a resistor.
// The label is a string with a resistance value with a unit and tolerance appended
// (e.g. "33 ohms ±5.00%", "470 kiloohms ±0.25%").
// @param colors: slice of color bands (only first four will be used)
// @returns: String representation of resistance with a number then ohm rating in (/kilo/mega/giga)ohms followed by the tolerance
func Label(colors []string) string {
	var amount float64 = 0.0
	colorCount := min(2, len(colors))
	if len(colors) == 5 {
		colorCount = 3
	}

	for i := 0; i < colorCount; i++ {
		amount = (amount * 10.0) + float64(colorLookup[colors[i]])
	}

	if len(colors) > colorCount {
		amount *= math.Pow(10.0, float64(colorLookup[colors[colorCount]]))
	}
	prefixIdx := 0
	for prefixIdx < len(prefix) && amount >= 1000.0 {
		prefixIdx++
		amount /= 1000.0
	}

	toleranceValue := ""
	if len(colors) > colorCount+1 {
		toleranceValue = " " + tolerance[colors[colorCount+1]]
	}

	return fmt.Sprintf("%s %sohms%s", strconv.FormatFloat(amount, 'f', -1, 64), prefix[prefixIdx], toleranceValue)
}
