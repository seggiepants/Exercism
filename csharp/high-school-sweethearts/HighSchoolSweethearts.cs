using System.Globalization;

public static class HighSchoolSweethearts
{
    public static string DisplaySingleLine(string studentA, string studentB)
    {
        return $"{studentA,29} ♡ {studentB,-29}";
    }

    public static string DisplayBanner(string studentA, string studentB)
    {
        string heart = @"     ******       ******
   **      **   **      **
 **         ** **         **
**            *            **
**                         **
**     {0}. {1}.  +  {2}. {3}.     **
 **                       **
   **                   **
     **               **
       **           **
         **       **
           **   **
             ***
              *";
        string[] namesA = studentA.Split(' ');
        string[] namesB = studentB.Split(' ');
        return string.Format(heart, namesA[0].Substring(0, 1), namesA[1].Substring(0, 1), namesB[0].Substring(0, 1), namesB[1].Substring(0, 1));
    }

    public static string DisplayGermanExchangeStudents(string studentA
        , string studentB, DateTime start, float hours)
    {
        CultureInfo german = new("de-DE");
        return String.Format(german, "{0} and {1} have been dating since {2:d} - that's {3:#,#.####} hours", studentA, studentB, start, hours);
    }
}
