//
// This is only a SKELETON file for the 'Run Length Encoding' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const encode = (message) => {
  let ret = ''
  let i = 0
  while (i < message.length)
  {
    let repeatCount = 0;
    let ch = message[i]
    while(message[i + repeatCount] === ch)
    {
      repeatCount++;
    }
    i += repeatCount
    if (repeatCount === 1)
      ret = ret + ch
    else
      ret = ret + `${repeatCount}${ch}`
  }
  return ret
};

const isDigit = (ch) => {
  return ch[0] >= '0' && ch[0] <= '9'
}

export const decode = (message) => {
  let ret = ''
  let i = 0
  while (i < message.length)
  {
    let ch = message[i]
    if (isDigit(ch))
    {
      let repeatCount = 0
      while (isDigit(ch) && i < message.length)
      {
        repeatCount = 10 * repeatCount + parseInt(ch)
        i++
        if (i < message.length)
          ch = message[i]
      }
      if (i < message.length)
      {
        ret = ret + ch.repeat(repeatCount)
        i++
      }
    }
    else
    {
      ret = ret + ch
      i = i + 1
    }
  }
  return ret
};
