using System.Linq;

public static class BafflingBirthdays
{
    // How many days in a month with a zero first so that
    // we align to 1-12 for months
    static int[] DaysOfMonth = {0, 31, 28, 31, 30,
    31, 30, 31, 31, 
    30, 31, 30, 31};

    public static DateOnly[] RandomBirthdates(int numberOfBirthdays)
    {
        Random r = new();
        List<DateOnly> ret = new();
        for(int i = 0; i < numberOfBirthdays; i++)
        {
            // Last 100 years or so.
            int year = 1926 + (int)r.NextInt64(0, 1001);
            while (DateTime.IsLeapYear(year))
                year = 1926 + (int)r.NextInt64(0, 1001);
            int month = 1 + (int)r.NextInt64(12);
            int day = 1 + (int)r.NextInt64(DaysOfMonth[month]);
            ret.Add(new DateOnly(year, month, day));
        }
        return ret.ToArray<DateOnly>();
    }

    public static bool SharedBirthday(DateOnly[] birthdays)
    {
        // Shared if number of unique birthdays not equal to number of given birthdays.
        HashSet<Tuple<int, int>> UniqueBirthdays = new(birthdays.Select((DateOnly value) => new Tuple<int, int>(value.Month, value.Day)));
        return UniqueBirthdays.Count != birthdays.Length;
    }
    
    public static double EstimatedProbabilityOfSharedBirthday(int numberOfBirthdays)
    {
        // Swiped from the referenced wikipedia page.
        if (numberOfBirthdays >= 365)
            return 1.0;
        if (numberOfBirthdays <= 1)
            return 0.0;

        double ret = 1.0;
        for(int i = 0; i < numberOfBirthdays; i++)
        {
            ret *= (double)(365 - i)/365.0d;
        }
        return (1.0d - ret) * 100.0d;
    }    
}
