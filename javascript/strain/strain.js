//
// This is only a SKELETON file for the 'Strain' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const keep = (arr, fn) => {
  let ret = []
  // not using this as requested:
  // return arr.filter((value) => fn(value))
  for(let i = 0; i < arr.length; i++)
  {
    if (fn(arr[i]))
      ret.push(arr[i])
  }
  return ret
};

export const discard = (arr, fn) => {
  let ret = []
  // not using this as requested
  // return arr.filter((value) => !fn(value))
  for(let i = 0; i < arr.length; i++)
  {
    if (!fn(arr[i]))
      ret.push(arr[i])
  }
  return ret
};
