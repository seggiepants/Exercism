using System.Globalization;

public static class Series
{
    public static string[] Slices(string numbers, int sliceLength)
    {
        List<string> ret = new();

        if (sliceLength < 1)
            throw new ArgumentException("Invalid slice length, must be greater or equal to 1");
        else if (sliceLength > numbers.Length)
            throw new ArgumentException("Invalid slice length, must be less than or equal to input value");

        for (int i = 0; i <= numbers.Length - sliceLength; i++)
            {
                ret.Add(numbers.Substring(i, sliceLength));
            }

        return ret.ToArray<string>();
    }
}