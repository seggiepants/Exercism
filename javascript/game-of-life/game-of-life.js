//
// This is only a SKELETON file for the 'Conway's Game of Life' exercise. It's been provided
// as a convenience to get you started writing code faster.
//

export class GameOfLife {
  /**
   * Initialize the game of life
   * @param {Array[][]} matrix - 2D Array of beginning game state, 0 is dead, > 0 = alive
   */
  constructor(matrix) {
    this._matrix = matrix
    this._rows = matrix.length
    this._cols = matrix.reduce((accumulator, row) => accumulator = Math.max(row.length, accumulator), 0)

  }

  /**
   * Run one step of the game of life.
   */
  tick() {
    var next = Array.from({ length: this._rows }, () => Array(this._cols).fill(0));
    for(let y = 0; y < this._rows; y++)
    {
      for (let x = 0; x < this._cols; x++)
      {
        let neighbors = this.getNeighbors(x, y)
        let alive = this._matrix[y][x] > 0
        if (alive && (neighbors === 2 || neighbors === 3))
          next[y][x] = 1 
        else if (!alive && neighbors === 3)
          next[y][x] = 1
        else 
          next[y][x] = 0
      }
    }
    this._matrix = next
  }

  /**
   * Return the number of neighboring cells that are alive.
   * @param {*} x - x-coordinate of current cell
   * @param {*} y - y-coordinate of current cell
   * @returns - Number of live (value > 0) cells
   */
  getNeighbors(x, y)
  {
    let neighbors = 0
    for(let j = y - 1; j <= y + 1; j++)
    {
      if (j < 0 || j >= this._rows)
        continue  // out of bounds
      for (let i = x - 1; i <= x + 1; i++)
      {
        if (i < 0 || i >= this._cols)
          continue  // out of bounds
        if (i === x && j === y)
          continue; // you are not your own neighbor
        neighbors += this._matrix[j][i] > 0 ? 1 : 0
      }
    }
    return neighbors
  }

  /**
   * Return the current state of the game.
   * @returns The current state of the game.
   */
  state() {
    return this._matrix
  }
}
