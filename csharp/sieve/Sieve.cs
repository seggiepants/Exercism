
public static class Sieve
{
    public static int[] Primes(int limit)
    {
        bool[] candidates = (from n in Enumerable.Range(0, limit + 1)
                                 select true).ToArray<bool>();
        for (int i = 2; i <= limit; i++)
        {
            if (candidates[i] == true) // prime
            {
                for (int j = i + i; j <= limit; j += i)
                {
                    candidates[j] = false;
                }
            }
        }
        return (from n in candidates.Index()
                where n.Item1 >= 2 && n.Item2 == true
                select n.Item1).ToArray<int>();
    }
}