//
// This is only a SKELETON file for the 'Simple Cipher' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

const alphabet = 'abcdefghijklmnopqrstuvwxyz'
const alphabet_len = alphabet.length 

export class Cipher {
  constructor(key = "") {
    if (key.trim().length === 0)
      this._key = this.generateKey()
    else
      this._key = key
  }

  transform(text, direction)
  {
    let output = ""

    for(let i = 0; i < text.length; i++)
    {
      let ch = text[i]
      let index = alphabet.indexOf(ch)
      if (index < 0)
      {
        output += ch // maybe I should exclude non a-z
      }
      else
      {
        let offset = alphabet.indexOf(this._key[i % this.key.length])
        output += alphabet[(index + (direction * offset) + alphabet_len) % alphabet_len]
      }
    }

    return output
  }

  encode(plainText) {
    return this.transform(plainText, 1)
  }

  decode(cipherText) {
    return this.transform(cipherText, -1)
  }

  generateKey() {
    const KEY_LENGTH = 100
    let key = ""
    for(let i = 0; i < KEY_LENGTH; i++)
      key += alphabet[Math.floor(Math.random() * alphabet.length)]
    return key
  }

  get key() {
    return this._key
  }
}
