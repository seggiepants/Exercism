"""
Complex Number Class and Mathematical Operations
"""
import math

def param_as_complex(function):
    """
    Decorator that makes sure that the other parameter is a complex number
    :param function: which function to decorate
    :returns: function used to decorate and call the given function
    """
    def as_complex(self, other):
        """
        Make sure the parameter other is a ComplexNumber if it is not turn it into one
        before calling the function.
        :param other: number or ComplexNumber
        :returns: result of calling the function with other changed to a ComplexNumber object if needed.
        """
        if not isinstance(other, ComplexNumber):
            other = ComplexNumber(other, 0)
        return function(self, other)
    return as_complex

class ComplexNumber:
    """
    Complex Number Class and Mathematical Operations
    """
    def __init__(self, real, imaginary):
        """
        Complex Number initalization
        :param real: real component of the ComplexNumber
        :param imaginary: imaginary component of the Complex Number (before being multiplied by i)
        """
        self.real = real
        self.imaginary = imaginary

    def __eq__(self, other):
        """
        Check if this complex number is equal to another
        :param other: The complex number to check equality against
        :returns: True when equal and False otherwise
        """
        return self.real == other.real and self.imaginary == other.imaginary
    
    def __hash__(self):
        """
        Hash the current object. Test harness cries if I implement 
        __eq__ but not __hash__ so here it is.
        :returns: hash for this object
        """
        return hash((self.real, self.imaginary))    

    @param_as_complex
    def __add__(self, other):
        """
        Addition of this complex number with another
        :param other: The complex number to add to this one.
        :returns: A new complex number that is the sum of self and other
        """
        # z1 + z2 = (a + b * i) + (c + d * i)
        # = (a + c) + (b + d) * i
        return ComplexNumber((self.real + other.real), (self.imaginary + other.imaginary))
    
    @param_as_complex
    def __radd__(self, other):
        """
        Addition of another number with this complex number
        :param other: The complex number/number this one will be added to.
        :returns: A new complex number that is the sum of other and self
        """
        return other + self

    @param_as_complex
    def __mul__(self, other):
        """
        Multiplication of this complex number with another
        :param other: The complex number to multiply with this one.
        :returns: A new complex number that is the product of self and other
        """
        # z1 * z2 = (a + b * i) * (c + d * i)
        # = (a * c - b * d) + (b * c + a * d) * i
        return ComplexNumber(((self.real * other.real) - (self.imaginary * other.imaginary)), ((self.imaginary * other.real) + (self.real * other.imaginary)))

    @param_as_complex
    def __rmul__(self, other):
        """
        Multiplication of another number/complex number with this one
        :param other: The number/complex number to multiply with this one.
        :returns: A new complex number that is the product of other and self
        """
        return other * self

    @param_as_complex
    def __sub__(self, other):
        """
        Subtraction of another complex number from this one
        :param other: The complex number to subtract from this one.
        :returns: A new complex number that is the value of self - other
        """
        # z1 - z2 = (a + b * i) - (c + d * i)
        # = (a - c) + (b - d) * i
        return ComplexNumber((self.real - other.real), (self.imaginary - other.imaginary))
    
    @param_as_complex
    def __rsub__(self, other):
        """
        Subtraction this complex number from another complex number/number
        :param other: The complex number to subtract this from.
        :returns: A new complex number that is the value of other - self
        """
        return other - self

    @param_as_complex
    def __truediv__(self, other):
        """
        Divison of this complex number by another
        :param other: The complex number to divide this one by
        :returns: A new complex number that is the value of self / other
        """
        # z1 / z2 = z1 * (1 / z2)
        # = (a + b * i) / (c + d * i)
        # = (a * c + b * d) / (c^2 + d^2) + (b * c - a * d) / (c^2 + d^2) * i
        real = self.real * other.real + self.imaginary * other.imaginary
        real /= (other.real ** 2 + other.imaginary ** 2)
        imaginary = self.imaginary * other.real - self.real * other.imaginary
        imaginary /= (other.real ** 2 + other.imaginary ** 2)
        return ComplexNumber(real, imaginary)
    
    @param_as_complex
    def __rtruediv__(self, other):
        """
        Divison of another number/complex number from this one
        :param other: The complex number to be divided by this one
        :returns: A new complex number that is the value of other / self
        """
        return other / self

    def __abs__(self):
        """
        Return the absolute value of this Complex Number
        :returns: Absolute value of this complex number
        """
        # |z| = sqrt(a^2 + b^2)
        return math.sqrt(self.real ** 2 + self.imaginary ** 2)

    def conjugate(self):
        """
        Return the conjugate of this Complex Number
        :returns: Conjugate of this complex number
        """
        # zc = a - b * i
        return ComplexNumber(self.real, -1 * self.imaginary)

    def exp(self):
        """
        This complex number raised to the power of e
        :returns: This complex number raised to the power of e
        """
        # e^(a + b * i) = e^a * e^(b * i)
        # = e^a * (cos(b) + i * sin(b))
        multiplier = math.exp(self.real) 
        return ComplexNumber(multiplier * math.cos(self.imaginary), multiplier * math.sin(self.imaginary))
