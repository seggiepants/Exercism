package hamming

import "errors"

func Distance(a, b string) (int, error) {

	var differences int
	a_runes := []rune(a)
	b_runes := []rune(b)
	if len(a_runes) != len(b_runes) {
		return 0, errors.New("Strand lengths are not equal")
	}
	differences = 0
	for index, char := range a_runes {
		if char != b_runes[index] {
			differences++
		}
	}
	return differences, nil
}
