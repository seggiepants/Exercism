public static class ResistorColorDuo
{
    public static Dictionary<string, int> lookup = new()
    {
        ["black"] = 0,
        ["brown"] = 1,
        ["red"] = 2,
        ["orange"] = 3,
        ["yellow"] = 4,
        ["green"] = 5,
        ["blue"] = 6,
        ["violet"] = 7,
        ["grey"] = 8,
        ["white"] = 9,
    };
    public static int Value(string[] colors)
    {
        int ret = 0;
        int matched = 0;
        foreach (string color in colors)
        {
            string clr = color.ToLowerInvariant().Trim();
            if (lookup.ContainsKey(clr))
            {
                matched++;
                ret = 10 * ret + lookup[clr];
            }
            if (matched >= 2)
                break;
        }
        return ret;
    }
}
