// Matching Brackets example program
package matchingbrackets

import "slices"

// Test if a given input string has matching brackets (), [], and {}
// @pram input: The string to check
// @returns: True if matching, False if not.
func Bracket(input string) bool {
	var bracketStack []rune = []rune{}
	var pair map[rune]rune = map[rune]rune{
		'}': '{',
		')': '(',
		']': '[',
	}

	for _, char := range input {
		switch char {
		case '{', '(', '[':
			bracketStack = slices.Insert(bracketStack, 0, char)
		case '}', ')', ']':
			var match rune = pair[char]
			if len(bracketStack) > 0 && bracketStack[0] == match {
				bracketStack = bracketStack[1:] // pop
			} else {
				return false
			}
		}
	}

	return len(bracketStack) == 0
}
