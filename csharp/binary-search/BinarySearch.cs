public static class BinarySearch
{
    /// <summary>
    /// Binary search an array for a given value.
    /// </summary>
    /// <param name="input">Array to search through for a value. Values must be sorted (ascending) in the array.</param>
    /// <param name="value">The value to search for.</param>
    /// <returns>Index of the desired value. If not found it returns -1. Unspecified behavior if not given a sored array.</returns>
    public static int Find(int[] input, int value)
    {
        int start = 0;
        int stop = input.Length - 1;

        while (stop >= start)
        {
            // single entry.
            if (start - stop == 0)
                return input[start] == value ? start : -1;

            // middle of current range
            int middle = start + ((stop - start + 1) / 2);

            // Found it
            if (input[middle] == value)
                return middle;

            if (input[middle] > value)
            {
                // search the first half
                stop = middle - 1;
            }
            else if (input[middle] < value)
            {
                // search the second half
                start = middle + 1;
            }

        }
        return -1;
    }
}