// Space Age exercise.
package spaceage

type Planet string

const EARTH_YEAR_SECONDS float64 = 31_557_600.0

var PLANET_YEAR_DIVISOR = map[Planet]float64{
	//Planet	Orbital period in Earth Years
	"Mercury": 0.2408467,
	"Venus":   0.61519726,
	"Earth":   1.0,
	"Mars":    1.8808158,
	"Jupiter": 11.862615,
	"Saturn":  29.447498,
	"Uranus":  84.016846,
	"Neptune": 164.79132,
}

// Calculate how many years old you are on a given planet
// based on how many seconds old you are and what planet.
// @param seconds: How old you are in seconds (float64)
// @param planet: The planet you are on. Only the 8 sol-system planets are available
// @returns: -1.0 if you pass in an unknown planet otherwise your age in that planet's years
func Age(seconds float64, planet Planet) float64 {

	divisor, ok := PLANET_YEAR_DIVISOR[planet]
	if !ok {
		return -1.0
	}
	return seconds / (EARTH_YEAR_SECONDS * divisor)
}
