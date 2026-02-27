//
// This is only a SKELETON file for the 'Word Search' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

class WordSearch {
  /**
   * 
   * @param {*} grid - Array of character strings. String lengths should all be the same.
   */
  constructor(grid) {
    this._grid = [... grid]
  }

  find(wordArray) {
    /*
    Expected output format
    {
      word: {
        start: [row, col],
        end: [row, col],
      },
    };
    */
    let ret = {}
    for (let word of wordArray)
    {
      let wordReverse = word.split('').reverse().join('')
      for(let row = 0; row < this._grid.length; row++)
      {
        // left to right, or right to left
        let index = this._grid[row].indexOf(word)
        if (index >= 0)
        {
          ret[word] = {start: [row + 1, index + 1 ], end: [row + 1, index + word.length]}
          break;
        }
        
        index = this._grid[row].indexOf(wordReverse)
        if (index >= 0)
        {
          ret[word] = {start: [row + 1, index + word.length], end: [row + 1, index + 1 ]}
          break;
        }        
      }

      for(let col = 0; col < this._grid[0].length; col++)
      {
        let column = this._grid.map((value) => value[col]).join('')

        // top to bottom, or bottom to top
        let index = column.indexOf(word)
        if (index >= 0)
        {
          ret[word] = {start: [index + 1, col + 1 ], end: [index + word.length, col + 1, ]}
          break;
        }
        
        index = column.indexOf(wordReverse)
        if (index >= 0)
        {
          ret[word] = {start: [index + word.length, col + 1], end: [index + 1, col + 1]}
          break;
        }        
      }

      let topRow = [...this._grid[0].split('').map((_, index) => [index, 0])]
      let bottomRow = [...this._grid[0].split('').map((_, index) => [index, this._grid.length - 1])]
      let leftColumn = [...this._grid.slice(1).map((_, index) => [0, index])]
      //let rightColumn = [...this._grid.slice(1).map((_, index) => [this._grid[0].lenght - 1, index])]
      let diagonalsLR = [...topRow, ...leftColumn]
      let diagonalsRL = [...bottomRow, ...leftColumn]
      let rowCount = this._grid.length
      let colCount = this._grid[0].length 
      for(let pair of diagonalsLR)
      {
        let line = []
        let i = pair[0]
        let j = pair[1]
        while (i < colCount && j < rowCount)
        {
          line.push(this._grid[j][i])
          i++
          j++
        }
        let textLine = line.join('')
        let index = textLine.indexOf(word)
        if (index >= 0)
        {
          ret[word] = {start: [pair[0] + index + 1, pair[1] + index + 1 ], end: [pair[0] + index + word.length, pair[1] + index + word.length]}
          break;
        }
        
        index = textLine.indexOf(wordReverse)
        if (index >= 0)
        {
          ret[word] = {start: [pair[1] + index + word.length, pair[0] + index + word.length], end: [pair[1] + index + 1, pair[0] + index + 1]}
          break;
        }        
      }

      for(let pair of diagonalsRL)
      {
        let line = []
        let i = pair[0]
        let j = pair[1]
        while (i < this._grid[0].length && j >= 0)
        {
          line.push(this._grid[j][i])
          i++
          j--
        }
        let textLine = line.join('')
        let index = textLine.indexOf(word)
        if (index >= 0)
        {
          ret[word] = {start: [pair[0] + word.length + index, pair[1] - index - word.length + 2], end: [pair[0] + index + 1, pair[1] - index + 1]}
          break;
        }
        
        index = textLine.indexOf(wordReverse)
        if (index >= 0)
        {
          ret[word] = {start: [pair[0] + index + 2, pair[1] - index], end: [pair[0] + index + word.length + 1, pair[1] - index - word.length + 1]}
          break;
        }        
      }

      if (!(word in ret))
      {
        ret[word] = undefined
      }
    }
    return ret
  }
}

export default WordSearch;
