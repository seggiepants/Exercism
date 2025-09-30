using System.Text;

public static class Grep
{
    public static string Match(string pattern, string flags, string[] files)
    {
        /*
            -n Prepend the line number and a colon (':') to each line in the output, placing the number after the filename (if present).
            -l Output only the names of the files that contain at least one matching line.
            -i Match using a case-insensitive comparison.
            -v Invert the program -- collect all lines that fail to match.
            -x Search only for lines where the search string matches the entire line.
        */
        Dictionary<string, bool> flag = new Dictionary<string, bool>()
        {
            ["-n"] = flags.IndexOf("-n") >= 0,
            ["-l"] = flags.IndexOf("-l") >= 0,
            ["-i"] = flags.IndexOf("-i") >= 0,
            ["-v"] = flags.IndexOf("-v") >= 0,
            ["-x"] = flags.IndexOf("-x") >= 0,
        };

        List<string> lines = new();
        List<string> matchingFiles = new();
        StringComparison compareFlag = flag["-i"] ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture;

        foreach (string file in files)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                string prefix = files.Count() > 1 ? $"{file}:" : "";
                int lineNum = 0;
                while (!sr.EndOfStream)
                {
                    lineNum++;
                    string prefixNum = flag["-n"] ? $"{lineNum}:" : "";
                    string? line = sr.ReadLine();

                    if (line != null)
                    {
                        int index = line.IndexOf(pattern, compareFlag);

                        if (flag["-x"])
                            index = String.Compare(pattern, line, flag["-i"]) == 0 ? 0 : -1;

                        if (index >= 0)
                        {
                            if (!matchingFiles.Contains(file))
                            {
                                matchingFiles.Add(file);
                            }
                        }

                        if (index >= 0 && !flag["-v"])
                        {
                            lines.Add(prefix + prefixNum + line);
                        }
                        else if (index < 0 && flag["-v"])
                        {
                            lines.Add(prefix + prefixNum + line);
                        }

                    }
                }
            }
        }

        if (flag["-l"])
            return String.Join("\n", matchingFiles);
        else
            return String.Join("\n", lines);
    }
}