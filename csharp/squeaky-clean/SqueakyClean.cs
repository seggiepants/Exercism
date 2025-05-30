using System.Text;

public static class Identifier
{
    public static string Clean(string identifier)
    {
        // Rewriting. I guess you can't do passes. You have to do all of the checks at once
        // Hence why '_' passes through. I think that is poor specification. 
        // They list it in sections but you are supposed to do it all at once.

        // Replace spaces with underscores.
        StringBuilder sb = new();
        char previous = ' '; // don't trip over uninitialized use.

        foreach (char c in identifier)
        {
            if (c == ' ')
            {
                sb.Append('_');
            }
            else if (Char.IsControl(c))
            {
                sb.Append("CTRL");
            }
            else if (previous == '-' && Char.IsLetter(c) && Char.IsLower(c))
            {
                sb.Append(Char.ToUpperInvariant(c));
            }
            else if (Char.IsLetter(c))
            {
                if (!Char.IsBetween(c, 'α', 'ω'))
                    sb.Append(c);
            }
            previous = c;
        }

        return sb.ToString();
    }
}
