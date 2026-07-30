package pythagorean

import "slices"

type Triplet [3]int

// Range generates list of all Pythagorean triplets with side lengths
// in the provided range.
// @param min: Minimum allowed side length.
// @param max: Maximum allowed side length.
// @returns: Slice of triplets with with side lengths between min and max.
func Range(min, max int) []Triplet {
	var result = make([]Triplet, 0)
	// (max * 3) - 3 in case you had a triplet of [max, max - 1, max - 2]
	for i := min; i <= (max*3)-3; i++ {
		result = slices.Concat(result, SumHelper(i, min, max))
	}
	return result
}

// Sum returns a list of Pythagorean triplets with a given perimeter.
// @param p: Perimeter
// @returns: Slice of Triplets with perimeter p where a^2 + b^2 = c^2
func Sum(p int) []Triplet {
	return SumHelper(p, 1, p)
}

// Sum Helper returns a list of all Pythagorean triplets with a certain
// perimeter and minimum and maximum values.
// @param p: Desired perimeter.
// @param minValue: least possible triplet value.
// @param maxValue: maximum possible triplet value
// @returns: slice of triplets that match the given restrictions.
func SumHelper(p int, minValue int, maxValue int) []Triplet {
	var result = make([]Triplet, 0)

	for a := 1; a < p-2; a++ {
		if a < minValue || a > maxValue {
			continue
		}
		for b := a + 1; b < p-a; b++ {
			if b < minValue || b > maxValue {
				continue
			}
			c := p - a - b
			if c < minValue || c > maxValue {
				continue
			}
			if (a*a)+(b*b) == (c * c) {
				result = append(result, Triplet{a, b, c})
			}
		}
	}

	return result
}
