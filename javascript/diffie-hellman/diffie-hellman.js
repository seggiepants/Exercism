//
// This is only a SKELETON file for the 'Diffie Hellman' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class DiffieHellman {
  constructor(p, g) {
    if (p <= 1)
      throw new Error('p must be greater than one')
    if (!isPrime(p))
      throw new Error('p must be a prime number')
    if (!isPrime(g))
      throw new Error('g must be a prime number')
    this._p = p 
    this._g = g 
  }

  getPublicKey(privateKey) {
    // A = gᵃ mod p
    if (privateKey <= 1 || privateKey >= this._p)
      throw new Error('Private key is outside of bounds.')
    return Math.pow(this._g, privateKey) % this._p

  }

  getSecret(theirPublicKey, myPrivateKey) {
    // s = Bᵃ mod p
    return Math.pow(theirPublicKey, myPrivateKey) % this._p
  }

  static getPrivateKey(p) {
    return 2 + Math.floor(Math.random() * (p - 2))
  }
}

let isPrime = (num) => {
  let flags = Array.from({length: num},() => true)

  flags[0] = false
  for(let i = 1; i < Math.sqrt(num); i++)
  {
    if (flags[i] === false)
      continue

    let n = i + 1
    flags[i] = true;
    for(let j = i + n; j < num; j+=n)
    {
      flags[j] = false
    }
  }
  return flags[num - 1]
};
