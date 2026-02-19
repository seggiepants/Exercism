//
// This is only a SKELETON file for the 'ISBN Verifier' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const isValid = (isbn) => {
  const reValid = /^\d{9}[0-9X]$/
  let scrubbed = isbn.replaceAll('-', '').toUpperCase()

  if (!reValid.test(scrubbed))
    return false

  const digits = [...scrubbed].map((ch) => ch === 'X' ? 10 : Number(ch))
  const sum = digits.reduce((accumulator, current, index) => 
    accumulator += (current * (10 - index)), 0)

  return sum % 11 === 0
};

