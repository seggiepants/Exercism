//
// This is only a SKELETON file for the 'Food Chain' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

let animals = ['fly', 'spider', 'bird', 'cat', 'dog', 'goat', 'cow', 'horse']

let reactions = {
  'fly': 'I don\'t know why she swallowed the fly. Perhaps she\'ll die.',
  'spider': 'It wriggled and jiggled and tickled inside her.',
  'bird': 'How absurd to swallow a bird!',
  'cat': 'Imagine that, to swallow a cat!',
  'dog': 'What a hog, to swallow a dog!',
  'goat': 'Just opened her throat and swallowed a goat!',
  'cow': 'I don\'t know how she swallowed a cow!',
  'horse': 'She\'s dead, of course!'
}

export class Song {

  verse(verseNumber) {

    let lastVerse = animals.length
    let animalIndex = verseNumber - 1
    let animal = animals[animalIndex]
    let ret = []
    
    ret.push(this.firstLine(animal))
    ret.push(reactions[animal])

    while (verseNumber < lastVerse && animalIndex > 0)
    {
      let predator = animals[animalIndex]
      let prey = animals[animalIndex - 1]
      ret.push(this.sheSwallowed(predator, prey))
      
      animalIndex -= 1
      if (animalIndex === 0)
        ret.push(reactions[animals[0]])
    }

    return ret.join('\n') + '\n'
  }

  verses(first, last) {
    let ret = []
    for(let i = first; i <= last; i++)
    {
      ret.push(this.verse(i))
    }
    return ret.join('\n') + '\n'
  }

  firstLine(animal) {
    return `I know an old lady who swallowed a ${animal}.`
  }

  sheSwallowed(predator, prey) {
    let extra = ''
    if (prey === 'spider')
      extra = ' that wriggled and jiggled and tickled inside her'
    return `She swallowed the ${predator} to catch the ${prey}${extra}.`
  }
}
