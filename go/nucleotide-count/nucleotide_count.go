// Convert DNA Nucleotide string to a map of nucleotide to occurence count.
package nucleotidecount

import (
	"errors"
	"strings"
)

// Histogram is a mapping from nucleotide to its count in given DNA.
type Histogram map[rune]int

// DNA is a list of nucleotides.
type DNA string

var NUCLEOTIDES = "ACTG"

// Counts generates a histogram of valid nucleotides in the given DNA.
// Returns an error if the DNA contains an invalid nucleotide.
// @returns: Histogram of nuclotide to occurence count
// @raises: When invalid nucleotide is found.
func (d DNA) Counts() (Histogram, error) {
	var h Histogram = Histogram{}
	for _, char := range NUCLEOTIDES {
		h[char] = 0
	}
	for _, char := range d {
		if !strings.Contains(NUCLEOTIDES, string(char)) {
			return map[rune]int{}, errors.New("Invalid nucleotide: " + string(char))
		}
		h[char]++
	}
	return h, nil
}
