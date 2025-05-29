class RemoteControlCar
{
    public int _distance = 0;
    public int _batteryPercent = 100;
    public static RemoteControlCar Buy()
    {
        return new RemoteControlCar();
    }

    public string DistanceDisplay()
    {
        return $"Driven {_distance} meters";
    }

    public string BatteryDisplay()
    {
        if (_batteryPercent <= 0)
        {
            return "Battery empty";
        }
        else
        {
            return $"Battery at {_batteryPercent}%";
        }
        throw new NotImplementedException("Please implement the RemoteControlCar.BatteryDisplay() method");
    }

    public void Drive()
    {
        if (_batteryPercent > 0)
        {
            _distance += 20;
            _batteryPercent--;
        }
    }
}
