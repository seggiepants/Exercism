// Run Length Encoding exercise
package runlengthencoding

import (
	"fmt"
	"strings"
)

// Encode a given input string using run length encoding
// @param input: The text to encode
// @returns: The encoded text
func RunLengthEncode(input string) string {
	var ret strings.Builder = strings.Builder{}
	var duplicateCount int = 0
	var previous rune = '?'
	for index, char := range input {
		if index == 0 {
			previous = char
			duplicateCount = 1
		} else {
			if char == previous {
				duplicateCount++
			} else {
				// Write out the old.
				if duplicateCount <= 1 {
					ret.WriteRune(previous)
				} else {
					ret.WriteString(fmt.Sprintf("%d%c", duplicateCount, previous))
				}
				previous = char
				duplicateCount = 1
			}
		}
	}
	// Write out the last.
	if len(input) > 0 {
		if duplicateCount <= 1 {
			ret.WriteRune(previous)
		} else {
			ret.WriteString(fmt.Sprintf("%d%c", duplicateCount, previous))
		}
	}

	return ret.String()
}

// Decode a given input string using run length encoding
// @param input: The text to decode
// @returns: The decoded text
func RunLengthDecode(input string) string {
	var duplicateCount int = 0
	var ret strings.Builder = strings.Builder{}
	for _, char := range input {
		if char >= '0' && char <= '9' {
			duplicateCount *= 10
			duplicateCount += int(char - '0')
		} else if (char >= 'a' && char <= 'z') || (char >= 'A' && char <= 'Z' || char == ' ') {
			if duplicateCount > 0 {
				ret.WriteString(strings.Repeat(string(char), duplicateCount))
				duplicateCount = 0
			} else {
				ret.WriteRune(char)
			}
		}
	}

	return ret.String()
}
