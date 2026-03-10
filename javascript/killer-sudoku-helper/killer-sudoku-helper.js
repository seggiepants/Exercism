//
// This is only a SKELETON file for the 'Killer Sudoku Helper' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const combinations = (cage) => {
  /* cage looks like this:
  {
      sum: What to Add up to,
      size: Number of cells,
      exclude: [Values to exclude],
    };
    returns an array of arrays each array is sorted by number, and the whole set
    are sorted by the smallest digit in each sub-array.
  */
  let ret = []
  let success = getDigits([], cage.sum, cage.size, cage.exclude, ret)
  if (!success) 
    throw new Error('No solution found.')

  return ret
};

let getDigits = (digits, sum, size, exclude, results) => {

  let subTotal = digits.reduce((acc, current) => acc + current, 0, results)
  let maxDigit = Math.max(...digits, 0)
  if (digits.length === size)
  {
    if (subTotal === sum)
    {
      results.push(digits)
      return true
    }
    else 
      return false
  }

  if (subTotal > sum)
    return false

  let available = [1, 2, 3, 4, 5, 6, 7, 8, 9].filter((n) => !exclude.includes(n) && subTotal + n <= sum && n > maxDigit )

  for(let i = 0; i < available.length; i++)
  {
    getDigits([...digits, available[i]], sum, size, [...exclude, available[i]], results)    
  }
  return results.length > 0
}
