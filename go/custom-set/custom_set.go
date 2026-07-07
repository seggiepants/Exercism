// Custom Set Exercise.
package customset

import (
	"fmt"
	"sort"
	"strings"
)

// Implement Set as a collection of unique string values.
//
// For Set.String, use '{' and '}', output elements as double-quoted strings
// safely escaped with Go syntax, and use a comma and a single space between
// elements. For example, a set with 2 elements, "a" and "b", should be formatted as {"a", "b"}.
// Format the empty set as {}.

// Don't think I escaped the strings right.

// Define the Set type here.
// I put it in a struct so the data field is by reference
// which allows us to change it (when sorting or adding)
type Set struct {
	data []string
}

// Required for sorting. Return the length of the set.
// @returns: Number of elements in the set.
func (s Set) Len() int {
	return len(s.data)
}

// Required for sorting. Swap two elements in set by index.
// @param i: First index
// @param j: Second index
func (s Set) Swap(i, j int) {
	s.data[i], s.data[j] = s.data[j], s.data[i]
}

// Required for sorting. Return if value at index i is less than the value at index j
// @param i: First index
// @param j: Second index
// @returns: True if data at i < data at j.
func (s Set) Less(i, j int) bool {
	return s.data[i] < s.data[j]
}

// Create a new empty set
// @returns: The new set.
func New() Set {
	return Set{data: []string{}}
}

// Create a new set with the elements of the given slice.
// @param l: Slice of string to populate the set with.
// @returns: New set object populated with the elements in l.
func NewFromSlice(l []string) Set {
	ret := Set{}
	for i := 0; i < len(l); i++ {
		ret.Add(l[i])
	}
	return ret
}

// Convert a set into a string representation.
// @returns: String representation of the set.
func (s Set) String() string {
	ret := strings.Builder{}
	ret.WriteString("{")
	for i := 0; i < s.Len(); i++ {
		if i > 0 {
			ret.WriteString(", ")
		}
		ret.WriteString(fmt.Sprintf("\"%s\"", s.data[i]))
	}
	ret.WriteString("}")
	return ret.String()
}

// Check to see if a set is empty
// @returns: True if empty.
func (s Set) IsEmpty() bool {
	return s.Len() == 0
}

// Check to see if the set has a specific element.
// @param elem: The value to search for.
// @returns: True if the element was found in the set.
func (s Set) Has(elem string) bool {
	if s.Len() == 0 {
		return false
	}
	return s.BinSearch(elem, 0, s.Len()-1) != -1
}

// Binary Search. We keep the elements in the set sorted for fast lookup
// @param elem: The string to search for.
// @param begin: Beginning of range to search in.
// @param end: End of range to search in.
// @returns: -1 if not found otherwise the current index of the element in the set.
func (s Set) BinSearch(elem string, begin, end int) int {
	// degenerate
	if begin > end {
		return -1
	}

	// One element
	if begin == end {
		if s.data[begin] == elem {
			return begin
		} else {
			return -1
		}
	}
	// has a range.
	middle := begin + int((end-begin)/2)
	if s.data[middle] == elem {
		return middle
	}
	if s.data[middle] > elem {
		return s.BinSearch(elem, begin, middle-1)
	}

	// Not equal or greater, must be less.
	return s.BinSearch(elem, middle+1, end)
}

// Add an element to the set.
// @param elem: text to add to the set (if not already present).
func (s *Set) Add(elem string) {
	if !s.Has(elem) {
		s.data = append(s.data, elem)
		sort.Slice(s.data, s.Less)
	}
}

// Check to see if s1 is a subset of s2
// @param s1: The first set
// @param s2: The second set
// @returns: True if all elements in s1 are also in s2.
func Subset(s1, s2 Set) bool {
	if s1.Len() == 0 {
		return true
	}
	if s2.Len() == 0 && s1.Len() > 0 {
		return false
	}
	return Intersection(s1, s2).Len() == s1.Len()
}

// Check if two sets are disjoint
// @param s1: The first set
// @param s2: The second set
// @returns: True if s1 and s2 have no elements in common.
func Disjoint(s1, s2 Set) bool {
	if s1.Len() == 0 || s2.Len() == 0 {
		return true
	}
	return Intersection(s1, s2).Len() == 0
}

// Check if two sets are equal
// @param s1: The first set
// @param s2: The second set
// @returns: True if s1 is the same length as s2 and has the same elements
func Equal(s1, s2 Set) bool {
	if s1.Len() != s2.Len() {
		return false
	}
	return Intersection(s1, s2).Len() == s1.Len()
}

// Return the intersection of two sets
// @param s1: The first set
// @param s2: The second set
// @returns: A new set with the elements in both s1 and s2.
func Intersection(s1, s2 Set) Set {
	var ret Set = Set{}
	for i := 0; i < s1.Len(); i++ {
		elem := s1.data[i]
		if s2.Has(elem) {
			ret.Add(elem)
		}
	}
	return ret
}

// Return the Difference of two sets
// @param s1: The first set
// @param s2: The second set
// @returns: A set with everything in s1, that is not in s2.
func Difference(s1, s2 Set) Set {
	var ret Set = Set{}
	for i := 0; i < s1.Len(); i++ {
		elem := s1.data[i]
		if !s2.Has(elem) {
			ret.Add(elem)
		}
	}
	return ret
}

// Return the union of two sets.
// @param s1: The first set
// @param s2: The second set
// @returns: A new set with the elements of both sets.
func Union(s1, s2 Set) Set {
	var ret Set = Set{}
	for i := 0; i < s1.Len(); i++ {
		ret.Add(s1.data[i])
	}
	for i := 0; i < s2.Len(); i++ {
		ret.Add(s2.data[i])
	}
	return ret
}
