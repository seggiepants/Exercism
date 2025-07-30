public static class PrimeFactors
{
    public static long[] Factors(long number)
    {
        List<long> results = new();

        long factor = 2;
        while (number > 1)
        {
            if (number % factor == 0)
            {
                number /= factor;
                results.Add(factor);
            }
            else
            {
                factor++;
            }
        }


        return results.ToArray<long>();
    }
}