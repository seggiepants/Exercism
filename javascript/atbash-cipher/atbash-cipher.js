//
// This is only a SKELETON file for the 'Atbash Cipher' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

const alphaDigits = "abcdefghijklmnopqrstuvwxyz0123456789";
const reverseAlphaDigits = "zyxwvutsrqponmlkjihgfedcba0123456789";
const CHUNK_SIZE = 5;

const transform = (message, key, value) => {
  return [...message].reduce((output, ch) => {
    let index = key.indexOf(ch)
    if (index >= 0)
      return output + value[index]
    return output
  }, "")
}

const chunk = (message) =>
{
  let ret = "";
  for(let i = 0; i < message.length; i += CHUNK_SIZE)
  {
    ret += message.slice(i, i + CHUNK_SIZE) + " "
  }
  return ret.trimEnd();
}

export const encode = (message) => {
  return chunk(transform(message.toLowerCase(), alphaDigits, reverseAlphaDigits))
};

export const decode = (message) => {
  return transform(message.toLowerCase(), reverseAlphaDigits, alphaDigits)
};
