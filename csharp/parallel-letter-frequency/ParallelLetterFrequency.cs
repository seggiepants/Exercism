public static class ParallelLetterFrequency
{
    public static Dictionary<char, int> Calculate(IEnumerable<string> texts)
    {
        object lockObj = new object();
        Dictionary<char, int> ret = new();

        // Here is your parallel should be up to a thread per entry in text.
        Parallel.ForEach(texts, text =>
        {
            foreach (char c in text.ToLowerInvariant())
            {
                if (Char.IsLetter(c))
                {
                    // Need to lock or the dictionary can be corrupted.
                    lock (lockObj)
                    {
                        if (ret.ContainsKey(c))
                            ret[c] += 1;
                        else
                            ret.Add(c, 1);
                    }
                }
            }
        });

        return ret;
    }
}