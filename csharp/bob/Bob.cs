using System.Linq;

public static class Bob
{
    public static string Response(string statement)
    {
        statement = statement.Trim();
        bool isQuestion = statement.EndsWith('?');
        bool hasLetters = statement.Where(c => Char.IsLetter(c)).Count() > 0;
        bool isYelling = hasLetters && statement.Equals(statement.ToUpperInvariant());
        if (statement.Length == 0)
        {
            return "Fine. Be that way!";
        }
        else if (isYelling && isQuestion)
        {
            return "Calm down, I know what I'm doing!";
        }
        else if (isYelling)
        {
            return "Whoa, chill out!";
        }
        else if (isQuestion)
        {
            return "Sure.";
        }

        return "Whatever.";
    }
}