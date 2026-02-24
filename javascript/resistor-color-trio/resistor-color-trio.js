//
// This is only a SKELETON file for the 'Resistor Color Trio' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

const colors = {
  'black': 0,
  'brown': 1,
  'red': 2,
  'orange': 3,
  'yellow': 4,
  'green': 5,
  'blue': 6,
  'violet': 7,
  'grey': 8,
  'gray': 8,
  'white': 9,
}

export class ResistorColorTrio {
  constructor(bands) {
    const values = bands.map((band) => colors[band])
    if (values.length !== bands.length || values.some((value) => typeof value === 'undefined'))
      throw new Error('invalid color')

    let amount = 0
    for(let i = 0; i < values.length - 1; i++)
      if (i >= 2)
        amount *= 10
      else 
        amount = (amount * 10) + values[i]

    let  multiplier = values[values.length - 1]
    amount *= Math.pow(10, multiplier)
    let unit = 'ohms'

    if (amount >= 1_000_000_000)
    {
      unit = 'gigaohms'
      amount /= 1_000_000_000
    }
    else if (amount >= 1_000_000)
    {
      unit = 'megaohms'
      amount /= 1_000_000
    }
    else if (amount >= 1_000)
    {
      unit = 'kiloohms'
      amount /= 1_000
    }

    this._label = `Resistor value: ${amount} ${unit}`

  }

  get label() {
    return this._label
  }
}
