//
// This is only a SKELETON file for the 'Palindrome Products' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class Palindromes {
  static generate(properties) {
    let found = []

    let isPalindrome = (value) =>
    {
      let str = String(value)
      return (str.length === 1 || str === [... str].reverse().join(''))
    }

    let getFactors = (value, min, max) =>
    {
      let ret = []
      let limit = Math.min(Math.sqrt(value), max)
      for(let i = min; i <= limit; i++)
      {
        if (value % i === 0)
        {          
          let other = value / i
          if (other >= min && other <= max)
            ret.push([i, other])
        }
      }
      return ret
    }

    if (properties.minFactor > properties.maxFactor)
      throw new Error('min must be <= max')

    let min = 0
    let max = 0
    for(let j = properties.minFactor; j <= properties.maxFactor; j++)
    {
      for(let i = j; i <= properties.maxFactor; i++)
      {
        let product = i * j
        if (found.length > 0 && product > min && product < max)
          continue

        if (isPalindrome(product))
        {
          found.push(product)
          if (found.length === 1)
          {
            min = product
            max = product
          }
          else 
          {
            min = Math.min(product, min)
            max = Math.max(product, max)
          }
        }
      }
    }

    if (found.length === 0)
      return { smallest: {value: null, factors:[]}, largest: {value: null, factors: []}}

    let minKey = Math.min(...found)
    let maxKey = Math.max(...found)

    return { 
      smallest: {value: minKey, factors: getFactors(minKey, properties.minFactor, properties.maxFactor)}, 
      largest: {value: maxKey, factors: getFactors(maxKey, properties.minFactor, properties.maxFactor) }}
  }  
}
