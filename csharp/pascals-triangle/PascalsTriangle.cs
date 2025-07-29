using System.ComponentModel.DataAnnotations;

public static class PascalsTriangle
{
    public static IEnumerable<IEnumerable<int>> Calculate(int rows)
    {
        List<List<int>> data = new();
        if (rows <= 0)
            return data; // Why isn't this an Argument Exception?

        for (int i = 1; i <= rows; i++)
        {
            List<int> current = new();
            if (i == 1)
            {
                current.Add(1);
            }
            else
            {
                List<int> previous = data[data.Count - 1];
                for (int j = 0; j <= previous.Count; j++)
                {
                    int a = (j - 1) < 0 ? 0 : previous[j - 1];
                    int b = j == previous.Count ? 0 : previous[j];
                    current.Add(a + b);
                }
            }
            data.Add(current);
        }
        return data;
    }

}