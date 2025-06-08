using System;

static class AssemblyLine
{
    private const double CARS_PER_HOUR = 221.0;
    
    public static double SuccessRate(int speed)
    {
        if (speed <= 0)
        {
            return 0.0;
        }
        else if (speed >= 1 && speed <= 4)
        {
            return 1.0;
        }
        else if (speed >= 5 && speed <= 8)
        {
            return 0.9;
        }
        else if (speed == 9)
        {
            return 0.8;
        }
        else if (speed == 10)
        {
            return 0.77;
        }
        throw new Exception("speed outside of range only 0-10 accepted.");
    }
    
    public static double ProductionRatePerHour(int speed)
    {
        return SuccessRate(speed) * (double) speed * CARS_PER_HOUR;
    }

    public static int WorkingItemsPerMinute(int speed)
    {
        return (int)(ProductionRatePerHour(speed) / 60.0);
    }
}