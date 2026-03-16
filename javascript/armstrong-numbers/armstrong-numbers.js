//
// This is only a SKELETON file for the 'Armstrong Numbers' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

/**
 * Check if a number is an armstrong number.
 * Sum of its each digits raised to the power of the number in the number.
 * @param {Number} number - to check if Armstrong number or not.
 * @returns True if armstrong number, and false otherwise
 */
export const isArmstrongNumber = (number) => {
  let digits = String(number).split('').map((n) => BigInt(n))
  let numDigits = BigInt(digits.length)
  let computed = digits.reduce((sum, digit) => sum - (digit ** numDigits), BigInt(number))
  return computed === 0n
};