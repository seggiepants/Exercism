<?php

// Exercise: Complex Numbers

declare(strict_types=1);

// Complex Number class
class ComplexNumbers
{
    public $real;
    public $imaginary;

    // Initialize the complex number.
    // @param $real: The real part of the complex number
    // @param $imaginary: The imaginary part of the complex number (value times i or square root of negative one).
    public function __construct($real, $imaginary = 0.0)
    {
        $this->real = $real;
        $this->imaginary = $imaginary;
    }

    // Return a new Complex Number that is this Complex Number's conjugate.
    // @returns ComplexNumbers: The mathematical result.    
    public function conjugate() : ComplexNumbers {
        // zc = a - b * i
        return new ComplexNumbers($this->real, -1 * $this->imaginary);
    }

    // Return a real number that is the absolute value of this Complex Number.
    // @returns ComplexNumbers: The mathematical result.    
    public function abs() : float {
        // |z| = sqrt(a^2 + b^2)
        return sqrt(pow($this->real, 2) + pow($this->imaginary, 2));
    }

    // Return a new Complex Number that is this plus other.
    // @param ComplexNumbers $other: The number to add
    // @returns ComplexNumbers: The mathematical result.
    public function add(ComplexNumbers $other) : ComplexNumbers {
        // z1 + z2 = (a + b * i) + (c + d * i)
        // = (a + c) + (b + d) * i
        return new ComplexNumbers($this->real + $other->real, $this->imaginary + $other->imaginary);
    }

    // Return a new Complex Number that is this minus other.
    // @param ComplexNumbers $other: The number to subtract
    // @returns ComplexNumbers: The mathematical result.    
    public function sub(ComplexNumbers $other) : ComplexNumbers {
        // z1 - z2 = (a + b * i) - (c + d * i)
        // = (a - c) + (b - d) * i
        return new ComplexNumbers($this->real - $other->real, $this->imaginary - $other->imaginary);
    }

    // Return a new Complex Number that is this multiplied by other.
    // @param ComplexNumbers $other: The number to multiplied by
    // @returns ComplexNumbers: The mathematical result.
    public function mul(ComplexNumbers $other) : ComplexNumbers {
        // z1 * z2 = (a + b * i) * (c + d * i)
        // = (a * c - b * d) + (b * c + a * d) * i
        return new ComplexNumbers(($this->real * $other->real) - ($this->imaginary * $other->imaginary), ($this->imaginary * $other->real) + ($this->real * $other->imaginary));
    }

    // Return a new Complex Number that is this divided by other.
    // @param ComplexNumbers $other: The number to divide by
    // @returns ComplexNumbers: The mathematical result.
    public function div(ComplexNumbers $other) : ComplexNumbers {
        // z1 / z2 = z1 * (1 / z2)
        // = (a + b * i) / (c + d * i)
        // = (a * c + b * d) / (c^2 + d^2) + (b * c - a * d) / (c^2 + d^2) * i
        $denominator = pow($other->real, 2) + pow($other->imaginary, 2);
        return new ComplexNumbers((($this->real * $other->real) + ($this->imaginary * $other->imaginary)) / $denominator, (($this->imaginary * $other->real) - ($this->real * $other->imaginary)) / $denominator);
    }

    // Return a new Complex Number that is this raised to the power of (Eulers number)
    // @returns ComplexNumbers: e^(this)
    public function exp() : ComplexNumbers {
        // e^(a + b * i) = e^a * e^(b * i)
        //       = e^a * (cos(b) + i * sin(b))
        $multiplier = exp($this->real);
        return  new ComplexNumbers($multiplier * cos($this->imaginary), $multiplier * sin($this->imaginary));
    }

}
