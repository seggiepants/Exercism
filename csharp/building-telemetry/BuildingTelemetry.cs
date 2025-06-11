using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Xunit.Sdk;

public class RemoteControlCar
{
    private int batteryPercentage = 100;
    private int distanceDrivenInMeters = 0;
    private string[] sponsors = new string[0];
    private int latestSerialNum = 0;

    public void Drive()
    {
        if (batteryPercentage > 0)
        {
            batteryPercentage -= 10;
            distanceDrivenInMeters += 2;
        }
    }

    public void SetSponsors(params string[] sponsors)
    {
        this.sponsors = sponsors;
    }

    public string DisplaySponsor(int sponsorNum)
    {
        if (sponsorNum < 0 || sponsorNum >= sponsors.Length)
            throw new Exception("Sponsor number out of range.");
        return sponsors[sponsorNum];
    }

    public bool GetTelemetryData(ref int serialNum,
        out int batteryPercentage, out int distanceDrivenInMeters)
    {
        if (latestSerialNum > 0 && serialNum < latestSerialNum)
        {
            batteryPercentage = -1;
            distanceDrivenInMeters = -1;
            serialNum = this.latestSerialNum;
            return false;
        }
        batteryPercentage = this.batteryPercentage;
        distanceDrivenInMeters = this.distanceDrivenInMeters;
        latestSerialNum = serialNum;
        return true;
    }

    public static RemoteControlCar Buy()
    {
        return new RemoteControlCar();
    }
}

public class TelemetryClient
{
    private RemoteControlCar car;

    public TelemetryClient(RemoteControlCar car)
    {
        this.car = car;
    }

    public string GetBatteryUsagePerMeter(int serialNum)
    {
        bool hasTelemetry = car.GetTelemetryData(ref serialNum, out int batteryPercentage, out int distanceDrivenInMeters);
        if (hasTelemetry && distanceDrivenInMeters > 0)        
        {
            return $"usage-per-meter={(100 - batteryPercentage) / distanceDrivenInMeters}";
        }
        else
        {
            return "no data";
        }
    }
}
