//
// This is only a SKELETON file for the 'Spiral Matrix' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const spiralMatrix = (size) => {
  let ret = []
  // First make an size x size matrix (arbitrarily filled with zeros)
  for(let j = 0; j < size; j++)
  {
    let row = []
    for(let i = 0; i < size; i++)
    {
      row.push(0)
    }
    ret.push(row)
  }

  let x1 = 0
  let x2 = size - 1
  let y1 = 0
  let y2 = size - 1
  let dx = 1
  let dy = 0
  let x = 0
  let y = 0
  let count = 0
  while (x2 >= x1 || y2 >= y1)
  {
    count++
    ret[y][x] = count
    x += dx
    y += dy
    if (x > x2)
    {
      // point down
      // increment row, reset to x2, and set dy = 1, dx = 0
      y1++
      x = x2 
      y++
      dy = 1
      dx = 0
    }

    if (y > y2)
    {
      // point left
      // decrement col, reset to y2, and set dx = -1, dy = 0
      x2--
      y = y2 
      x--
      dx = -1
      dy = 0
    }

    if (x < x1)
    {
      // point up
      // decrement row, reset to x1, and set dy = -1, dx = 0
      y2--
      x = x1
      y--
      dy = -1
      dx = 0
    }

    if (y < y1)
    {
      // point right
      // increment col, reset to y1, and set dx = 1, dy = 0
      x1++
      y = y1
      x++
      dx = 1
      dy = 0
    }

  }


  return ret
};
