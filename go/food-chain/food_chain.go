// I know an old lady who swallowed a fly exercise.
package foodchain

import (
	"fmt"
	"strings"
)

// Holds the information that makes a single verse unique
type VerseInfo struct {
	animal      string
	description string
	commentary  string
}

// The set of verses in the song with what makes each unique.
var VerseInfos []VerseInfo = []VerseInfo{
	{animal: "fly", description: "", commentary: "I don't know why she swallowed the fly. Perhaps she'll die."},
	{animal: "spider", description: " that wriggled and jiggled and tickled inside her", commentary: "It wriggled and jiggled and tickled inside her."},
	{animal: "bird", description: "", commentary: "How absurd to swallow a bird!"},
	{animal: "cat", description: "", commentary: "Imagine that, to swallow a cat!"},
	{animal: "dog", description: "", commentary: "What a hog, to swallow a dog!"},
	{animal: "goat", description: "", commentary: "Just opened her throat and swallowed a goat!"},
	{animal: "cow", description: "", commentary: "I don't know how she swallowed a cow!"},
	{animal: "horse", description: "", commentary: "She's dead, of course!"},
}

// Recite an individual verse from the I know an old lady who swallowed a fly rhyme.
// @param v: The verse number 1 to 8 (1 = fly, 8 = horse)
// @returns: A string with the text of the verse with a line break between lines.
func Verse(v int) string {
	if v < 1 || v > len(VerseInfos) {
		return ""
	}
	var ret strings.Builder = strings.Builder{}

	ret.WriteString(fmt.Sprintf("I know an old lady who swallowed a %s.\n", VerseInfos[v-1].animal))
	ret.WriteString(VerseInfos[v-1].commentary)
	if v > 1 && v < len(VerseInfos) {
		for i := v - 1; i > 0; i-- {
			ret.WriteString(fmt.Sprintf("\nShe swallowed the %s to catch the %s%s.", VerseInfos[i].animal, VerseInfos[i-1].animal, VerseInfos[i-1].description))
		}
	}

	if v > 1 && v < len(VerseInfos) {
		ret.WriteString("\n")
		ret.WriteString(VerseInfos[0].commentary)
	}
	return ret.String()
}

// Recite a set of verses for the I know an old lady who swallowed a fly rhyme
// @param start: The verse to stop on (smallest)
// @param end: The verse start with (largest)
// @returns: string The individual verses with a line break inbetween each verse.
func Verses(start, end int) string {
	var ret strings.Builder = strings.Builder{}

	for i := start; i <= end; i++ {
		if i > start {
			ret.WriteString("\n\n")
		}
		ret.WriteString(Verse(i))
	}

	return ret.String()
}

// Recite the entire I know an old lady who swallowed a fly song.
// @returns: The song with line breaks between each line and an extra one between verses.
func Song() string {
	return Verses(1, len(VerseInfos))
}
