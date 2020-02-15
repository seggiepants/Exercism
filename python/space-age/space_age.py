# Number of seconds in one Earth year
seconds_per_earth_year = 31557600

# Orbital period multiplier for the planets of the solar system
# compared to one Earth year.
orbit_in_years = {
    'Mercury': 0.2408467,
    'Venus': 0.61519726,
    'Earth': 1,
    'Mars': 1.8808158,
    'Jupiter': 11.862615,
    'Saturn': 29.447498,
    'Uranus': 84.016846,
    'Neptune': 164.79132
}

class SpaceAge(object):    
    """SpaceAge object. For the passed in seconds return the number
    of years for a given planet or just the number of seconds that
    was originally passed in.
    """
    def __init__(self, seconds):
        """Constructor, you may get the passed in seconds as number
        of years for a planet in the Sol Solar System (sorry Pluto).
        """        
        self.num_seconds = seconds

    def on_mercury(self):
        """Computes the number of years for Mercury's orbital period."""
        return self.get_years('Mercury')

    def on_venus(self):
        """Computes the number of years for Venus' orbital period."""
        return self.get_years('Venus')

    def on_earth(self):
        """Computes the number of years for Earth's orbital period."""
        return self.get_years('Earth')
    
    def on_mars(self):
        """Computes the number of years for Mars' orbital period."""
        return self.get_years('Mars')
    
    def on_jupiter(self):
        """Computes the number of years for Jupiter's orbital period."""
        return self.get_years('Jupiter')
    
    def on_saturn(self):
        """Computes the number of years for Saturn's orbital period."""
        return self.get_years('Saturn')

    def on_uranus(self):
        """Computes the number of years for Uranus' orbital period."""
        return self.get_years('Uranus')
    
    def on_neptune(self):
        """Computes the number of years for Neptune's orbital period."""
        return self.get_years('Neptune')

    @property
    def seconds(self):
        """
        Property to return (but not set) the seconds passed 
        in when the class was created.
        """
        return self.num_seconds
    
    def get_years(self, planet):
        """
        Using the class member seconds variable. Compute the number of
        years for the given planet.
        Parameters:
        * planet: The planet to compute for. Must match a key in the orbit_in_years dictionary.
        """
        return round(self.num_seconds / (seconds_per_earth_year * orbit_in_years[planet]), 2)
