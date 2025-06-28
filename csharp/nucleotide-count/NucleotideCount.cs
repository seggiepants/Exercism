public static class NucleotideCount
{
    public static IDictionary<char, int> Count(string sequence)
    {
        Dictionary<char, int> letters = new()
        {
            ['A'] = 0,
            ['C'] = 0,
            ['G'] = 0,
            ['T'] = 0,
        };
        foreach (char c in sequence)
        {
            if (letters.ContainsKey(c))
                letters[c] = letters[c] + 1;
            else
            {
                throw new ArgumentException("Only A, C, G, or T are accepted values.");
            }
        }
        return letters;
    }
}