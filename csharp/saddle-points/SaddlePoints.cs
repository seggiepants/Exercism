using System.Data;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

using Xunit.v3;

public static class SaddlePoints
{
    public static IEnumerable<(int, int)> Calculate(int[,] matrix)
    {
        if (matrix.GetUpperBound(0) < 0 || matrix.GetUpperBound(1) < 0)
            return [];

        // Maximums among rows
        int[] rowLarge = (from int y in Enumerable.Range(0, matrix.GetUpperBound(0) + 1)
                            select (from int x in Enumerable.Range(0, matrix.GetUpperBound(1) + 1)
                                    select matrix[y, x]).Max()).ToArray<int>();

        // Minimums among columns
        int[] colSmall = (from int x in Enumerable.Range(0, matrix.GetUpperBound(1) + 1)
                            select (from int y in Enumerable.Range(0, matrix.GetUpperBound(0) + 1)
                                    select matrix[y, x]).Min()).ToArray<int>();
                                    
        // Cells that match both
        return (from int y in Enumerable.Range(0, matrix.GetUpperBound(0) + 1)
                from int x in Enumerable.Range(0, matrix.GetUpperBound(1) + 1)
                where matrix[y, x] == colSmall[x] && matrix[y, x] == rowLarge[y]
                select (y + 1, x + 1));
    }
}
