//
// This is only a SKELETON file for the 'ETL' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const transform = (data) => {
  let ret = {}

  for(let key of Object.keys(data))
  {
    for(let ch of data[key])
    {
      ret[ch.toLowerCase()] = Number(key)
    }
  }

  return ret
};
