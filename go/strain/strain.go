// strain exercise implement Keep and Discard with generics.
package strain

// Implement the "Keep" and "Discard" function in this file.

// You will need typed parameters (aka "Generics") to solve this exercise.
// They are not part of the Exercism syllabus yet but you can learn about
// them here: https://go.dev/tour/generics/1

// Iterate over a slice of generic type returning a new slice of each
// item in the slice that returns true when passed to the given function.
// @param data: Slice of generic type T
// @param f: Function taking a value of generic type T that returns true or false.
// @returns: Slice of items from the data list that return true when passed to f.
func Keep[T any](data []T, f func(T) bool) []T {
	ret := make([]T, 0)
	for _, item := range data {
		if f(item) {
			ret = append(ret, item)
		}
	}
	return ret
}

// Iterate over a slice of generic type returning a new slice of each
// item in the slice that returns false when passed to the given function.
// @param data: Slice of generic type T
// @param f: Function taking a value of generic type T that returns true or false.
// @returns: Slice of items from the data list that return false when passed to f.
func Discard[T any](data []T, f func(T) bool) []T {
	ret := make([]T, 0)
	for _, item := range data {
		if !f(item) {
			ret = append(ret, item)
		}
	}
	return ret
}
