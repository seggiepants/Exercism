//
// This is only a SKELETON file for the 'Twelve Days' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

const data = [
  [1, 'first', 'a Partridge in a Pear Tree'],
  [2, 'second', 'two Turtle Doves'],
  [3, 'third', 'three French Hens'],
  [4, 'fourth', 'four Calling Birds'],
  [5, 'fifth', 'five Gold Rings'],
  [6, 'sixth', 'six Geese-a-Laying'],
  [7, 'seventh', 'seven Swans-a-Swimming'],
  [8, 'eighth', 'eight Maids-a-Milking'],
  [9, 'ninth', 'nine Ladies Dancing'],
  [10, 'tenth', 'ten Lords-a-Leaping'],
  [11, 'eleventh', 'eleven Pipers Piping'],
  [12, 'twelfth', 'twelve Drummers Drumming'],
]

export const recite = (start, stop = -1) => {
  if (stop === -1)
    stop = start
  let ret = []
  for(let i = start - 1; i < stop; i++)
  {
    let sequence = data.filter((value, index) => index <= i).sort((a, b) => b[0] - a[0]).map((item) => item[0] === 1 && i > 0 ? 'and ' + item[2] : item[2]).join(', ')

    ret.push(`On the ${data[i][1]} day of Christmas my true love gave to me: ${sequence}.\n`)
  }
  return ret.join('\n')
};
