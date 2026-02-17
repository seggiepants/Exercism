//
// This is only a SKELETON file for the 'Saddle Points' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const saddlePoints = (grid) => {

  let isSaddlePoint = (x, y) => {
    let maxHeight = Math.max(...grid[y])
    let minHeight = Math.min(...grid.map((row) => row[x]))
    let current = grid[y][x]
    return maxHeight === current && minHeight === current
  }

  let ret = []
  let rowCount = grid.length
  for(let y = 0; y < rowCount; y++)
  {
    let colCount = grid[y].length
    for(let x = 0; x < colCount; x++)
    {
      if (isSaddlePoint(x, y))
      {
        ret.push({
          row: y + 1,
          column: x + 1,
        })
      }
    }
  }
  return ret 
  
};
