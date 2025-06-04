using System.Threading.Tasks.Dataflow;

// checked(), and unchecked are new to me. Looks like you can do checked { ... }, and unchecked { ... } too.
// DisplayDenomination and DisplayCheifEconomistSalary() are the same except types, parameter names and error messages.
public static class CentralBank
{
    public static string DisplayDenomination(long @base, long multiplier)
    {
        try
        {
            return (checked(@base * multiplier)).ToString();
        }
        catch (OverflowException)
        {
            return "*** Too Big ***";
        }
    }

    public static string DisplayGDP(float @base, float multiplier)
    {
        float gdp = @base * multiplier;
        return float.IsInfinity(gdp) ? "*** Too Big ***" : gdp.ToString();
    }

    public static string DisplayChiefEconomistSalary(decimal salaryBase, decimal multiplier)
    {
        try
        {
            return (checked(salaryBase * multiplier)).ToString();
        }
        catch (OverflowException)
        {
            return "*** Much Too Big ***";
        }
    }
}
