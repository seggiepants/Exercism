//
// This is only a SKELETON file for the 'OCR Numbers' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const convert = (text) => {
  const CHAR_WIDTH = 3
  const CHAR_HEIGHT = 4
  const NUM_DIGITS = 10
  const digits = [
    ' _     _  _     _  _  _  _  _ ',
    '| |  | _| _||_||_ |_   ||_||_|',
    '|_|  ||_  _|  | _||_|  ||_| _|',
    '                              '
  ]

  let rows = text.split('\n')
  if (rows.length === 0 || rows.length % CHAR_HEIGHT !== 0)
    throw new Error('Doesn\'t match character height')
  
  let minWidth = Math.min(...rows.map((row) => row.length))
  let maxWidth = Math.max(...rows.map((row) => row.length))
  if (minWidth !== maxWidth)
    throw new Error('Ragged text length not allowed.')

  if (maxWidth === 0 || maxWidth % CHAR_WIDTH !== 0)
    throw new Error('Doesn\'t match character width')

  let ret = ''
  for(let y = 0; y < rows.length; y += CHAR_HEIGHT)
  {
    ret += ','
    for(let x = 0; x < rows[y].length; x += CHAR_WIDTH)
    {
      let found = true
      for(let n = 0; n < NUM_DIGITS; n++)
      {
        found = true
        for(let i = 0; i < CHAR_HEIGHT; i++)
        {
          if (digits[i].substring(n * CHAR_WIDTH, (n + 1) * CHAR_WIDTH) !== rows[y + i].substring(x, x + CHAR_WIDTH))
          {
            found = false;
            break
          }
          if (!found)
            break
        }
        if (found)
        {
          ret += String(n)
          break
        }
      }
      if (!found)
        ret += '?'

    }
  }
  // remove leading comma
  return ret.length > 0 ? ret.substring(1) : ""
};
