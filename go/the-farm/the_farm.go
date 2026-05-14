package thefarm

import (
	"errors"
	"fmt"
)

type CowCountError struct {
	numCows int
	message string
}

func (err *CowCountError) Error() string {
	return fmt.Sprintf("%d cows are invalid: %s", err.numCows, err.message)
}

// TODO: define the 'DivideFood' function
func DivideFood(calc FodderCalculator, numCows int) (float64, error) {
	amount, err := calc.FodderAmount(numCows)
	if err != nil {
		return 0.0, err
	}
	factor, err := calc.FatteningFactor()
	if err != nil {
		return 0.0, err
	}

	return (amount * factor) / float64(numCows), nil
}

// TODO: define the 'ValidateInputAndDivideFood' function
func ValidateInputAndDivideFood(calc FodderCalculator, numCows int) (float64, error) {
	if numCows <= 0 {
		return 0, errors.New("invalid number of cows")
	}
	return DivideFood(calc, numCows)
}

// TODO: define the 'ValidateNumberOfCows' function
func ValidateNumberOfCows(numCows int) error {
	if numCows < 0 {
		return &CowCountError{
			numCows: numCows,
			message: "there are no negative cows",
		}
	} else if numCows == 0 {
		return &CowCountError{
			numCows: numCows,
			message: "no cows don't need food",
		}
	}
	return nil
}

// Your first steps could be to read through the tasks, and create
// these functions with their correct parameter lists and return types.
// The function body only needs to contain `panic("")`.
//
// This will make the tests compile, but they will fail.
// You can then implement the function logic one by one and see
// an increasing number of tests passing as you implement more
// functionality.
