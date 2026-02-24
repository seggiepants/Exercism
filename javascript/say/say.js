//
// This is only a SKELETON file for the 'Say' exercise. It's been provided as a
// convenience to get you started writing code faster.
//
let zeroToNineteen = [
  'zero', 'one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine', 'ten', 
  'eleven', 'twelve', 'thirteen', 'fourteen', 'fifteen', 'sixteen', 'seventeen', 'eighteen', 'nineteen']

let tens = ['', '', 'twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety']

export const say = (n) => {
  
  if (n < 0 || n > 999_999_999_999)
    throw new Error('Number must be between 0 and 999,999,999,999.')

  if (n == 0)
    return zeroToNineteen[n]

  let triples = [
    { name: 'billion', value: 1_000_000_000},
    { name: 'million', value: 1_000_000},
    { name: 'thousand', value: 1_000}
  ]
  
  let ret = ''
  for(let i = 0; i < triples.length; i++)
  {
    let value = triples[i].value
    let larger = value * 1000
    if (n % larger >= value)
    {
      let threeDigit = ((n - (n % value)) / value) % 1_000

      if (threeDigit > 0)
      {
        if (ret.length > 0)
          ret += ' '
        ret += zeroToNineHundredNinteyNine(threeDigit) + ' ' + triples[i].name 
      }
    }       
  }
  
  if (n % 1_000 > 0)
  {
    if (ret.length > 0)
      ret = ret + ' '
    ret += zeroToNineHundredNinteyNine(n % 1_000)    
  }
  return ret
};

function zeroToNineHundredNinteyNine(n)
{
  let ret = ''
  if (n % 1_000 >= 100)
  {
    let threeDigit = n % 1000
    let digit = (threeDigit - (threeDigit % 100)) / 100
    
    if (ret.length > 0)
      ret += ' '
    ret += zeroToNineteen[digit] + ' hundred'
  }
  if (n % 100 > 0)
  {
    let twoDigit = zeroToNinteyNine(n % 100)
    if (twoDigit.length > 0)
    {
      if (ret.length > 0)
        ret += ' '
      ret += twoDigit
    }
  }
  return ret
}

function zeroToNinteyNine(n)
{
  let ret = ""
  if (n % 100 > 0)
  {
    let twoDigit = n % 100
    if (twoDigit < 20)
      ret += zeroToNineteen[twoDigit]
    else
    {
      ret += tens[(twoDigit - (twoDigit % 10))/ 10] 
      if (twoDigit % 10 != 0)
        ret += '-' + zeroToNineteen[twoDigit % 10]
    }
  }
  return ret
}
