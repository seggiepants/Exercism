// Exercise: Space Age.
class SpaceAge {
  // Put your code here
  final Map<String, double> planetPeriod = {
    "Mercury":	0.2408467,
    "Venus":	0.61519726,
    "Earth":	1.0,
    "Mars":	1.8808158,
    "Jupiter":	11.862615,
    "Saturn":	29.447498,
    "Uranus":	84.016846,
    "Neptune":	164.79132,
  };

  final double earthYearInSeconds = 31557600.0;

  // Given a number of seconds compute the number of years that would be on a given planet.
  // @param String planet: The planet we are calculating for. Must be one of: "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", or "Neptune"
  // @param double seconds: The number of seconds in the time we are measuring.
  // @returns double: The age in years for the given planet.
  // @raises Exception: If the planet was not one of the anticipated planets. Spelling must match too. List in the String planet parameter help text.
  double age({required String planet, required double seconds}) {
    double earthYears = seconds / earthYearInSeconds;
    double? period = planetPeriod[planet];
    if (period == null) {
      throw Exception("Planet not found: $planet.");
    }
    // Unspoken requirement round to two places after the decimal point.
    return ((earthYears * 100) / (planetPeriod[planet] ?? 1.0)).round() / 100.0;
  }
}
