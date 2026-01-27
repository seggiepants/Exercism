//
// This is only a SKELETON file for the 'Pangram' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const isPangram = (phrase) => {
  const codeA = 'a'.charCodeAt(0)
  const codeZ = 'z'.charCodeAt(0)
  let alphaCount = '00000000000000000000000000'.split('').map((ch) => Number(ch))
  let nonAlphaCount = 0

  phrase.toLowerCase().split('').map((ch) => ch.charCodeAt(0) >= codeA && ch.charCodeAt(0) <= codeZ ? alphaCount[ch.charCodeAt(0) - codeA]+= 1 : nonAlphaCount++)
  return alphaCount.findIndex((value) => value === 0) < 0
  
};
