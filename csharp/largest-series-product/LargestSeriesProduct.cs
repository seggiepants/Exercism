using System.Globalization;

public static class LargestSeriesProduct
{
    public static long GetLargestProduct(string digits, int span)
    {
        if (span < 0)
            throw new ArgumentException("Parameter span must be a positive number greater than zero.");
        else if (span == 0)
            return 1;

        if (digits.Length == 0)
            throw new ArgumentException("Parameters digits may not be empty, and must contain only digits 0-9.");
        else if (span > digits.Length)
            throw new ArgumentException("Parameter span must be less than or equal to the length of parameter digits.");

        List<long> data = new();
        for (int i = 0; i <= digits.Length - span; i++)
            data.Add(SpanProduct(digits.Substring(i, span)));
        return data.Max();
    }

    private static long SpanProduct(string digits)
    {
        long accumulator = 1;
        foreach (char c in digits)
        {
            if (c >= '0' && c <= '9')
                accumulator *= (long)(c - '0');
            else
                throw new ArgumentException("Digits may only be 0-9.");
        }
        return accumulator;
    }
}