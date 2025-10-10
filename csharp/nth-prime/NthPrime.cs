public static class NthPrime
{
    static List<int> primes = new() { 2, 3, 5, 7, 11, 13 };
    public static int Prime(int nth)
    {
        while (primes.Count<int>() < nth)
        {
            bool isPrime = false;
            int current = primes.Last<int>() + 1;
            while(!isPrime)
            {
                isPrime = !primes.Any<int>(prime => current % prime == 0);
                if (isPrime)
                    primes.Add(current);
                else
                    current++;                
            }
        }
        return primes[nth - 1];
    }
}