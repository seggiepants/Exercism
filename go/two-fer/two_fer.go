// Decide how to address a person you are sharing one of two items with.
// value will change if we know the name or not.

package twofer

import "fmt"

// Return a one-fer statement that changes depending on the name or lack thereof
// :param name: The name of the person you are sharing with or empty string if name is not known
// :returns: One for you, one for me. statement personalized with a name if present.
func ShareWith(name string) string {
	if len(name) == 0 {
		name = "you"
	}
	return fmt.Sprintf("One for %s, one for me.", name)
}
