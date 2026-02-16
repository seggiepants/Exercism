//
// This is only a SKELETON file for the 'Rectangles' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

function scanRight(grid, x, y, rows, cols)
{
  let rectCount = 0
  let i = x + 1
  let leftChar = i < cols ? grid[y].at(i) : ' '
  while (leftChar === '-' || leftChar === '+')
  {
    if (leftChar === '+')
      rectCount += scanDown(grid, x, i, y, rows)
    i++
    if (i >= cols)
      break
    else
      leftChar = grid[y].at(i)
  }
  return rectCount
}

function scanDown(grid, x1, x2, y, rows)
{
  let rectCount = 0
  let j = y + 1
  let downChar = j < rows ? grid[j].at(x2) : ' '
  while (downChar === '|' || downChar === '+')
  {
    if (downChar === '+')
      rectCount += scanLeft(grid, x1, x2, y, j)
    j++
    if (j >= rows)
      break
    else
      downChar = grid[j].at(x2)
  }
  return rectCount
}

function scanLeft(grid, x1, x2, y1, y2)
{
  let rectCount = 0
  let i = x2 - 1
  let leftChar = i >= 0 && i >= x1 ? grid[y2].at(i) : ' '
  while (leftChar === '-' || leftChar === '+')
  {
    if (i === x1)
    {
      if (leftChar !== '+')
        return 0
      else
        return scanUp(grid, x1, x2, y1, y2)
    }
    i--
    if (i < 0)
      break
    else
      leftChar = grid[y2].at(i)
  }
  return rectCount
}

function scanUp(grid, x1, x2, y1, y2)
{
  let rectCount = 0
  let j = y2 - 1
  let upChar = j >= 0 && j >= y1 ? grid[j].at(x1) : ' '
  while (upChar === '|' || upChar === '+')
  {
    if (j === y1)
    {
      if (upChar !== '+')
        return 0
      else
        return 1
    }
    j--
    if (j < 0)
      break
    else
      upChar = grid[j].at(x1)
  }
  return rectCount
}

export function count(grid) {
  let rows = grid.length
  let columns = grid.length < 1 ? 0 : grid[0].length

  if ((rows === 0) || (columns === 0))
    return 0

  let rectangleCount = 0
  for (let j = 0; j < rows; j++)
  {
    for (let i = 0; i < columns; i++)
    {
      if (grid[j].at(i) === '+')
        rectangleCount += scanRight(grid, i, j, rows, columns)
    }
  }
  return rectangleCount
}
