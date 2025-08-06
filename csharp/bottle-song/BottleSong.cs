using System.Collections.Generic;
using System.Text;

public static class BottleSong
{

    static Dictionary<int, string> numberLabels = new()
    {
        [0] = "Zero", [1] = "One", [2] = "Two", [3] = "Three", [4] = "Four",
        [5] = "Five", [6] = "Six", [7] = "Seven", [8] = "Eight", [9] = "Nine",
        [10] = "Ten", [11] = "Eleven", [12] = "Twelve", [13] = "Thirteen", [14] = "Fourteen",
        [15] = "Fifteen", [16] = "Sixteen", [17] = "Seventeen", [18] = "Eighteen", [19] = "Nineteen",
        [20] = "Twenty", [30] = "Thirty", [40] = "Forty", [50] = "Fifty", [60] = "Sixty",
        [70] = "Seventy", [80] = "Eighty", [90] = "Nintey", [100] = "Hundred"
    };

    public static IEnumerable<string> Recite(int startBottles, int takeDown)
    {
        for (int i = 0; i < takeDown; i++)
        {
            if (i > 0)
                yield return "";
            
            int bottleNum = startBottles - i;
            string bottle = bottleNum > 1 ? "bottles" : "bottle";
            string bottleNext;
            if (bottleNum - 1 == 0)
                bottleNext = "no green bottles";
            else if (bottleNum - 1 == 1)
                bottleNext = "one green bottle";
            else
                bottleNext = NumberToString(bottleNum - 1).ToLowerInvariant() + " green bottles";

            string firstLine = $"{NumberToString(bottleNum)} green {bottle} hanging on the wall,";
            yield return firstLine;
            yield return firstLine;
            yield return "And if one green bottle should accidentally fall,";
            yield return $"There'll be {bottleNext} hanging on the wall.";

        }
    }

    public static string NumberToString(int number)
    {
        string ret = "";
        int working = 0;
        int remainder = number;
        if (number >= 1000 || number < 0)
            throw new ArgumentException("Out of bounds, only 0-999 are supported.");
        if (number >= 100)
        {
            remainder = number % 100;
            working = (number - remainder) / 100;
            if (ret.Length > 0) ret = ret + " ";
            ret = ret + NumberToString(working) + " " + numberLabels[100];
            number = remainder;
        }

        if (number <= 20)
        {
            if (ret.Length > 0) ret = ret + " ";
            ret = ret + numberLabels[number];
        }
        else
        {
            remainder = number % 10;
            working = number - remainder;
            if (ret.Length > 0) ret = ret + " ";
            ret = ret + numberLabels[working] + " " + numberLabels[remainder];
        }

        return ret;
    }
}
