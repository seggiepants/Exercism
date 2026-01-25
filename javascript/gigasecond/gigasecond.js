//
// This is only a SKELETON file for the 'Gigasecond' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

/**
 * Return a gigasecond past the given date.
 * @param {Date} value 
 * @returns 
 */
export const gigasecond = (value) => new Date(value.valueOf() + 1e12)

