public static class OcrNumbers
{
    static string[] digits = [
    " _     _  _     _  _  _  _  _ ", //
    "| |  | _| _||_||_ |_   ||_||_|", // decimal numbers.
    "|_|  ||_  _|  | _||_|  ||_| _|", //
    "                              "  // fourth line is always blank
        ];
    
    public static string Convert(string input)
    {
        List<string> output = new();
        string[] rows = input.Split('\n');
        string[] data = [
            "   ",
            "   ",
            "   ",
            "   "];

        if (!inputOK(rows))
        {
            return "?"; // Shouldn't actually get to the return ?. Will throw an error if something bad is found.
        }

        for (int row = 0; row < rows.Length; row += 4)
        {
            string line = "";
            for (int col = 0; col < rows[row].Length; col += 3)
            {
                data[0] = rows[row].Substring(col, 3);
                data[1] = rows[row + 1].Substring(col, 3);
                data[2] = rows[row + 2].Substring(col, 3);
                data[3] = rows[row + 3].Substring(col, 3);
                line += ConvertDigit(data);
            }
            output.Add(line);
        }

        return String.Join(',',output);
    }

    public static string ConvertDigit(string[] rows)
    {
        for (int col = 0; col < digits[0].Length; col += 3)
        {
            if ((digits[0].Substring(col, 3) == rows[0]) &&
            (digits[1].Substring(col, 3) == rows[1]) &&
            (digits[2].Substring(col, 3) == rows[2]) &&
            (digits[3].Substring(col, 3) == rows[3]))
                return (col / 3).ToString();
        }
        return "?";
    }

    public static bool inputOK(string[] rows)
    {
        // Test 3x4 with an empty row 4        
        if (rows.Length < 4 || rows.Length % 4 != 0)
            throw new ArgumentException($"Incorrect row count, expected multiple of 4, got {rows.Length}.");

        int badLastRowCount = (from pair in rows.Index()
                           where pair.Index > 0 && pair.Index % 4 == 3 && pair.Item.Trim() != ""
                           select 1).Count();
        if (badLastRowCount != 0)
            throw new ArgumentException($"Found {badLastRowCount} instances where the last row should be blank but was not.");

        int badColCount = (from string row in rows
                           where row.Length > 0 && row.Length % 3 != 0
                           select 1).Count();
        if (badColCount > 0)
            throw new ArgumentException($"Digits should be three characters long, found {badColCount} row(s) that are not a multiple of 3.");

        return true;
    }
}