

public static class Isogram
{
    public static bool IsIsogram(string word)
    {
        return word.ToLowerInvariant().CountBy(c => c).Where(c => c.Value > 1 && Char.IsLetter(c.Key)).Count() == 0;        
    }
}
