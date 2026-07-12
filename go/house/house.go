// This is the House that Jack build rhyme exercise.
package house

import (
	"fmt"
	"strings"
)

// Holds the verse specific data
type VerseInfo struct {
	object string
	verb   string
}

// The full list of verse specific data
var VerseInfos []VerseInfo = []VerseInfo{
	{object: "house that Jack built.", verb: ""},
	{object: "malt", verb: "lay in"},
	{object: "rat", verb: "ate"},
	{object: "cat", verb: "killed"},
	{object: "dog", verb: "worried"},
	{object: "cow with the crumpled horn", verb: "tossed"},
	{object: "maiden all forlorn", verb: "milked"},
	{object: "man all tattered and torn", verb: "kissed"},
	{object: "priest all shaven and shorn", verb: "married"},
	{object: "rooster that crowed in the morn", verb: "woke"},
	{object: "farmer sowing his corn", verb: "kept"},
	{object: "horse and the hound and the horn", verb: "belonged to"},
}

// Return a single verse from the This is the House that Jack Built rhyme
// @param v: The verse number (1-12)
// @returns: The text of the verse.
func Verse(v int) string {
	var ret strings.Builder = strings.Builder{}
	if v < 1 || v > len(VerseInfos) {
		return ""
	}
	ret.WriteString(fmt.Sprintf("This is the %s", VerseInfos[v-1].object))
	for i := v - 1; i >= 1; i-- {
		ret.WriteString(fmt.Sprintf("\nthat %s the %s", VerseInfos[i].verb, VerseInfos[i-1].object))
	}
	return ret.String()
}

// Return the full This is the House that Jack Build rhyme
// @returns: The text of the song with extra new lines between verses.
func Song() string {
	var ret strings.Builder = strings.Builder{}
	for i := 1; i <= len(VerseInfos); i++ {
		if i > 1 {
			ret.WriteString("\n\n")
		}
		ret.WriteString(Verse(i))
	}
	return ret.String()
}
