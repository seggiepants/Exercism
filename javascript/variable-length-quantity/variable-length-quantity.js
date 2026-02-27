//
// This is only a SKELETON file for the 'Variable Length Quantity' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const encode = (numArray) => {  
  let ret = []
  for(let i = 0; i < numArray.length; i++)
  {
    let num = numArray[i]
    let first = true
    let numStack = []
    do
    {
      let current = num & 0b01111111
      
      if (first)
        first = false 
      else 
        current  |= 0b10000000

      numStack.push(current)
      num = num >>> 7
    } while (num !== 0)

    while (numStack.length > 0)
      ret.push(numStack.pop())    
  }
  return ret 
}

export const decode = (numArray) => {
  let ret = []
  let current = 0
  let sign
  for(let i = 0; i < numArray.length; i++)
  {
    let num = numArray[i]
    sign = (num & 0b10000000) >>> 7
    current = ((current << 7) >>> 0) | (num & 0b01111111)
    if (sign === 0)
    {
      ret.push(current >>> 0)
      current = 0
    }
  }
  
  if (sign !== 0)
    throw new Error('Incomplete sequence')

  return ret
};
