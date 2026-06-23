// exercise: grains on a chessboard
// May fail if the presicion of float64 to/from uint64 is not precise enough.
package grains

import (
	"errors"
	"math"
)

// Get the number of grains on a specific square of the chessboard 1-64
// @param number: The number of the square (1-64)
// @returns: The number of grains on the given square or an error if square number not in the range 1-64
func Square(number int) (uint64, error) {
	if number < 1 || number > 64 {
		return uint64(0), errors.New("Index out of range.")
	}
	//return Pow2_Uint64(number - 1), nil
	return uint64(math.Pow(2.0, float64(number-1))), nil
}

// The total number of grains of rice on the chessboard.
// @returns: The total number of gains on the chessboard.
func Total() uint64 {
	var total uint64 = 0
	for number := 1; number <= 64; number++ {
		squareValue, error := Square(int(number))
		if error == nil {
			total += squareValue
		}
	}
	return total
}
