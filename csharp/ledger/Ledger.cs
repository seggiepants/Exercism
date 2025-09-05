using System.Globalization;


/*
Refactor Log
============
* - Run test suite confirm things are working which they are.
* - Change Date Time Parse to TryParse. Failed parse returns the current date (UTC).
* - Currency symbol is related to currency not locale moved it out of locale to reduce redundancy.
* - Moved local out of currency check since it does the same thing either way. Also reduces redundancy.
* - Move currency symbol to a dictionary lookup removing the if else entirely. Leaves room for easy expansion too.
* - Can reuse dictionary keys to check for allowed currency too.
* - Same dictionary trick with locale.
* - Same key contains trick again, but now can merge down to just one if.
* - Didn't like the tuple (locale had two data items) (too obsure) so made a class to hold the tuple info.
* - PrintHead dictionary and key check again.
* - Reduced Description to a single iif line. Then to an arrow function like date.
* - Reduced Change to a single iif line. Kept as a normal function since I did the original change conversion once before the compare.
* - Print entry - changed string concatenations to one string interpolation. Could have merged in the function calls but that makes it less readable.
* - Sort merge negative and positive entries into one linq query with ThenBy then returned the results directly.
* - Format formats lines with a linq query now which also integrates the sort call.
* - Test Suite is still working.
*/

class LocaleData
{
    public int CurrencyNegativePattern { get; }
    public string ShortDatePattern { get; }
    public LocaleData(int CurrencyNegativePattern, string ShortDatePattern)
    {
        this.CurrencyNegativePattern = CurrencyNegativePattern;
        this.ShortDatePattern = ShortDatePattern;
    }
}

public class LedgerEntry
{
    public LedgerEntry(DateTime date, string desc, decimal chg)
    {
        Date = date;
        Desc = desc;
        Chg = chg;
    }

    public DateTime Date { get; }
    public string Desc { get; }
    public decimal Chg { get; }
}

public static class Ledger
{
    public static LedgerEntry CreateEntry(string date, string desc, int chng)
    {
        bool success = DateTime.TryParse(date, CultureInfo.InvariantCulture, out DateTime entryDate);
        if (!success)
            entryDate = DateTime.UtcNow;
        return new LedgerEntry(entryDate, desc, chng / 100.0m);
    }

    private static CultureInfo CreateCulture(string cur, string loc)
    {
        Dictionary<string, string> currencySymbol = new Dictionary<string, string>() 
        {
            ["USD"] = "$",
            ["EUR"] = "€",
        };

        Dictionary<string, LocaleData> localData = new Dictionary<string, LocaleData>()
        {
            ["en-US"] = new LocaleData(0, "MM/dd/yyyy"),
            ["nl-NL"] = new LocaleData(12, "dd/MM/yyyy"),
        };

        if (!currencySymbol.ContainsKey(cur) || !localData.ContainsKey(loc))
        {
            throw new ArgumentException("Invalid currency");
        }

        var culture = new CultureInfo(loc, false);
        culture.NumberFormat.CurrencySymbol = currencySymbol[cur]!;
        culture.NumberFormat.CurrencyNegativePattern = localData[loc].CurrencyNegativePattern;
        culture.DateTimeFormat.ShortDatePattern = localData[loc].ShortDatePattern;
        return culture;
    }

    private static string PrintHead(string loc)
    {
        Dictionary<string, string> localeHeader = new Dictionary<string, string>()
        {
            ["en-US"] = "Date       | Description               | Change       ",
            ["nl-NL"] = "Datum      | Omschrijving              | Verandering  ",
        };

        if (!localeHeader.ContainsKey(loc))
        {
            throw new ArgumentException("Invalid locale");
        }

        return localeHeader[loc];
    }

    private static string Date(IFormatProvider culture, DateTime date) => date.ToString("d", culture);

    private static string Description(string desc) => desc.Length > 25 ? desc.Substring(0, 22).PadRight(25, '.') : desc;

    private static string Change(IFormatProvider culture, decimal cgh)
    {
        string change = cgh.ToString("C", culture);
        return ((cgh < 0.0m && change.Contains("-")) || cgh >= 0.0m) ? change + " " : change;
    }

    private static string PrintEntry(IFormatProvider culture, LedgerEntry entry)
    {
        var date = Date(culture, entry.Date);
        var description = Description(entry.Desc);
        var change = Change(culture, entry.Chg);

        return $"{date} | {string.Format("{0,-25}", description)} | {string.Format("{0,13}", change)}";
    }


    private static IEnumerable<LedgerEntry> sort(LedgerEntry[] entries)
    {
        return entries.OrderBy(e => e.Chg < 0 ? 0 : 1).ThenBy(x => x.Date + "@" + x.Desc + "@" + x.Chg).ToList<LedgerEntry>();
    }

    public static string Format(string currency, string locale, LedgerEntry[] entries)
    {
        var formatted = "";
        formatted += PrintHead(locale);

        var culture = CreateCulture(currency, locale);

        if (entries.Length > 0)
        {
            formatted += '\n' + String.Join('\n', (from LedgerEntry entry in sort(entries) select PrintEntry(culture, entry)));
        }

        return formatted;
    }
}
