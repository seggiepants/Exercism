// Manually Reverse a String.
package reversestring

import "unicode/utf8"

// Return a string that is a copy of the input string in reverse rune order.
// @param input: The string to reverse
// @returns: New string that is a copy of parameter input but in reverse order.
func Reverse(input string) string {
	// I am assuming using the built in function is not what was wanted.
	var buffer []rune = make([]rune, utf8.RuneCountInString(input))
	writeIndex := len(buffer) - 1

	for _, char := range input {
		buffer[writeIndex] = char
		writeIndex--
	}
	return string(buffer)
}
