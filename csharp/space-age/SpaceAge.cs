using System.Reflection.Metadata;

public class SpaceAge
{
    static readonly Dictionary<string, double> OrbitalRatio = new Dictionary<string, double>()
    {
        ["Mercury"] = 0.2408467,
        ["Venus"] = 0.61519726,
        ["Earth"] = 1.0,
        ["Mars"] = 1.8808158,
        ["Jupiter"] = 11.862615,
        ["Saturn"] = 29.447498,
        ["Uranus"] = 84.016846,
        ["Neptune"] = 164.79132,
    };

    const double earthDaysInYear = 365.25;
    double seconds = 0.0d;

    public SpaceAge(int seconds)
    {
        this.seconds = seconds;

    }

    private double SecondsToYears() => (this.seconds / (60.0d * 60.0d * 24.0d * earthDaysInYear));

    private double AgeOnPlanet(string planet)
    {
        double ratio = OrbitalRatio.GetValueOrDefault(planet, 1.0);
        double earthYears = SecondsToYears();
        return earthYears / ratio;
    }

    public double OnEarth() => AgeOnPlanet("Earth");

    public double OnMercury() => AgeOnPlanet("Mercury");

    public double OnVenus() => AgeOnPlanet("Venus");

    public double OnMars() => AgeOnPlanet("Mars");

    public double OnJupiter() => AgeOnPlanet("Jupiter");

    public double OnSaturn() => AgeOnPlanet("Saturn");

    public double OnUranus() => AgeOnPlanet("Uranus");

    public double OnNeptune() => AgeOnPlanet("Neptune");
}