using System.Security.Cryptography;

public static class CryptoSquare
{
    public static string NormalizedPlaintext(string plaintext)
    {
        return String.Join("", (from ch in plaintext
                 where !Char.IsPunctuation(ch) && !Char.IsWhiteSpace(ch)
                 select Char.ToLowerInvariant(ch)));
    }

    public static IEnumerable<string> PlaintextSegments(string plaintext)
    {
        string text = NormalizedPlaintext(plaintext);
        if (text.Length == 0)
            return new string[] { "" };
        int columns = Convert.ToInt32(Math.Ceiling(Math.Sqrt(text.Length)));
        int rows = text.Length / columns;
        if (columns * rows < text.Length)
            rows++; // Round up for fractions.
        return (from chars in text.PadRight(columns * rows, ' ').Chunk(columns)
                    select new string(chars));
    }

    public static string Encoded(string plaintext)
    {
        string[] segments = PlaintextSegments(plaintext).ToArray<string>();
        int columns = segments[0].Length;
        
        return String.Join('\n', (from i in Enumerable.Range(0, columns)
                 select new string((from segment in segments
                                    select segment[i]).ToArray<char>())));        
    }

    public static string Ciphertext(string plaintext)
    {
        return Encoded(plaintext).Replace('\n', ' ');
    }
}