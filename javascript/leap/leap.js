//
// This is only a SKELETON file for the 'Leap' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const isLeap = (year) => {
  let four = year % 4
  let oneHundred = year % 100
  let fourHundred = year % 400
  if (four === 0)
  {
    return oneHundred !== 0 || (oneHundred === 0 && fourHundred === 0)
  }
  return false
};
