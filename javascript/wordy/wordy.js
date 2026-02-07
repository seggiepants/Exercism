//
// This is only a SKELETON file for the 'Wordy' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const answer = (text) => {
  const skipWords = ['what', 'is', 'by'] // unnecesary accepted but pointless words
  // operations and a function that implements the operation
  const operators = {
    'plus': (a, b) => a + b, 
    'minus': (a, b) => a - b, 
    'multiplied': (a, b) => a * b, 
    'divided': (a, b) => a / b 
  }
  // remove trailing ? and split into words
  let words = text.replace("?", "").split(/\s+/)
  let operator = ''         // latest operation -- empty means none.
  let result = 0            // current result
  let waitForNumber = true  // have we found a number yet?

  for(let word of words)
  {
    word = word.toLowerCase() // Lets make this case-insensitive
    if (isNumber(word))
    {
      let num = Number.parseInt(word)
      if (operator.length > 0)
      {
        if (operator in operators)
        {
          // calculate the current value
          result = operators[operator](result, num)
        }
        else
        {
          // operator not in operations dictionary
          throw new Error('Unknown operation')
        }
        operator = '' // operator consumed wait for another
      }
      else if (waitForNumber)
      {
        result = num          // first number is the value of the expression until we find more numbers/operations
        waitForNumber = false // we found the first number
      }
      else 
      {
        // operator before a number.
        throw new Error('Syntax error')
      }
    }
    else if (skipWords.includes(word))
    {
      // Do nothing, we want to skip that word
    }
    else if (word in operators && operator !== '')
    {
      // Two operators in a row
      throw new Error('Syntax error')
    }
    else if (word in operators && operator === '')
    {
      // Operator but not two or more in a row
      if (waitForNumber)
      {
        // Have to have at least one parsed number before carrying out an operation
        throw new Error('Syntax error')
      }

      operator = word
    } 
    else
    {
      // Unhandled
      throw new Error('Unknown operation')
    }
  }

  if (operator !== '' || waitForNumber)
  {
    // hanging operator or no number in text
    throw new Error('Syntax error')
  }
  return result
}

function isNumber(text)
{
  // test if a word is a number or not.
  // only works with integers
  let re = /^-?\d+$/
  return re.test(text)
}
