//
// This is only a SKELETON file for the 'Binary Search' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const find = (data, value) => {
  let index = bsearch(data, value, 0, data.length - 1)
  if (index < 0)
    throw new Error('Value not in array')
  return index
};

let bsearch = (data, value, min, max) =>
  {
    if (data.length === 0)
      return -1
    else if (max < min)
      return -1 
    else if (max === min)
      if (data[min] === value)
        return min
      else 
        return -1
    
    let midPoint = Math.floor(((max - min) / 2) + min)
    let midPointValue = data[midPoint]
    if (midPointValue === value)
      return midPoint 
    else if (midPointValue > value)
      return bsearch(data, value, min, midPoint - 1)
    else // midPointValue < value
      return bsearch(data, value, midPoint + 1, max)
  }
