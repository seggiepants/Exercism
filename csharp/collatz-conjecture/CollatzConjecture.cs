public static class CollatzConjecture
{
    public static int Steps(int number)
    {
        int current = number;
        int steps = 0;
        if (current <= 0)
            throw new ArgumentOutOfRangeException("Number must be a positive integer greater than or equal to 1.");
            
        while (current != 1)
        {
            if (current % 2 == 0)
            {
                // Even divide by 2.
                current /= 2;
            }
            else
            {
                // Odd Multiply by 3 and add 1.
                current = (3 * current) + 1;
            }
            steps++;
        }
        return steps;
    }
}