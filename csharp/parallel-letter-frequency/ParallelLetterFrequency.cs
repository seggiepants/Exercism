using System.Data;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.Encodings.Web;

using Newtonsoft.Json.Linq;

using Xunit.Internal;

public static class ParallelLetterFrequency
{
    public static Dictionary<char, int> Calculate(IEnumerable<string> texts)
    {
        /*
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

        */
        
        // It looks like I am supposed to do this instead from community solutions.
        // Is Parallel.ForEach not good enough?
        return texts.AsParallel()
        .SelectMany(ch => ch.ToLowerInvariant())
        .Where(c => Char.IsLetter(c))
        .GroupBy(c => c)
        .ToDictionary(c => c.Key, c => c.Count());               
    }
}