//
// This is only a SKELETON file for the 'Eliud's Eggs' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const eggCount = (displayValue) => {
  let eggs = 0
  let value = displayValue

  while (value !== 0)
  {
    eggs += value & 0b1
    value >>= 1
  }

  return eggs
};
