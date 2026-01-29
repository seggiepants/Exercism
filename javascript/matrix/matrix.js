//
// This is only a SKELETON file for the 'Matrix' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class Matrix {
  constructor(data) {
    this.matrix = data.split('\n').map((row) => row.split(' ').map((item) => Number(item)))
    let colCount = Math.max(... this.matrix.map((row) => row.length))
    this.transpose = Array.from({length: colCount},(_, i) => i).map((n) => this.matrix.map((row) => row[n]));
  }

  get rows() {
    return this.matrix
  }

  get columns() {
    return this.transpose
  }
}
