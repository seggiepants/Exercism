//
// This is only a SKELETON file for the 'Space Age' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

// Key=Planet, Value=Orbital period in Earth Years
const YearModifier = {
'mercury':	0.2408467,
'venus':	0.61519726,
'earth':	1.0,
'mars':	1.8808158,
'jupiter':	11.862615,
'saturn':	29.447498,
'uranus':	84.016846,
'neptune':	164.79132,
}

const EarthYearInSeconds = 31_557_600 

export const age = (planet, seconds) => {
  console.log(Object.keys(YearModifier))
  if (!(planet in YearModifier))
    throw new Error("not a planet")
  return Math.round(seconds * 100 / (EarthYearInSeconds * YearModifier[planet]))/100
}
