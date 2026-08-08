// Rail Fence Cipher Exercise.
package railfencecipher

// Encode a message using the rail-fence cipher.
// @param message: The value to encode.
// @param rails: The number of rails to use.
// @returns: The encoded string.
func Encode(message string, rails int) string {
	parts := make([][]rune, rails)
	index := 0
	dir := 1
	for _, char := range message {
		parts[index] = append(parts[index], char)
		index += dir
		if index == 0 {
			dir = 1
		}
		if index == rails-1 {
			dir = -1
		}
	}

	result := ""
	for _, row := range parts {
		result += string(row)
	}
	return result
}

// Decode a message that was encoded using the rail-fence cipher
// @param message: The message to decode.
// @param rails: The number of rails originally used.
// @returns: The decoded string.
func Decode(message string, rails int) string {

	// Want the length of each rail so pretend to encode.
	parts := make([][]rune, rails)
	index := 0
	dir := 1
	for _, char := range message {
		parts[index] = append(parts[index], char)
		index += dir
		if index == 0 {
			dir = 1
		}
		if index == rails-1 {
			dir = -1
		}
	}

	// now overwrite it with correct data at correct lengths.
	rail := 0
	index = 0
	for _, char := range message {
		if index >= len(parts[rail]) {
			rail++
			index = 0
		}
		parts[rail][index] = char
		index++
	}

	// Read it back in order.
	result := make([]rune, len(message))
	railIndex := make([]int, rails)
	for i := 0; i < rails; i++ {
		railIndex[i] = 0
	}
	rail = 0
	dir = 1
	for i := 0; i < len(message); i++ {
		result[i] = parts[rail][railIndex[rail]]
		railIndex[rail]++
		rail += dir
		if rail == 0 {
			dir = 1
		}
		if rail == rails-1 {
			dir = -1
		}
	}
	// Package the result as a string and return it.
	return string(result)
}
