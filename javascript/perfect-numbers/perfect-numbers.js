//
// This is only a SKELETON file for the 'Perfect Numbers' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const classify = (num) => {
  if (num <= 0 || num != Math.floor(num))
    throw new Error('Classification is only possible for natural numbers.')

  // aliquot sum
  let aliquotSum = 0
  for(let i = 1; i <= num / 2; i++)
  {
    if (num % i === 0)
      aliquotSum += i
  }

  // perfect, abundant, deficient
  if (aliquotSum === num)
    return 'perfect'
  else if (aliquotSum > num)
    return 'abundant'
  return 'deficient'
};
