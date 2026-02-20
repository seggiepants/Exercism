//
// This is only a SKELETON file for the 'Pig Latin' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

const vowels = 'aeiou'
const consonants = 'bcdfghjklmnpqrstvwxyz'

export const translate = (message) => {
  return message.split(' ').map((word) => translateWord(word.toLowerCase())).join(' ')
};

function translateWord(word)
{
  let rule1 = /^([aeiou]|yt|xr)[a-z]+$/gi
  let rule2 = /^[b-df-hj-np-tv-z]+[aeiouy][a-z]*$/gi
  let rule3 = /^[b-df-hj-np-tvwxz]*qu[a-z]*$/gi
  let rule4 = /^[b-df-hj-np-tvwxz]+y[a-z]*$/gi
  let index

  // Rule 1 - starts with 'xr', or 'yt'
  if (rule1.test(word))
  {
    return word + 'ay'
  }

  // Check rules 3 and 4 first as rule 2 is a more generic

  // Rule 4 - starts with one or more consonant followed by a y 
  if (rule4.test(word))
  {
    index = word.split('').findIndex((ch) => ch === 'y')
    return word.substring(index) + word.substring(0, index) + 'ay'
  }

  // Rule 3 - One or more consonants followed by qu
  if (rule3.test(word))
  {
    index = 0
    while ((index < word.length - 1) && (word.substring(index, index + 2) !== 'qu'))
      index++
    if (word.substring(index, index + 2) !== 'qu')
      throw new Error('Should never get here')
    return word.substring(index + 2) + word.substring(0, index + 2) + 'ay'
  }

  // Rule 2 - One or more consonants followed by a vowel
  if (rule2.test(word))
  {
    index = 0
    while (consonants.includes(word[index]) && index < word.length)
    {
      index += 1
    }
    if (index < word.length && vowels.includes(word[index]))
    {
      return word.substring(index) + word.substring(0, index) + 'ay'
    }
    else 
      throw new Error('Should never get here')
  }

  return word
}
