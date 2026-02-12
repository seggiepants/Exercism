//
// This is only a SKELETON file for the 'Luhn' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const valid = (value) => {
  const digits = '0123456789'
  // check for non-space non-digit characters fail if found.
  let invalidChars = [... value].filter((ch) => ch !== ' ' && !digits.includes(ch))
  if (invalidChars.length > 0) 
    return false

  // convert to an array of numbers removing spaces
  let working = [... value].filter((ch) => digits.includes(ch)).map((ch) => Number(ch))
  // are we checking the odd or even characters
  let flag = working.length % 2

  // if this is too short then fail
  if (working.length <= 1)
    return false

  // map the number doubler, then reduce to a count and return if that count is 
  // evenly divisible by ten.
  return working.map((num, index) => {
    if (index % 2 === flag)
    {
      let temp = num * 2
      if (temp > 9) 
        return temp - 9
      else
        return temp
    }
    return num
  }).reduce((previous, current) => previous + current, 0) % 10 === 0
};
