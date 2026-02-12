//
// This is only a SKELETON file for the 'Difference Of Squares' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class Squares {
  constructor(num) {
    this._squareOfSum = 0
    this._sumOfSquares = 0

    for(let i = 0; i < num; i++)
    {
      this._squareOfSum += (i + 1)
      this._sumOfSquares += ((i + 1) * (i + 1))
    }
    this._squareOfSum *= this._squareOfSum
  }

  get sumOfSquares() {
    return this._sumOfSquares
  }

  get squareOfSum() {
    return this._squareOfSum
  }

  get difference() {
    return this._squareOfSum - this._sumOfSquares
  }
}
