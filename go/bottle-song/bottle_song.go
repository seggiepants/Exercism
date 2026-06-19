// Recite the bottle song a wimpy recast of 100 bottles of beer on the wall except just ten
// and 'green bottles' because we can't say beer (someone think of the children).
//
// By the way WTF is going on with Title() which is deprecated and ToTitle which is the same as all caps?
// I was going to use the dreprecation's preferred library but I decided exercism may not allow non-standard libraries.
// So I title cased it and lower cased it as needed instead.
package bottlesong

import (
	"fmt"
	"strings"
)

var digits []string = []string{"No", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten"}

// Recite the ten green bottles song a Bawlderized version of 100 Bottles of beer on the wall.
// You will get an error if you try to refer to a bottle outside of 0-10
// @param startBottles: The number of bottles to start with
// @param takeDown: The number of verses to produce
// @returns: A slice of strings with the desired verses (with a empty string between verses)
func Recite(startBottles, takeDown int) []string {
	result := []string{}

	if startBottles > 10 || startBottles-takeDown < 0 {
		return []string{"ERROR: Bottles out of range only bottles 1-10 are allowed"}
	}
	for i := startBottles; i > startBottles-takeDown; i-- {
		if i < startBottles {
			result = append(result, "")
		}
		digit := digits[i]
		bottle := "bottles"
		if i == 1 {
			bottle = "bottle"
		}
		nextDigit := strings.ToLower(digits[i-1])
		result = append(result, fmt.Sprintf("%s green %s hanging on the wall,", digit, bottle))
		result = append(result, fmt.Sprintf("%s green %s hanging on the wall,", digit, bottle)) // yes repeating myself, but it matches the song better, and a loop for two items seems a bit silly.
		result = append(result, "And if one green bottle should accidentally fall,")
		if i-1 == 1 {
			bottle = "bottle"
		} else {
			bottle = "bottles"
		}
		result = append(result, fmt.Sprintf("There'll be %s green %s hanging on the wall.", nextDigit, bottle))
	}

	return result
}
