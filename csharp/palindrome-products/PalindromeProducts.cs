using System.Net.NetworkInformation;

public static class PalindromeProducts
{
    public static (int, IEnumerable<(int, int)>) Largest(int minFactor, int maxFactor)
    {
        IEnumerable<int> results = GetPalindromes(minFactor, maxFactor);
        if (results.Count() == 0)
            throw new ArgumentException($"No factors found for range of ({minFactor}, {maxFactor})");
        int largest = results.Max();
        return (largest, Factors(largest, minFactor, maxFactor));
    }

    public static (int, IEnumerable<(int, int)>) Smallest(int minFactor, int maxFactor)
    {
        IEnumerable<int> results = GetPalindromes(minFactor, maxFactor);
        if (results.Count() == 0)
            throw new ArgumentException($"No factors found for range of ({minFactor}, {maxFactor})");
        int smallest = results.Min();
        return (smallest, Factors(smallest, minFactor, maxFactor));
    }

    public static IEnumerable<int> GetPalindromes(int minFactor, int maxFactor)
    {
        if (minFactor > maxFactor)
            throw new ArgumentException($"Range of ({minFactor}, {maxFactor}) is not valid.");

        for (int j = minFactor; j <= maxFactor; j++)
        {
            for (int i = minFactor; i <= maxFactor; i++)
            {
                if (i > j) // no use computing this twice.
                    continue;

                if (i * j == 0) // Zero doesn't count.
                    continue;

                if (IsPalindrome(i * j))
                    yield return (i * j);
            }

        }
    }

    public static bool IsPalindrome(int value)
    {
        string num = value.ToString();
        int start = 0;
        int end = num.Length - 1;
        while (start < end)
        {
            if (num[start] != num[end])
                return false;
            start++;
            end--;
        }
        return true;
    }

    public static IEnumerable<(int, int)> Factors(int value, int minFactor, int maxFactor)
    {
        for (int j = minFactor; j <= maxFactor; j++)
        {
            for (int i = minFactor; i <= maxFactor; i++)
            {
                if (j > i)
                    continue; // Only want each factor pair once.
                if ((i * j) == 0) // Zero doesn't count.
                    continue;
                if (i > value || j > value) // Won't be a factor if larger than value.
                    continue;
                if ((i * j) == value)
                    yield return (j, i);
            }
        }
    }
}
