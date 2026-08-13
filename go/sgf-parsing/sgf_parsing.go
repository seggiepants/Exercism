// SGF Parsing exercise
package sgfparsing

import (
	"errors"
	"fmt"
	"slices"
	"strings"
)

// Node represents an SGF node with properties and child nodes.
type Node struct {
	Properties map[string][]string
	Children   []*Node
}

// Parse decodes an SGF string and returns the root node of the tree.
// @param encoded: The string to parse
// @returns: Root node of the SGF tree
// @raises: An error is raised if there is a parse error.
func Parse(encoded string) (*Node, error) {
	const cutset string = " \t\r\n"
	encoded = strings.Trim(encoded, cutset)
	if len(encoded) == 0 {
		return nil, errors.New("tree missing")
	}
	properties := make(map[string][]string, 0)
	children := make([]*Node, 0)
	charIndex, err := ParseNode(properties, &children, encoded, 0)
	if charIndex == -1 || err != nil {
		return nil, err
	}

	var n Node = Node{Properties: properties, Children: children}
	return &n, nil
}

// Parse a node of the Sgf input
// @param properties: dictionary to add properties to.
// @param children: list of children to add to (ParseProperty will do this)
// @param input: text to parse
// @param index: Where we are at in the text.
// @returns: index to continue parsing from
// @raises: error when it encounters a parsing errors.
func ParseNode(properties map[string][]string, children *[]*Node, input string, index int) (int, error) {
	var charIndex int = index

	// Expect (
	if input[charIndex] != '(' {
		return -1, errors.New("tree missing")
	}

	charIndex++

	// Expect properties
	if input[charIndex] != ';' {
		return -1, errors.New("tree with no nodes")
	}

	charIndex++
	nextIndex, err := ParseProperty(properties, children, input, charIndex)
	for err == nil && nextIndex != -1 {
		charIndex = nextIndex
		nextIndex, err = ParseProperty(properties, children, input, charIndex)
	}

	// Expect )
	if input[charIndex] != ')' {
		return -1, errors.New("properties without delimiter")
	}
	charIndex++
	return charIndex, nil
}

// Parse a node property
// @param properties: Map of properties to add to
// @param children: Slice of child nodes to fill out.
// @param input: text to parse
// @param index: where in the text to parse from
// @returns: index to continue parsing from
// @raises: error when it encounteres a problem parsing
func ParseProperty(properties map[string][]string, children *[]*Node, input string, index int) (int, error) {
	charIndex := index
	char, nextIndex := getNextChar(input, charIndex, true, false)
	name := ""
	for nextIndex >= 0 {
		name += char
		charIndex = nextIndex
		char, nextIndex = getNextChar(input, charIndex, false, false)
	}

	if charIndex >= len(input) {
		return -1, nil // Not found.
	}

	if name != strings.ToUpper(name) {
		return -1, errors.New("property must be in uppercase")
	}

	// Read Properties
	propertyList := make([]string, 0)
	for charIndex < len(input) && input[charIndex] == '[' {
		// Expect [
		if charIndex >= len(input) || input[charIndex] != '[' {
			return -1, errors.New("Parse error [ expected, not found.")
		}
		charIndex++

		valueStart := charIndex
		char, nextIndex = getNextChar(input, charIndex, true, true)
		value := ""
		for nextIndex >= 0 {
			value += char
			charIndex = nextIndex
			char, nextIndex = getNextChar(input, charIndex, false, true)
		}

		if charIndex == valueStart { // No value
			return -1, errors.New("No value found for property")
		}

		// Expect ]
		if charIndex >= len(input) || input[charIndex] != ']' {
			return -1, errors.New("] expected but not found.")
		}
		charIndex++
		propertyList = append(propertyList, value)
	}

	if len(propertyList) < 1 {
		return -1, errors.New("No properties found.")
	}

	if charIndex < len(input) && input[charIndex] == ';' {
		charIndex++
		childProperties := make(map[string][]string, 0)
		grandChildren := make([]*Node, 0)
		nextIndex, err := ParseProperty(childProperties, &grandChildren, input, charIndex)
		if nextIndex != -1 && err == nil {
			var n Node = Node{
				Properties: childProperties,
				Children:   grandChildren,
			}
			*children = append(*children, &n)
			charIndex = nextIndex
		}
	} else if charIndex < len(input) && input[charIndex] == '(' {
		var err error
		for charIndex < len(input) && input[charIndex] == '(' {
			childProperties := make(map[string][]string, 0)
			grandChildren := make([]*Node, 0)
			nextIndex, err = ParseNode(childProperties, &grandChildren, input, charIndex)
			if nextIndex != -1 && err == nil {
				var n Node = Node{
					Properties: childProperties,
					Children:   grandChildren,
				}
				*children = append(*children, &n)
				charIndex = nextIndex
			}
		}
	}
	_, isDuplicate := properties[name]

	if isDuplicate {
		return -1, fmt.Errorf("Key \"%s\" exists multiple times.", name)
	}
	properties[name] = propertyList

	return charIndex, nil
}

// Get the next character (or two) from the input string with some substitutions
// @param input: The text to read from
// @param index: The read location in the text
// @param first_char: is this the first character we are reading from?
// @param allow_square_bracket_start: do we allow a plain [ in the input?
// @returns: the character read/subtituted followed by the index to read from next
func getNextChar(input string, index int, firstChar bool, allowSquareBracketStart bool) (string, int) {
	if index >= len(input) {
		return "", -1
	}
	var char string = input[index : index+1]
	twoChars := char
	if index+1 < len(input) {
		twoChars = input[index : index+2]
	}
	if ("a" <= char && char <= "z") ||
		("A" <= char && char <= "Z") ||
		("0" <= char && char <= "9") ||
		strings.Contains(" ;=\n", char) {
		return char, index + 1
	}

	if !firstChar && strings.Contains("()", char) {
		return char, index + 1
	}

	if !firstChar && char == "[" && allowSquareBracketStart {
		return char, index + 1
	}
	if char == "\t" {
		return " ", index + 1
	}

	escapeCodes := []string{"\\[", "\\]", "\\\\", "\\\t", "\\\n", "\\t", "\\n"}
	if slices.Contains(escapeCodes, twoChars) {
		if twoChars == "\\\t" {
			return " ", index + 2
		}
		if twoChars == "\\\n" {
			return "", index + 2
		}
		if twoChars == "\\t" || twoChars == "\\n" {
			return string(twoChars[1]), index + 2
		}
		return string(twoChars[1]), index + 2
	}
	return "", -1
}
