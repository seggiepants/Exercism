using System.Text.RegularExpressions;

public static class SwiftScheduling
{
    private static int MonthToQuarter(int month)
    {
        switch(month)
        {
            case 1:
            case 2:
            case 3:
                return 1;
            case 4:
            case 5:
            case 6:
                return 2;
            case 7:
            case 8:
            case 9:
                return 3;
            case 10:
            case 11:
            case 12:
                return 4;
            default:
                throw new Exception("Invalid Month only 1-12 are accepted.");
        }
    }

    private static DateTime LastDayOfQuarter(int quarter, int year)
    {
        if (quarter >= 1 && quarter <= 4)
        {
            DateTime temp;

            // April starts Q2, July Q3, October Q4, January Q1 next year.
            int month = (quarter * 3) + 1;
            int targetYear = year;
            if (month > 12)
            {
                month = 1;
                targetYear++;
            }
            temp = new DateTime(targetYear, month, 1);
            // subtract a day brings you to last day of desired quarter.
            temp = temp.AddDays(-1);
            return temp;
        }
        throw new Exception("Quarter out of bounds 1-4 only.");
    }

    public static DateTime DeliveryDate(DateTime meetingStart, string description) 
    {
        Regex quarter = new Regex(@"Q[1-4]", RegexOptions.IgnoreCase);
        Regex month = new Regex(@"\d+M", RegexOptions.IgnoreCase);
        DayOfWeek[] earlyWeek = {DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday};
        DayOfWeek[] weekEnd = {DayOfWeek.Saturday, DayOfWeek.Sunday};

        if (description == "NOW")
        {
            return meetingStart + new TimeSpan(2, 0, 0);
        }
        else if (description == "ASAP")
        {
            if (meetingStart.Hour < 13)
            {
                // Today at 17:00
                return new DateTime(meetingStart.Year
                                   , meetingStart.Month
                                   , meetingStart.Day
                                   , 17, 0, 0, 0);
            }
            else
            {
                // Tomorrow as 13:00
                return new DateTime(meetingStart.Year, 
                                    meetingStart.Month,
                                   meetingStart.Day
                                   , 13, 0, 0, 0).AddDays(1);
            }            
        }
        else if (description == "EOW")
        {
            if (earlyWeek.Contains<DayOfWeek>(meetingStart.DayOfWeek))
            {
                DateTime endOfWeek = new DateTime(meetingStart.Year, meetingStart.Month, meetingStart.Day, 17, 0, 0);
                while (endOfWeek.DayOfWeek != DayOfWeek.Friday)
                    endOfWeek = endOfWeek.AddDays(1);
                return endOfWeek;
            }
            else
            {
                DateTime endOfWeek = new DateTime(meetingStart.Year, meetingStart.Month, meetingStart.Day, 20, 0, 0);
                while (endOfWeek.DayOfWeek != DayOfWeek.Sunday)
                    endOfWeek = endOfWeek.AddDays(1);
                return endOfWeek;
            }
        }

        Match m = month.Match(description);
        if (m.Success)
        {
            bool success = int.TryParse(m.Value.ToUpperInvariant().Replace("M", ""), out int selectedMonth);
            if (!success) 
                throw new Exception("Invalid month format.");
            int year = meetingStart.Year;
            if (meetingStart.Month >= selectedMonth)
                year++;

            DateTime nthMonth = new DateTime(year, selectedMonth, 1, 8, 0, 0);
            while (weekEnd.Contains<DayOfWeek>(nthMonth.DayOfWeek))
                nthMonth = nthMonth.AddDays(1);
            return nthMonth;
        }

        Match q = quarter.Match(description);
        if (q.Success)
        {
            bool success = int.TryParse(q.Value.ToUpperInvariant().Replace("Q", ""), out int selectedQuarter);
            if (!success) 
                throw new Exception("Invalid quarter format.");
            int year = meetingStart.Year;
            if (MonthToQuarter(meetingStart.Month) > selectedQuarter)
                year++;

            DateTime nthQuarter = LastDayOfQuarter(selectedQuarter, year);
            while (weekEnd.Contains<DayOfWeek>(nthQuarter.DayOfWeek))
                nthQuarter = nthQuarter.AddDays(-1);
            return nthQuarter.AddHours(8);
        }

        
        return meetingStart;
    }
}
