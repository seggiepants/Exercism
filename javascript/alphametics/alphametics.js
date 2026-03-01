//
// This is only a SKELETON file for the 'Alphametics' exercise. It's been provided as a
// convenience to get you started writing code faster.
//
export const solve = (puzzle) => {
  let tokens = [...puzzle.matchAll(/[A-Z]+/g)].map((value) => value[0])
  let notZero = new Set(tokens.map((value) => value[0]))
  let uniqueChars = new Set(puzzle)
  uniqueChars.delete(' ')
  uniqueChars.delete('+')
  uniqueChars.delete('=')  
  let digitMap = [null, null, null, null, null, null, null, null, null, null]
  
  let ret = step(tokens.join(' '), digitMap, notZero, uniqueChars.size, [...uniqueChars].join(''), 0)
  if (typeof(ret) !== 'object')
    return null 
  return ret
};

const step = (puzzle, digitMap, notZero, totalUnique, uniqueChars, mappedDigits ) => {
  // base case check for a valid solution
  if (mappedDigits === totalUnique)
  {
    let valid = compute(puzzle)
    if (!valid) // character => digit
      return false 
    else 
    {
      let ret = {}
      for(let i = 0; i < digitMap.length; i++)
      {
        if (digitMap[i] !== null)
          ret[digitMap[i]] = i 
      }
      return ret
    }
  }

  // find the first unmapped character
  if (uniqueChars.length <= 0)
    return false;

  let candidate = uniqueChars[0]
  for (let i = digitMap.length - 1; i >= 0; i--)
  {
    if ((i === 0 && notZero.has(candidate)) || digitMap[i] !== null)
      continue
    digitMap[i] = candidate 
    let ret = step(puzzle.replaceAll(candidate, String(i)), digitMap, notZero, totalUnique, uniqueChars.replace(candidate, ''), mappedDigits + 1)
    if (typeof(ret) === 'object')
      return ret 
    else 
      digitMap[i] = null
  }
  return false
}
const compute = (puzzle) => {
  let nums = puzzle.split(' ').map((value) => Number(value))
  let rhsAmount = nums.pop()
  return rhsAmount === nums.reduce((accumulator, value) => accumulator + value, 0)
}