// Complex Number implementation
package complexnumbers

import "math"

// Define the Number type here.
type Number struct {
	real      float64
	imaginary float64
}

// Return real part of a complex number
// @returns: real component of complex number
func (n Number) Real() float64 {
	return n.real
}

// Return imaginary part of a complex number
// @returns: imaginary component of complex number
func (n Number) Imaginary() float64 {
	return n.imaginary
}

// Add two complex numbers together returning a new complex number result.
// @param n2: Number to add to this number
// @returns: Complex number = n1 + n2
func (n1 Number) Add(n2 Number) Number {
	return Number{real: n1.real + n2.real, imaginary: n1.imaginary + n2.imaginary}
}

// Subtract a complex number from this one returning a new complex number result.
// @param n2: Number to subtract from this number
// @returns: Complex number = n1 - n2
func (n1 Number) Subtract(n2 Number) Number {
	return Number{real: n1.real - n2.real, imaginary: n1.imaginary - n2.imaginary}
}

// Multiply a complex number with this one returning a new complex number result.
// @param n2: Number to multiply with this number
// @returns: Complex number = n1 * n2
func (n1 Number) Multiply(n2 Number) Number {
	return Number{
		real:      (n1.real * n2.real) - (n1.imaginary * n2.imaginary),
		imaginary: (n1.imaginary * n2.real) + (n1.real * n2.imaginary),
	}
}

// Multiply a complex number by a scalar.
// @param factor: Scalar to multiply this number with
// @returns: Complex number scaled by factor
func (n Number) Times(factor float64) Number {
	return Number{
		real:      n.real * factor,
		imaginary: n.imaginary * factor,
	}
}

// Divid this complex number by another complex number returning a new complex number result.
// @param n2: Number to divid with this number by
// @returns: Complex number = n1 / n2
func (n1 Number) Divide(n2 Number) Number {
	denominator := (n2.real * n2.real) + (n2.imaginary * n2.imaginary)
	return Number{
		real:      ((n1.real * n2.real) + (n1.imaginary * n2.imaginary)) / denominator,
		imaginary: ((n1.imaginary * n2.real) - (n1.real * n2.imaginary)) / denominator,
	}
}

// Return the conjugate of this complex number.
// @returns: Conjugate of this complex number (just imaginary part sign flipped)
func (n Number) Conjugate() Number {
	return Number{real: n.real, imaginary: n.imaginary * -1}
}

// Return a new complex number that is the absolute value of this one
// @returns: Abs(n)
func (n Number) Abs() float64 {
	return math.Sqrt(n.real*n.real + n.imaginary*n.imaginary)
}

// Take this complex number to the power of e and return the result.
// @returns: n ^ e
func (n Number) Exp() Number {
	// e^a * (cos(b) + i * sin(b))
	multiplier := math.Pow(math.E, n.real)
	return Number{
		real:      multiplier * math.Cos(n.imaginary),
		imaginary: multiplier * math.Sin(n.imaginary),
	}
}
