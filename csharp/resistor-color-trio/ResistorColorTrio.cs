public static class ResistorColorTrio
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
    public static string Label(string[] colors)
    {
        long ret = 0;
        string measurement = "ohms";

        for (int i = 0; i < colors.Length; i++)
        {
            string clr = colors[i].ToLowerInvariant().Trim();
            if (lookup.ContainsKey(clr))
            {
                if (i == 2)
                {
                    ret *= (long)Math.Pow(10, lookup[clr]);
                    break;
                }
                else
                {
                    ret = 10 * ret + lookup[clr];
                }
            }
        }
        if (ret >= Math.Pow(10, 9))
        {
            ret /= (long)Math.Pow(10, 9);
            measurement = "gigaohms";
        }
        else if (ret >= Math.Pow(10, 6))
        {
            ret /= (long)Math.Pow(10, 6);
            measurement = "megaohms";
        }
        else if (ret >= Math.Pow(10, 3))
        {
            ret /= (long)Math.Pow(10, 3);
            measurement = "kiloohms";
        }

        
        return $"{ret} {measurement}";
    }
}
