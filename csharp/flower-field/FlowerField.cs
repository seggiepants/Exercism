using System.Globalization;

public static class FlowerField
{
    const char blank = ' ';
    const char flower = '*';
    private static int GetCount(string[] input, int x, int y)
    {
        int minX = Math.Max(0, x - 1);
        int minY = Math.Max(0, y - 1);
        int countX = Math.Min(input[0].Length - 1, x + 1) - minX + 1;
        int countY = Math.Min(input.Length - 1, y + 1) - minY + 1;
        
        return (from j in Enumerable.Range(minY, countY)
                select (from i in Enumerable.Range(minX, countX)
                        where (i != x || j != y) && input[j][i] == flower
                        select 1).Sum()).Sum();
    }

    private static char nextState(string[] input, int x, int y)
    {
        if (input[y][x] == flower)
            return flower;
        int count = GetCount(input, x, y);
        if (count <= 0)
            return blank;
        return (char)((int)'0' + count);

    }

    public static string[] Annotate(string[] input)
    {
        return (from row in input.Index()
         select new string((from ch in row.Item.Index()
                            select nextState(input, ch.Index, row.Index)).ToArray<char>())).ToArray<string>();
    }
}
