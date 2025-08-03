using Xunit.v3;

public static class RealNumberExtension
{
    public static double Expreal(this int realNumber, RationalNumber r)
    {
        double power = r.Denominator != 0 ? (double)r.Numerator / (double)r.Denominator : 0.0;
        return Math.Pow(realNumber, power);
    }
}


public struct RationalNumber
{
    public int Numerator { get; set; }
    public int Denominator { get; set; }

    public RationalNumber(int numerator, int denominator)
    {
        Tuple<int, int> reduced = Reduce(numerator, denominator);
        Numerator = reduced.Item1;
        Denominator = reduced.Item2;        
    }

    public static int GCD(int a, int b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }

    public static List<int> GCD(int number)
    {
        List<int> ret = new();
        for (int i = 1; i <= Math.Abs(number); i++)
        {
            if ((number % i) == 0)
                ret.Add(i);
        }
        return ret;
    }

    public static RationalNumber operator +(RationalNumber r1, RationalNumber r2)
    {
        return new RationalNumber(r1.Numerator * r2.Denominator + r2.Numerator * r1.Denominator, r1.Denominator * r2.Denominator);
    }

    public static RationalNumber operator -(RationalNumber r1, RationalNumber r2)
    {
        return new RationalNumber(r1.Numerator * r2.Denominator - r2.Numerator * r1.Denominator, r1.Denominator * r2.Denominator);
    }

    public static RationalNumber operator *(RationalNumber r1, RationalNumber r2)
    {
        return new RationalNumber(r1.Numerator * r2.Numerator, r1.Denominator * r2.Denominator);
    }

    public static RationalNumber operator /(RationalNumber r1, RationalNumber r2)
    {
        return new RationalNumber(r1.Numerator * r2.Denominator, r1.Denominator * r2.Numerator);
    }

    public RationalNumber Abs()
    {
        return new RationalNumber(Math.Abs(Numerator), Math.Abs(Denominator));
    }

    public Tuple<int, int> Reduce(int Numerator, int Denominator)
    {
        int n = Numerator;
        int d = Denominator;
        if (d != 0)
        {
            int gcd = GCD(n, d);
            if (d < 0 && n < 0)
            {
                n = Math.Abs(n);
                d = Math.Abs(d);
            }
            else if (d < 0 && n > 0)
            {
                n = -1 * Math.Abs(n);
                d = Math.Abs(d);
            }
            n = n / gcd;
            d = d / gcd;
        }
        return new Tuple<int, int>(n, d);
    }

    public RationalNumber Reduce()
    {
        return new RationalNumber(Numerator, Denominator);
    }

    public RationalNumber Exprational(int power)
    {
        int pwr = Math.Abs(power);
        if (power > 0)
        {
            return new RationalNumber((int)(Math.Pow(Numerator, pwr)), (int)(Math.Pow(Denominator, pwr)));
        }
        else
        {
            return new RationalNumber((int)Math.Pow(Denominator, pwr), (int)(Math.Pow(Numerator, pwr)));
        }

    }

    public double Expreal(int baseNumber)
    {
        return Math.Pow(Numerator, baseNumber) / Math.Pow(Denominator, baseNumber);
    }

    public override string ToString()
    {
        return $"({Numerator}/{Denominator})";
    }
}