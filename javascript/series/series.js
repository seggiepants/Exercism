//
// This is only a SKELETON file for the 'Series' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class Series {
  constructor(series) {
    this._series = [...series].map((value) => Number(value))
  }

  slices(sliceLength) {
    if (this._series.length === 0)
      throw new Error('series cannot be empty')
    else if (sliceLength === 0)
      throw new Error('slice length cannot be zero')
    else if (sliceLength < 0)
      throw new Error('slice length cannot be negative')
    else if (sliceLength > this._series.length)
      throw new Error('slice length cannot be greater than series length')
  
    return this._series.reduce((previous, current, index, arr) => {
      if (index + sliceLength <= arr.length)
      {
        previous.push(arr.slice(index, index + sliceLength))
      }
      return previous
    }, [])
  }
}
