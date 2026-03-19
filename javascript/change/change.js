//
// This is only a SKELETON file for the 'Change' exercise. It's been provided as a
// convenience to get you started writing code faster.
//
const sumArray = (arr) => arr.reduce((total, current) => total + current, 0)

export class Change {
  calculate(coinArray, target) {
    if (target === 0)
      return []
    else if (target < 0)
      throw new Error('Negative totals are not allowed.')
    
    // Yes I will try greedy first.
    const sortedCoins = coinArray.toSorted((a, b) => b - a) 
    // the result should be sorted smallest to largest.
    let ret = this.calculate_helper(sortedCoins, [], target + 1, target, {}).sort((a, b) => a - b)
    // If we return an empty set we did not find a solution.
    if (ret.length === 0)
      throw new Error(`The total ${target} cannot be represented in the given currency.`)
    return ret
  } 

  // Without the memoization complex requests take too much time.
  calculate_helper(coinArray, coins, smallestSoFar, target, memoized)
  {
    let subTotal = sumArray(coins)
    if (subTotal === target)
      return coins

    let next = []
    for(let coin of coinArray)
    {
      if (coin + subTotal <= target)
      {        
        let smaller = []
        let key = String(target - subTotal - coin)
        if (coin + subTotal === target)
          smaller = [coin]
        else if (key in memoized)
          smaller = [coin, ...memoized[key]]
        else 
        {
          smaller = this.calculate_helper(coinArray, [], target - subTotal - coin + 1, target - subTotal - coin, memoized)
          memoized[key] = smaller
          smaller = [coin, ...smaller]
        }
        let current = [...coins, ...smaller]
        if (current.length > 0 && 
          sumArray(current) === target && 
          current.length < smallestSoFar && 
          (next.length === 0 || next.length > current.length))
        {
          next = [...current]
          smallestSoFar = next.length
        }
      }
    }
    return next
  }
  
}

