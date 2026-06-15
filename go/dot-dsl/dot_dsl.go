// DOT domain specific language exercise.
package dotdsl

import (
	"errors"
	"fmt"
	"regexp"
	"strconv"
	"strings"
	"unicode/utf8"
)

const whiteSpace = " \n\r\n"                // Skip these characters when passing over whitespace.
const stopChars = whiteSpace + "[]-=\";#/?" // If you find one of these characters stop parsing a key/value.

// Properties holds the properties of a node or edge.
// The values can be int, bool or string.
type Properties map[string]any

// Graph stores the parts of a dot graph.
// All entities are stored as a Properties map (`nil` Properties when none set)
// attrs is the Properties for the entire Graph, vs a specific node or edge.
type Graph struct {
	nodes map[string]Properties
	edges map[string]Properties
	attrs Properties
}

// I have a regex to parse and attribute string
var regexAttrib *regexp.Regexp

// Parse creates a Graph from a text blob.
// @param data: The text to parse
// @returns: Pointer to filled in graph object, and a nil/error if success/failed.
func Parse(data string) (*Graph, error) {
	var err error
	regexAttrib, err = regexp.Compile(`^\[(?<key>\w+)\s*=\s*\"?(?<value>[\w|\d| |\!]+)\"?\]`)
	if err != nil {
		panic("Unable to compile regular expression (attribute).")
	}
	var graph Graph = Graph{
		attrs: make(Properties),
		nodes: make(map[string]Properties),
		edges: make(map[string]Properties),
	}

	err = ParseGraph(data, 0, &graph)
	if err != nil {
		return nil, err
	}
	return &graph, nil
}

// Parse a graph object
// @param data: The text to parse.
// @param index: location to search from
// @param graph: the graph object to populate.
// @returns: error if there was a problem or nil on success.
func ParseGraph(data string, index int, graph *Graph) error {
	i, ok := ExpectToken("graph", data, 0)
	if !ok {
		return errors.New("expected token not found: graph")
	}

	i, ok = ExpectToken("{", data, i)
	if !ok {
		return errors.New("expected token not found: {")
	}

	// Check for the other types (node, edge, attribute, comment)
	previousNode := ""
	currentNode := ""
	var edges []string = []string{}
	for anyOK := true; anyOK; {
		anyOK = false
		i, ok = ParseComment(data, i)
		anyOK = ok || anyOK
		i, currentNode, ok = ParseNode(data, i, &graph.nodes)
		anyOK = ok || anyOK
		if ok {
			edges = []string{}
			for ok {
				i, ok = ExpectToken("--", data, i)
				if ok {
					previousNode = currentNode
					i, currentNode, ok = ParseNode(data, i, &graph.nodes)
					if ok {
						edgeName := fmt.Sprintf("{%s %s}", currentNode, previousNode)
						_, nameOK := graph.edges[edgeName]
						if !nameOK {
							edgeName = fmt.Sprintf("{%s %s}", previousNode, currentNode)
						}
						edges = append(edges, edgeName)
						_, nameOK = graph.edges[edgeName]
						if !nameOK {
							graph.edges[edgeName] = nil
						}
					} else {
						// -- with no edge pair
						return errors.New("expected edge pair - not found")
					}
				}
			}

			var props Properties = Properties{}
			i, ok = ParseAttribute(data, i, &props)
			if ok && len(props) > 0 {
				if len(edges) > 0 {
					for _, edgeName := range edges {
						graph.edges[edgeName] = props
					}
				} else {
					graph.nodes[currentNode] = props
				}
			}

			i, ok = ExpectToken(";", data, i)
			anyOK = ok || anyOK
			if !ok {
				return errors.New("expected token not found: ;")
			}
		}
		i, ok = ParseAttribute(data, i, &graph.attrs)
		anyOK = ok || anyOK
		if ok {
			i, ok = ExpectToken(";", data, i)
			anyOK = ok || anyOK
			if !ok {
				return errors.New("expected token not found: ;")
			}
		}
	}

	i, ok = ExpectToken("}", data, i)
	if !ok {
		return errors.New("expected token not found: }")
	}

	return nil
}

// Skip over whitespace in the input (spaces, tabs, carriage return, and line feed)
// @param data: The text to parse.
// @param index: location to search from
// @returns: int - index to continue from (same as passed in if no whitespace found).
func SkipWhitespace(data string, index int) int {
	for i := index; index < len(data); {
		rune, size := utf8.DecodeRuneInString(data[i:])
		char := string(rune)
		if strings.Contains(whiteSpace, char) {
			i += size
		} else {
			index = i
			break
		}
	}
	return index
}

// We are expecting a token, look for it and report success if found.
// @param value: The expected token string
// @param data: The text to parse.
// @param index: location to search from
// @returns: int - index to continue from, and bool true if the expected token was found.
func ExpectToken(value string, data string, index int) (int, bool) {
	i := SkipWhitespace(data, index)
	var word strings.Builder
	for i < len(data) {
		rune, size := utf8.DecodeRuneInString(data[i:])
		char := string(rune)
		if strings.Contains(whiteSpace, char) {
			break
		}

		i += size
		word.WriteString(char)
	}
	ok := word.Len() > 0
	if !ok {
		return index, false
	}
	if strings.ToLower(word.String()) != value {
		return index, false
	}
	return i, true
}

// Remove leading and trailing double quotes from a string
// @param data: Data to remove quotes from is present.
// @returns: The input string without leading and trailing double quotes if it had them otherwise just a copy of the input.
func RemoveQuotes(data string) string {
	output := strings.Trim(data, whiteSpace)
	if output[0] == '"' && output[len(output)-1] == '"' {
		return output[1 : len(output)-1]
	}
	return output
}

// Parse and toss a comment (not trailing ; for this type).
// @param data: The text to parse.
// @param index: location to search from
// @returns: int - index to continue from, and bool true if node found.
func ParseComment(data string, index int) (int, bool) {
	i := SkipWhitespace(data, index)
	var ok bool
	i, ok = ExpectToken("//", data, i)

	if !ok {
		i, ok = ExpectToken("#", data, i)
	}

	if !ok {
		// No comment found
		return index, false
	}

	for i < len(data) {
		rune, size := utf8.DecodeRuneInString(data[i:])
		char := string(rune)
		if char == "\n" {
			ok = true
			break

		}
		i += size
	}

	if !ok {
		// reached end of file probably
		return index, false
	}

	return i, true
}

// Parse an attribute object (other code will look for the statement ending ;).
// @param data: The text to parse.
// @param index: location to search from
// @param attr: attributes map to add to if found.
// @returns: int - index to continue from, and bool true if node found.
func ParseAttribute(data string, index int, attr *Properties) (int, bool) {

	i := SkipWhitespace(data, index)
	loc := regexAttrib.FindStringIndex(data[i:])
	if loc == nil {
		return index, false
	}
	expression := data[i+loc[0]+1 : i+loc[1]-1]
	i += loc[1]
	parts := strings.Split(expression, "=")
	if len(parts) != 2 {
		return index, false
	}

	key := RemoveQuotes(parts[0])
	value := RemoveQuotes(parts[1])
	intValue, err := strconv.ParseInt(value, 10, 64)
	if err == nil {
		(*attr)[key] = int(intValue)
	} else {
		boolValue, err := strconv.ParseBool(value)
		if err == nil {
			(*attr)[key] = boolValue
		} else {
			(*attr)[key] = value
		}
	}

	return i, true
}

// Parse a node object (do not get associate attributes in case this is part of an edge).
// @param data: The text to parse.
// @param index: location to search from
// @param nodes: nodes map to add to if found.
// @returns: int - index to continue from, string - node name if found, "" if not found, and bool true if node found.
func ParseNode(data string, index int, nodes *map[string]Properties) (int, string, bool) {
	name := ""
	i := SkipWhitespace(data, index)
	start := i
	for i < len(data) {
		rune, size := utf8.DecodeRuneInString(data[i:])
		char := string(rune)
		if strings.Contains(stopChars, char) {
			name = data[start:i]
			break
		}
		i += size
	}

	if len(name) == 0 {
		return index, "", false
	}

	_, ok := (*nodes)[name]
	if !ok {
		(*nodes)[name] = nil
	}
	return i, name, true
}
