//
// This is only a SKELETON file for the 'Flower Field' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

/**
 * Return a copy of the input but with empty squares with at least one "flower" neighbor annotated with number of surrounding squares with "flowers".
 * @param {Array(string)} input - Each string is a row in the flower field. Expected for each string to share the same length
 * @returns {Array(string)} annotated version of the input with "flower" counts added for count > 0
 */
export const annotate = (input) => {
  // I want an actual 2d array of characters, it is just easier to me
  let map = input.map((row) => row.split(''))
  let height = input.length
  let width = Math.min(...map.map((row) => row.length)) // if ragged array truncate at smallest width

  let ret = []
  for(let j = 0; j < height; j++)
  {
    let row = ""
    for(let i = 0; i < width; i++)
    {
      let ch = map[j][i]
      if (ch === '*')
        row += ch
      else 
      {
        let count = neighborCount(map, width, height, i, j)
        if (count === 0)
          row += ' '
        else
          row += String(count)
      }
    }
    ret.push(row)
  }
  return ret
};

/**
 * Get the number of neighboring cells with a "flower" or character '*'
 * @param {Array(Array(character))} map 
 * @param {Width of the map} width 
 * @param {Height of the map} height 
 * @param {x-coordinate of location to compute neighbor count} x 
 * @param {y-coordinate of location to compute neighbor count} y 
 */
const neighborCount = (map, width, height, x, y) => {
  let neighbors = 0
  for(let j = y - 1; j <= y + 1; j++)
  {
    if (j < 0 || j >= height)
      continue
    for(let i = x - 1; i <= x + 1; i++)
    {
      if (i === x && j === y)
        continue
      if (i < 0 || i >= width)
        continue
      if (map[j][i] === '*')
        neighbors++
    }
  }
  return neighbors
}
