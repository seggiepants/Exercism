// Why this this called saddlepoints. It is the matrix exercise.
package saddlepoints

import (
	"errors"
	"strconv"
	"strings"
)

// Define the Matrix type here.
type Matrix [][]int

// Create a new matrix from the given string.
// rows are delimited by \n, values by space (trim leading/trailing), values should be integers.
// @param s: string to parse.
// @returns: new Matrix instance
// @raises: Error if ragged length, or a value can't be cast to integer.
func New(s string) (Matrix, error) {
	ret := Matrix{}
	for _, row := range strings.Split(strings.Trim(s, " "), "\n") {
		var data []int = []int{}
		for _, value := range strings.Split(strings.Trim(row, " "), " ") {
			num, err := strconv.Atoi(value)
			if err != nil {
				return nil, errors.New("Not an integer: " + value)
			}
			data = append(data, num)
		}
		ret = append(ret, data)
	}

	if len(ret) > 0 {
		expected := len(ret[0])
		for i := 1; i < len(ret); i++ {
			if len(ret[i]) != expected {
				return nil, errors.New("Rows are not consistent length")
			}
		}
	}

	return ret, nil
}

// Cols and Rows must return the results without affecting the matrix.

// Return the matrix columns as elements in a int slice
// @returns matrix columns as a series of int slices in a slice.
func (m Matrix) Cols() [][]int {
	ret := Matrix{}
	if len(m) > 0 {
		for i := 0; i < len(m[0]); i++ {
			row := []int{}
			for j := 0; j < len(m); j++ {
				row = append(row, m[j][i])
			}
			ret = append(ret, row)
		}
	}
	return ret
}

// Return the matrix rows as elements in a int slice
// @returns matrix rows as a series of int slices in a slice.
func (m Matrix) Rows() [][]int {
	ret := Matrix{}
	if len(m) > 0 {
		for j := 0; j < len(m); j++ {
			row := []int{}
			for i := 0; i < len(m[0]); i++ {
				row = append(row, m[j][i])
			}
			ret = append(ret, row)
		}
	}
	return ret
}

// Set the value of a matrix at the given position.
// @param row: The row to select
// @param col: Column within the row to select
// @param val: The value to set at that position
// @returns: True if value was set, false if there was a problem such as out of bounds.
func (m Matrix) Set(row, col, val int) bool {
	if row < 0 || row >= len(m) {
		return false
	}
	if col < 0 || col >= len(m[row]) {
		return false
	}
	m[row][col] = val
	return true
}
