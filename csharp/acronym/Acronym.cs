
public static class Acronym
{
    public static string Abbreviate(string phrase)
    {
        char[] delim = { ' ', '\t', '\r', '\n', '-', '_' };
        return String.Join("", (from word in phrase.Split(delim)
                         where word.Trim().Length > 0
                         select word.ToUpperInvariant().Trim()[0]));
    }
}