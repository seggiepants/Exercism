//
// This is only a SKELETON file for the 'Largest Series Product' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const largestProduct = (text, sliceLength) => {
  let re = /^\d*$/g // all digits from beginning to end
  if (!re.test(text))
    throw new Error('digits input must only contain digits')

  if (sliceLength > text.length)
    throw new Error('span must not exceed string length')
  else if (sliceLength === 0)
    throw new Error('span must not be zero')
  else if (sliceLength < 0)
    throw new Error('span must not be negative')
  
  let digits = [...text].map((ch) => Number(ch))

  return digits.reduce((previous, current, index, arr) => {
      if (index + sliceLength <= arr.length)
      {
        return Math.max(previous, arr.slice(index, index + sliceLength).reduce((accumulator, value) => accumulator * value, 1))        
      }
      return previous
    }, [])

};
