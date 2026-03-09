const ALPHABET_LEN = 26
const CODE_A = 'a'.charCodeAt(0)

export const encode = (phrase, key) => {
  let a = key.a
  let b = key.b

  if (!isCoPrime(a, ALPHABET_LEN))
    throw new Error('a and m must be coprime.')

  return group(phrase.toLowerCase().split('').map((ch) => encodeChar(ch, a, b)).join(''))
};

const encodeChar = (ch, a, b) =>
{
  if (ch >= 'a' && ch <= 'z')
  {
    let charIndex = ch.charCodeAt(0) - CODE_A
    let e = ((a * charIndex) + b) % ALPHABET_LEN
    return String.fromCharCode(e + CODE_A)
  }
  else if (ch >= '0' && ch <= '9')
      return ch
  return ''
}

const decodeChar = (ch, a, b) =>
{
  if (ch >= 'a' && ch <= 'z')
  {
    let charIndex = ch.charCodeAt(0) - CODE_A
    let e = (mmi(a, ALPHABET_LEN) * (charIndex - b)) % ALPHABET_LEN
    while (e < 0)
      e += ALPHABET_LEN
    return String.fromCharCode(e + CODE_A)
  }
  else if (ch >= '0' && ch <= '9')
      return ch
  return ''
}

const mmi = (a, m) => 
{
  for(let i = 1; i < m; i++)
    if ((a * i) % m === 1)
      return i;
  throw new Error(`No MMI for (${a}, ${m})`)
}


const group = (phrase, batchSize = 5) => {
  let ret = ''
  let first = true
  for(let i = 0; i < phrase.length; i+=batchSize)
  {
    if (first)
      first = false
    else
      ret += ' '
    ret += phrase.substring(i, i + 5)
  }
  return ret
}

const isCoPrime = (a, b) => {
  for(let i = 2; i <= a && i <= b; i++)
    if (a % i === 0 && b % i === 0)
      return false
  return true
}

export const decode = (phrase, key) => {
  let a = key.a
  let b = key.b

  if (!isCoPrime(a, ALPHABET_LEN))
    throw new Error('a and m must be coprime.')

  return phrase.toLowerCase().split('').map((ch) => decodeChar(ch, a, b)).join('')

};
