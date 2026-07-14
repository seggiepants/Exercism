package parallelletterfrequency

import (
	"maps"
	"slices"
	"strings"
)

// FreqMap records the frequency of each rune in a given text.
type FreqMap map[rune]int

var skip []rune = []rune{' ', '\t', '\r', '\n', '0', '1', '2', '3', '4', '5',
	'6', '7', '8', '9', '!', '.', ',', ';', '?', '-', ':', ')', '(', '\'', '"'}

// Frequency counts the frequency of each rune in a given text and returns this
// data as a FreqMap.
// @param text: The text to calculate the frequencies for
// @returns: FreqMap instance with values for the given text.
func Frequency(text string) FreqMap {
	var ret FreqMap = FreqMap{}
	for _, rune := range strings.ToLower(text) {
		if slices.Contains(skip, rune) {
			continue
		}
		_, ok := ret[rune]
		if !ok {
			ret[rune] = 1
		} else {
			ret[rune]++
		}
	}
	return ret
}

// ConcurrentFrequency counts the frequency of each rune in the given strings,
// by making use of concurrency.
// @param texts: slice of string to calculate Frequency Maps for
// @returns: Frequency Map instance for all texts
func ConcurrentFrequency(texts []string) FreqMap {
	var ret FreqMap = FreqMap{}
	ch := make(chan FreqMap)
	for _, text := range texts {
		go func() { ch <- Frequency(text) }()
	}
	for i := 0; i < len(texts); i++ {
		temp := <-ch
		for key := range maps.Keys(temp) {
			_, ok := ret[key]
			if ok {
				ret[key] += temp[key]
			} else {
				ret[key] = temp[key]
			}
		}
	}
	return ret
}
