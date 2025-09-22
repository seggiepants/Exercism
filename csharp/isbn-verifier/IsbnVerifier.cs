using System.Diagnostics.CodeAnalysis;

public static class IsbnVerifier
{
    public static bool IsValid(string number)
    {
        const int EXPECTED_LENGTH = 9; // Length not counting check digit.
        const int MOD_VALUE = 11;

        string digits = number.Trim().ToLowerInvariant();
        digits = digits.Replace("-", "");

        // Too many/few characters? Should just be check digit and 9 digits
        if (digits.Length != (EXPECTED_LENGTH + 1))
            return false;

        // Grab check digit, make sure it is x or 0-9
        char checkDigit = digits[digits.Length - 1];
        if (!Char.IsDigit(checkDigit) && checkDigit != 'x')
            return false;

        // Remove any non-digits. Make sure the correct length afterward.
        digits = new string((from c in digits.Substring(0, digits.Length - 1) where Char.IsDigit(c) select c).ToArray<char>());
        if (digits.Length != EXPECTED_LENGTH)
            return false;

        // Sum up the digits with their reverse counter.
        int total = (from pair in Enumerable.Index(digits)
                     where (pair.Item == 'x' && pair.Index == (number.Length - 1)) || Char.IsDigit(pair.Item)
                     select (10 - pair.Index) * (pair.Item == 'x' ? 10 : (int)pair.Item - (int)'0')).Sum();

        // Special case. If 0 and 10 work make sure we use 0 for the check digit.
        if (total % MOD_VALUE == 0 && checkDigit != '0')
            return false;

        // Calculate the check digit's value and then use it to check if a valid ISBN.
        int checkInt = checkDigit == 'x' ? 10 : (int)checkDigit - (int)'0';
        return (checkInt + total) % MOD_VALUE == 0;
    }
}