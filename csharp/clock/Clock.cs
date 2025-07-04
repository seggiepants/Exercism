public class Clock : IEquatable<Clock>
{
    const int minutes_per_hour = 60;
    const int hours_per_cycle = 24;
    int hours;
    int minutes;
    public Clock(int hours, int minutes)
    {
        this.hours = hours;
        this.minutes = minutes;
        Sanitize();
    }

    public Clock Add(int minutesToAdd)
    {
        this.minutes += minutesToAdd;
        Sanitize();
        return this;
    }

    public bool Equals(Clock? other)
    {
        if (other == null)
            return false;

        return ((other.hours == this.hours) && (other.minutes == this.minutes));
    }


    public Clock Subtract(int minutesToSubtract)
    {
        this.minutes -= minutesToSubtract;
        Sanitize();
        return this;
    }

    private void Sanitize()
    {
        while (minutes < 0)
        {
            hours--;
            minutes += minutes_per_hour;
        }

        while (minutes >= minutes_per_hour)
        {
            hours++;
            minutes -= minutes_per_hour;
        }

        while (hours < 0)
        {
            hours += hours_per_cycle;
        }

        while (hours >= hours_per_cycle)
        {
            hours -= hours_per_cycle;
        }
    }

    public override string? ToString()
    {
        return $"{hours:D2}:{minutes:D2}";
    }
}
