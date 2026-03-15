//
// This is only a SKELETON file for the 'Knapsack' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const knapsack = (maximumWeight, items) => {
  items.sort((a, b) => a.weight === b.weight ? b.value - a.value : b.weight - a.weight)
  return knapsack_helper(maximumWeight, items, {})
}

const knapsack_helper = (maximumWeight, items, memoized) => {
  // items are array of weight, value pairs.
  let scores = []
  for(let i = 0; i < items.length; i++)
  {
    let item = items[i]
    if (item.weight < maximumWeight)
    {
      let nextItems = items.filter((value, index) => index !== i && value.weight <= maximumWeight)
      let nextWeight = maximumWeight - item.weight;
      let value
      let key = nextWeight + ',[' + nextItems.reduce((accum, current) => accum + `${current.weight}|${current.value},`, '') + ']'
      if (key in memoized)
        value = memoized[key]
      else 
      {
        value = knapsack_helper(nextWeight, nextItems, memoized)
        memoized[key] = value
      }
      scores.push(item.value + value)
    }
    else if (item.weight === maximumWeight)
      scores.push(item.value)
  }
  return Math.max(0, ...scores)
};
