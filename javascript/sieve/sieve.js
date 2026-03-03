//
// This is only a SKELETON file for the 'Sieve' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const primes = (maxValue) => {
  let nums = Array.from({length: maxValue}, (_, i) => i + 1)
  let flags = Array.from({length: maxValue},() => true)

  flags[0] = false
  for(let i = 1; i < maxValue; i++)
  {
    if (flags[i] === false)
      continue

    let n = i + 1
    flags[i] = true;
    for(let j = i + n; j < maxValue; j+=n)
    {
      flags[j] = false
    }
  }
  return nums.filter((value, index) => flags[index] === true)
};
