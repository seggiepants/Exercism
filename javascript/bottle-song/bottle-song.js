//
// This is only a SKELETON file for the 'Bottle Song' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

const numOneToNineteen = ['zero', 'one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine', 
  'ten', 'eleven', 'twelve', 'thirteen', 'fourteen', 'fifteen', 'sixteen', 'seventeen', 'eighteen', 'nineteen'
]

const numTens = ['zero', 'ten', 'twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety', 'one hundred']

/**
 * Convert a number 0-100 to text. Only integer values are supported (no 1.25)
 * @param {Number} num - the value to convert to a text representation
 * @throws {Error} throws an error if the number is not in the range of 0 to 100. 
 */
const numToText = (num) => {
  if (num < 0 || num > 100)
    throw new Error("Index out of Range.")

  if (num < 20)
    return numOneToNineteen[num]

  let ones = num % 10
  let tens = (num - ones) / 10

  if (ones === 0)
    return numTens[tens]  
  return numTens[tens] + " " + numOneToNineteen[ones]
}

const capitalize = (text) => {
  if (text.length === 0)
    return ''
  return `${text.charAt(0).toUpperCase()}${text.slice(1)}`;
}

/**
 * 
 * @param {Number} initialBottlesCount - Number of bottles to start with
 * @param {Number} takeDownCount - How many verses to write.
 */
export const recite = (initialBottlesCount, takeDownCount) => {
  /*
  `Three green bottles hanging on the wall,`,
  `Three green bottles hanging on the wall,`,
  `And if one green bottle should accidentally fall,`,
  `There'll be two green bottles hanging on the wall.`,
  */
  let currentCount = initialBottlesCount;
  let ret = []
  for(let i = 0; i < takeDownCount && currentCount > 0; i++)
  {
    let nextCount = currentCount - 1

    ret.push(`${capitalize(numToText(currentCount))} green ${currentCount === 1 ? 'bottle' : 'bottles'} hanging on the wall,`)
    ret.push(`${capitalize(numToText(currentCount))} green ${currentCount === 1 ? 'bottle' : 'bottles'} hanging on the wall,`)
    ret.push('And if one green bottle should accidentally fall,')
    ret.push(`There'll be ${nextCount === 0 ? 'no' : numToText(nextCount)} green ${nextCount === 1 ? 'bottle' : 'bottles'} hanging on the wall.`)
    ret.push('')
    currentCount = nextCount
  }
  ret.pop() // remove last blank line
  return ret
};
