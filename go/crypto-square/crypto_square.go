// Crypto Square exercise.
package cryptosquare

import (
	"math"
	"regexp"
	"strings"
)

// Encode given text using the Crypto-Square algorithm.
// @param pt: The text to encode.
// @returns: The encoded text with a space between sections.
func Encode(pt string) string {
	re := regexp.MustCompile("[A-Za-z0-9]+")
	sanitized := strings.ToLower(strings.Join(re.FindAllString(pt, -1), ""))
	width := int(math.Ceil(math.Sqrt(float64(len(sanitized)))))
	height := 0
	for width*height < len(sanitized) {
		height++
	}
	for len(sanitized) < width*height {
		sanitized += " "
	}
	rows := make([]string, width)
	for i := 0; i < width; i++ {
		for j := i; j < len(sanitized); j += width {
			rows[i] = rows[i] + string(sanitized[j])
		}
	}
	return strings.Join(rows, " ")
}
