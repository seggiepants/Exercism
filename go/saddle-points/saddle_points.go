// Saddle Points exercise.
package saddlepoints

import (
	"errors"
	"strconv"
	"strings"
)

// Define the Matrix and Pair types here.

// Matrix data structure.
type Matrix [][]int

// x, y pair. Must be in y, x format to satisfy the test cases.
type Pair struct {
	y int
	x int
}

// Intitialize a new matrix from the given string. Rows are delimited by \n,
// and columns by spaces. The data can be empty for an empty matrix.
// @param s: String with matrix information
// @returns: Reference to matrix or error
// @raises: Error if a cell in the matrix is not a number or other exception.
func New(s string) (*Matrix, error) {
	const cutset string = " \n\r\t"
	var ret Matrix = Matrix{}

	s = strings.Trim(s, cutset)
	if len(s) > 0 {
		for _, row := range strings.Split(s, "\n") {
			var current []int = []int{}
			row = strings.Trim(row, cutset)
			for _, cell := range strings.Split(row, " ") {
				num, err := strconv.Atoi(cell)
				if err != nil {
					return nil, errors.New("Invalid cell: " + cell)
				}
				current = append(current, num)
			}
			ret = append(ret, current)
		}
	}

	return &ret, nil
}

// Return a slice of integers for a row of this matrix
// @param num: The row to return.
// @returns: A slice of integers with the data on the given row
// boolean says if the operation was successful or not (could be out of bounds)
func (m *Matrix) Row(num int) ([]int, bool) {
	if num < 0 || num >= len(*m) {
		return nil, false
	}
	return (*m)[num], true
}

// Return a slice of integers for a column of this matrix
// @param num: The column to return.
// @returns: A slice of integers with the data on the given column
// boolean says if the operation was successful or not (could be out of bounds)
func (m *Matrix) Col(num int) ([]int, bool) {
	if num < 0 || len(*m) < 1 || num >= len((*m)[0]) {
		return nil, false
	}
	var ret []int = []int{}
	for i := 0; i < len(*m); i++ {
		ret = append(ret, (*m)[i][num])
	}
	return ret, true
}

// Get the smallest of a slice of integers (why isn't this in the common library)
// @param: nums: A slice of integers
// @returns: Smallest integer in the slice or -1 if no data.
func IntMin(nums []int) int {
	if len(nums) <= 0 {
		return -1
	}
	var ret int = nums[0]
	for i := 1; i < len(nums); i++ {
		if nums[i] < ret {
			ret = nums[i]
		}
	}
	return ret
}

// Get the maximum of a slice of integers (why isn't this in the common library)
// @param: nums: A slice of integers
// @returns: Largest integer in the slice or -1 if no data.
func IntMax(nums []int) int {
	if len(nums) <= 0 {
		return -1
	}
	var ret int = nums[0]
	for i := 1; i < len(nums); i++ {
		if nums[i] > ret {
			ret = nums[i]
		}
	}
	return ret
}

// Find Saddle Points for the given matrix and return as slice of Pair
// @returns: slice of Pair (y, x order) of saddle points in the matrix. May be empty.
func (m *Matrix) Saddle() []Pair {
	var ret []Pair = []Pair{}
	for j := 0; j < len(*m); j++ {
		for i := 0; i < len((*m)[j]); i++ {
			col, success := m.Col(i)
			if !success {
				continue
			}
			row, success := m.Row(j)
			if !success {
				continue
			}
			minOfCol := IntMin(col)
			maxOfRow := IntMax(row)
			if minOfCol == (*m)[j][i] && maxOfRow == (*m)[j][i] {
				ret = append(ret, Pair{y: j + 1, x: i + 1})
			}
		}
	}
	return ret
}
