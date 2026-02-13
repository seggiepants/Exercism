//
// This is only a SKELETON file for the 'Palindrome Products' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class Palindromes {
  static generate(properties) {
    let found = {}

    let isPalindrome = (value) =>
    {
      let str = String(value)
      return (str.length === 1 || str === [... str].reverse().join(''))
    }

    if (properties.minFactor > properties.maxFactor)
      throw new Error('min must be <= max')

    for(let j = properties.minFactor; j <= properties.maxFactor; j++)
    {
      for(let i = j; i <= properties.maxFactor; i++)
      {
        let product = i * j
        
        if (isPalindrome(product))
        {
          if (!(product in found))
          {
            found[product] = []
          }
          found[product].push([j, i])
        }
      }
    }
    let keys = Object.keys(found).map((key) => Number(key))
    if (keys.length === 0)
      return { smallest: {value: null, factors:[]}, largest: {value: null, factors: []}}

    let minKey = String(Math.min(...keys))
    let maxKey = String(Math.max(...keys))

    return { smallest: {value: Number(minKey), factors: found[minKey]}, largest: {value: Number(maxKey), factors: found[maxKey] }}
  }
}
