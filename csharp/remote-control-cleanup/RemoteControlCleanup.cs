using Microsoft.VisualStudio.TestPlatform.ObjectModel;

public class RemoteControlCar
{
    public class _Telemetry
    {        
        private RemoteControlCar parent;

        public _Telemetry(RemoteControlCar parent)
        {
            this.parent = parent;
        }

        // TODO encapsulate the methods suffixed with "_Telemetry" in their own class
        // dropping the suffix from the method name
        public void Calibrate()
        {

        }

        public bool SelfTest()
        {
            return true;
        }

        public void ShowSponsor(string sponsorName)
        {
            SetSponsor(sponsorName);
        }

        public void SetSpeed(decimal amount, string unitsString)
        {
     
            parent.SetSpeed(new Speed(amount, unitsString));
        }

        public void SetSponsor(string sponsorName)
        {
            parent.CurrentSponsor = sponsorName;

        }
    }

    public enum SpeedUnits
    {
        MetersPerSecond,
        CentimetersPerSecond
    }

    public struct Speed
    {
        public decimal Amount { get; }
        public SpeedUnits SpeedUnits { get; }

        public Speed(decimal amount, String speedUnits)
        {
            SpeedUnits units = SpeedUnits.MetersPerSecond;
            if (speedUnits == "cps")
            {
                units = SpeedUnits.CentimetersPerSecond;
            }

            Amount = amount;
            SpeedUnits = units;
        }

        public override string ToString()
        {
            string unitsString = "meters per second";
            if (SpeedUnits == SpeedUnits.CentimetersPerSecond)
            {
                unitsString = "centimeters per second";
            }

            return Amount + " " + unitsString;
        }
    }

    private Speed currentSpeed;
    public _Telemetry Telemetry { get; set; }
    public string CurrentSponsor { get; private set; } = "";

    public RemoteControlCar()
    {
        Telemetry = new _Telemetry(this);
    }

    public string GetSpeed()
    {
        return currentSpeed.ToString();
    }

    private void SetSpeed(Speed speed)
    {
        currentSpeed = speed;
    }    
}