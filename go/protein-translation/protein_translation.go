// Protein Translation Exercise
package proteintranslation

import "errors"

var codonMap = map[string]string{
	"AUG": "Methionine",
	"UUU": "Phenylalanine",
	"UUC": "Phenylalanine",
	"UUA": "Leucine",
	"UUG": "Leucine",
	"UCU": "Serine",
	"UCC": "Serine",
	"UCA": "Serine",
	"UCG": "Serine",
	"UAU": "Tyrosine",
	"UAC": "Tyrosine",
	"UGU": "Cysteine",
	"UGC": "Cysteine",
	"UGG": "Tryptophan",
	"UAA": "STOP",
	"UAG": "STOP",
	"UGA": "STOP",
}

var ErrInvalidBase error = errors.New("ErrInvalidBase")
var ErrStop error = errors.New("ErrStop")

// Return a list of proteins from an RNA string. Stopping if you hit a stop codon.
// @param rna: The rna string to process
// @returns: list of strings. Each string is a translated protein (stop early if we reach a stop codon)
// @raises: ErrInvalidBase if we don't have a three character combination in our lookup table, or the string is too short.
func FromRNA(rna string) ([]string, error) {
	var result []string
	for index := 0; index < len(rna); index += 3 {
		stopIndex := index + 3
		if stopIndex > len(rna) {
			return []string{}, ErrInvalidBase
		}
		codon, err := FromCodon(rna[index:stopIndex])
		if err != nil {
			if err.Error() == "ErrStop" {
				break
			}
			return []string{}, err
		}
		result = append(result, codon)
	}
	return result, nil
}

// Translate a codon to a protein name via our lookup map.
// @param codon: The codon to search for
// @returns: The protein name if found.
// @raises: ErrInvalidBase if not a base in the lookup table or ErrStop if a stop codon was found.
func FromCodon(codon string) (string, error) {
	result, ok := codonMap[codon]
	if !ok {
		return "", ErrInvalidBase
	}
	if result == "STOP" {
		return "", ErrStop
	}
	return result, nil
}
