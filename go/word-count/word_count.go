// Word Count: Separate a phrase into words and count how many times a word occurs in the phrase.
package wordcount

import (
	"regexp"
	"strings"
)

type Frequency map[string]int

// Count the occurences of words in a string.
// @param string: The text to extract words from
// @returns: a Frequency type (map[string]int) that has keys of words (lower case) with a value of their counts.
// will return an empty set if no words are found or the regexp used doesn't compile.
func WordCount(phrase string) Frequency {
	ret := Frequency{}
	re, err := regexp.Compile(`\w+('\w+)?`)
	if err != nil {
		return ret // empty on error.
	}
	results := re.FindAllString(phrase, -1)
	for _, word := range results {
		ret[strings.ToLower(word)]++
	}

	return ret
}
