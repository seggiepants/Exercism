// Programatically recreate the 12 Days of Christmas song.
package twelvedays

import (
	"fmt"
	"strings"
)

type SongLine struct {
	nth  string
	gift string
}

var verses []SongLine = []SongLine{
	{"", ""},
	{"first", "a Partridge in a Pear Tree."},
	{"second", "two Turtle Doves"},
	{"third", "three French Hens"},
	{"fourth", "four Calling Birds"},
	{"fifth", "five Gold Rings"},
	{"sixth", "six Geese-a-Laying"},
	{"seventh", "seven Swans-a-Swimming"},
	{"eighth", "eight Maids-a-Milking"},
	{"ninth", "nine Ladies Dancing"},
	{"tenth", "ten Lords-a-Leaping"},
	{"eleventh", "eleven Pipers Piping"},
	{"twelfth", "twelve Drummers Drumming"},
}

// Recite a single verse of the 12 days of christmas song.
// @param i: Verse number (only 1-12 are valid)
// @returns: The ith line of the 12 days of christmas song
// or empty string - "" if i is not between 1 and 12.
func Verse(i int) string {
	var ret strings.Builder
	if i <= 0 || i > 12 {
		return "" // Out of bounds
	}
	ret.WriteString(fmt.Sprintf("On the %s day of Christmas my true love gave to me: ", verses[i].nth))
	for n := i; n >= 1; n-- {
		if i != n {
			ret.WriteString(", ")
		}
		if n == 1 && i != 1 {
			ret.WriteString("and ")
		}
		ret.WriteString(verses[n].gift)
	}
	return ret.String()
}

// Return the full 12 days of Christmas song.
// @returns: 12 Days of Christmas lyrics as one big string.
func Song() string {
	var ret strings.Builder
	for i := 1; i < len(verses); i++ {
		if i > 1 {
			ret.WriteString("\n")
		}
		ret.WriteString(Verse(i))
	}
	return ret.String()
}
