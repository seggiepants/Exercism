public static class PigLatin
{
    static string vowels = "aeiou";
    static string consonants = "bcdfghjklmnpqrstvwxyz";
    public static string Translate(string word)
    {
        string[] parts = word.ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        List<string> ret = new();

        foreach (string part in parts)
        {
            int indexQu = part.IndexOf("qu");
            int indexY = part.IndexOf("y");
            var indexVowels = (from char vowel in vowels
                               where part.IndexOf(vowel) >= 0
                               select part.IndexOf(vowel)).ToList<int>();
            int indexFirstVowel = indexVowels.Count > 0 ? indexVowels.Min() : -1;
            if (part.StartsWith("xr") || part.StartsWith("yt") || indexFirstVowel == 0)
            {
                // Rule 1
                ret.Add($"{part}ay");
            }
            else if (consonants.Contains(part[0]))
            {
                int index = indexFirstVowel;

                if (indexQu >= 0 && indexQu < indexFirstVowel && indexFirstVowel >= 0)
                {
                    index = indexQu + 2;
                }
                else if (indexY >= 1 && (indexFirstVowel < 0 || indexY < indexFirstVowel))
                {
                    index = indexY;
                }
                ret.Add($"{part.Substring(index)}{part.Substring(0, index)}ay");
            }
            else
                ret.Add(part);
        }
        return String.Join(' ', ret);
    }
}