using System.Text;
using System.Text.RegularExpressions;

/*
* Change List:
* - IsTag not used and removed.
* - Wrap, SearchReplace uses string interpolation
* - Rename Parse__ -> ParseStrong, Parse_ -> ParseEm, Parse(string, string, string) -> SearchReplace
* - Parse: For loop to Foreach loop, list flag to isList, use a string builder in that one too.
* - var to named types.
* - += 1 to ++.
* - While Loop for # counting changed to a TakeWhile LINQ expression.
* - Setting inLoopAfter to false on both sides of an if/else moved out of if.
* - ParseLine use null coalescing ?? instead of checking for null each time.
* - if/else that checks if in a list or not replaced with a ternary if that sets a prefix to </ul> or nothing that is added to the result.
* - substring to range operator.
*/
public static class Markdown
{
    private static string Wrap(string text, string tag) => $"<{tag}>{text}</{tag}>";

    private static string SearchReplace(string markdown, string delimiter, string tag)
    {
        var pattern = $"{delimiter}(.+){delimiter}";
        var replacement = $"<{tag}>$1</{tag}>";
        return Regex.Replace(markdown, pattern, replacement);
    }

    private static string ParseStrong(string markdown) => SearchReplace(markdown, "__", "strong");

    private static string ParseEm(string markdown) => SearchReplace(markdown, "_", "em");

    private static string ParseText(string markdown, bool inList)
    {
        string parsedText = ParseEm(ParseStrong((markdown)));
        return inList ? parsedText : Wrap(parsedText, "p");
    }

    private static string? ParseHeader(string markdown, bool inList, out bool inListAfter)
    {
        var count = markdown.TakeWhile<char>(ch => ch == '#').Count();

        if (count == 0 || count > 6)
        {
            inListAfter = inList;
            return null;
        }

        inListAfter = false;
        string prefix = inList ? "</ul>" : "";
        return prefix + Wrap(markdown[(count + 1)..], $"h{count}");
    }

    private static string? ParseLineItem(string markdown, bool inList, out bool inListAfter)
    {
        if (markdown.StartsWith("*"))
        {
            string innerHtml = Wrap(ParseText(markdown[2..], true), "li");

            inListAfter = true;
            string prefix = inList ? "" : "<ul>";
            return prefix + innerHtml;
        }

        inListAfter = inList;
        return null;
    }

    private static string ParseParagraph(string markdown, bool inList, out bool inListAfter)
    {
        string prefix = inList ? "</ul>" : "";
        inListAfter = false;
        return prefix + ParseText(markdown, false);
    }

    private static string ParseLine(string markdown, bool list, out bool inListAfter)
    {
        string? result = ParseHeader(markdown, list, out inListAfter) ??
            ParseLineItem(markdown, list, out inListAfter) ??
            ParseParagraph(markdown, list, out inListAfter);

        if (result == null)
        {
            throw new ArgumentException("Invalid markdown");
        }

        return result;
    }

    public static string Parse(string markdown)
    {
        StringBuilder result = new();
        bool isList = false;

        foreach (string line in markdown.Split('\n'))
        {
            result.Append(ParseLine(line, isList, out isList));
        }

        if (isList)
            result.Append("</ul>");
        
        return result.ToString();        
    }
}