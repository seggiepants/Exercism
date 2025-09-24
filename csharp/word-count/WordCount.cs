using System.Text.RegularExpressions;
public static class WordCount
{
    public static IDictionary<string, int> CountWords(string phrase)
    {
        const string re = @"[\w|\']+";
        Dictionary<string, int> ret = new();

        Match m = Regex.Match(phrase, re);
        while (m.Success)
        {
            string entry = StripQuotes(m.Value.ToLowerInvariant());
            if (entry.Length > 0)
                ret[entry] = ret.GetValueOrDefault<string, int>(entry, 0) + 1;
            m = m.NextMatch();
        }

        return ret;
    }

    static string StripQuotes(string value)
    {
        string ret = value;
        while (ret.StartsWith('\'') || ret.StartsWith('"'))
        {
            ret = ret.Substring(1);
        }
        while (ret.EndsWith('\'') || ret.EndsWith('"'))
        {
            ret = ret.Substring(0, ret.Length - 1);
        }
        return ret;
    }
}