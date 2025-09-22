public static class IsbnVerifier
{
    public static bool IsValid(string number)
    {
        const int MOD_VALUE = 11;

        long index = 10; // Should be 10 digits including check digit.
        long total = 0;  // Running total.
        
        foreach (char c in number)
        {
            if (Char.IsDigit(c))
            {
                total += index * ((int)c - (int)'0');
                index--;
            }
            else if ((c == 'x' || c == 'X') && index == 1)
            {
                // Don't use 10 if 0 works.
                if (total % MOD_VALUE == 0)
                    return false;

                total += 10;
                index--;
            }
            else if (!Char.IsWhiteSpace(c) && c != '-')
            {
                // Ignore white space, and dash.
                // return false for anything else.
                return false;
            }
        }

        // If too short/long return false;
        if (index != 0)
            return false;

        // ISBN if total mod 11 is zero.
        return total % MOD_VALUE == 0;
    }
}