public static class Rectangles
{
    // Check if a given rectangle has the correct points and uninterrupted sides.
    // bottom right corner is not guaranteed.
    private static bool isRect(String[] map, int width, int height, int x1, int y1, int x2, int y2)
    {
        const string rowChars = "-+";
        const string colChars = "|+";

        // Check in bounds.
        if (x2 <= x1 || y2 <= y1 ||
            x1 >= width || x2 >= width ||
            x1 < 0 || x2 <= 0 ||
            y1 >= height || y2 >= height
            || y1 < 0 || y2 < 0)
            return false;

        // Check for + at all four corners
        if (map[y1][x1] != '+') return false;
        if (map[y1][x2] != '+') return false;
        if (map[y2][x1] != '+') return false;
        if (map[y2][x2] != '+') return false;

        // Check continous rows.
        for (int i = x1 + 1; i < x2; i++)
        {
            if (!rowChars.Contains(map[y1][i]) || !rowChars.Contains(map[y2][i]))
                return false;
        }

        for (int j = y1 + 1; j < y2; j++)
        {
            if (!colChars.Contains(map[j][x1]) || !colChars.Contains(map[j][x2]))
                return false;
        }

        return true;
    }
    public static int Count(string[] rows)
    {
        // Empty means no rectangles.
        if (rows.Length == 0)
            return 0;

        int width = (from row in rows select row.Length).Max();
        int height = rows.Length;
        // Nice non-ragged array for easy access.
        String[]? map = (from row in rows select row.PadRight(width)).ToArray<string>();

        // Return zero on degenerate cases.
        if (height == 0 || width == 0 || map == null)
            return 0;

        int rectCount = 0;
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if (map[j][i] == '+')
                {
                    // found a corner see if we can make a rectangle.
                    int[] cornerCols = (from pair in map[j].Index()
                                        where pair.Index > i && pair.Item == '+'
                                        select pair.Index).ToArray<int>();
                    int[] cornerRows = (from pair in (from row in map select row[i]).Index()
                                        where pair.Index > j && pair.Item == '+'
                                        select pair.Index).ToArray<int>();
                    foreach(int y in cornerRows)
                    {
                        foreach (int x in cornerCols)
                        {
                            if (isRect(map, width, height, i, j, x, y))
                                rectCount++;
                        }
                    }
                }
            }
        }
        return rectCount;
    }
}