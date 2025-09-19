public static class AffineCipher
{
    const int LENGTH_OF_ALPHABET = 26;

    public static string Encode(string plainText, int a, int b)
    {
        // If a and b are co-prime the Bezout coeffients are the 
        // modular multiplicitive inverse of a mod b, then b mod a
        (int bezoutX, int bezoutY, int gcd, int t, int s) = extended_gcd(a, LENGTH_OF_ALPHABET);
        // If GCD != 1 they are not co-prime, throw an error.
        if (gcd != 1)
            throw new ArgumentException($"{a} is not co-prime to {LENGTH_OF_ALPHABET}.");

        return String.Join(" ", (from char c in plainText.ToLowerInvariant()
                                 where Char.IsLetterOrDigit(c)
                                 select Char.IsLetter(c) ? Encrypt(a, c, b) : c).ToArray<char>().Chunk<char>(5).Select(c => new string(c)).ToArray<string>());
    }

    public static string Decode(string cipheredText, int a, int b)
    {
        // If a and b are co-prime the Bezout coeffients are the 
        // modular multiplicitive inverse of a mod b, then b mod a
        (int bezoutX, int bezoutY, int gcd, int t, int s) = extended_gcd(a, LENGTH_OF_ALPHABET);
        // If GCD != 1 they are not co-prime, throw an error.
        if (gcd != 1)
            throw new ArgumentException($"{a} is not co-prime to {LENGTH_OF_ALPHABET}.");
        
        return new string((from char c in cipheredText.ToLowerInvariant()
                           where Char.IsLetterOrDigit(c)
                           select Char.IsLetter(c) ? Decrypt(bezoutX, c, b) : c).ToArray<char>());
    }

    private static char Encrypt(int a, char c, int b, int m = LENGTH_OF_ALPHABET)
    {
        int i = CharToInt(c); 
        return IntToChar((a * i + b) % m);
    }

    private static char Decrypt(int bezoutX, char c, int b, int m = LENGTH_OF_ALPHABET)
    {
        int y = (int)c - (int)'a';
        return IntToChar((bezoutX * (y - b)) % m);
    }

    /// <summary>
    /// Find the Extended GCD of two number. The function returns
    /// not just that but the Bezout coeffients and the quotients
    /// by the greatest common divisor.
    /// https://en.wikipedia.org/wiki/Modular_multiplicative_inverse
    /// https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm
    /// </summary>
    /// <param name="a">First co-prime</param>
    /// <param name="b">Second co-prime</param>
    /// <returns>
    /// First two numbers are the Bézout coefficients (x, y)
    /// third number is Greatest Common Divisor
    /// Last two are quotients by the Greatest Common Divisor (t, s)
    /// </returns>
    private static (int, int, int, int, int) extended_gcd(int a, int b)
    {
        int old_r = a;
        int r = b;
        int old_s = 1;
        int s = 0;
        int old_t = 0;
        int t = 1;

        while (r != 0)
        {
            int quotient = old_r / r;
            (old_r, r) = (r, old_r - (quotient * r));
            (old_s, s) = (s, old_s - (quotient * s));
            (old_t, t) = (t, old_t - (quotient * t));
        }
        return (old_s, old_t, old_r, t, s);
    }

    static char IntToChar(int a)
    {
        // For negative numbers add the alphabet length back in
        // to bring it back into range.
        while (a < 0)
            a += LENGTH_OF_ALPHABET;
        return (char)((int)'a' + a);
    }

    static int CharToInt(char a)
    {
        return (int)((int)a - (int)'a');
    }
}
