public static class RomanNumeralExtension
{
    /*
    M|D|C|L|X|V|I
    --|--|--|--|--|--|--
    1000|500|100|50|10|5|1
    */
    public static string ToRoman(this int value)
    {
        string num = "";
        int remaining = value;

        // Yes the remaining checks were largely trial and error.
        //
        // If next in line * 3 > current symbol then use two symbols down
        // instead of just one for the next symbol in line.
        if (remaining >= 800) // CCM is better than DCCC
            num += ToRoman_Helper(remaining, 'M', 1000, 'C', 100, out remaining);
        if (remaining > 300) // CCC better than CCD
            num += ToRoman_Helper(remaining, 'D', 500, 'C', 100, out remaining);
        if (remaining > 80) // XXC is better than CXXX
            num += ToRoman_Helper(remaining, 'C', 100, 'X', 10, out remaining);
        if (remaining > 30) // XXX better than XXL
            num += ToRoman_Helper(remaining, 'L', 50, 'X', 10, out remaining);
        if (remaining > 8) // IIX better than VIII
            num += ToRoman_Helper(remaining, 'X', 10, 'I', 1, out remaining);
        if (remaining > 3) // III better than IIV
            num += ToRoman_Helper(remaining, 'V', 5, 'I', 1, out remaining);
        if (remaining >= 1) // Pad out remainder.
            num = num.PadRight(num.Length + remaining, 'I');

        return num;
    }

    static string ToRoman_Helper(int value, char symbol, int symbolValue, char nextSymbol, int nextSymbolValue, out int remainder)
    {
        int remaining;
        string num;
        int symbolCount;

        // Process one Roman numeral digit. I really should have something to cut it off by three but I think the 
        // filters coming in handle that.
        num = "";
        remaining = value;
        while (symbolValue > remaining)
        {
            num += nextSymbol;
            remaining += nextSymbolValue;
        }

        symbolCount = 0;
        while (remaining >= symbolValue)
        {
            symbolCount++;
            num += symbol;
            remaining -= symbolValue;
        }

        // Wee bit more that will otherwise be degenerate later.
        if (remaining >= symbolValue - nextSymbolValue)
        {
            num += $"{nextSymbol}{symbol}";
            remaining += nextSymbolValue;
            remaining -= symbolValue;
        }

        remainder = remaining;
        return num;
    }
}