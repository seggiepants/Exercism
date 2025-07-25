using Xunit.Internal;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max)
    {
        HashSet<int> uniqueFactors = new();
        foreach (int multiple in multiples)
        {
            if ((multiple < max) && (multiple > 0))
            {
                // Decrement total iterations by one if we would get to exactly max.
                int fudgeFactor = 0;
                if (max % multiple == 0) fudgeFactor = 1;
                uniqueFactors.AddRange(Enumerable.Range(1, (max / multiple) - fudgeFactor).Select(n => n * multiple));
            }
        }
        return uniqueFactors.Sum();
    }
}