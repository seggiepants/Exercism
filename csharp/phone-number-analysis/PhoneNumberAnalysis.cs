using System.Text.RegularExpressions;

public static class PhoneNumber
{
    // Apparantly we want New York, New York only and not just any area code in New York state.
    // so the following was worthless. Can we update the description to say New York City area code
    // don't make me do web searches because the problem was vague.
    /*
    private static readonly List<string> NewYorkDialingCodes = new() {"208", "212", "315", "334", "501",
        "516", "518", "562", "585", "607",
        "631", "716", "760", "845", "870",
        "905", "914" };
    */

    public static (bool IsNewYork, bool IsFake, string LocalNumber) Analyze(string phoneNumber)
    {
        Regex re = new(@"(?<dialingCode>\d{3})-(?<prefix>\d{3})-(?<digits>\d{4})");
        Match m = re.Match(phoneNumber);
        string dialingCode = "";
        if (m.Groups.ContainsKey("dialingCode"))
            dialingCode = m.Groups["dialingCode"].Value.ToString();

        string prefix = "";
        if (m.Groups.ContainsKey("prefix"))
            prefix = m.Groups["prefix"].Value.ToString();

        string digits = "";
        if (m.Groups.ContainsKey("digits"))
            digits = m.Groups["digits"].Value.ToString();

        return ("212".Equals(dialingCode), prefix.Equals("555"), digits);
    }

    public static bool IsFake((bool IsNewYork, bool IsFake, string LocalNumber) phoneNumberInfo)
    {
        return phoneNumberInfo.IsFake;
    }
}
