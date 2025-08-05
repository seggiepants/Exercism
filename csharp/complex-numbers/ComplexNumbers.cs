public struct ComplexNumber
{
    double real;
    double imaginary;

    public ComplexNumber(double real, double imaginary)
    {
        this.real = real;
        this.imaginary = imaginary;
    }

    public double Real()
    {
        return this.real;
    }

    public double Imaginary()
    {
        return this.imaginary;
    }

    public ComplexNumber Mul(ComplexNumber other)
    {
        return new ComplexNumber((this.Real() * other.Real()) - (this.Imaginary() * other.Imaginary()), (this.Imaginary() * other.Real()) + (this.Real() * other.Imaginary()));
    }

    public ComplexNumber Mul(double value)
    {
        return new ComplexNumber(this.Real() * value, this.Imaginary() * value);
    }

    public ComplexNumber Add(ComplexNumber other)
    {
        return new ComplexNumber(this.Real() + other.Real(), this.Imaginary() + other.Imaginary());
    }

    public ComplexNumber Add(double value) 
    {
        return new ComplexNumber(this.Real() + value, this.Imaginary());
    }

    public ComplexNumber Sub(ComplexNumber other)
    {
        return new ComplexNumber(this.Real() - other.Real(), this.Imaginary() - other.Imaginary());
    }

    public ComplexNumber Sub(double value)
    {
        return new ComplexNumber(this.Real() - value, this.Imaginary());
    }

    public ComplexNumber Div(ComplexNumber other)
    {
        double divisor = ((other.Real() * other.Real()) + (other.Imaginary() * other.Imaginary()));
        return new ComplexNumber(((this.Real() * other.Real()) + (this.Imaginary() * other.Imaginary())) / divisor,
                                 ((this.Imaginary() * other.Real()) - (this.Real() * other.Imaginary())) / divisor);
    }

    public ComplexNumber Div(double value)
    {
        return new ComplexNumber(this.Real() / value, this.Imaginary() / value);
    }

    public double Abs()
    {
        return Math.Sqrt((this.Real() * this.Real()) + (this.Imaginary() * this.Imaginary()));
    }

    public ComplexNumber Conjugate()
    {
        return new ComplexNumber(this.Real(), -1 * this.Imaginary());
    }

    public ComplexNumber Exp()
    {
        double ePowerA = Math.Pow(Math.E, this.Real());
        return new ComplexNumber(ePowerA * Math.Cos(this.Imaginary()), ePowerA * Math.Sin(this.Imaginary()));
        
    }
}