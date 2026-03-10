public static class KillerSudokuHelper
{
    public static IEnumerable<int[]> Combinations(int sum, int size, int[] exclude)
    {
        List<int[]> results = new();
        bool success = getDigits(new List<int>(), sum, size, new List<int>(exclude), results);
        if (!success)
            throw new Exception("No solution found.");
        return results;
    }

    private static bool getDigits(List<int>digits, int sum, int size, List<int> exclude, List<int[]> results)
    {
        int subTotal = digits.Sum();
        int maxDigit = digits.Count > 0 ? digits.Max() : 0;
        if (digits.Count == size)
        {
            if (subTotal == sum)
            {
                results.Add(digits.ToArray<int>());
                return true;
            }
            else 
                return false;
        }

        if (subTotal > sum)
            return false;

        int[] available = (from int digit in new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9}
            where (!exclude.Contains(digit) && subTotal + digit <= sum && digit > maxDigit)
            select digit).ToArray<int>();

        for(int i = 0; i < available.Length; i++)
        {
            List<int> nextDigits = new(digits);
            nextDigits.Add(available[i]);
            List<int> nextExclude = new(exclude);
            nextExclude.Add(available[i]);
            getDigits(nextDigits, sum, size, nextExclude, results);
        }
        return results.Count > 0;
    }
}
