//
// This is only a SKELETON file for the 'Transpose' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const transpose = (data) => {
  // list of strings return a row for each column
  let numRows = Math.max(...data.map((row) => row.length))
  let numCols = data.length
  let ret = []

  for(let j = 0; j < numRows; j++)
  {
    let row = ''
    for(let i = 0; i < numCols; i++)
    {
      if (i < data.length && j < data[i].length)
        row += data[i].at(j)
      else 
        row += ' '
    }
    
    // how many chars can we keep. Count backwards
    // until we have a character to find out.
    let maxLength
    for(maxLength = numCols; maxLength >= 1; maxLength--)
      if (data[maxLength - 1].length > j)
        break
    
    // If more than we can keep trim it.
    if (row.length > maxLength)
      row = row.substring(0, maxLength)
    ret.push(row)
  }
  return ret
};
