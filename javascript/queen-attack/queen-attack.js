//
// This is only a SKELETON file for the 'Queen Attack' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class QueenAttack {
  constructor({
    black: [blackRow, blackColumn] = [0, 3],
    white: [whiteRow, whiteColumn] = [7, 3],
  } = {}) {

    if (blackRow === whiteRow && blackColumn === whiteColumn)
      throw new Error('Queens cannot share the same space')

    if (!this.onGameBoard(blackRow, blackColumn) || 
        !this.onGameBoard(whiteRow, whiteColumn))
      throw new Error('Queen must be placed on the board')
    
    this.black = [blackRow, blackColumn]
    this.white = [whiteRow, whiteColumn]
  }

  toString() {
    let ret = [
      ['_', '_', '_', '_', '_', '_', '_', '_']
    , ['_', '_', '_', '_', '_', '_', '_', '_']
    , ['_', '_', '_', '_', '_', '_', '_', '_']
    , ['_', '_', '_', '_', '_', '_', '_', '_']
    , ['_', '_', '_', '_', '_', '_', '_', '_']
    , ['_', '_', '_', '_', '_', '_', '_', '_']
    , ['_', '_', '_', '_', '_', '_', '_', '_']
    , ['_', '_', '_', '_', '_', '_', '_', '_']]

    ret[this.black[0]][this.black[1]] = 'B'
    ret[this.white[0]][this.white[1]] = 'W'

    return ret.map((row) => row.join(' ')).join('\n')
  }

  get canAttack() {
    let dx = Math.abs(this.black[1] - this.white[1])
    let dy = Math.abs(this.black[0] - this.white[0])
    return (dx === 0 || dy === 0 || dx === dy)
  }

  onGameBoard(y, x) {
    return x >= 0 && x < 8 && y >= 0 && y < 8
  }
  
}
