
using System.Runtime.CompilerServices;

public static class PythagoreanTriplet
{
    public static IEnumerable<(int a, int b, int c)> TripletsWithSum(int sum)
    {
        for (int x = 1; x < sum; x++)
        {
            for (int y = x + 1; y < sum; y++)
            {
                int z = sum - (x + y); // There can be only one Z. This changes us from O(N^3) to O(N^2)
                if (z < y)
                    continue;
                if (((x * x) + (y * y) == (z * z)) && (x + y + z == sum))
                    yield return (x, y, z);
            }
        }

        // Yes, I can do it in LINQ too. The for loop is 9x faster 
        /*
        return (from int x in Enumerable.Range(1, sum)
                from int y in Enumerable.Range(x + 1, sum - x)
                where ((sum - (x + y) > y) && (x * x) + (y * y) == (sum - (x + y)) * (sum - (x + y)))
                select (x, y, sum - (x + y))
        );
        */

    }
}