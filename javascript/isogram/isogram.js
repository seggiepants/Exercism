//
// This is only a SKELETON file for the 'Isogram' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const isIsogram = (text) => {
  let simplified = text.replace(' ', '').replace('-', '').toLowerCase()
  
  let ret = [...simplified]
    .sort()
    .every((elem, index, arr) => index <= 0 ? true : elem !== arr[index - 1])
  return ret
}
