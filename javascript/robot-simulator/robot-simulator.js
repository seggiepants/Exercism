//
// This is only a SKELETON file for the 'Robot Simulator' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class InvalidInputError extends Error {
  constructor(message) {
    super();
    this.message = message || 'Invalid Input';
  }
}

const delta = {
  'north': [0, 1],
  'south': [0, -1], 
  'east': [1, 0],
  'west': [-1, 0]
}

const turnLeft = {
  'north': 'west',
  'west': 'south',
  'south': 'east',
  'east': 'north'
}

const turnRight = {
  'north': 'east',
  'east': 'south',
  'south': 'west',
  'west': 'north'
}

export class Robot {
  constructor()
  {
    this.x = this.y = 0;
    this.direction = 'north'
  }

  get bearing() {
    return this.direction
  }

  advance() {
    let [dx, dy] = this.direction in delta ? delta[this.direction] : [0, 0]
    this.x += dx
    this.y += dy
  }

  turnLeft() {
    this.direction = this.direction in turnLeft ? turnLeft[this.direction] : this.direction
  }

  turnRight() {
    this.direction = this.direction in turnRight ? turnRight[this.direction] : this.direction
  }

  get coordinates() {
    return [this.x, this.y]
  }

  place({ x, y, direction }) {
    this.x = x
    this.y = y
    if (!(direction in delta))
      throw new InvalidInputError(`"${direction}" is not a valid direction. Valid options are "north", "south", "east", or "west".`)
    this.direction = direction
  }

  evaluate(instructions) {
    for(let ch of instructions)
    {
      switch(ch)
      {
        case 'R':
          this.turnRight()
          break
        case 'L':
          this.turnLeft()
          break
        case 'A':
          this.advance()
          break
        default:
          throw new InvalidInputError(`Unrecognized Token: "${ch}".`)
      }
    }
  }
}
