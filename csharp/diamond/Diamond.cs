using System.Text;

public static class Diamond
{
    const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static string Make(char target)
    {
        char middle = Char.ToUpperInvariant(target);
        int index = alphabet.IndexOf(middle);
        if (index < 0)
            throw new ArgumentException("target should be in the range of A-Z.");

        int totalWidth = 1 + (2 * index);
        StringBuilder sb = new();
        bool first = true;
        foreach (int i in Enumerable.Range(0, index + 1).Concat(Enumerable.Range(0, Math.Max(0, index)).Reverse()))
        {
            int sidePad = index - i;
            int middlePad = (2 * i) - 1;
            string newLine = "\n";
            if (first)
            {
                newLine = "";
                first = false;
            }
            if (i == 0)
                sb.Append(newLine + new string(' ', sidePad) + alphabet[i].ToString() + new string(' ', sidePad));
            else
                sb.Append(newLine + new string(' ', sidePad) + alphabet[i].ToString() + new string(' ', middlePad) + alphabet[i].ToString() + new string(' ', sidePad));
        }
        return sb.ToString();        
    }
}