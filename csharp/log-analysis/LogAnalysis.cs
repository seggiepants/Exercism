public static class LogAnalysis
{
    // TODO: define the 'SubstringAfter()' extension method on the `string` type
    public static string SubstringAfter(this string message, string delim)
    {
        int index = message.IndexOf(delim);
        if (index >= 0)
            index += delim.Length;
        else
            return "";

        return message.Substring(index);
    }

    // TODO: define the 'SubstringBetween()' extension method on the `string` type
    public static string SubstringBetween(this string message, string delimStart, string delimEnd)
    {
        int indexStart = message.IndexOf(delimStart);
        if (indexStart >= 0)
            indexStart += delimStart.Length;
        else
            return "";

        int indexEnd = message.IndexOf(delimEnd, indexStart);
        if (indexEnd < 0)
            return message.Substring(indexStart);

        return message.Substring(indexStart, indexEnd - indexStart);
    }

    // TODO: define the 'Message()' extension method on the `string` type
    public static string Message(this string message)
    {
        return message.SubstringAfter("]: ");
    }

    // TODO: define the 'LogLevel()' extension method on the `string` type
    public static string LogLevel(this string message)
    {
        return message.SubstringBetween("[", "]");
    }
}