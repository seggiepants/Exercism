public class Anagram
{
    string word { get; set; }
    public Anagram(string baseWord)
    {
        word = baseWord;
    }

    public string[] FindAnagrams(string[] potentialMatches)
    {
        Dictionary<char, int> reference = WordToDict(word);

        return (from match in potentialMatches
                where match.ToLowerInvariant() != word.ToLowerInvariant() && DictionaryEqual(WordToDict(match), reference)
                select match).ToArray<string>();
    }

    private Dictionary<char, int> WordToDict(string word)
    {
        return word.ToLowerInvariant().ToCharArray()
            .GroupBy(ch => ch)
            .Select(g => new KeyValuePair<char, int>(g.Key, g.Count()))
            .ToDictionary<char, int>();
    }

    private bool DictionaryEqual(Dictionary<char, int> a, Dictionary<char, int> b)
    {
        return a.OrderBy(kvp => kvp.Key).SequenceEqual(b.OrderBy(kvp => kvp.Key));
    }
}