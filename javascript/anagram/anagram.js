//
// This is only a SKELETON file for the 'Anagram' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const findAnagrams = (word, candidates) => {
  let canonical = (text) => [...text.toLowerCase()].sort().join('')
  let baseline = canonical(word)
  let ret = []
  for(let candidate of candidates)
  {
    if (candidate.toLowerCase() !== word.toLowerCase())
    {
      let current = canonical(candidate)
      if (current === baseline)
        ret.push(candidate)
    }
  }
  return ret
};


