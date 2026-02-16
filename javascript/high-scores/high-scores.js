//
// This is only a SKELETON file for the 'High Scores' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class HighScores {
  constructor(scores) {
    this._scores = scores
  }

  get scores() {
    return this._scores
  }

  get latest() {
    return this._scores[this.scores.length - 1]
  }

  get personalBest() {
    return Math.max(... this._scores)
  }

  get personalTopThree() {
    let startIndex = 0
    let endIndex = Math.min(this._scores.length, startIndex + 3)
    return [...this._scores].sort((a, b) => b - a).slice(startIndex, endIndex)
  }
}
