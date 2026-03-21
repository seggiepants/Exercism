using System.Net.NetworkInformation;

public static class LineUp
{
    public static string Format(string name, int number)
    {
        int lastDigit = number % 10;
        string suffix;
        if ((lastDigit == 1) && (number % 100 != 11))
            suffix = "st";
        else if ((lastDigit == 2) && (number % 100 != 12))
            suffix = "nd";
        else if ((lastDigit == 3) && (number % 100 != 13))
            suffix = "rd";
        else 
            suffix = "th";

        return $"{name}, you are the {number}{suffix} customer we serve today. Thank you!";
    }
}
