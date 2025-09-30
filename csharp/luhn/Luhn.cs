public static class Luhn
{
    public static bool IsValid(string number)
    {
        // Spaces should be removed.
        string value = number.Replace(" ", "");

        // Check if too short.
        if (value.Length <= 1)
            return false;

        // depending on if length % 2 == 1 or 0  double on even or odd chars
        // want 2nd from last to be doubled.
        int doubleIt = value.Length % 2;
        int sum = 0;
        for (int i = 0; i < value.Length; i++)
        {
            char ch = value[i];
            // Non-Digit characters are not allowed.
            if (!Char.IsDigit(ch))
                return false;

            // Convert to number.
            int temp = (int)(value[i] - '0');
            // Double if required
            if (i % 2 == doubleIt)
            {
                temp *= 2;
                // Subtract 9 if >= 10
                if (temp >= 10)
                    temp -= 9;
            }
            // Total all the digits.
            sum += temp;
        }
        // True if total is evenly divisible by 10.
        return sum % 10 == 0;
    }
}