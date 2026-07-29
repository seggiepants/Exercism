// Perfect Numbers exercise
package perfectnumbers

import "errors"

// Define the Classification type here.
type Classification int

const (
	ClassificationPerfect Classification = iota
	ClassificationAbundant
	ClassificationDeficient
)

// Secret requirement have a global ErrOnlyPositive error variable.
// Thought they wanted a custom error type at first.
var ErrOnlyPositive error = errors.New("Only positive integers are allowed.")

// Classify a number as perfect, abundant, or deficient.
// @param n: Number to classify
// @returns: enumerated classification of ClassificationPerfect, ClassificationAbundant or ClassificationDeficient
// @raises: Returns an error if the number to evaluate is <= 0
func Classify(n int64) (Classification, error) {
	var aliquot_sum int64 = 0
	var i int64 = 0
	if n <= 0 {
		return ClassificationPerfect, ErrOnlyPositive
	}
	for i = 1; i < n; i++ {
		if n%i == 0 {
			aliquot_sum += i
		}
	}

	switch {
	case n < aliquot_sum:
		return ClassificationAbundant, nil
	case n > aliquot_sum:
		return ClassificationDeficient, nil
	default:
		return ClassificationPerfect, nil
	}

}
