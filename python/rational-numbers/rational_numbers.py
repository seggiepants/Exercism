"""
Rational Number implementation
"""
import math

class Rational:
    """
    Rational Number class implementation
    """
    def __init__(self, numer, denom):
        """
        Create a new Rational number with given numerator and denominator values
        :param numer: numerator
        :param denom: denominator
        """
        self.numer = numer
        self.denom = denom
        self.reduce()
    
    def reduce(self):
        """
        Reduce a rational number to lowest terms and allow only a negative sign on the numerator
        """
        factor = math.gcd(int(self.numer), int(self.denom))
        self.numer = self.numer / factor
        self.denom = self.denom / factor

        if self.denom < 0:
            self.numer *= -1
            self.denom *= -1
            
        if self.denom == 0:
            raise ValueError("Denominator may not be zero.")

    def __eq__(self, other):
        """
        Check if two Rational numbers are equal.
        :param other: The value to compare against
        :returns: True if numerator and denominator are equal to held by the other Rational Number
        """
        return self.numer == other.numer and self.denom == other.denom
    
    def __hash__(self) -> int:
        """
        Compute a hash of the object. Exercism complains if not present.
        :returns: hash (integer) of this object."""
        return hash((self.numer, self.denom))

    def __repr__(self):
        """
        Return a string represention of the Rational Number.
        :returns: String version of the number.
        """
        return f'{self.numer}/{self.denom}'

    def __add__(self, other):
        """
        Add this rational number to another and return the result.
        :param other: The value to add with
        :returns: New rational number that is the sum of self and other.
        """        
        return Rational(self.numer * other.denom + self.denom * other.numer, self.denom * other.denom)

    def __sub__(self, other):
        """
        Subtract another rational number from self and return the result
        :param other: The value to subtract from self
        :returns: New rational number that is self - other
        """
        return Rational(self.numer * other.denom - self.denom * other.numer, self.denom * other.denom)

    def __mul__(self, other):
        """
        Multiply another rational number with self and return the result
        :param other: The value to multiply with self
        :returns: New rational number that is self * other
        """
        return Rational(self.numer * other.numer, self.denom * other.denom)

    def __truediv__(self, other):
        """
        Divide self by another rational number and return the result
        :param other: The value to divide self by
        :returns: New rational number that is the self / other
        """
        return Rational(self.numer * other.denom, self.denom * other.numer)

    def __abs__(self):
        """
        Return the absolute value of this Rational Number
        :returns: New rational number that is the absolute value of self
        """
        return Rational(abs(self.numer), abs(self.denom))

    def __pow__(self, power):
        """
        Raise this rational number to the power of "power" and return the result
        :param power: The value to raise self to the power of
        :returns: New rational number that is the self ** power
        """
        if isinstance(power, float):
            return (self.denom ** power) / (self.numer ** power)
        if power >= 0:
            return Rational(self.numer ** power, self.denom ** power)
        return Rational(self.denom ** abs(power), self.numer ** abs(power))

    def __rpow__(self, base):
        """
        Raise a number to the power of self
        :param base: The value to raise to the power of self
        :returns: New rational number that is the base ** self
        """
        return (base ** self.numer) ** (1 / self.denom)
