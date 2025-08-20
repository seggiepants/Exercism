public class Matrix
{
    int[][]? values;
    public Matrix(string input)
    {
        values = (from row in input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    where row.Length > 0
                    select (from value in row.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                            where value.Length > 0
                            select Convert.ToInt32(value)).ToArray<int>()).ToArray();
    }

    public int[] Row(int row)
    {
        if (values != null && row <= values.Length && values[row - 1] != null)
            return values[row - 1];
        throw new IndexOutOfRangeException();
    }

    public int[] Column(int col)
    {
        if (values != null && col <= values[0].Length)
            return (from row in values
                    select row[col - 1]).ToArray<int>();
        throw new IndexOutOfRangeException();
    }
}