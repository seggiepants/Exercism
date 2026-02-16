//
// This is only a SKELETON file for the 'Phone Number' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const clean = (original) => {
  let punctuation = '!@#$%^&*()_+-=~`[]\\{}|;\':\',./<>?'
  let letters = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ'

  // Actually fix it.
  let simplified = original.
    trim().
    replaceAll(' ', '',).
    replaceAll('.', '').
    replaceAll('-', '').
    replaceAll('(', '').
    replaceAll(')', '').
    replaceAll('+', '')
  
  // Validate it is ok
  if (simplified.length > 11)
    throw new Error('Must not be greater than 11 digits')
  else if (simplified.length === 11)
  {
    if (simplified[0] !== '1')
      throw new Error('11 digits must start with 1')
    simplified = simplified.substring(1) // chop it off
  }
  else if (simplified.length < 10)
    throw new Error('Must not be fewer than 10 digits')
  
  for(let i = 0; i < simplified.length; i++)
  {
    let ch = simplified[i]
    if (ch < '0' || ch > '9') // isDigit
    {
      if (punctuation.includes(ch))
        throw new Error('Punctuations not permitted')
      else if (letters.includes(ch))
        throw new Error('Letters not permitted')
      else 
        throw new Error('Non-digit found')
    }

    if (i === 0)
    {
      if (ch === '0')
        throw new Error('Area code cannot start with zero')
      else if (ch === '1')
        throw new Error('Area code cannot start with one')
    }
    else if (i === 3)
    {
      if (ch === '0')
        throw new Error('Exchange code cannot start with zero')
      else if (ch === '1')
        throw new Error('Exchange code cannot start with one')
    }
  }
  return simplified
}
