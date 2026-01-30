//
// This is only a SKELETON file for the 'Bob' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const hey = (message) => {
  let re = /[a-zA-Z]+/g
  let hasLetters = (message.replace(re, '').length !== message.length)
  if (message.trim().length === 0)
    return "Fine. Be that way!"
  if ((message.toUpperCase() === message) && hasLetters && (message.trim().endsWith('?')))
    return "Calm down, I know what I'm doing!"
  else if (message.toUpperCase() === message && hasLetters)
    return "Whoa, chill out!"
  else if (message.trim().endsWith('?'))
    return "Sure."
  return "Whatever."
};
