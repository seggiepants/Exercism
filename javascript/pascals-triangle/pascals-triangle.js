//
// This is only a SKELETON file for the 'Pascals Triangle' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const rows = (count) => {
  let ret = []
  if (count >= 1)
    ret.push([1])

  for(let i = 2; i <= count; i++)
  {
    let previous = [0, ...ret[i - 2], 0]
    let current = []
    for(let j = 0; j < previous.length - 1; j++)
      current.push(previous[j] + previous[j + 1])
    ret.push(current)
  }

  return ret
};
