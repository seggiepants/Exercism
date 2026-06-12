// Pig Latin - convert a sentence to pig-latin. Only valid for English language text
package piglatin

import (
	"regexp"
	"strings"
)

var vowels string = "aeiou"
var consonants string = "bcdfghjklmnpqrstvwxyz"

// Convert a sentence of words into a string of pig-latin words.
// assumes we can reconstruct the sentence by adding a single space between words
// @param sentence: The string to convert to pig-latin
// @returns: The a copy of the sentence but in pig-latin.
func Sentence(sentence string) string {
	regexp, err := regexp.Compile(`\w+`)
	if err != nil {
		return ""
	}

	words := regexp.FindAllString(sentence, -1)
	var ret strings.Builder = strings.Builder{}
	var first bool = true

	for _, word := range words {
		if first {
			first = false
		} else {
			ret.WriteRune(' ')
		}
		ret.WriteString(PigLatin(word))
	}
	return ret.String()
}

// Convert a single word to Pig-Latin
// @param word: The word to convert
// @returns: Pig-Latin version of the input.
func PigLatin(word string) string {
	if len(word) == 0 {
		return ""
	}

	wordLower := strings.ToLower(word)
	// Rule 1: If a word begins with a vowel, or starts with "xr" or "yt", add an "ay" sound to the end of the word.
	if strings.HasPrefix(wordLower, "xr") || strings.HasPrefix(wordLower, "yt") || strings.Contains(vowels, string(wordLower[0])) {
		return word + "ay"
	}

	// Rule 2: If a word begins with one or more consonants, first move those consonants to the end of the word and then add an "ay" sound to the end of the word.
	var firstVowel int
	var ok bool = false
	for firstVowel = 0; firstVowel < len(wordLower); firstVowel++ {
		// Rule 4: If a word starts with one or more consonants followed by "y", first move the consonants preceding the "y"to the end of the word, and then add an "ay" sound to the end of the word.
		// or just treat y like a vowel if at position 1+
		if strings.Contains(vowels, string(wordLower[firstVowel])) || (wordLower[firstVowel] == 'y' && firstVowel > 0) {
			ok = true
			break
		}
	}

	if ok {
		// Rule 3: If a word starts with zero or more consonants followed by "qu", first move those consonants (if any) and the "qu" part to the end of the word, and then add an "ay" sound to the end of the word.
		if len(word) >= firstVowel+1 && word[firstVowel-1:firstVowel+1] == "qu" {
			return word[firstVowel+1:] + word[0:firstVowel+1] + "ay"
		}

		return word[firstVowel:] + word[:firstVowel] + "ay"
	}

	return word
}
