class RemoteControlCar
{
    int _speed;
    int _batteryDrained;
    int _battery;
    int _distance;

    public RemoteControlCar(int speed, int BatteryDrained)
    {
        _speed = speed;
        _batteryDrained = BatteryDrained;
        _battery = 100;
        _distance = 0;
    }

    public bool BatteryDrained()
    {
        return _battery < _batteryDrained;
    }

    public int DistanceDriven()
    {
        return _distance;
    }

    public void Drive()
    {
        if (!BatteryDrained())
        {
            _distance += _speed;
            _battery -= _batteryDrained;
        }
    }

    public static RemoteControlCar Nitro()
    {
        return new RemoteControlCar(50, 4);
    }

    // I don't want to make my private member variables public so I added this method
    // to compute the cars remaining distance. (Not assuming a full battery, just what is remaining).
    public int DistanceRemaining()
    {
        int battery = _battery;
        int distance = 0;
        while (battery >= _batteryDrained)
        {
            distance += _speed;
            battery -= _batteryDrained;
        }
        return distance;
    }
}

class RaceTrack
{
    int _distance;
    public RaceTrack(int distance)
    {
        _distance = distance;
    }

    public bool TryFinishTrack(RemoteControlCar car)
    {
        // With the new method added to the RemoteControlCar it is a simple matter to see
        // if the remaining distance is >= the race track distance.
        return car.DistanceRemaining() >= _distance;
    }
}
