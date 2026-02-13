//
// This is only a SKELETON file for the 'Pythagorean Triplet' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export function triplets({ minFactor, maxFactor, sum }) {
  let ret = []
  let minNum = minFactor || 1
  let maxNum = maxFactor || sum
  for(let a = minNum; a <= maxNum; a++)
  {
    for(let b = a + 1; b <= maxNum; b++)
    {
      // We can infer c. Since a + b + c must equal sum then c = sum - (a + b)
      // if c is not greater than b (which is greater than a) then it is not a match.
      // check the min/max factors too.
      let c = sum - (a + b)
      if (c <= b || a*a + b*b !== c*c || c > maxNum || c < minNum)
        continue
      ret.push(new Triplet(a, b, c))
    }

  }
  return ret 
}

class Triplet {
  constructor(a, b, c) {
    this.a = a
    this.b = b
    this.c = c
  }

  toArray() {
    return [this.a, this.b, this.c]
  }
}
