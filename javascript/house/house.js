//
// This is only a SKELETON file for the 'House' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

let nouns = [{object: 'malt', verb: 'lay'}
  , {object: 'rat', verb: 'ate'}
  , {object: 'cat', verb: 'killed'}
  , {object: 'dog', verb: 'worried'}
  , {object: 'cow with the crumpled horn', verb: 'tossed'}
  , {object: 'maiden all forlorn', verb: 'milked'}
  , {object: 'man all tattered and torn', verb: 'kissed'}
  , {object: 'priest all shaven and shorn', verb: 'married'}
  , {object: 'rooster that crowed in the morn', verb: 'woke'}
  , {object: 'farmer sowing his corn', verb: 'kept'}
  , {object: 'horse and the hound and the horn', verb: 'belonged to'}
]

export class House {
  static verse(verseNumber) {
    if (verseNumber === 1)
      return ['This is the house that Jack built.']
    let ret = []
    let object = nouns[verseNumber - 2].object
    let verb = nouns[verseNumber - 2].verb
    ret.push(`This is the ${object}`)
    for(let i = verseNumber - 3; i >= 0; i--)
    {
      object = nouns[i].object
      ret.push(`that ${verb} the ${object}`)
      verb = nouns[i].verb

    }
    ret.push('that lay in the house that Jack built.')
    return ret
  }

  static verses(first, last) {
    let ret = []

    for(let i = first; i <= last; i++)
    {
      ret.push(... this.verse(i))
      ret.push('')
    }
    ret.pop()
    return ret
  }
}
