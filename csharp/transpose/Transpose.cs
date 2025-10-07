public static class Transpose
{
    public static string String(string input)
    {
        string[] rows = input.Split('\n');
        int rowMax = (from string row in rows select row.Length).Max<int>();
        List<string> output = new();
        for (int j = 0; j < rowMax; j++)
        {
            var nextRow = (from string row in rows
                               select j < row.Length ? row[j] : (char)0);
            if (nextRow != null)
            {
                output.Add(new string(nextRow.ToArray<char>()).TrimEnd((char)0).Replace((char)0, ' ')); 
            }
        }
        return string.Join('\n', output);
    }
}