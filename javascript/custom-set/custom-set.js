//
// This is only a SKELETON file for the 'Custom Set' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

// Implementation is a sorted list. Binary search to see if an element exists.
// Add if not present, skip if present.

// After seeing community solutions it now uses higher order functions.

export class CustomSet {
  constructor(initial = []) {
    this._data = []
    for(let value of initial)
      this.add(value)
  }

  find(value, min, max)
  {
    if (value.length === 0)
      return -1
    else if (max < min)
      return -1 
    else if (max === min)
      if (this._data[min] === value)
        return min
      else 
        return -1
    
    let midPoint = Math.floor(((max - min) / 2) + min)
    let midPointValue = this._data[midPoint]
    if (midPointValue === value)
      return midPoint 
    else if (midPointValue > value)
      return this.find(value, min, midPoint - 1)
    else // midPointValue < value
      return this.find(value, midPoint + 1, max)
  }

  empty() {
    return this._data.length === 0
  }

  contains(value) {
    return this.find(value, 0, this._data.length - 1) >= 0
  }

  add(value) {
    let index = this.find(value, 0, this._data.length - 1)
    if (index < 0)
    {
      this._data.push(value)
      this._data.sort((a, b) => a - b)
    }
    return this
  }

  subset(other) {
    return this._data.every((value) => other.contains(value))
  }

  disjoint(other) {    
    return !this._data.some((value) => other.contains(value))
  }

  eql(other) {
    if (this._data.length !== other._data.length)
      return false 
    return this.subset(other)
  }

  union(other) {
    return  new CustomSet([...this._data, ...other._data])
  }

  intersection(other) {
    return new CustomSet(this._data.filter((value) => other.contains(value)))
  }

  difference(other) {
    return new CustomSet(this._data.filter((value) => !other.contains(value)))
  }
}
