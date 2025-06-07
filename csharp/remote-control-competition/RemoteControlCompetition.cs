// TODO implement the IRemoteControlCar interface

// They really need to say what they want in the TestTrackRace. I decided just calling drive on the car was good enough.
// 
public interface IRemoteControlCar
{
    public void Drive();
    public int DistanceTravelled { get; }
}

public class ProductionRemoteControlCar : IRemoteControlCar, IComparable<ProductionRemoteControlCar>
{
    public int DistanceTravelled { get; private set; }
    public int NumberOfVictories { get; set; }

    public int CompareTo(ProductionRemoteControlCar? other)
    {
        if (other == null)
            throw new ArgumentNullException();
        return NumberOfVictories == other.NumberOfVictories ? 0 : NumberOfVictories > other.NumberOfVictories ? 1 : -1;
    }


    public void Drive()
    {
        DistanceTravelled += 10;
    }
}

public class ExperimentalRemoteControlCar : IRemoteControlCar
{
    public int DistanceTravelled { get; private set; }

    public void Drive()
    {
        DistanceTravelled += 20;
    }
}

public static class TestTrack
{
    public static void Race(IRemoteControlCar car)
    {
        // I don't know what you want here.
        car.Drive();
    }

    public static List<ProductionRemoteControlCar> GetRankedCars(ProductionRemoteControlCar prc1,
        ProductionRemoteControlCar prc2)
    {
        return prc1.CompareTo(prc2) >= 0 ? new List<ProductionRemoteControlCar>() {prc2, prc1} : new List<ProductionRemoteControlCar>() { prc1, prc2 };
    }
}
