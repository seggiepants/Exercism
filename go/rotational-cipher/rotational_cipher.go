package rotationalcipher

import "strings"

const alpha_size int = 26

// Perform the "Caeser" Rotational Cipher on a given plain text string and rotation value.
// @param plain: Plain text to encode.
// @param shiftKey: How much to rotate the alphabet by.
// @returns: Encoded version of the input string.
func RotationalCipher(plain string, shiftKey int) string {
	var ret strings.Builder = strings.Builder{}

	for _, char := range plain {
		switch {
		case char >= 'A' && char <= 'Z':
			ret.WriteRune(ShiftRune(char, 'A', shiftKey))
		case char >= 'a' && char <= 'z':
			ret.WriteRune(ShiftRune(char, 'a', shiftKey))
		default:
			ret.WriteRune(char)
		}
	}

	return ret.String()
}

// Rotate a single rune by shiftKey amount where the alphabet starts at minRune
// @param char: The rune to rotate.
// @param minRune: Where the alphabet starts 'a', or 'A' for example.
// @param shiftKey: How many characters to shift the alphabet by.
// @returns: rotated version of the input rune.
func ShiftRune(char rune, minRune rune, shiftKey int) rune {
	return rune((((int(char) - int(minRune)) + shiftKey) % alpha_size) + int(minRune))
}
