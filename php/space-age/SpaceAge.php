<?php
// Space Age exercise. What is your age computed in the orbital period of other planets in the Sol System

declare(strict_types=1);

// Class that computes Space Ages.
class SpaceAge
{
    //Planet => Orbital period in Earth Years
    private const OrbitMultiplier = [
        "Mercury" => 0.2408467,
        "Venus" => 0.61519726,
        "Earth" => 1.0,
        "Mars" => 1.8808158,
        "Jupiter" => 11.862615,
        "Saturn" => 29.447498,
        "Uranus" => 84.016846,
        "Neptune" => 164.79132
    ];

    // How long an earth year is in seconds.
    private const EarthYearSeconds = 31557600.0;

    // Constructor
    // @param $seconds: How the human is in seconds
    public function __construct(int $seconds)
    {
        $this->seconds = floatval($seconds);
    }

    private function compute($planet) : float {
        return ($this->seconds / self::EarthYearSeconds) / self::OrbitMultiplier[$planet];
    }

    // Compute your age in years based on the orbit of Earth
    // @returns: Age scaled to the orbital period
    public function earth(): float
    {
        return $this->compute("Earth");
    }

    // Compute your age in years based on the orbit of Mercury
    // @returns: Age scaled to the orbital period
    public function mercury(): float
    {
        return $this->compute("Mercury");
    }

    // Compute your age in years based on the orbit of Venus
    // @returns: Age scaled to the orbital period
    public function venus(): float
    {
        return $this->compute("Venus");
    }

    // Compute your age in years based on the orbit of Mars
    // @returns: Age scaled to the orbital period
    public function mars(): float
    {
        return $this->compute("Mars");
    }

    // Compute your age in years based on the orbit of Jupiter
    // @returns: Age scaled to the orbital period
    public function jupiter(): float
    {
        return $this->compute("Jupiter");
    }

    // Compute your age in years based on the orbit of Saturn
    // @returns: Age scaled to the orbital period
    public function saturn(): float
    {
        return $this->compute("Saturn");
    }

    // Compute your age in years based on the orbit of Uranus
    // @returns: Age scaled to the orbital period
    public function uranus(): float
    {
        return $this->compute("Uranus");
    }

    // Compute your age in years based on the orbit of Neptune
    // @returns: Age scaled to the orbital period
    public function neptune(): float
    {
        return $this->compute("Neptune");
    }
}
