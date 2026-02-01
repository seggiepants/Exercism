//
// This is only a SKELETON file for the 'BookStore' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

// we really need to determine all permutations then score all of them 
// and take the min.

function getPermutations() 
{
  let permutations = []
  // all 5
  permutations.push([1, 2, 3, 4, 5])
  
  // include 4 -- skip 1
  for(let i = 0; i < 5; i++)
  {
    let baseline = [1, 2, 3, 4, 5]
    baseline.splice(i, 1)
    permutations.push(baseline)
  }

  // Length 3
  for(let k = 1; k <= 3; k++)
  {
    for(let j = k + 1; j <= 4; j++)
    {
      for(let i = j + 1; i <= 5; i++)
      {
        permutations.push([k, j, i])
      }
    }
  }

  // Length 2
  for(let j = 1; j <= 5; j++)
  {
    for(let i = j + 1; i <= 5; i++)
    {
      permutations.push([j, i])

    }
  }

  // single 
  for(let i = 0; i < 5; i++)
  {
    let baseline = []
    baseline.push(i + 1)
    permutations.push(baseline)
  }

  return permutations

}

function dictToKey(dict)
{
  return Object.keys(dict).map((key) => `${key}=${dict[key]}`).join('|')
}

function bookHelper(permutations, dictBooks)
{
  const BOOK_PRICE = 800
  const DISCOUNT = { 1: 0, 2: 0.05, 3: 0.10, 4: 0.20, 5: 0.25}
  const PRICES = {
    1: BOOK_PRICE, 
    2: (2 * BOOK_PRICE) * (1 - DISCOUNT[2]),
    3: (3 * BOOK_PRICE) * (1 - DISCOUNT[3]),
    4: (4 * BOOK_PRICE) * (1 - DISCOUNT[4]),
    5: (5 * BOOK_PRICE) * (1 - DISCOUNT[5]),
  }

  let sum = Object.keys(dictBooks).reduce((current, key) => current + dictBooks[key], 0)
  if (sum === 0)
    return 0

  let key = dictToKey(dictBooks)

  if (bookHelper.cache === undefined) 
  {
    bookHelper.cache = {}
  }

  if (key in bookHelper.cache)
  {
    return bookHelper.cache[key]
  }

  let scores = []

  for(let permutation of permutations)
  {
    let copy = {}
    Object.assign(copy, dictBooks)
    if (!permutation.some((n) => copy[n] === 0))
    {
      permutation.map((n) => copy[n] -= 1)
      scores.push(PRICES[permutation.length] + bookHelper(permutations, copy))
    }
  }
  bookHelper.cache[key] = Math.min(... scores)
  return bookHelper.cache[key]
}


export const cost = (books) => {

  let dictBooks = {
    1: 0, 2: 0, 3: 0, 4: 0, 5: 0
  }
  books.map((num) => dictBooks[num] += 1)
  let permutations = getPermutations()
  return bookHelper(permutations, dictBooks)
}
