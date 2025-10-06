public static class RunLengthEncoding
{
    public static string Encode(string input)
    {
        int count = 0;
        char lastChar = ' ';
        bool first = true;
        string ret = "";
        foreach(char c in input)
        {
            if (first)
            {
                first = false;
                lastChar = c;
                count = 1;
            }
            else if (c == lastChar)
            {
                count++;
            }
            else
            {
                if (count == 1)
                    ret += lastChar;
                else
                    ret += $"{count}{lastChar}";

                lastChar = c;
                count = 1;                
            }
        }
        if (count > 0)
        {
            if (count == 1)
                ret += lastChar;
            else
                ret += $"{count}{lastChar}";
        }
        return ret;
    }

    public static string Decode(string input)
    {
        string ret = "";
        int count = 0;

        foreach (char c in input)
        {
            if (Char.IsDigit(c))
            {
                count *= 10;
                count += (int)c - (int)'0';
            }
            else
            {
                if (count == 0)
                    count = 1;
                ret += new string(c, count);
                count = 0;
            }
        }
        return ret;
    }
}
