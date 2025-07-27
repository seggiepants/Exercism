public static class AllYourBase
{
    public static int[] Rebase(int inputBase, int[] inputDigits, int outputBase)
    {
        if ((inputBase <= 1) || (outputBase <= 1))
            throw new ArgumentException("Base must be 2 or higher.");
        
        List<int> output = new();
        int accumulator = 0;
        for (int index = 0; index < inputDigits.Length; index++)
        {
            if ((inputDigits[index] < 0) || (inputDigits[index] >= inputBase))
                throw new ArgumentException("Individual digit may not be less than zero.");
            int exponent = inputDigits.Length - index - 1;
            accumulator += (int)Math.Pow(inputBase, exponent) * inputDigits[index];
        }
        if (accumulator == 0)
        {
            output.Add(0);
        }
        else
        {
            while (accumulator != 0)
            {
                int current = accumulator % outputBase;
                accumulator -= current;
                accumulator /= outputBase;
                output.Add(current);
            }
        }
        return output.Reverse<int>().ToArray<int>();
    }
}