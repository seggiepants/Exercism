using System.Text;

public static class House
{
    // The unique data per verse.
    static Dictionary<int, Tuple<string, string>> nounVerb = new Dictionary<int, Tuple<string, string>>()
    {
        [1] = new Tuple<string, string>("house that Jack built.", ""),
        [2] = new Tuple<string, string>("malt", "lay in"),
        [3] = new Tuple<string, string>("rat", "ate"),
        [4] = new Tuple<string, string>("cat", "killed"),
        [5] = new Tuple<string, string>("dog", "worried"),
        [6] = new Tuple<string, string>("cow with the crumpled horn", "tossed"),
        [7] = new Tuple<string, string>("maiden all forlorn", "milked"),
        [8] = new Tuple<string, string>("man all tattered and torn", "kissed"),
        [9] = new Tuple<string, string>("priest all shaven and shorn", "married"),
        [10] = new Tuple<string, string>("rooster that crowed in the morn", "woke"),
        [11] = new Tuple<string, string>("farmer sowing his corn", "kept"),
        [12] = new Tuple<string, string>("horse and the hound and the horn", "belonged to"),
    };

    /// <summary>
    /// Build the House that Jack built poem starting from the given verse number.
    /// 
    /// Sorry, not going to do recursion when a loop works better.
    /// I need a bit of state from the previous run (the verb), and it is easier as a loop.
    /// </summary>
    /// <param name="verseNumber">The line of the poem to start from</param>
    /// <returns>String with all of the verses starting at verse number and working its
    /// way down to verse 1
    /// </returns>
    public static string Recite(int verseNumber)
    {
        Tuple<string, string>? current;
        bool hasCurrent = nounVerb.TryGetValue(verseNumber, out current);
        bool isFirst = true;
        StringBuilder sb = new();
        string verb = "";
        if (!hasCurrent || current == null)
        {
            return "";
        }
        else
        {
            sb.Clear();
            while (hasCurrent && current != null)
            {
                if (isFirst)
                {
                    sb.Append($"This is the {current.Item1}");
                }
                else
                {
                    if (sb.Length > 0)
                        sb.Append(" ");
                    sb.Append($"that {verb} the {current.Item1}");
                }
                verb = current.Item2;
                isFirst = false;
                verseNumber--;
                hasCurrent = nounVerb.TryGetValue(verseNumber, out current);
            }
            return sb.ToString();
        }

    }

    // Just iterate from start to end and glue together with new lines
    /// <summary>
    /// Return a new line delimited string of the house that Jack built poem starting with successively larger start verses.
    /// </summary>
    /// <param name="startVerse">The verse to start on. Should be a valid verse number (1-12).</param>
    /// <param name="endVerse">What verse to end on. Should be greater or equal to startVerse and a valid verse number (1-12)</param>
    /// <returns></returns>
    public static string Recite(int startVerse, int endVerse)
    {
        StringBuilder sb = new();
        sb.Append(Recite(startVerse));
        for (int i = startVerse + 1; i <= endVerse; i++)
        {
            sb.Append("\n" + Recite(i));
        }
        return sb.ToString();
    }
}