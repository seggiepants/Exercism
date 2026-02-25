//
// This is only a SKELETON file for the 'Diamond' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const rows = (letter) => {
  const letterCount = letter.charCodeAt() - 'A'.charCodeAt() + 1
  const maxWidth = (2 * letterCount) - 1
  let ret = []
  for(let i = 0; i < letterCount; i++)
  {
    ret.push(renderRow(i, maxWidth))
  }

  if (letterCount > 0)
  {
    for(let i = letterCount - 2; i >= 0; i--)
    {
      ret.push(renderRow(i, maxWidth))
    }
  }
  return ret

};

function renderRow(i, maxWidth)
{
  let numChars = i === 0 ? 1 : 2
  let interiorPadding = i === 0 ? 0 : (2 * i) - 1
  let exteriorPaddingLeft = Math.floor((maxWidth - numChars - interiorPadding) / 2)
  let exteriorPaddingRight = maxWidth - exteriorPaddingLeft - numChars - interiorPadding
  let ch = String.fromCharCode('A'.charCodeAt() + i)

  if (i === 0)
  {
    return ''.padStart(exteriorPaddingLeft, ' ') + ch + ''.padEnd(exteriorPaddingRight, ' ')
  }
  else 
  {
    return ''.padStart(exteriorPaddingLeft, ' ') + ch + ''.padEnd(interiorPadding, ' ') + ch + ''.padEnd(exteriorPaddingRight, ' ')
  }
}