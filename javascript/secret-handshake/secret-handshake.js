//
// This is only a SKELETON file for the 'Secret Handshake' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

// I can't have this as an object, javascript turns the keys into strings
// so I swapped it for an array instead. 
const actions = ['wink', 'double blink', 'close your eyes', 'jump']

export const commands = (number) => {
  let ret = []
  for(let i = 0; i < actions.length; i++)
  {
    let key = Math.pow(2, i)  // index to power of 2.
    if (number & key)
      ret.push(actions[i])
  }
  // This is separate since it doesn't act like the others. I think the ret = part is redundant.
  if (number & 0b10000)
    ret = ret.reverse()

  return ret
};
