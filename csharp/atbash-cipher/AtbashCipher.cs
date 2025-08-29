
public static class AtbashCipher
{
    static string reference = "abcdefghijklmnopqrstuvwxyz0123456789";
    static string transposed = "zyxwvutsrqponmlkjihgfedcba0123456789";
    public static string Encode(string plainValue)
    {
        return String.Join(' ', (from char[] ch in (from char c in plainValue.ToLowerInvariant()
                                                    where reference.Contains(c)
                                                    select transposed[reference.IndexOf(c)]).Chunk(5)
                                 select new string(ch) ?? "").ToArray<string>());
    }

    public static string Decode(string encodedValue)
    {
        return new String((from char c in encodedValue.Replace(" ", "")
                 where transposed.Contains(c)
                 select reference[transposed.IndexOf(c)]).ToArray<char>()).ToLowerInvariant();
    }
}
