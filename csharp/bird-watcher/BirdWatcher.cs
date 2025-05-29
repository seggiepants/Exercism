class BirdCount
{
    private int[] birdsPerDay;
    private static int[] _lastWeek = [0, 2, 5, 3, 7, 8, 4];
    private int[] _birdsPerDay = [2, 5, 0, 7, 4, 1, 0];
    public BirdCount(int[] birdsPerDay)
    {
        this._birdsPerDay = birdsPerDay;
    }

    public static int[] LastWeek()
    {
        return _lastWeek;
    }

    public int Today()
    {
        return _birdsPerDay.Last();
    }

    public void IncrementTodaysCount()
    {
        int lastIndex = _birdsPerDay.Length - 1;
        _birdsPerDay[lastIndex]++;
    }

    public bool HasDayWithoutBirds()
    {
        foreach (int day in _birdsPerDay)
        {
            if (day <= 0)
            {
                return true;
            }
        }
        return false;
    }

    public int CountForFirstDays(int numberOfDays)
    {
        int total = 0;
        for (int i = 0; i < Math.Min(_birdsPerDay.Length, numberOfDays); i++)
        {
            total += _birdsPerDay[i];
        }
        return total;
    }

    public int BusyDays()
    {
        int busyDayCount = 0;
        foreach (int visits in _birdsPerDay)
        {
            if (visits >= 5)
                busyDayCount++;
        }
        return busyDayCount;
    }
}
