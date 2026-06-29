// Cipher - Implementations of the Caeser, Shift, and Vigenere ciphers.
package cipher

import "strings"

const ALPHABET_LEN = 26

// Define the shift and vigenere types here.
type shift struct {
	distance int
}

type vigenere struct {
	key string
}

// Create a new shift encoding struct suitable for the caser cipher
func NewCaesar() Cipher {
	return NewShift(3)
}

// Create a new shift encoding struct.
// @param key: The distance to encode with.
// @returns: nil on error or a initialized shift struct
func NewShift(distance int) Cipher {
	if distance == 0 || distance < -25 || distance > 25 {
		return nil
	}

	for distance < 0 {
		distance += ALPHABET_LEN
	}
	for distance >= ALPHABET_LEN {
		distance -= ALPHABET_LEN
	}
	return shift{distance: distance}
}

// Encode a string with shift encoding
// @param input: The string to encode.
// @returns: The encoded string.
func (c shift) Encode(input string) string {
	var ret strings.Builder = strings.Builder{}
	for _, ch := range strings.ToLower(input) {
		if ch >= 'a' && ch <= 'z' {
			ret.WriteRune(ShiftRune(ch, c.distance))
		}
	}
	return ret.String()
}

// Decode a string that has been shift encoded
// @param input: The string to decode.
// @returns: The decoded string.
func (c shift) Decode(input string) string {
	var ret strings.Builder = strings.Builder{}
	for _, ch := range strings.ToLower(input) {
		if ch >= 'a' && ch <= 'z' {
			ret.WriteRune(ShiftRune(ch, ALPHABET_LEN-c.distance))
		}
	}
	return ret.String()
}

// Create a new vigenere encoding struct.
// @param key: The key to encode with.
// @returns: nil on error or a initialized vigenere struct
func NewVigenere(key string) Cipher {
	var foundNotA bool = false
	if len(key) == 0 {
		return nil
	}
	for _, ch := range key {
		if ch < 'a' || ch > 'z' {
			return nil
		}
		if ch != 'a' {
			foundNotA = true
		}
	}
	if !foundNotA {
		return nil
	}
	return vigenere{key: key}
}

// Encode a string with vigenere encoding
// @param input: The string to encode.
// @returns: The encoded string.
func (v vigenere) Encode(input string) string {
	var ret strings.Builder = strings.Builder{}
	keyIndex := 0
	for _, ch := range strings.ToLower(input) {
		if ch >= 'a' && ch <= 'z' {
			distance := int(v.key[keyIndex] - 'a')
			ret.WriteRune(ShiftRune(ch, distance))
			keyIndex = (keyIndex + 1) % len(v.key)
		}
	}
	return ret.String()
}

// Decode an vigenere encrypted string.
// @param input: The string to decode
// @returns: The decoded string.
func (v vigenere) Decode(input string) string {
	var ret strings.Builder = strings.Builder{}
	keyIndex := 0
	for _, ch := range strings.ToLower(input) {
		if ch >= 'a' && ch <= 'z' {
			distance := ALPHABET_LEN - int(v.key[keyIndex]-'a')
			ret.WriteRune(ShiftRune(ch, distance))
			keyIndex = (keyIndex + 1) % len(v.key)
		}
	}
	return ret.String()
}

// Return a rune that is shifted distance places away from the given one.
// Wraps around before a or after z.
// @param ch: Rune to shift
// @param distance: How many places to shift.
// @returns: run that is distance spots away from the source.
func ShiftRune(ch rune, distance int) rune {
	if ch < 'a' || ch > 'z' {
		return ch
	}

	return 'a' + (((ch - 'a') + rune(distance)) % ALPHABET_LEN)
}
