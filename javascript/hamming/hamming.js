//
// This is only a SKELETON file for the 'Hamming' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const compute = (strandA, strandB) => {
  if (strandA.length !== strandB.length)
    throw new Error('strands must be of equal length')

  return strandA.split('')
    .map((ch, index) => ch === strandB[index] ? 0 : 1)
    .reduce((previous, current) => previous + current, 0)
}
