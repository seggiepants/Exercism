public static class ResistorColor
{
    // Sorry, I already made a dictionary before I read that you want this in an array.
    // I like dictionary better for lookup so I kept it.
    static readonly Dictionary<string, int> ColorLookup = new Dictionary<string, int>()
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
        ["white"] = 9
    };
    public static int ColorCode(string color)
    {
        return ColorLookup.GetValueOrDefault<string, int>(color, -1);
    }

    public static string[] Colors()
    {
        return [.. ColorLookup.Keys]; // vs code suggested this syntax, I don't like it.
    }
}