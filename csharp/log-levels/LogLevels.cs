using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

static class LogLine
{
    private static KeyValuePair<string, string> ParseLine(string line)
    {
        const string pattern = @"^\s*\[(?<severity>\w+)\]:\s*(?<message>.+)\s*$";
        KeyValuePair<string, string> ret = new KeyValuePair<string, string>();
        Match match = Regex.Match(line, pattern);
        if (match.Success)
        {
            ret = new KeyValuePair<string, string>(
                match.Groups[1].Value
                , match.Groups[2].Value);
        }
        return ret;
    }

    public static string Message(string logLine)
    {
        KeyValuePair<string, string> data = ParseLine(logLine);
        return data.Value.Trim();
    }

    public static string LogLevel(string logLine)
    {
        KeyValuePair<string, string> data = ParseLine(logLine);
        return data.Key.ToLowerInvariant();
    }

    public static string Reformat(string logLine)
    {
        KeyValuePair<string, string> data = ParseLine(logLine);
        return $"{data.Value.Trim()} ({data.Key.ToLowerInvariant()})";
    }
}