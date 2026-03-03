//
// This is only a SKELETON file for the 'Rotational Cipher' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const rotate = (text, num) => {
  return [...text].map((value) => rotateLetter(value, num)).join('')
};

const rotateLetter = (ch, num) =>
{
  let reference = -1
  
  if (ch >= 'a' && ch <= 'z')  
    reference = 'a'.charCodeAt(0)
  else if (ch >= 'A' && ch <= 'Z')
    reference = 'A'.charCodeAt(0)

  if (reference < 0)
    return ch 

  let code = (ch.charCodeAt(0) - reference + num) % 26
  return String.fromCharCode(code + reference)
}
