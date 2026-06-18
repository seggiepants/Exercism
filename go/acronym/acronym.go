// Turn a phrase into an acronym
package acronym

import (
	"regexp"
	"strings"
)

// Turn a phrase into an acronym
// @param s: The phrase to abbreviate
// @returns: The abbreviated phrase
func Abbreviate(s string) string {
	re := regexp.MustCompile(`[a-zA-Z][\w|\']*`)
	matches := re.FindAllString(strings.ToUpper(s), -1)
	if matches == nil {
		return ""
	}
	var letters strings.Builder = strings.Builder{}
	for _, word := range matches {
		letters.WriteString(word[0:1])
	}
	return letters.String()
}
