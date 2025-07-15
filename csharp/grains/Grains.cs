public static class Grains
{
    public static ulong Square(int n)
    {
        if (n < 1 || n > 64)
            throw new ArgumentOutOfRangeException("Only 1 based square numbers from 1 to 64 are accepted.");
        return (ulong)Math.Pow(2, n - 1);
    }

    public static ulong Total()
    {
        ulong total = 0;
        for (int i = 1; i <= 64; i++)
        {
            total += Square(i);
        }
        return total;
    }
}