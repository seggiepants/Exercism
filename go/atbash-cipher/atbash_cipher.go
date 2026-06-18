// Atbash Cipher example - a substitution cipher
package atbashcipher

import "strings"

const plain string = "abcdefghijklmnopqrstuvwxyz"
const cipher string = "zyxwvutsrqponmlkjihgfedcba"
const punctuation string = "\"',.?\\@#$%^&*()+=[]{}|;:,.<>/`~ \t\r\n"

// Encrypt a string using the Atbash Cipher
// @param s: The string to encrypt
// @returns: The encrypted string
func Atbash(s string) string {
	var sections []string = []string{}
	var current strings.Builder = strings.Builder{}
	input := []rune(strings.ToLower(s))

	for _, letter := range input {
		index := strings.IndexRune(punctuation, letter)
		if index != -1 {
			continue
		}
		index = strings.IndexRune(plain, letter)
		if index >= 0 {
			current.WriteString(cipher[index : index+1])
		} else {
			current.WriteRune(letter)
		}
		if current.Len() == 5 {
			sections = append(sections, current.String())
			current.Reset()
		}
	}
	if current.Len() > 0 {
		sections = append(sections, current.String())
	}

	return strings.Join(sections, " ")
}
