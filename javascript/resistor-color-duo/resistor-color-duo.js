//
// This is only a SKELETON file for the 'Resistor Color Duo' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

const COLORS = {
  'black': 0,
  'brown': 1,
  'red': 2,
  'orange': 3,
  'yellow': 4,
  'green': 5,
  'blue': 6,
  'violet': 7,
  'grey': 8,
  'white': 9,
}

/**
 * Compute resistor color value for first two digits of values array
 * @param {Array<string>} values list of resistor color codes.
 */
export const decodedValue = (values) => values.reduce((result, current, index) => result = index < 2 ? (result * 10) + COLORS[current] : result, 0)

