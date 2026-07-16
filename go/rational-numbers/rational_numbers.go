// Rational Number library exercise.
package rationalnumbers

import "math"

// Rational number structure.
type Rational struct {
	numerator, denominator int
}

// Absolute value of an integer - This really should be in the standard library
// @param a: Number to compute absolute value of
// @returns: a, or -a if a is negative.
func AbsInt(a int) int {
	if a < 0 {
		return a * -1
	}
	return a
}

// Greatest Common Denominator
// @param a: First number
// @param b: Second number
// @returns: Greatest value that divides and and b evenly
func GCD(a, b int) int {
	smaller := MinInt(a, b)
	larger := MaxInt(a, b)

	if smaller == 0 {
		return larger
	}

	remainder := larger % smaller

	for remainder != 0 {
		larger = smaller
		smaller = remainder
		remainder = larger % smaller
	}
	return smaller
}

// Maximum of two integers
// @param a: First integer
// @param b: Second integer
// @returns: larger of a and b
func MaxInt(a, b int) int {
	if a > b {
		return a
	}
	return b
}

// Minimum of two integers
// @param a: First integer
// @param b: Second integer
// @returns: smaller of a and b
func MinInt(a, b int) int {
	if a < b {
		return a
	}
	return b
}

// Reduce simplifies a Rational, eg changing Rational{4, 2} into Rational{2, 1}.
// @returns: New reduced rational number
func (r Rational) Reduce() Rational {
	if r.numerator == 0 || r.denominator == 0 {
		return Rational{numerator: 0, denominator: 1}
	}
	n := r.numerator
	d := r.denominator
	if d < 0 {
		n *= -1
		d *= -1
	}

	factor := GCD(AbsInt(n), AbsInt(d))
	n /= factor
	d /= factor
	return Rational{numerator: n, denominator: d}
}

// Add two rational numbers together, returning the sum.
// @param s: The rational number to add to this number.
// @returns: New rational number that is the sum of self and s.
func (r Rational) Add(s Rational) Rational {
	numerator := (r.numerator * s.denominator) + (r.denominator * s.numerator)
	denominator := (r.denominator * s.denominator)
	return Rational{numerator: numerator, denominator: denominator}.Reduce()
}

// Subtract a rational number from this one, returning the result.
// @param s: The rational number to subtract from this number.
// @returns: New rational number that is self - s.
func (r Rational) Sub(s Rational) Rational {
	numerator := (r.numerator * s.denominator) - (r.denominator * s.numerator)
	denominator := (r.denominator * s.denominator)
	return Rational{numerator: numerator, denominator: denominator}.Reduce()
}

// Multiply two rational numbers together, returning the product.
// @param s: The rational number to multiply with this number.
// @returns: New rational number that is the product of self and s.
func (r Rational) Mul(s Rational) Rational {
	numerator := (r.numerator * s.numerator)
	denominator := (r.denominator * s.denominator)
	return Rational{numerator: numerator, denominator: denominator}.Reduce()
}

// Divide this rational number by another, returning the result.
// @param s: The rational number to divide this number by.
// @returns: New rational number that is self / s.

func (r Rational) Div(s Rational) Rational {
	numerator := (r.numerator * s.denominator)
	denominator := (r.denominator * s.numerator)
	return Rational{numerator: numerator, denominator: denominator}.Reduce()
}

// Absolute Value of a Rational Number
// @returns: Rational number's absolute value.
func (r Rational) Abs() Rational {
	return Rational{numerator: AbsInt(r.numerator), denominator: AbsInt(r.denominator)}.Reduce()
}

// Compute r ^ power, a rational raised to an int exponent.
// @param power: The power to raise this rational number to.
// @returns: Rational number computed value.
func (r Rational) Exprational(power int) Rational {
	pwr := AbsInt(power)
	n := math.Pow(float64(r.numerator), float64(pwr))
	d := math.Pow(float64(r.denominator), float64(pwr))
	if power < 0 {
		n, d = d, n
	}
	return Rational{numerator: int(n), denominator: int(d)}.Reduce()
}

// Compute base ^ r, an int raised to a rational.
// @param base: The float value to raise to a rational power.
// @returns: computed value.
func (r Rational) Expreal(base int) float64 {
	return math.Pow(math.Pow(float64(base), 1.0/float64(r.denominator)), float64(r.numerator))
}
