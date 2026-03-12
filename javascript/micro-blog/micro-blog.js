//
// This is only a SKELETON file for the 'Micro-blog' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

const MAX_CHARS = 5

export const truncate_hard = (input) => {
  let codes = input.split("")
  let ret = ""
  let chars = 0
  let index = 0
  while (index < codes.length && chars < MAX_CHARS)
  {
    let codeNum = codes[index].charCodeAt(0)
    if (codeNum >= 0xD800 && codeNum <= 0xDFFF)
    {
      ret += codes[index];
      let nextChar = index + 1 < codes.length ? codes[index + 1] : " ";
      codeNum = nextChar.charCodeAt(0)
      if (codeNum >= 0xD800 && codeNum <= 0xDFFF) 
      {
        index++;
        ret += codes[index];
        nextChar = index + 1 < codes.length ? codes[index + 1] : " ";
        codeNum = nextChar.charCodeAt(0)
      }
      chars++
      index++
    }
    else 
    {
      ret += codes[index];
      chars++
      index++
    }
  }
  return ret
}

export const truncate_easy = (input) => {
  
  if (input.length <= MAX_CHARS)
    return input;
  
  let ret = ""
  let chars = 0
  const iterator = input[Symbol.iterator]();
  let char = iterator.next();

  while (!char.done && chars < MAX_CHARS) 
  {
    ret += char.value;
    chars++;
    char = iterator.next()
  }
  return ret
};

export const truncate_stupid_easy = (input) => {
  return [...input].slice(0, 5).join('')
}

export const truncate = (input) => {
  return truncate_stupid_easy(input)
}
