//
// This is only a SKELETON file for the 'Zebra Puzzle' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

// I made this global so I only have to compute the puzzle once.
let data = [{ color: '', nationality: '', drink: '', pet: '', hobby: ''}
      , { color: '', nationality: '', drink: '', pet: '', hobby: ''}
      , { color: '', nationality: '', drink: '', pet: '', hobby: ''}
      , { color: '', nationality: '', drink: '', pet: '', hobby: ''}
      , { color: '', nationality: '', drink: '', pet: '', hobby: ''}
    ]

export class ZebraPuzzle {
  constructor() {
    
    // Don't mind the suspicious ordering. I don't want a timeout.
    this.colors = ['ivory', 'yellow', 'blue', 'red', 'green']
    this.nationalities = ['Englishman', 'Japanese', 'Spainiard', 'Norwegian', 'Ukranian']
    this.drinks = ['water', 'tea', 'milk', 'coffee', 'orange juice']
    this.pets = ['dog', 'zebra', 'fox', 'horse', 'snail']
    this.hobbies = ['dancing', 'painting', 'reading', 'football', 'chess']

    // populate the givens
    data[0].nationality = 'Norwegian' // The Norwegian lives in the first house.
    data[1].color = 'blue' // The Norwegian lives next to the blue house
    data[2].drink = 'milk' // The middle house drinks milk
    if (!this.isFull())
    {
      if (!this.fillPuzzle([]))
        console.log('Fill failed')
    }
    
    console.log(this.toString())
  }

  undo(undoStack, stackPointer)
  {
    while (undoStack.length >= stackPointer)
    {
      let [house, key] = undoStack.pop()
      data[house][key] = ''
    }
  }

  // For debugging
  toString()
  {
    let msg = ''
    for(let i = 0; i < data.length; i++)
    {
      msg += `${i+1}: ${data[i].color}, ${data[i].nationality}, ${data[i].drink}, ${data[i].hobby}, ${data[i].pet}` + '\n'
    }
    return msg
  }

  isFull()
  {
    for(let house = 0; house < data.length; house++)
    {
      if (data[house].color === '' ||
        data[house].nationality === '' ||
        data[house].drink === '' ||
        data[house].pet === '' || 
        data[house].hobby === '')
        return false
    }
    return true
  }

  // Find the house that has a key with the given value
  find(key, value)
  {
    return data.findIndex((house) => house[key] === value )
  }

  // Populate if house[key] === value then house[otherKey] = otherValue
  populatePair(undoStack, key1, value1, key2, value2)
  {
    let index = this.find(key1, value1)
    if (index >= 0)
    {
      if (data[index][key2] === '')
      {
        undoStack.push([index, key2])
        data[index][key2] = value2
        return true
      }
      else if (data[index][key2] !== value2)
        return false
    }
    
    index = this.find(key2, value2)
    if (index >= 0)
    {
      if (data[index][key1] === '')
      {
        undoStack.push([index, key1])
        data[index][key1] = value1
        return true
      }
      else if (data[index][key1] !== value1)
        return false
    }
    return true
  }

  // Populate if house[key] === value then houseToRight[otherKey] = otherValue
  populatePairRight(undoStack, key1, value1, key2, value2)
  {
    let index1 = this.find(key1, value1)
    let index2 = this.find(key2, value2)
    // if you have both they had better match
    if (index1 >= 0 && index2 >=0 && (index1 + 1) !== index2)
      return false

    if (index1 >= 0 && index1 <= data.length - 2)
    {
      if (data[index1 + 1][key2] === '')
      {
        undoStack.push([index1 + 1, key2])
        data[index1 + 1][key2] = value2 
        return true 
      }
      else if (data[index1 + 1][key2] !== value2)
        return false 
    }

    if (index2 >= 1 && index2 < data.length)
    {
      if (data[index2 - 1][key1] === '')
      {
        undoStack.push([index2 - 1, key1])
        data[index2 - 1][key1] = value1
        return true 
      }
      else if (data[index2 - 1][key1] !== value1)
        return false 
    }
    return true
  }

  // These searches are lengthy to write so I broke this one in two and
  // just call it with both possible combinations (should be redundant, really).
  populatePairNeighbor_Step(undoStack, key1, value1, key2, value2)
  {
    let index = this.find(key1, value1)
    if (index >= 0)
    {
      let sides = []
      if (index - 1 >= 0)
        sides.push([index -1, data[index - 1][key2]])
      if (index + 1 < data.length)
        sides.push([index + 1, data[index + 1][key2]])

      if (sides.length === 1 && sides[0][1] !== value2 && sides[0][1] !== '')
        return false
      else if (sides.length === 1 && sides[0][1] === value2)
        return true
      else if (sides.length === 1 && sides[0][1] === '')
      {
        // only one and it is empty, fill it
        undoStack.push([sides[0][0], key2])
        data[sides[0][0]][key2] = value2
        return true
      }      
      else if (sides.length === 2 && sides[0][1] !== '' && sides[1][1] !== '' && sides[0][1] !== value2 && sides[1][1] !== value2)
      {
        // both sides not target
        return false
      }
      else if (sides.length === 2 && (sides[0][1] === value2 || sides[1][1] === value2))
      {
        // one side is target
        return true
      }
      else if (sides.length === 2 && sides[0][1] === '' && sides[1][1] !== '' && sides[1][1] !== value2)
      {
        // left empty, right not empty not target
        undoStack.push([sides[0][0], key2])
        data[sides[0][0]][key2] = value2
      }
      else if (sides.length === 2 && sides[0][1] !== '' && sides[1][1] === '' && sides[0][1] !== value2)
      {
        // right empty, let not empty not target
        undoStack.push([sides[1][0], key2])
        data[sides[1][0]][key2] = value2
      }
      
    }
    return true
  }

  // Populate if house[key] === value then houseToLeftOrRight[otherKey] = otherValue
  populatePairNeighbor(undoStack, key1, value1, key2, value2)
  {
    let index1 = this.find(key1, value1)
    let index2 = this.find(key2, value2)
    // Both populated but more than one spot apart is an error
    if (index1 >= 0 && index2 >= 0)
    {
      if ((data[index1][key1] === value1 && data[index2][key2] === value2) ||
          (data[index2][key1] === value1 && data[index1][key2] === value2))
        return true 

      if (Math.abs(index2 - index1) > 1)
        return false
    }

    if (!this.populatePairNeighbor_Step(undoStack, key1, value1, key2, value2))
      return false 
    if (!this.populatePairNeighbor_Step(undoStack, key2, value2, key1, value1))
      return false 
    return true
  }

  // Run through all the rules (except the givens -- can be filled in without futher data)
  // if a rule fails the tests return false, if no errors found return true.
  populate(undoStack)
  {
    // 1 There are five houses
    if (data.length !== 5)
      return false    
    // 2 Englishman lives in the red house
    if (!this.populatePair(undoStack, 'nationality', 'Englishman', 'color', 'red'))
      return false
    // 3 Spaniard owns a dog
    if (!this.populatePair(undoStack, 'nationality', 'Spainiard', 'pet', 'dog'))      
      return false
    // 4 Green house drinks coffee
    if (!this.populatePair(undoStack, 'color', 'green', 'drink', 'coffee'))
      return false
    // 5 Ukranian drinks tea
    if (!this.populatePair(undoStack, 'nationality', 'Ukranian', 'drink', 'tea'))      
      return false
    // 6 - Green house right of Ivory house
    //if (!this.populatePairRight(undoStack, 'color', 'green', 'color', 'ivory'))
    if (!this.populatePairRight(undoStack, 'color', 'ivory', 'color', 'green'))
      return false
    // 7 Snail owner goes dancing
    if (!this.populatePair(undoStack, 'pet', 'snail', 'hobby', 'dancing'))      
      return false
    // 8 Yellow house likes painting
    if (!this.populatePair(undoStack, 'color', 'yellow', 'hobby', 'painting'))      
      return false
    // 9 Middle house drinks milk is a given
    // 10 1st house is Norwegian is a given
    // 11 - Reading next to fox
    if (!this.populatePairNeighbor(undoStack, 'hobby', 'reading', 'pet', 'fox'))
      return false
    // 12 - Painter next to horse
    if (!this.populatePairNeighbor(undoStack, 'hobby', 'painting', 'pet', 'horse'))
      return false
    // 13 Football drinks orange juice
    if (!this.populatePair(undoStack, 'hobby', 'football', 'drink', 'orange juice'))      
      return false
    // 14 Japanese plays chess
    if (!this.populatePair(undoStack, 'nationality', 'Japanese', 'hobby', 'chess'))      
      return false
    // 15 is also a given Norwegian (1st house) is next to blue house where 2 is only neighbor 
    
    return true 
  }

  // Populate the puzzle
  fillPuzzle(undoStack)
  {
    for(let house = 0; house < data.length;house++)
    {
      for(let pair of [
        ['color', this.colors]
        , ['nationality', this.nationalities]
        , ['drink', this.drinks]
        , ['pet', this.pets]
        , ['hobby', this.hobbies]])
      {
        if (data[house][pair[0]] === '')
        {
          let key = pair[0]
          let values = pair[1]
          let used = data.filter((value) => value[key] !== '').map((item) => item[key])
          let free = values.filter((value) => !used.includes(value))
          if (free.length > 0)
          {
            for(let attempt of free)
            {
              data[house][key] = attempt
              undoStack.push([house, key])
              let stackPointer = undoStack.length
              let success = this.populate(undoStack)
              if (!success) 
                this.undo(undoStack, stackPointer)
              else 
              {
                success = this.fillPuzzle(undoStack)
                if (!success)
                  this.undo(undoStack, stackPointer)
                else
                  break
              }
            }
          }
        }        
      }
      if (data[house].color === '' || 
        data[house].nationality === '' || 
        data[house].drink === '' ||
        data[house].hobby === '' ||
        data[house].pet === '')
        return false
      if (this.isFull())
        return true
    }
    return this.populate(undoStack) && this.isFull()
  }

  waterDrinker() {
   return data[this.find('drink', 'water')].nationality
  }

  zebraOwner() {
    return data[this.find('pet', 'zebra')].nationality
  }
}
