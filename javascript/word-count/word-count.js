//
// This is only a SKELETON file for the 'Word Count' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const countWords = (text) => {
  let re = /[\w|']+/g
  let ret = {}
  const matches = [...text.matchAll(re)];
  for(const match of matches)
  {
    let key = match[0].toLowerCase()
    // clean out any starting/ending single quotes and skip empty values
    if (key.startsWith('\''))
      key = key.substring(1)
    if (key.endsWith('\''))
      key = key.substring(0, key.length - 1)
    if (key.length === 0)
      continue

    let count = ret[key] || 0 // if we don't have the key yet use 0
    ret[key] = count + 1
  }
  return ret
}
