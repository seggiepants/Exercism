//
// This is only a SKELETON file for the 'Collatz Conjecture' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const steps = (value) => {
  let numSteps = 0
  while (value !== 1)
  {
    if (value <= 0)
      throw new Error('Only positive integers are allowed')
    if (value % 2 === 0)
      value /= 2
    else 
      value = 1 + (3 * value)
    numSteps++
  }
  return numSteps
};


