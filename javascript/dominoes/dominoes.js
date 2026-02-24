//
// This is only a SKELETON file for the 'Dominoes' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const chain = (tiles) => {
  if (tiles.length === 0)
    return []
  // Start the chain with the first tile.
  let chained = [tiles[0]]
  let result = helper(chained, tiles.slice(1))
  if (result != null && result.length !== tiles.length)
  {
    // If that didn't work, try again with the tile reversed.
    chained = [tileReverse(tiles[0])]
    result = helper(chained, tiles.slice(1))
  }
  if (result == null || result.length !== tiles.length)
    return null

  if (result[0][0] !== result[result.length - 1][1])
    return null
  return result
  
};

function helper(chained, remaining)
{
  if (remaining.length === 0)
    return chained

  let first = chained[0][0]
  let last = chained[chained.length - 1][1]

  // last one?
  if (remaining.length === 1)
  {
    let lastTile = remaining[0]
    if (lastTile[1] === first && lastTile[0] === last)
    {
      // match chain ends when normal
      return [...chained, lastTile]
    }
    else if (lastTile[0] === first && lastTile[1] === last)
    {
      // match chain ends when reversed
      return [...chained, tileReverse(lastTile)]
    }
    else
      return null
  }

  for(let i = 0; i < remaining.length; i++)
  {
    let current = remaining[i]
    let currentReverse = tileReverse(current)

    if (current[1] === first)
    {
      // match first tile 
      let result = helper([current, ...chained], [...remaining.filter((value, index) => index !== i)])
      if (result !== null && result.length === chained.length + remaining.length)
        return result      
    }
    else if (currentReverse[1] === first)
    {
      // match first tile reversed
      let result = helper([currentReverse, ...chained], [...remaining.filter((value, index) => index !== i)])
      if (result !== null && result.length === chained.length + remaining.length)
        return result      
    }
    else if (current[0] === last)
    {
      // match last tile 
      let result = helper([...chained, current], [...remaining.filter((value, index) => index !== i)])
      if (result !== null && result.length === chained.length + remaining.length)
        return result      
    }
    else if (currentReverse[0] === last)
    {
      // match last tile 
      let result = helper([...chained, currentReverse], [...remaining.filter((value, index) => index !== i)])
      if (result !== null && result.length === chained.length + remaining.length)
        return result      
    }
  }

  return null // No matches for current chain
}

function tileReverse(tile)
{
  return [tile[1], tile[0]]
}


