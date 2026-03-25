//
// This is only a SKELETON file for the 'All Your Base' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const convert = (digits, sourceBase, targetBase) => {
  if (sourceBase < 2)
    throw new Error('Wrong input base')
  if (targetBase < 2)
    throw new Error('Wrong output base')
  if ((digits.length === 0) || 
      (digits.length >= 2 && digits[0] === 0) ||
      (digits.some((digit) => digit < 0 || digit >= sourceBase)))
      throw new Error('Input has wrong format')
  
  let value = digits.reduce((total, current) => (total * sourceBase) + current, 0)
  let ret = []
  do
  {
    let digit = value % targetBase
    ret.push(digit)
    value -= digit
    value /= targetBase
  } while (value !== 0)
  
  return ret.toReversed()
};
