// Determine if three line lengths can form a triangle, and if so is it
// Equilateral, Isosceles, or Scalene.
package triangle

type Kind int

const (
	NaT Kind = iota // not a triangle
	Equ             // equilateral
	Iso             // isosceles
	Sca             // scalene
)

// Determine if a triangle is equliateral, isosceles, scalene or not a
// triangle at all.
// @param a: The length of the first side of the triangle
// @param b: The length of the second side of the triangle
// @param c: The length of the third side of the triangle.
func KindFromSides(a, b, c float64) Kind {
	if a == 0 || b == 0 || c == 0 || a+b < c || a+c < b || b+c < a {
		return NaT
	}

	if a == b && b == c {
		return Equ
	}

	if a == b || b == c || a == c {
		return Iso
	}

	return Sca
}
