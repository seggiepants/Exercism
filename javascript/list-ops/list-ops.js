//
// This is only a SKELETON file for the 'List Ops' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class List {
  constructor(starter = null) {
    this.values = []
    if (starter !== null && starter.length > 0)
    {
      for(let value of starter)
      {
        this.values.push(value)
      }
    }
  }

  append(other) {
    for(let value of other.values)
      this.values.push(value)
    return this
  }

  isList(other)
  {
    return typeof other == "object" && other.constructor.name === 'List'
  }

  concat(other) {
    for(let value of other.values)
    {
      if (this.isList(value))
        this.concat(value)
      else
        this.values.push(value)
    }
    return this
  }

  filter(fn) {
    let ret = new List()
    for(let value of this.values)
    {
      if (fn(value))
        ret.values.push(value)
    }
    return ret
  }

  map(fn) {
    let ret = new List()
    for(let value of this.values)
    {
      ret.values.push(fn(value))
    }
    return ret
  }

  length() {
    return this.values.length
  }

  foldl(fn, accumulator) {
    let ret = accumulator
    for(let i = 0; i < this.values.length; i++)
    {
      ret = fn(ret, this.values[i])
    }
    return ret
  }

  foldr(fn, accumulator) {
    let ret = accumulator
    for(let i = this.values.length - 1; i >= 0; i--)
    {
      ret = fn(ret, this.values[i])
    }
    return ret
  }

  reverse() {
    let start = 0;
    let end = this.values.length - 1
    while (start < end)
    {
      let temp = this.values[start]
      this.values[start] = this.values[end]
      this.values[end] = temp
      start++
      end--
    }
    return this
  }
}
