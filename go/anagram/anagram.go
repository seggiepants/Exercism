package anagram

import (
	"sort"
	"strings"
)

// Detect if a subject string has an anagram in a slice of candidates
// case-insensitive comparison.
// @param subject: The string to look for anagrams of.
// @param candidates: A slice of possible anagrams.
// @returns: A slice of strings that match as anagrams (in processing order)
func Detect(subject string, candidates []string) []string {
	target := strings.ToLower(subject)
	sortedTarget := SortString(target)
	results := []string{}

	for _, candidate := range candidates {
		compare := strings.ToLower((candidate))
		if compare != target {
			if sortedTarget == SortString(compare) {
				results = append(results, candidate)
			}
		}
	}
	return results
}

// Sort a string putting characters in ascending order.
// Yes I did find this code when searching how to sort a string.
// @param text: The string to sort.
// @returns: The given string but sorted.
func SortString(text string) string {
	runes := []rune(text)
	sort.Slice(runes, func(a int, b int) bool {
		return runes[a] < runes[b]
	})
	return string(runes)
}
