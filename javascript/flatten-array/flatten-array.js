//
// This is only a SKELETON file for the 'Flatten Array' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const flatten = (arr) => {
  return arr.filter((value) => !Object.is(value, null)).reduce((acc, value) => {
    if (Array.isArray(value))
      return acc = [...acc, ...flatten(value)] 
    else
    {
      acc.push(value)
    }
    return acc
  }, [])
};
