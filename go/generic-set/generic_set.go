package genericset

// Implement Set[T comparable] as a map[T]struct{}.
type Set[T comparable] map[T]struct{}

// Create a new set object.
// @returns Set[T]: Empty Set[T] struct.
func NewSet[T comparable]() Set[T] {
	return make(Set[T])
}

// Add an element to a Set.
// @param elem T: The element to add/update
func (s Set[T]) Add(elem T) {
	s[elem] = struct{}{}
}

// Check if an element is in a Set.
// @param elem T: The element to look for
// @returns bool: True if exists in the set.
func (s Set[T]) Contains(elem T) bool {
	_, ok := s[elem]
	return ok
}

// Check if two sets have the same elements.
// @param other Set[T]: The set to check if it equals this one.
// @returns bool: True if same length and items.
func (s Set[T]) Equals(other Set[T]) bool {
	if len(s) != len(other) {
		return false
	}
	for index, elem := range s {
		if !other.Contains(index) || elem != other[index] {
			return false
		}
	}
	return true
}
