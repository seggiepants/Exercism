using System.Text.RegularExpressions;

public class LogParser
{
    public bool IsValidLine(string text)
    {
        Regex re = new(@"^\[(TRC|DBG|INF|WRN|ERR|FTL)\].*$");
        return re.IsMatch(text);        
    }

    public string[] SplitLogLine(string text)
    {
        Regex re = new(@"\<(\^|\*|=|-)+\>");

        List<string> results = re.Split(text).ToList<String>();
        return results.Where((value, count) => count % 2 == 0).ToArray<string>();
    }

    public int CountQuotedPasswords(string lines)
    {
        Regex re = new(@""".*password.*""", RegexOptions.IgnoreCase);
        MatchCollection mc = re.Matches(lines);
        return mc.Count();
    }

    public string RemoveEndOfLineText(string line)
    {
        Regex re = new(@"end-of-line\d+");
        return re.Replace(line, "");
    }

    public string[] ListLinesWithPasswords(string[] lines)
    {
        Regex re = new(@"password\w+", RegexOptions.IgnoreCase);
        List<string> results = new();
        foreach (string line in lines)
        {
            Match m = re.Match(line);
            if (m.Captures.Count > 0)
            {
                results.Add($"{m.Value}: {line}");
            }
            else
            {
                results.Add($"--------: {line}");
            }
        }
        return results.ToArray<string>();
    }
}
