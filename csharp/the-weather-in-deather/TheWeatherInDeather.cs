using System.Diagnostics.CodeAnalysis;

// These may be more consise but I wouldn't say they are more readable.
public class WeatherStation
{
    private Reading reading;
    private List<DateTime> recordDates = new List<DateTime>();
    private List<decimal> temperatures = new List<decimal>();

    public void AcceptReading(Reading reading)
    {
        this.reading = reading;
        recordDates.Add(DateTime.Now);
        temperatures.Add(reading.Temperature);
    }

    public void ClearAll()
    {
        reading = new Reading();
        recordDates.Clear();
        temperatures.Clear();
    }

    public decimal LatestTemperature
    {
        get => reading.Temperature;
    }

    public decimal LatestPressure
    {
        get => reading.Pressure;
    }

    public decimal LatestRainfall
    {
        get => reading.Rainfall;
    }

    public bool HasHistory
    {
        get => recordDates.Count > 1;
    }

    public Outlook ShortTermOutlook
    {
        get
        {
            return reading switch
            {
                _ when reading.Equals(new Reading()) => throw new ArgumentException(),
                _ when ((reading.Pressure < 10m) && (reading.Temperature < 30m)) => Outlook.Cool,
                _ when reading.Temperature > 50 => Outlook.Good,
                _ => Outlook.Warm
            };
        }
    }

    public Outlook LongTermOutlook
    {
        get
        {
            return reading switch
            {
                _ when reading.WindDirection == WindDirection.Southerly || reading.WindDirection == WindDirection.Easterly && reading.Temperature > 20 => Outlook.Good,
                _ when reading.WindDirection == WindDirection.Northerly => Outlook.Cool,
                _ when reading.WindDirection == WindDirection.Easterly && reading.Temperature <= 20 => Outlook.Warm,
                _ when reading.WindDirection == WindDirection.Westerly => Outlook.Rainy,
                _ => throw new ArgumentException()
            };
        }
    }

    public State RunSelfTest() => reading.Equals(new Reading()) ? State.Bad : State.Good;
}

/*** Please do not modify this struct ***/
public struct Reading
{
    public decimal Temperature { get; }
    public decimal Pressure { get; }
    public decimal Rainfall { get; }
    public WindDirection WindDirection { get; }

    public Reading(decimal temperature, decimal pressure,
        decimal rainfall, WindDirection windDirection)
    {
        Temperature = temperature;
        Pressure = pressure;
        Rainfall = rainfall;
        WindDirection = windDirection;
    }
}

/*** Please do not modify this enum ***/
public enum State
{
    Good,
    Bad
}

/*** Please do not modify this enum ***/
public enum Outlook
{
    Cool,
    Rainy,
    Warm,
    Good
}

/*** Please do not modify this enum ***/
public enum WindDirection
{
    Unknown, // default
    Northerly,
    Easterly,
    Southerly,
    Westerly
}
