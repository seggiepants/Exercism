// Kindergarten garden exercise.
package kindergartengarden

import (
	"errors"
	"fmt"
	"slices"
	"strings"
)

// Define the Garden type here.
type Garden struct {
	rows     []string
	children []string
}

// The diagram argument starts each row with a '\n'.  This allows Go's
// raw string literals to present diagrams in source code nicely as two
// rows flush left, for example,
//
//     diagram := `
//     VVCCGG
//     VVCCGG`
//
// If the children argument is empty, use the list of children defined in the instructions.
// If it is not empty, use the given value.

const DEFAULT_CHILDREN string = "Alice,Bob,Charlie,David,Eve,Fred,Ginny,Harriet,Ileana,Joseph,Kincaid,Larry"

// Return a new garden instance with the given diagram and slice of children.
// if the slice of children is empty default children will be used.
// @param diagram: \n delimited string of rows (should start with a \n) Two rows and two characters per child.
// @param children: A slice of the names of children. List will be sorted A-Z.
// @returns: A new garden instance if no errors.
// @raises: Error if you have rows that don't match, or don't match number of children,
// or when there are duplicate child names, or when there are incorrect plant codes.
func NewGarden(diagram string, children []string) (*Garden, error) {
	if len(children) == 0 {
		children = strings.Split(DEFAULT_CHILDREN, ",")
	}
	for pos, child := range children {
		if slices.Index(children[pos+1:], child) >= 0 {
			return nil, fmt.Errorf("Child: %s is a duplicate.", child)
		}
	}
	rows := strings.Split(diagram, "\n")
	if len(rows) > 0 && len(rows[0]) == 0 {
		rows = rows[1:]
	} else {
		return nil, errors.New("Wrong diagram format, missing leading blank line.")
	}
	if len(rows) != 2 {
		return nil, errors.New("Must have two rows.")
	}
	if len(rows[0]) != len(rows[1]) {
		return nil, errors.New("Row lengths must be the same.")
	}
	if len(children)*2 != len(rows[0]) {
		return nil, errors.New("Children must match row length.")
	}
	for _, row := range rows {
		for _, rune := range row {
			if !(rune == 'R' || rune == 'G' || rune == 'C' || rune == 'V') {
				return nil, errors.New("Invalid plant code.")
			}
		}
	}
	sortedChildren := slices.Clone(children)
	slices.Sort(sortedChildren)

	return &Garden{rows: rows, children: sortedChildren}, nil
}

// Return the plants for a given child.
// @param child: The child to retrieve plants for.
// @returns: Array of string with the plants they have and a
// boolean saying wether it was a successful lookup (could fail if child not found).
func (g *Garden) Plants(child string) ([]string, bool) {
	childIndex := slices.Index(g.children, child)
	if childIndex < 0 {
		return nil, false
	}
	plants := []string{}
	for i := 0; i < len(g.rows); i++ {
		plants = append(plants, g.StringToPlants(g.rows[i][childIndex*2:(childIndex+1)*2])...)
	}
	return plants, true
}

// Change a string of plant codes to a slice of plant names.
// only good for G, C, R, V. Will write an error to list if there is an unknown plant code.
// @param plants: The string to parse
// @returns: Slice of string with plant names (combinartion of: grass, clover, radishes, violets).
func (g *Garden) StringToPlants(plants string) []string {
	ret := []string{}
	for _, rune := range plants {
		switch rune {
		case 'G':
			ret = append(ret, "grass")
		case 'C':
			ret = append(ret, "clover")
		case 'R':
			ret = append(ret, "radishes")
		case 'V':
			ret = append(ret, "violets")
		default:
			ret = append(ret, "Unknown: "+string(rune))
		}
	}
	return ret
}
