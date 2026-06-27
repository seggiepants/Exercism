package series

// Return a slice of all substrings of length n from string s
// @param s: String to get substrings from
// @param n: Length of the slice.
// @returns: slice of strings with the substrings.
func All(n int, s string) []string {
	if n > len(s) || n <= 0 {
		return []string(nil)
	}
	ret := make([]string, len(s)-n+1)

	for i := 0; i < len(s)-n+1; i++ {
		ret[i] = s[i : i+n]
	}

	return ret
}

// Return the first n characters of a string.
// @param n: Number of characters to return
// @param s: The string to return the first n characters from.
// @returns: The first n characters of the given string or the whole string if n is too large/small.
func UnsafeFirst(n int, s string) string {
	if n > len(s) || n <= 0 {
		return s
	}
	return s[0:n]
}

// Bonus function to test that returns the desired value and a boolean to flag success/failure.
// Return the first n characters of a string.
// @param n: Number of characters to return
// @param s: The string to return the first n characters from.
// @returns: first = The sliced string or "" on error, ok = The success flag.
func First(n int, s string) (first string, ok bool) {
	if n > len(s) || n <= 0 {
		return "", false
	}
	return s[0:n], true
}
