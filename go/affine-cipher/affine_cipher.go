// Affine Cipher Exercise - a better mono-alphabetic cipher.
package affinecipher

import (
	"fmt"
	"strings"
)

// Encode text with the affine cipher
// @param text: The text to encode
// @param a: first part of encryption key
// @param b: second part of encryption key
// @returns: encoded text, or error if a and b are not co-prime.
func Encode(text string, a, b int) (string, error) {
	const GROUP_SIZE int = 5
	const ALPHABET_LEN int = 26
	var ret strings.Builder = strings.Builder{}

	if !IsCoPrime(a, ALPHABET_LEN) {
		return text, fmt.Errorf("%d and %d are not co-prime.", a, b)
	}

	var groupIndex int = -1
	var textLen int = len(text)
	var writeAChar bool // only write out a space if we are writing a character.
	for i, char := range strings.ToLower(text) {
		writeAChar = false
		if char >= 'a' && char <= 'z' {
			var index int = (int)(char - 'a')
			char = 'a' + (rune)((a*index+b)%ALPHABET_LEN)
			groupIndex++
			writeAChar = true
		} else if char >= '0' && char <= '9' {
			groupIndex++
			writeAChar = true
		}
		if writeAChar {
			if i > 0 && groupIndex >= GROUP_SIZE && i+1 < textLen {
				groupIndex = 0
				ret.WriteString(" ")
			}
			ret.WriteRune(char)
		}
	}
	return ret.String(), nil
}

// Decode text encoded with the affine cipher
// @param text: The text to decode
// @param a: first part of encryption key
// @param b: second part of encryption key
// @returns: decoded text, or error if a and b are not co-prime.
func Decode(text string, a, b int) (string, error) {
	const ALPHABET_LEN int = 26
	var ret strings.Builder = strings.Builder{}

	if !IsCoPrime(a, ALPHABET_LEN) {
		return text, fmt.Errorf("%d and %d are not co-prime.", a, b)
	}

	for _, char := range strings.ToLower(text) {
		if char >= 'a' && char <= 'z' {
			index := (int)(char - 'a')
			offset := (MMI(a, ALPHABET_LEN) * (index - b)) % ALPHABET_LEN
			for offset < 0 {
				offset += ALPHABET_LEN
			}
			for offset >= ALPHABET_LEN {
				offset -= ALPHABET_LEN
			}
			char = 'a' + (rune)(offset)
			ret.WriteRune(char)
		} else if char >= '0' && char <= '9' {
			ret.WriteRune(char)
		}
	}
	return ret.String(), nil
}

// Check if the two numbers have a common factor larger than one.
// @param a: First number to check
// @param b: Second number to check
// @returns: True if no common factor > 1.
func IsCoPrime(a, b int) bool {

	for i := 2; i <= min(a, b); i++ {
		if a%i == 0 && b%i == 0 {
			return false
		}
	}

	return true
}

// Find the Modular Multiplicative Inverse
// @param a: first parameter
// @param b: second parameter
// @returns: -1 if not found otherwise the
// Modular Multiplicative Inverse of a and b.
func MMI(a, b int) int {
	for i := 1; i <= b; i++ {
		// Check if (a * x) % b == 1
		if ((a%b)*(i%b))%b == 1 {
			return i
		}
	}
	return -1
}
