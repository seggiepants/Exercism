// Piecing it together - Compute missing data from a jigsaw data set.
package piecingittogether

// This shows the problem with testing first. I am coding to pass the tests
// but I don't know that I have all of the possible ways the data could be
// recovered. I just stopped when all the tests passed.

import (
	"errors"
	"math"
)

// Jigsaw dataset row
type PuzzleDetails struct {
	Pieces      int
	Border      int
	Inside      int
	Rows        int
	Columns     int
	AspectRatio float64
	Format      string
}

// Given the incomplete jigsaw data fill in the missing data.
// @param details: incomplete jigsaw data.
// @returns: Jigsaw data with missing information filled in.
// @raises: Error if all data could not be computed or in inconsistent.
func JigsawData(details PuzzleDetails) (PuzzleDetails, error) {
	var ret PuzzleDetails = PuzzleDetails{
		Pieces:      details.Pieces,
		Border:      details.Border,
		Inside:      details.Inside,
		Rows:        details.Rows,
		Columns:     details.Columns,
		AspectRatio: details.AspectRatio,
		Format:      details.Format,
	}

	for i := 0; i < 6; i++ { // Up to one pass for each field
		// If finished break out.
		if ret.AspectRatio != 0.0 && ret.Border != 0 && ret.Columns != 0 && ret.Format != "" && ret.Inside != 0 && ret.Pieces != 0 && ret.Rows != 0 {
			break
		}

		if ret.Pieces != 0 && ret.AspectRatio != 0 && (ret.Rows == 0 || ret.Columns == 0) {
			columns := math.Sqrt(float64(ret.Pieces) * ret.AspectRatio)
			if ret.Columns == 0.0 {
				ret.Columns = int(columns)
			}
			if ret.Rows == 0.0 {
				ret.Rows = int(columns / ret.AspectRatio)
			}
		}

		if ret.AspectRatio == 1.0 && ret.Inside != 0 {
			ret.Rows = int(math.Sqrt(float64(ret.Inside))) + 2
			ret.Columns = ret.Rows
			ret.Pieces = ret.Rows * ret.Columns
			ret.Border = ret.Pieces - ret.Inside
			ret.Format = "square"
		}

		if ret.Rows != 0 && ret.Columns != 0 {
			ret.AspectRatio = float64(ret.Columns) / float64(ret.Rows)
			ret.Format = CalcFormatFromAspectRatio(ret.AspectRatio)
		}

		if ret.Pieces != 0 && ret.Border != 0 && (ret.Rows == 0 || ret.Columns == 0) {
			a, b, err := FindBorder(Factors(ret.Pieces), ret.Border, 1, 1, ret.AspectRatio == 1.0 || ret.Format == "square")
			if err == nil {
				if ret.AspectRatio <= 1.0 || ret.Format == "portrait" || ret.Format == "square" {
					ret.Columns = a
					ret.Rows = b
				} else if ret.AspectRatio > 1.0 || ret.Format == "landscape" {
					ret.Columns = b
					ret.Rows = a
				}
			}
		}

		if ret.Rows != 0 && ret.Columns != 0 {
			if ret.Pieces == 0 {
				ret.Pieces = ret.Rows * ret.Columns
			}
			if ret.Border == 0 {
				ret.Border = (ret.Rows * 2) + (ret.Columns * 2) - 4
			}
			if ret.Inside == 0 {
				ret.Inside = ret.Pieces - ret.Border
			}

			if ret.AspectRatio == 0 {
				ret.AspectRatio = float64(ret.Columns) / float64(ret.Rows)
			}
		}

		if ret.Format == "" && ret.AspectRatio != 0.0 {
			ret.Format = CalcFormatFromAspectRatio(ret.AspectRatio)
		}

		if ret.Format == "square" && ret.AspectRatio == 0.0 {
			ret.AspectRatio = 1.0
		}

		if ret.AspectRatio != 0 {
			if ret.Rows == 0 && ret.Columns != 0 {
				ret.Rows = int(float64(ret.Columns) / ret.AspectRatio)
			}
			if ret.Columns == 0 && ret.Rows != 0 {
				ret.Columns = int(float64(ret.Rows) * ret.AspectRatio)
			}
		}
	}

	if (details.AspectRatio != 0.0 && ret.AspectRatio != details.AspectRatio) ||
		(details.Border != 0 && ret.Border != details.Border) ||
		(details.Columns != 0 && ret.Columns != details.Columns) ||
		(details.Format != "" && ret.Format != details.Format) ||
		(details.Inside != 0 && ret.Inside != details.Inside) ||
		(details.Pieces != 0 && ret.Pieces != details.Pieces) ||
		(details.Rows != 0 && ret.Rows != details.Rows) {
		return ret, errors.New("Contradictory data")
	}
	if (ret.AspectRatio == 0.0) ||
		(ret.Border == 0) ||
		(ret.Columns == 0) ||
		(ret.Format == "") ||
		(ret.Inside == 0) ||
		(ret.Pieces == 0) ||
		(ret.Rows == 0) {
		return ret, errors.New("Insufficient data")
	}
	return ret, nil
}

// Calculate format (portrait, landscape, square) from an aspect ratio
// @param AspectRatio: The aspect ration (with/height)
// @returns: String describing the given aspect ratio format.
func CalcFormatFromAspectRatio(AspectRatio float64) string {
	if AspectRatio > 1.0 {
		return "landscape"
	}
	if AspectRatio < 1.0 {
		return "portrait"
	}
	return "square"
}

// Calculate the integer factors of a given number
// Used in find border.
// @param n: The number to factorize
// @returns: slice of int with the factors.
func Factors(n int) []int {
	ret := make([]int, 0)

	done := false
	for !done {
		old := len(ret)

		for i := 2; i <= n; i++ {
			if n%i == 0 {
				ret = append(ret, i)
				n = n / i
				break
			}
		}

		done = len(ret) == old
	}
	return ret
}

// Find the side lengths of a rectangle given the desired border and factors of the length
// of the outside perimiter and if it is square or not.
// @param factors: slice of int with the factors of the perimeter.
// @param border: The desired border length
// @param a: First side length so far
// @param b: Second side length so far
// @param isSquare: Is the rectangle supposed to be a square
// @returns: Smaller side length, larger side length
// @raises: Error if no match was found.
func FindBorder(factors []int, border int, a int, b int, isSquare bool) (int, int, error) {
	// Base case
	if len(factors) == 0 && (2*a)+(2*b)-4 == border && (!isSquare || a == b) {
		if a <= b {
			return a, b, nil
		}
		return b, a, nil
	}
	if len(factors) > 0 {
		first := factors[0]
		var remaining []int
		if len(factors) == 1 {
			remaining = []int{}
		} else {
			remaining = factors[1:]
		}
		// Try on side a
		newA, newB, newError := FindBorder(remaining, border, a*first, b, isSquare)
		if newError == nil {
			return newA, newB, nil
		}
		newA, newB, newError = FindBorder(remaining, border, a, b*first, isSquare)
		if newError == nil {
			return newA, newB, nil
		}
	}
	return 0, 0, errors.New("No solution found.")
}
