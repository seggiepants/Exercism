// Binary Search exercise.
package binarysearch

// Binary search for the given key in the list of ints.
// @param list: Slice of integers in sorted order.
// @param key: The number to search for.
// @returns: index where the key was found or -1 if not found.
func SearchInts(list []int, key int) int {
	// degenerate case, empty slice.
	if len(list) == 0 {
		return -1
	}
	var middle int
	var value int
	start := 0
	end := len(list) - 1

	for end >= start {
		// Degenerate case one space.
		if start == end {
			if list[start] != key {
				return -1 // Not found.
			}
			return start // Found
		}

		middle = start + ((end - start) / 2)
		value = list[middle]

		if value == key {
			return middle
		}

		if value > key {
			end = middle - 1
		}

		if value < key {
			start = middle + 1
		}
	}
	return -1
}
