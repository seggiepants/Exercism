// Transcribe a DNA sequence to RNA
package rnatranscription

import "strings"

var DNAtoRNA map[rune]rune = map[rune]rune{
	'G': 'C',
	'C': 'G',
	'T': 'A',
	'A': 'U',
}

// Convert a string of DNA nucleotides to a corresponding string of RNA nucleotides
// @param dna: The sequence to convert
// @returns: string of corresponding RNA nuclotides for characters that match DNA.
func ToRNA(dna string) string {
	var result strings.Builder

	for _, dna := range strings.ToUpper(dna) {
		rna, ok := DNAtoRNA[dna]
		if ok {
			result.WriteRune(rna)
		} else {
			// It was either this or skip it.
			result.WriteRune(dna)
		}
	}
	return result.String()
}
