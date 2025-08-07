public static class Proverb
{
    public static string[] Recite(string[] subjects)
    {
        if (subjects.Length < 1)
            return [];

        List<string> lines = new();
        string? previous = null;
        string first = subjects[0];
        foreach (string current in subjects)
        {
            if (previous != null)
            {
                lines.Add($"For want of a {previous} the {current} was lost.");
            }
            previous = current;
        }
        lines.Add($"And all for the want of a {first}.");

        return lines.ToArray<string>();
    }
}