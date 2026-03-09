//
// This is only a SKELETON file for the 'Square root' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const squareRoot = (num, x0 = 1) => {
    // I basically swiped this from Wikipedia.org
    // ==========================================
    //isqrt via Newton-Heron iteration with specified initial guess. 
    //Uses 2-cycle oscillation detection.
    //
    //Preconditions:
    //    n >= 0                    # isqrt(0) = 0
    //    x0 > 0, defaults to 1     # initial guess
    //
    //Output:
    //    isqrt(n)

    if (num < 0 || x0 <= 0)
      throw new Error("Invalid input")

	  // isqrt(0) = 0; 
    // isqrt(1) = 1
    if (num < 2)
      return num

    let prev2 = -1   // x_{i-2}
    let prev1 = x0   // x_{i-1}

    while (true)
    {
      let x1 = Math.floor(Math.floor(prev1 + num / prev1) / 2)

      // Case 1: converged (steady value)
      if (x1 === prev1)
        return x1

      // Case 2: oscillation (2-cycle)
      if (x1 === prev2 && x1 !== prev1)
      {
        // We’re flipping between prev1 and prev2
        // Choose the smaller one (the true integer sqrt)
        return Math.min(prev1, x1)
      }

      // Move forward
      prev2 = prev1
      prev1 = x1  
    }
}
