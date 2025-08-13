public static class Etl
{
    public static Dictionary<string, int> Transform(Dictionary<int, string[]> old)
    {
        Dictionary<string, int> ret = new();
        foreach (KeyValuePair<int, string[]> entry in old)
        {
            foreach (string letter in entry.Value)
            {
                ret[letter.ToLowerInvariant()] = entry.Key;
            }
        }
        return ret;
    }
}