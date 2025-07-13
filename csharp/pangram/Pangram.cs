public static class Pangram
{
    public static bool IsPangram(string input)
    {
        Dictionary<char, int> letterCount = new();
        for (char letter = 'a'; letter <= 'z'; letter++)
        {
            letterCount[letter] = 0;
        }

        foreach (char letter in input)
        {
            char c = Char.ToLowerInvariant(letter);
            if (letterCount.ContainsKey(c))
                letterCount[c] += 1;
        }

        return (from KeyValuePair<char, int> pair in letterCount
                where pair.Value == 0
                select pair.Key).Count() == 0;
    }
}
