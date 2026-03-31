//
// This is only a SKELETON file for the 'Go Counting' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class GoCounting {
  /**
   * Initialize the Go score calculator
   * @param {Array[string]} board - The go board to count a score from
   */
  constructor(board) {
    // Turn into a 2D array of string so we can mark claims when flood filling
    this.board = board.map((row) => row.split(''))
    this.height = board.length
    this.width = Math.min(...board.map((row) => row.length)) // if ragged array width is smallest width
    // . = claimed not processed
    // _ = claimed by none
    // b = claimed by black
    // w = claimed by white
    // W = white tile
    // B = black tile
    // ' ' = empty space on the board
    this.claimed = ['.', '_', 'b', 'w']
  }

  /**
   * Figure out who owns a region which includes a given point and what points
   * are included in that region
   * @param {Number} x x-coordinate of point to check
   * @param {Number} y y-coordinate of point to check
   * @returns {Object} Object with two fields, owner - who owns the territory, 
   * and territory an array of coordinates (presented as an array of length 2) 
   * belonging to the territory
   */
  getTerritory(x, y) {
    // Coordinate outside of the board
    if (x < 0 || x >= this.width || y < 0 || y >= this.height)
      return { error: 'Invalid coordinate' }

    // Black/White tiles cannot be flood-filled
    if (this.board[y][x] !== ' ')
      return { owner: 'NONE', territory: [] }

    let owner = this.floodFill(this.board, x, y, this.width, this.height)
    let results = this.processClaims(this.board, this.width, this.height)
    // clean up our mess - object may be reused.
    this.clearClaims(this.board, this.width, this.height)
    return {owner: owner, territory: owner === 'WHITE' ? results.territoryWhite : owner === 'BLACK' ? results.territoryBlack : results.territoryNone}
  }

  /**
   * Calculate all of the board locations owned by black, white and neither.
   * @returns {Object} Object with three fields. territoryBlack an array of coordinates
   * belonging to the black player (coordinates are a array of size 2 where x is at 0 
   * and y is at 1). territoryWhite the same but territory belonging to the white player,
   * and finally territoryNone coorinates owned by neither the black or white player.
   */
  getTerritories() {
    // flood fill everything
    for(let i = 0; i < this.width; i++)
    {
      for(let j = 0; j < this.height; j++)
      {
        if (this.board[j][i] === ' ')
          this.floodFill(this.board, i, j, this.width, this.height)
      }
    }
    let results = this.processClaims(this.board, this.width, this.height)
    // clean up our mess - object may be reused.
    this.clearClaims(this.board, this.width, this.height)
    return results
  }

  /**
   * Clear claimed sections of the board. Split out so it could be reused.
   * @param {Array[Array[string]]} board - 2D array with a string/character for each location on the board
   * @param {Number} width - Width of the board
   * @param {Number} height - Height of the board
   */
  clearClaims(board, width, height)
  {
    for(let i = 0; i < width;i++)
    {
      for(let j = 0; j < height; j++)
      {
        if (this.claimed.includes(board[j][i]))
        {
          board[j][i] = ' '
        }
      }
    }
  }

  /**
   * 
   * @param {Array[Array[string]]} board - 2D Array of strings where each string is a location on the board
   * @param {Number} x - x-coordinate of location to begin flood fill.
   * @param {Number} y - y-coordinate of location to begin flood fill.
   * @param {Number} width - width of the board.
   * @param {Number} height - height of the board
   * @returns {String} Who owns the given region, WHITE, BLACK, or NONE
   */
  floodFill(board, x, y, width, height) {    
    let black = false
    let white = false
    let stackCheck = []
    stackCheck.push([x, y])
    while (stackCheck.length > 0)
    {
      let [i, j] = stackCheck.pop()

      if (board[j][i] === ' ')
      {
        board[j][i] = '.'
        if ((j - 1 >= 0) && !this.claimed.includes(board[j - 1][i]))
          stackCheck.push([i, j - 1])
        if ((i - 1 >= 0) && !this.claimed.includes(board[j][i - 1]))
          stackCheck.push([i - 1, j])
        if ((j + 1 < height) && !this.claimed.includes(board[j + 1][i]))
          stackCheck.push([i, j + 1])
        if ((i + 1 < width) && !this.claimed.includes(board[j][i + 1]))
          stackCheck.push([i + 1, j])
      }
      else if (board[j][i] === 'B')
      {
        black = true
      }
      else if (board[j][i] === 'W')
      {
        white = true 
      }
    }

    let claim = '_'
    let owner = 'NONE'
    if (black && !white)
    {
      owner = 'BLACK'
      claim = 'b'
    }
    else if (white && !black)
    {
      owner = 'WHITE'
      claim = 'w'
    }

    for(let i = 0; i < width;i++)
    {
      for(let j = 0; j < height; j++)
      {
        if (board[j][i] === '.')
        {
          board[j][i] = claim
        }
      }
    }

    return owner
  }

  /**
   * Process all the claimed locations on the board.
   * @param {Array[Array[String]]} board - 2D Array of string where each string is a location on the board
   * @param {Number} width - Width of the board
   * @param {Number} height - Height of the board
   * @returns {Object} - Has three fields territoryBlack an array of coordinates (coordinates are a 2 entry array)
   * of all locations claimed by player Black. Likewise territoryWhite and territoryNone have an array of coordinates
   * claimed by player White, and player None. Coordinates should be in y then x sort order.
   */
  processClaims(board, width, height)
  {
    let black = [] 
    let white = []
    let unclaimed = []

    for(let i = 0; i < width;i++)
    {
      for(let j = 0; j < height; j++)
      {
        if (this.claimed.includes(board[j][i]))
        {
          let claimedBy = board[j][i]
          switch(claimedBy)
          {
            case '_':
              unclaimed.push([i, j])
              break;
            case 'w':
              white.push([i, j])
              break;
            case 'b':
              black.push([i, j])
          }
        }
      }
    }
    return {territoryBlack: black, territoryWhite: white, territoryNone: unclaimed}
  }
}
