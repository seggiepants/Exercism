// Check if a string is an isogram (has no repeated letters)
package isogram

import "strings"

// Check if a string is an isogram (has no repeated letters)
// 'A', and 'a' count as the same letter
// @param word: The string to check
// @returns: Boolean, true if no repeated letters found.
func IsIsogram(word string) bool {
	letterMap := make(map[string]int)

	for _, rune := range word {
		char := strings.ToLower(string(rune))
		_, ok := letterMap[char]
		if ok {
			if ("a" <= char && char <= "z") || ("A" <= char && char <= "Z") {
				return false
			}
		} else {
			letterMap[char] = 1
		}
	}

	return true
}
