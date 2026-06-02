// ETL - extract transform load sample exercise
package etl

import "strings"

// Transform data stored as point value and a set of uppercase characters
// to data stored as character now lowercase to a point value.
// @param in: A map with the key as point value and a slice of strings as characters with that point value
// @returns: A map with the lower case character as key and the point value as the value
func Transform(in map[int][]string) map[string]int {
	out := map[string]int{}

	for pointValue, runes := range in {
		for _, rune := range runes {
			out[strings.ToLower(rune)] = pointValue
		}
	}

	return out
}
