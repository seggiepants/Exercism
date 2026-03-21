//
// This is only a SKELETON file for the 'Sum Of Multiples' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const sum = (baseValues, levelNumber) => {
  let factors = []
  for(let baseValue of baseValues)
    factors = factors.concat(multipleOfBaseValueLessThanLevelNumber(baseValue, levelNumber))
  return [...new Set(factors)].reduce((total, current) => total + current, 0)
};

let multipleOfBaseValueLessThanLevelNumber = (baseValue, levelNumber) => {
  let ret = []
  
  if (baseValue === 0) // Zero is a degenerate case
    return ret

  for(let i = baseValue; i < levelNumber; i += baseValue)
    ret.push(i)

  return ret
}
