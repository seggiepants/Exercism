// Pangram detect if an input contains all the letters of the english alphabet
package pangram

import (
	"slices"
	"strings"
)

// Detect if a given input is a Pangram, it contains all
// letters a-z at least once (case insensitive).
// @param input: the string to test
// @returns: true if a pangram otherwise false
func IsPangram(input string) bool {
	const alphabet string = "abcdefghijklmnopqrstuvwxyz"
	var present []string = []string{}

	for _, rune := range strings.ToLower(input) {
		char := string(rune)
		if strings.Contains(alphabet, char) && !slices.Contains(present, char) {
			present = append(present, char)
		}
	}
	return len(present) == len(alphabet)
}
