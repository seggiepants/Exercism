//
// This is only a SKELETON file for the 'Raindrops' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const convert = (num) => {
  let ret = ''
  let clean = true
  
  // is divisible by 3, add "Pling" to the result.
  if (num % 3 === 0)
  {
    ret += 'Pling'
    clean = false
  }

  // is divisible by 5, add "Plang" to the result.
  if (num % 5 === 0)
  {
    ret += 'Plang'
    clean = false
  }

  // is divisible by 7, add "Plong" to the result.
  if (num % 7 === 0)
  {
    ret += 'Plong'
    clean = false
  }

  // is not divisible by 3, 5, or 7, the result should be the number as a string.
  if (clean)
    ret += String(num)

  return ret
};
