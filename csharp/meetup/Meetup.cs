public enum Schedule
{
    Teenth,
    First,
    Second,
    Third,
    Fourth,
    Last
}

public class Meetup
{
    private int month { get; set; }
    private int year { get; set; }
    public Meetup(int month, int year)
    {
        this.month = month;
        this.year = year;
    }

    public DateTime Day(DayOfWeek dayOfWeek, Schedule schedule)
    {
        DateTime firstDayOfMonth = new DateTime(year, month, 1);
        DateTime rangeStart = DateTime.MinValue;
        DateTime rangeEnd = DateTime.MinValue;
        switch (schedule)
        {
            case Schedule.First:
                rangeStart = firstDayOfMonth;
                rangeEnd = rangeStart.AddDays(6);
                break;
            case Schedule.Second:
                rangeStart = firstDayOfMonth.AddDays(7);
                rangeEnd = rangeStart.AddDays(6);
                break;
            case Schedule.Third:
                rangeStart = firstDayOfMonth.AddDays(14);
                rangeEnd = rangeStart.AddDays(6);
                break;
            case Schedule.Fourth:
                rangeStart = firstDayOfMonth.AddDays(21);
                rangeEnd = rangeStart.AddDays(6);
                break;
            case Schedule.Last:
                rangeEnd = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
                rangeStart = rangeEnd.AddDays(-6);
                break;
            case Schedule.Teenth:
                rangeStart = new DateTime(year, month, 13);
                rangeEnd = rangeStart.AddDays(6);
                break;
            default:
                throw new ArgumentException();

        }
        for (DateTime current = rangeStart; current <= rangeEnd; current = current.AddDays(1))
        {
            if (current.DayOfWeek == dayOfWeek)
                return current;
        }
        throw new ArgumentException();
    }
}