//
// This is only a SKELETON file for the 'Connect' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class Board {
  constructor(board) {
    this._board = board.map((value) => value.split(''))
  }

  /**
   * Check to see if anyone has won the given game of Hex/Polygon/Con-Tac-Tix
   * The player can move left, right, upper left, upper right, lower left, and lower-right
   * @returns '' if no winner, X if path of X left to right, and O if path of O from top to bottom
   */
  winner() {
    let xCandidates = this._board.reduce((results, value, index) => value[index] === 'X' ? [...results, [index, index]] : results, [])
    let oCandidates = this._board[0].reduce((results, value, index) => value === 'O' ? [...results, [index, 0]] : results, [])

    if (typeof xCandidates !== 'undefined' && xCandidates.length > 0)
    {
      let ret = floodFill(this._board, xCandidates, new Set(), 'X')
      if (ret.some((pair) => pair[0] === pair[1]) && ret.some((pair) => pair[0] == this._board[pair[1]].length - 1))
        return 'X'
    }

    if (typeof oCandidates !== 'undefined' && oCandidates.length > 0)
    {
      let ret = floodFill(this._board, oCandidates, new Set(), 'O')
      if (ret.some((pair) => pair[1] === 0) && ret.some((pair) => pair[1] == this._board.length - 1))
        return 'O'
    }

    return ''
  }
}

const getNeighbors = (board, visited, x, y, ch) =>
{
  const delta = [[-1, -1], [1, -1], [-2, 0], [2, 0], [-1, 1], [1, 1]]
  let ret = []
  let maxY = board.length
  for(let i = 0; i < delta.length; i++)
  {
    let newY = y + delta[i][1]
    if (newY < 0 || newY >= maxY)
      continue

    let maxX =board[y + delta[i][1]].length 
    let newX = x + delta[i][0]
    if (newX < 0 || newX >= maxX)
      continue

    if (board[newY][newX] !== ch)
      continue

    let key = `${newX},${newY}`
    if (visited.has(key))
      continue

    ret.push([newX, newY])
  }

  return ret 
}

const floodFill = (board, queue, visited, ch) =>
{
  while (queue.length > 0)
  {
    let pair = queue.shift()
    let key = `${pair[0]},${pair[1]}`
    if (visited.has(key))
      continue

    visited.add(key)
    let neighbors = getNeighbors(board, visited, pair[0], pair[1], ch)
    queue = [...queue, ...neighbors]
  }  
  return [...visited].map((value) => value.split(',').map((item) => Number(item)))
}