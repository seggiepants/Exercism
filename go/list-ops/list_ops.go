package listops

// IntList is an abstraction of a list of integers which we can define methods on
type IntList []int

// Reduce the values in a IntList left to right
// @param fn: The function used to reduce two values to one (current value is first parameter).
// @param initial: The starting value
// @returns: The final reduced value
func (s IntList) Foldl(fn func(int, int) int, initial int) int {
	result := initial
	for _, value := range s {
		result = fn(result, value)
	}
	return result
}

// Reduce the values in a IntList right to left
// @param fn: The function used to reduce two values to one (current value is second parameter).
// @param initial: The starting value
// @returns: The final reduced value
func (s IntList) Foldr(fn func(int, int) int, initial int) int {
	result := initial
	index := len(s) - 1
	for index >= 0 {
		result = fn(s[index], result)
		index--
	}
	return result
}

// Return a new IntList with just the values in the original where fn(value) == True
// @param fn: The function used to decide if a value belongs in the destination IntList.
// @returns: A new IntList with just the original values that pass the test given by fn
func (s IntList) Filter(fn func(int) bool) IntList {
	var result IntList
	for _, value := range s {
		if fn(value) {
			result = append(result, value)
		}
	}
	return result
}

// Return the length of this IntList
// @returns: The count of items in the IntList
func (s IntList) Length() int {
	return len(s)
}

// Return a new IntList that contains the result of running a function over each item in the original
// @param fn: The function to run over each element
// @returns: New IntList composed of results of running fn over each item in the original
func (s IntList) Map(fn func(int) int) IntList {
	result := make(IntList, len(s))
	for index, value := range s {
		result[index] = fn(value)
	}
	return result
}

// Return a new IntList that is a copy of the original but the items in reverse order last->first, first->last
// @returns: New IntList with items in opposite order.
func (s IntList) Reverse() IntList {
	result := make(IntList, len(s))
	index := len(s) - 1
	insertion := 0
	for index >= 0 {
		result[insertion] = s[index]
		insertion++
		index--
	}
	return result
}

// Return a new copy of this IntList with elements from another IntList appended
// @param lst: The list to append to this list.
// @returns a new IntList that is the values of this IntList followed by the falues in lst.
func (s IntList) Append(lst IntList) IntList {
	result := s.Copy()
	for _, value := range lst {
		result = append(result, value)
	}
	return result
}

// Return a new copy of this IntList with each of the sub lists appended
// @param lists: A slice of IntList to append to this IntList
// @returns: A new IntList with this IntList's values followed by the value in each IntList in list
func (s IntList) Concat(lists []IntList) IntList {
	result := s.Copy()
	for _, current := range lists {
		result = result.Append(current)
	}
	return result
}

// Create a copy of this IntList
// @returns: A copy of the this IntList
func (s IntList) Copy() IntList {
	result := make(IntList, len(s))
	copy(result, s)
	return result
}
