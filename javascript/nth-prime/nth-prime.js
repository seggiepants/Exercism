//
// This is only a SKELETON file for the 'Nth Prime' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

let primes = [2, 3, 5, 7, 11, 13]

export const prime = (nth) => {

  if (nth === 0)
    throw new Error('there is no zeroth prime')
  else if (nth < 0)
    throw new Error(`${nth}th prime does not make sense.`)

  while(primes.length < nth)
  {
    sieve(primes)
  }
  return primes[nth - 1]
};

const sieve = (primes) => {
  // Add another prime to the list.
  let current = primes[primes.length - 1] + 1
  let isPrime = false
  while (!isPrime)
  {
    isPrime = true
    for(let i of primes)
    {
      if (current % i === 0)
      {
        isPrime = false 
        break
      }
    }
    if (!isPrime)
      current++
  }
  primes.push(current)
}
