// Reproduce the for want of a nail proverb with the given set of things to be lost
package proverb

import "fmt"

// Reproduce the for want of a nail proverb with the given items.
// @param rhyme: Slice of string with the items to be lost (in-order)
// @returns: The computed proverb as a slice of string with one line per entry.
// Empty if no items and just the final line if only one item
func Proverb(rhyme []string) []string {
	var ret []string

	for i := 0; i < len(rhyme)-1; i++ {
		ret = append(ret, fmt.Sprintf("For want of a %s the %s was lost.", rhyme[i], rhyme[i+1]))
	}

	if len(rhyme) >= 1 {
		ret = append(ret, fmt.Sprintf("And all for the want of a %s.", rhyme[0]))
	}

	return ret
}
