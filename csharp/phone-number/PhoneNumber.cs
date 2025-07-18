using System.Text.RegularExpressions;

public class PhoneNumber
{
    public static string Clean(string phoneNumber)
    {
        const string regExpPhoneNumber = @"(?<countryCode>(\+?1)(\.| |-)?)*\(?(?<areaCode>[2-9]\d{2})\)?(\.| |-)*(?<prefix>[2-9]\d{2})(\.| |-)*(?<number>\d{4})";
        string digits = String.Join("", (from char c in phoneNumber
                                         where Char.IsDigit(c)
                                         select c));

        if ((digits.Length == 11 && digits[0] == '1') || (digits.Length == 10))
        {
            Match m = Regex.Match(phoneNumber, regExpPhoneNumber);
            if (m.Success)
            {
                if (m.Groups.ContainsKey("areaCode") &&
                    m.Groups.ContainsKey("prefix") &&
                    m.Groups.ContainsKey("number"))
                    return m.Groups["areaCode"].Value + m.Groups["prefix"].Value + m.Groups["number"].Value;
                else
                    throw new ArgumentException("One or more phone number components not found.");
            }
            else
            {
                throw new ArgumentException("Not a valid phone number format.");
            }

        }
        else
        {
            throw new ArgumentException("Phone number does not have the correct number of digits");
        }
    }
}