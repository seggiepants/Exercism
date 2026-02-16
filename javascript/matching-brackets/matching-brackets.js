//
// This is only a SKELETON file for the 'Matching Brackets' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const isPaired = (code) => {
  let stack = []
  let match = {
    '}': '{', 
    ')': '(',
    ']': '[',
  }

  for(let ch of [...code])
  {
    switch(ch)
    {
      case '{':
      case '(':
      case '[':
        stack.push(ch)
        break;
      case '}':
      case ')':
      case ']':
        if (stack.length === 0 || stack.pop() !== match[ch])
          return false
        break;
    }
  }
  return stack.length === 0
}
