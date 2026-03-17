//
// This is only a SKELETON file for the 'Crypto Square' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class Crypto {
  constructor(message) {
    this.message = message
  }

  get ciphertext() {
    let re = /\W+/gm
    let filtered = this.message.toLowerCase().replace(re, '')
    let blockSize = Math.ceil(Math.sqrt(filtered.length))
    while (blockSize > 0 && filtered.length % blockSize !== 0)
      filtered += ' '
    let ret = ''
    for(let offset = 0; offset < blockSize; offset++)
    {
      for(let index = offset; index < filtered.length;index += blockSize)
      {
        if (index < filtered.length)
          ret += filtered[index]        
      }
      if (offset < blockSize - 1)
        ret += ' '
    }
    
    return ret
  }
}
