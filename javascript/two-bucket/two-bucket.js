//
// This is only a SKELETON file for the 'Two Bucket' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class TwoBucket {
  /**
   * Two bucket problem given buckets of a size, a goal amount and bucket to fill on step one
   * figure out how many steps to solve the problem. Assuming it can be solved.
   * @param {Number} bucketOne - starting amount in bucket one
   * @param {Number} bucketTwo - starting amount in bucket two
   * @param {Number} goal - amount desired 
   * @param {String} starterBucket - 'one', or 'two'
   */
  constructor(bucketOne, bucketTwo, goal, starterBucket) {

    if (goal > Math.max(bucketOne, bucketTwo))
      throw new Error('Invalid goal - larger than maximum possible.')

    // If I have to solve the problem enough to throw and error when not
    // solvable in the constructor. I may as well just make the solve
    // method a getter.
    let bucketOneAmt = starterBucket === 'one' ? bucketOne : 0
    let bucketTwoAmt = starterBucket === 'two' ? bucketTwo : 0
    this.result = this.step([[bucketOneAmt, bucketTwoAmt]], bucketOneAmt, bucketTwoAmt, starterBucket, bucketOne, bucketTwo, goal)
    if (this.result.goalBucket ==='n/a')
      throw new Error('Invalid goal - no possible path.')
  }

  /**
   * return the search results calculated in the constructor
   * @returns {Object} - Results object with keys 
   * moves: How many moves it took to get to goal state
   * goalBucket: which bucket has the goal amount
   * otherBucket: how much was in the remaining bucket
   */
  solve() {    
    return this.result 
  }

  /**
   * Solve the two bucket problem recursively. Try every possible move then reevaluate from that state
   * until you hit the goal. You can have multiple ways to the goal so return one with the shortest sequence
   * @param {Array} moveStack - Array of Bucket amount pairs. No use reevaluating a previous state
   * @param {*} bucketOneAmt - How many liters bucket one currently contains
   * @param {*} bucketTwoAmt - How many liters bucket two currently contains
   * @param {*} originalBucket - What bucket did we start on for the illegal move checks
   * @param {*} bucketOneMax - How many liters can bucket one hold
   * @param {*} bucketTwoMax - How many liters can bucket two hold
   * @param {*} goal - The number of liters we want to get to in one of the given buckets.
   * @returns {object} - The number of moves, bucket that ended up with the goal amount, and how much
   * was in the remaining bucket returned as an object expected by the test. On error moves will be -1
   * and goalBucket will be n/a also other bucket will be 0.
   */
  step(moveStack, bucketOneAmt, bucketTwoAmt, originalBucket, bucketOneMax, bucketTwoMax, goal)
  {
    // Base state, return on goal
    if (bucketOneAmt === goal)
    {
      return {
        "moves": moveStack.length 
        , "goalBucket": "one"
        , "otherBucket": bucketTwoAmt
      }
    }
    else if (bucketTwoAmt === goal)
    {
      return {
        "moves": moveStack.length 
        , "goalBucket": "two"
        , "otherBucket": bucketOneAmt
      }
    }

    // Three possible actions:
    // - fill one or two
    // - pour one into two or two into one until other full or current empty
    // - empty one or two
    // Move is invalid if it leaves originalBucket = empty and otherBucket = filled
    let results = []
    
    // Fill 1
    let illegalFill = originalBucket === 'two' && bucketTwoAmt === 0
    let wasVisited = moveStack.some((pair) => pair[0] === bucketOneMax && pair[1] === bucketTwoAmt)
    if (!(illegalFill || wasVisited || bucketOneAmt === bucketOneMax))
    {
      let result = this.step([...moveStack, [bucketOneMax, bucketTwoAmt]],
          bucketOneMax, 
          bucketTwoAmt,
          originalBucket,
          bucketOneMax,
          bucketTwoMax,
          goal)
        if (result.moves >= 0)
          results.push(result)
    }

    // Fill 2
    illegalFill = originalBucket === 'one' && bucketOneAmt === 0
    wasVisited = moveStack.some((pair) => pair[0] === bucketOneAmt && pair[1] === bucketTwoMax)
    if (!(illegalFill || wasVisited || bucketTwoAmt === bucketTwoMax))
    {
      let result = this.step([...moveStack, [bucketOneAmt, bucketTwoMax]],
          bucketOneAmt, 
          bucketTwoMax,
          originalBucket,
          bucketOneMax,
          bucketTwoMax,
          goal)
        if (result.moves >= 0)
          results.push(result)      
    }
    
    // Pour 1 to 2
    let amountToPour = Math.min(bucketOneAmt, bucketTwoMax - bucketTwoAmt)
    let illegalPour = originalBucket === 'one' && bucketOneAmt - amountToPour === 0 && bucketTwoAmt + amountToPour === bucketTwoMax
    wasVisited = moveStack.some((pair) => pair[0] === bucketOneAmt - amountToPour && pair[1] === bucketTwoAmt + amountToPour)
    if (!(illegalPour || wasVisited || amountToPour <= 0))
    {
      let result = this.step([...moveStack, [bucketOneAmt - amountToPour, bucketTwoAmt + amountToPour]],
          bucketOneAmt - amountToPour, 
          bucketTwoAmt + amountToPour,
          originalBucket,
          bucketOneMax,
          bucketTwoMax,
          goal)
        if (result.moves >= 0)
          results.push(result)      
    }

    // Pour 2 to 1
    amountToPour = Math.min(bucketTwoAmt, bucketOneMax - bucketOneAmt)
    illegalPour = originalBucket === 'two' && bucketTwoAmt - amountToPour === 0 && bucketOneAmt + amountToPour === bucketOneMax
    wasVisited = moveStack.some((pair) => pair[0] === bucketOneAmt + amountToPour && pair[1] === bucketTwoAmt - amountToPour)
    if (!(illegalPour || wasVisited || amountToPour <= 0))
    {
      let result = this.step([...moveStack, [bucketOneAmt + amountToPour, bucketTwoAmt - amountToPour]],
          bucketOneAmt + amountToPour, 
          bucketTwoAmt - amountToPour,
          originalBucket,
          bucketOneMax,
          bucketTwoMax,
          goal)
        if (result.moves >= 0)
          results.push(result)      
    }

    // Empty 1
    let illegalEmpty = (originalBucket === 'one' && bucketTwoAmt === bucketTwoMax)
    wasVisited = moveStack.some((pair) => pair[0] === 0 && pair[1] === bucketTwoAmt)
    if (!(illegalEmpty || wasVisited || bucketOneAmt === 0))
    {
      let result = this.step([...moveStack, [0, bucketTwoAmt]],
          0, 
          bucketTwoAmt,
          originalBucket,
          bucketOneMax,
          bucketTwoMax,
          goal)
        if (result.moves >= 0)
          results.push(result)      
    }

    // Empty 2
    illegalEmpty = (originalBucket === 'two' && bucketOneAmt === bucketOneMax)
    wasVisited = moveStack.some((pair) => pair[0] === bucketOneAmt && pair[1] === 0)
    if (!(illegalEmpty || wasVisited || bucketTwoAmt === 0))
    {
      let result = this.step([...moveStack, [bucketOneAmt, 0]],
          bucketOneAmt, 
          0,
          originalBucket,
          bucketOneMax,
          bucketTwoMax,
          goal)
        if (result.moves >= 0)
          results.push(result)      
    }

    // Failure if we ran out of legal moves.
    if (results.length === 0)
      return {
        "moves": -1
        , "goalBucket": 'n/a'
        , "otherBucket": 0
      }

    // Return the result with the shortest path. Don't care 
    // how a tie is sorted as we only really want the length.
    return results.sort((a, b) => a.moves - b.moves)[0]
  }
}
