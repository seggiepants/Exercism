using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using Xunit.Internal;

public enum Location
{
    NewYork,
    London,
    Paris
}

public enum AlertLevel
{
    Early,
    Standard,
    Late
}

public static class Appointment
{
    public static DateTime ShowLocalTime(DateTime dtUtc)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(dtUtc, TimeZoneInfo.Local);
    }

    private static TimeZoneInfo GetTimeZone(Location location)
    {
        Dictionary<Location, string> tzLookup = new();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                    RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD) ||
                    RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            tzLookup[Location.NewYork] = "America/New_York";
            tzLookup[Location.London] = "Europe/London";
            tzLookup[Location.Paris] = "Europe/Paris";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            tzLookup[Location.NewYork] = "Eastern Standard Time";
            tzLookup[Location.London] = "GMT Standard Time";
            tzLookup[Location.Paris] = "W. Europe Standard Time";
        }
        else
        {
            throw new PlatformNotSupportedException("Execution Platform not supported. (Windows, OSX, Linux and FreeBSD are supported platforms).");
        }

        if (!tzLookup.TryGetValue(location, out string? timeZoneId))
        {
            timeZoneId = TimeZoneInfo.Local.Id;
        }
        if (timeZoneId == null)
            timeZoneId = TimeZoneInfo.Local.Id;

        return TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
    }

    public static DateTime Schedule(string appointmentDateDescription, Location location)
    {
        DateTime scheduled;
        if (!DateTime.TryParse(appointmentDateDescription, out scheduled))
        {
            scheduled = DateTime.Now;
        }

        return TimeZoneInfo.ConvertTimeToUtc(scheduled, GetTimeZone(location));
    }

    public static DateTime GetAlertTime(DateTime appointment, AlertLevel alertLevel)
    {
        TimeSpan ts;
        switch (alertLevel)
        {
            case AlertLevel.Early:
                ts = new TimeSpan(1, 0, 0, 0);
                break;
            case AlertLevel.Standard:
                ts = new TimeSpan(1, 45, 0);
                break;
            default: // AlertLevel.Standard
                ts = new TimeSpan(0, 30, 0);
                break;
        }

        return appointment - ts;
    }

    public static bool HasDaylightSavingChanged(DateTime dt, Location location)
    {
        TimeZoneInfo tz = GetTimeZone(location);
        DateTime weekAgo = dt.AddDays(-7);
        return tz.IsDaylightSavingTime(weekAgo) != tz.IsDaylightSavingTime(dt);
    }

    public static DateTime NormalizeDateTime(string dtStr, Location location)
    {
        CultureInfo ci;

        TimeZoneInfo tz = GetTimeZone(location);
        switch (location)
        {
            case Location.London:
                ci = new CultureInfo("en-GB");
                break;
            case Location.Paris:
                ci = new CultureInfo("fr-FR");
                break;
            default: // Location.NewYork
                ci = new CultureInfo("en-US");
                break;
        }
        if (!DateTime.TryParse(dtStr, ci, out DateTime result))
            result = new DateTime(1, 1, 1);
        return result;
    }
}
