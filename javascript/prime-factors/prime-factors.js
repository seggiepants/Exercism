//
// This is only a SKELETON file for the 'Prime Factors' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const primeFactors = (value) => {
  let ret = []
  let current = value 
  while (current > 1 && current <= value)
  {
    let counter = 2
    while (current % counter > 0)
    {
      counter++
    }
    while (current % counter === 0)
    {
      ret.push(counter)
      current = current / counter
      if (current === 1)
        break
    }
  }
  return ret
};
