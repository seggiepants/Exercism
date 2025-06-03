// TODO: define the 'LogLevel' enum

using System.Text.RegularExpressions;

// Tests break if LogLevel is within the LogLine class. Ask me how I know.
public enum LogLevel
{
    Trace = 1,
    Debug = 2,
    Info = 4,
    Warning = 5,
    Error = 6,
    Fatal = 42,
    Unknown = 0
}
static class LogLine
{
    // Lookup table to convert text in the logLines to LogLevel enumeration values.
    private static Dictionary<string, LogLevel> logLevelLookup = new Dictionary<string, LogLevel>
    {
        ["TRC"] = LogLevel.Trace,
        ["DBG"] = LogLevel.Debug,
        ["INF"] = LogLevel.Info,
        ["WRN"] = LogLevel.Warning,
        ["ERR"] = LogLevel.Error,
        ["FTL"] = LogLevel.Fatal
    };

    // Find a [ followed by three normal character then a ] a colon a space 
    // and the rest to end of line is the message. 
    // I put them in groups to take later.
    const string parseRegEx = @"^\[(?<Level>(.){3})\]\: (?<message>.*)$";
    public static LogLevel ParseLogLevel(string logLine)
    {
        // I really shouldn't be using regex here, it is too advanced for
        // the exercise. But I feel silly parsing text without a regex.
        Regex r = new(parseRegEx);
        Match m = r.Match(logLine);
        if (m.Groups.ContainsKey("Level"))
        {
            string? level = m.Groups["Level"].Value;
            return logLevelLookup.GetValueOrDefault(level, LogLevel.Unknown);
        }
        else
            return LogLevel.Unknown;        
    }

    public static string OutputForShortLog(LogLevel logLevel, string message)
    {
        // Only trick here is casting the enumeration to an int.
        return $"{(int)logLevel}:{message}";
    }
}
