//
// This is only a SKELETON file for the 'State of Tic Tac Toe' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

/**
 * Compute the state of a tic-tac-toe board.
 * @param {Array[string]} board - Game board an array of three strings of length three. Space is a 
 * blank space on the board. Players are 'X', and 'O'
 * @returns {string} 'win' if X or O won, 'draw' if the game ended without a winner, 'ongoing' if the
 * game is incomplete. Throws errors if there is an invalid number of X or O characters (X always goes
 * first).
 */
export const gamestate = (board) => {
  
  let oCount = countPlayer(board, 'O')
  let xCount = countPlayer(board, 'X')
  if (oCount > xCount)
    throw new Error('Wrong turn order: O started')
  else if (xCount > oCount + 1)
    throw new Error('Wrong turn order: X went twice')

  let winX = isWon(board, 'X')
  let winO = isWon(board, 'O')
  let boardFull = isFull(board)

  if (winX && winO) 
  {
    throw new Error('Impossible board: game should have ended after the game was won')
  }

  if (!winX && !winO && boardFull)
    return 'draw'

  if (winX || winO)
    return 'win'

  return 'ongoing'

};

/**
 * Check if a tic-tac-toe board was won by the given player
 * @param {Array[string]} board - Array of three strings of length three representing the board
 * @param {string} player - The player to check for winning 'X', or 'O'
 * @returns (boolean) true if the player won, and false otherwise
 */
const isWon = (board, player) => {
  let current = board.map((row) => row.split(''))
  
  // rows & cols
  for(let i = 0; i < 3; i++)
  {
    if (current[i][0] === current[i][1] && current[i][1] === current[i][2] && current[i][0] === player)
      return true
    else if (current[0][i] === current[1][i] && current[1][i] === current[2][i] && current[0][i] === player)
      return true
  }

  // diagonals
  if (current[0][0] === current[1][1] && current[1][1] === current[2][2] && current[0][0] === player)
    return true

  if (current[2][0] === current[1][1] && current[1][1] === current[0][2] && current[2][0] === player)
    return true

  // No match.
  return false
}

/**
 * Check if a tic-tac-toe board is full
 * @param {Array[string]} board - Array of three strings of length three representing the board
 * @returns True if game board is full and false otherwise
 */
const isFull = (board) => {
  let full = true
  for(let i = 0; i < 3; i++)
  {
    if (board[i].indexOf(' ') >= 0)
    {
      full = false
      break;
    }
  }
  return full
}

const countPlayer = (board, player) => {
  let allSpaces = board[0] + board[1] + board[2]
  return allSpaces.split('').reduce((acc, current) => {
    if (current === player)
      return acc + 1
    return acc
  }, 0)
}
