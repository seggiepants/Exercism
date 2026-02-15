//
// This is only a SKELETON file for the 'Acronym' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const parse = (text) => {
  let words = text.replace(/[-|\t|\r|\n]/g, ' ').replace(/['|"|_]/g, '').split(' ')
  return words.reduce((previous, current) => previous + current.trim().substring(0, 1).toUpperCase(), '')
}
